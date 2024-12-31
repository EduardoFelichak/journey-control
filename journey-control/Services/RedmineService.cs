using journey_control.Helpers.AppData;
using journey_control.Models;
using journey_control.Repositories;
using Newtonsoft.Json;

namespace journey_control.Services
{
    public class RedmineService
    {
        private readonly string baseUrl = "https://redmine.questor.com.br";

        public async Task<User> GetUserAsync(string apiKey)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Redmine-API-Key", apiKey);

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/users/current.json");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var userResponse = JsonConvert.DeserializeObject<dynamic>(json);
                    return JsonConvert.DeserializeObject<User>(userResponse.user.ToString());
                }
                return null;
            }
        }

        public async Task<Models.Version> GetCurrentVersionAsync(DateTime date)
        {
            var user = UserDataManager.LoadUserData();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Redmine-API-Key", user.ApiKey);

                var response = await client.GetAsync($"{baseUrl}/issues.json?assigned_to_id={user.Id}&project_id={user.ProjectId}&status_id=*&limit=100");
                if (response.IsSuccessStatusCode) 
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<dynamic>(json);

                    foreach (var issue in data.issues)
                    {
                        DateTime startDate = DateTime.Parse((string)issue.start_date).ToUniversalTime();
                        DateTime dueDate = DateTime.Parse((string)issue.due_date).ToUniversalTime();

                        if (date >= startDate && date <= dueDate)
                        {
                            return new Models.Version
                            {
                                Id        = issue.fixed_version.id,
                                StartDate = startDate,
                                DueDate   = dueDate,
                                ProjectId = user.ProjectId,
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<List<Issue>> GetIssuesAsync(int versionId)
        {
            var tasks = new List<Issue>();

            try
            {
                var user = UserDataManager.LoadUserData();

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-Redmine-API-Key", user.ApiKey);

                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}/issues.json?assigned_to_id={user.Id}&status_id=*&fixed_version_id={versionId}&limit=100");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<dynamic>(json);

                        foreach (var issue in data.issues)
                        {
                            if (issue.fixed_version != null && issue.fixed_version.id == versionId)
                            {
                                string size = "N/A";

                                if (issue.custom_fields != null)
                                {
                                    foreach (var field in issue.custom_fields)
                                    {
                                        if (field.id == 127)
                                        {
                                            size = field.value ?? "N/A";
                                            break;
                                        }
                                    }
                                }

                                tasks.Add(new Issue
                                {
                                    Id = issue.id,
                                    Subject = issue.subject,
                                    Description = issue.description,
                                    ProjectId = issue.project.id,
                                    StartDate = issue.start_date != null ? DateTime.Parse((string)issue.start_date) : (DateTime?)null,
                                    DueDate = issue.due_date != null ? DateTime.Parse((string)issue.due_date) : (DateTime?)null,
                                    Size = size,
                                    FixedVersion = issue.fixed_version.id,
                                    Status = issue.status.name
                                });
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"Erro na requisição: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter tarefas: {ex.Message}");
            }

            return tasks;
        }

        public async Task<Issue> GetIssueAsync(int taskId)
        {
            try
            {
                var user = UserDataManager.LoadUserData();

                if (user == null)
                    throw new Exception("Usuário não encontrado.");

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-Redmine-API-Key", user.ApiKey);

                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}/issues/{taskId}.json");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<dynamic>(json);

                        var issue = data.issue;

                        string size = "N/A";

                        if (issue.custom_fields != null)
                        {
                            foreach (var field in issue.custom_fields)
                            {
                                if (field.id == 127)
                                {
                                    size = field.value ?? "N/A";
                                    break;
                                }
                            }
                        }

                        return new Issue
                        {
                            Id = issue.id,
                            Subject = issue.subject,
                            Description = issue.description,
                            ProjectId = issue.project.id,
                            StartDate = issue.start_date != null ? DateTime.Parse((string)issue.start_date) : (DateTime?)null,
                            DueDate = issue.due_date != null ? DateTime.Parse((string)issue.due_date) : (DateTime?)null,
                            Size = size,
                            FixedVersion = issue.fixed_version.id,
                            Status = issue.status.name
                        };
                    }
                    else
                    {
                        throw new Exception($"Erro ao buscar a tarefa {taskId}: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar a tarefa {taskId}: {ex.Message}");
                return null;
            }
        }
    }
}
