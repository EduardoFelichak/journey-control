using journey_control.Enums;
using journey_control.Helpers.AppData;
using journey_control.Infra.Context;
using journey_control.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;

namespace journey_control.Repositories
{
    public class TaskRepo
    {
        private readonly ApplicationDBContext _context = new ApplicationDBContext();

        public async Task<Boolean> ExistsPerUserAndVersion(Models.User user, int versionId)
        {
            return await _context.Tasks
                            .Where(t => t.UserId == user.Id && t.VersionId == versionId)
                            .AnyAsync();
        }
        
        public async Task<Boolean> StudyTaskExists()
        {
            return await _context.Tasks
                            .Where(t => t.Id == "Estudo")
                            .AnyAsync();
        }

        public async System.Threading.Tasks.Task AddRange(List<Models.Task> tasks)
        {
            _context.Tasks.AddRange(tasks);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddTaskFromIssueAsync(Issue issue)
        {
            var user = UserDataManager.LoadUserData();

            if (issue == null)
                throw new ArgumentNullException(nameof(issue), "A issue não pode ser nula.");

            // Verifica se a tarefa já existe no banco de dados
            var existingTask = await _context.Tasks
                .AsNoTracking() // Evita rastrear múltiplas instâncias
                .FirstOrDefaultAsync(t => t.Id == issue.Id.ToString() && t.UserId == user.Id);

            if (existingTask != null)
            {
                var startDate = DateTime.SpecifyKind(issue.StartDate ?? DateTime.MinValue, DateTimeKind.Utc);
                var dueDate   = DateTime.SpecifyKind(issue.DueDate ?? DateTime.MaxValue, DateTimeKind.Utc);

                existingTask.Title = issue.Subject;
                existingTask.Description = issue.Description ?? "Sem descrição.";
                existingTask.StartDate = startDate;
                existingTask.DueDate = dueDate;
                existingTask.Size = GetSizeEnum(issue.Size);
                existingTask.Status = issue.Status;

                _context.Entry(existingTask).State = EntityState.Modified; // Marca como modificada
            }
            else
            {
                var startDate = DateTime.SpecifyKind(issue.StartDate ?? DateTime.MinValue, DateTimeKind.Utc);
                var dueDate = DateTime.SpecifyKind(issue.DueDate ?? DateTime.MaxValue, DateTimeKind.Utc);

                var newTask = new Models.Task
                {
                    Id = issue.Id.ToString(),
                    Title = issue.Subject,
                    Description = issue.Description ?? "Sem descrição.",
                    StartDate = startDate,
                    DueDate = dueDate,
                    Size = GetSizeEnum(issue.Size),
                    Status = issue.Status,
                    Project = issue.ProjectId,
                    UserId = user.Id,
                    VersionId = issue.FixedVersion,
                    VersionProjectId = issue.ProjectId,
                };

                await _context.Tasks.AddAsync(newTask); // Adiciona a nova tarefa
            }

            await _context.SaveChangesAsync(); // Salva todas as alterações
        }

        public async Task<ICollection<Models.Task>> GetAllPerUserAndDate(DateTime date)
        {
            Models.User user = UserDataManager.LoadUserData();
            return await _context.Tasks.Where(t => t.UserId == user.Id && t.StartDate <= date && t.DueDate >= date).ToListAsync();
        }

        public async Task<ICollection<Models.Task>> GetAllPerUserAndVersion(int versionId)
        {
            Models.User user = UserDataManager.LoadUserData();
            return await _context.Tasks.Where(t => t.UserId == user.Id && t.VersionId == versionId).ToListAsync();
        }

        public async System.Threading.Tasks.Task RemoveRange(List<Models.Task> tasks)
        {
            _context.Tasks.RemoveRange(tasks);
            await _context.SaveChangesAsync();
        }
        public async System.Threading.Tasks.Task UpdateRange(List<Models.Task> tasks)
        {
            _context.Tasks.UpdateRange(tasks);
            await _context.SaveChangesAsync();
        }

        private SizeE GetSizeEnum(string size)
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
