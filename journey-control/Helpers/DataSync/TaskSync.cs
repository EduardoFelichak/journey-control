using journey_control.Enums;
using journey_control.Helpers.AppData;
using journey_control.Models;
using journey_control.Repositories;
using journey_control.Services;

namespace journey_control.Helpers.DataSync
{
    public static class TaskSync
    {
        public static async System.Threading.Tasks.Task Run(DateTime date)
        {
            var user = UserDataManager.LoadUserData();
            if (user == null)
                throw new Exception("Usuário não encontrado.");

            var versionRepo = new VersionRepo();
            var taskRepo = new TaskRepo();
            var redmineService = new RedmineService();

            Models.Version version = await versionRepo.GetVersionPerDate(date);
            if (version == null)
                throw new Exception("Versão correspondente à data não encontrada.");

            var issues = await redmineService.GetIssuesAsync(version.Id) ?? new List<Issue>();
            var existingTasks = await taskRepo.GetAllPerUserAndVersion(version.Id);

            var tasksToAdd = issues
                .Where(issue => existingTasks.All(et => et.Id != issue.Id.ToString()))
                .Select(issue => CreateTaskFromIssue(issue, version, user.Id))
                .ToList();

            var tasksToUpdate = existingTasks
                .Where(et => issues.Any(issue => issue.Id.ToString() == et.Id))
                .ToList();

            foreach (var task in tasksToUpdate)
            {
                var issue = issues.First(i => i.Id.ToString() == task.Id);
                UpdateTaskFromIssue(task, issue);
            }

            if (!existingTasks.Any(t => t.Id == "Estudo") && !await taskRepo.StudyTaskExists())
            {
                tasksToAdd.Add(new Models.Task
                {
                    Id = "Estudo",
                    Title = "Informe o tempo gasto em sua hora de estudo!",
                    Description = "Nenhuma descrição",
                    Status = "Geral",
                    Size = SizeE.P,
                    StartDate = DateTime.MinValue,
                    DueDate = DateTime.MaxValue,
                    UserId = user.Id,
                    VersionId = version.Id,
                    VersionProjectId = version.ProjectId,
                });
            }

            await taskRepo.AddRange(tasksToAdd);
            await taskRepo.UpdateRange(tasksToUpdate);
        }


        private static Models.Task CreateTaskFromIssue(dynamic issue, Models.Version version, int userId)
        {
            return new Models.Task
            {
                Id = issue.Id.ToString(),
                Title = issue.Subject,
                Description = issue.Description ?? "Nenhuma descrição.",
                Size = GetSizeEnum(issue.Size),
                Status = issue.Status,
                StartDate = version.StartDate,
                DueDate = version.DueDate,
                Project = issue.ProjectId,
                UserId = userId,
                VersionId = version.Id,
                VersionProjectId = version.ProjectId,
            };
        }

        private static void UpdateTaskFromIssue(Models.Task task, dynamic issue)
        {
            task.Title = issue.Subject;
            task.Description = issue.Description ?? "Nenhuma descrição.";
            task.Size = GetSizeEnum(issue.Size);
            task.Status = issue.Status;
        }

        private static SizeE GetSizeEnum(string size)
        {
            return size switch
            {
                "P" => SizeE.P,
                "M" => SizeE.M,
                "G" => SizeE.G,
                _ => SizeE.N,
            };
        }
    }
}
