using journey_control.Helpers.AppData;
using journey_control.Models;
using journey_control.Repositories;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;

namespace journey_control.Services
{
    public class RedmineService
    {
        private readonly string baseUrl = "https://redmine.questor.com.br";

        public async Task<Models.User> GetUserAsync(string apiKey)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Redmine-API-Key", apiKey);

                HttpResponseMessage response = await client.GetAsync($"{baseUrl}/users/current.json");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var userResponse = JsonConvert.DeserializeObject<dynamic>(json);
                    return JsonConvert.DeserializeObject<Models.User>(userResponse.user.ToString());
                }
                return null;
            }
        }

        public async Task<Models.Version> GetCurrentVersionAsync(DateOnly date)
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
                        DateOnly startDate = DateOnly.Parse((string)issue.start_date);
                        DateOnly dueDate = DateOnly.Parse((string)issue.due_date);

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
                                    StartDate = issue.start_date != null ? DateOnly.Parse((string)issue.start_date) : (DateOnly?)null,
                                    DueDate = issue.due_date != null ? DateOnly.Parse((string)issue.due_date) : (DateOnly?)null,
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
                            StartDate = issue.start_date != null ? DateOnly.Parse((string)issue.start_date) : (DateOnly?)null,
                            DueDate = issue.due_date != null ? DateOnly.Parse((string)issue.due_date) : (DateOnly?)null,
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

        public async Task<List<Entrie>> GetTimeEntriesAsync(DateOnly date)
        {
            var entries = new List<Entrie>();

            try
            {
                var user = UserDataManager.LoadUserData();

                if (user == null)
                    throw new Exception("Usuário não encontrado.");

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-Redmine-API-Key", user.ApiKey);
                    string dateString = date.ToString("yyyy-MM-dd");

                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}/time_entries.json?user_id={user.Id}&spent_on={dateString}");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<dynamic>(json);

                        foreach (var timeEntry in data.time_entries)
                        {
                            if (timeEntry.issue == null || timeEntry.hours == null)
                                continue;

                            string issueId = timeEntry.issue?.id?.ToString() ?? string.Empty;
                            double hours = timeEntry.hours != null ? (double)timeEntry.hours : 0;

                            if (!string.IsNullOrEmpty(issueId))
                            {
                                entries.Add(new Entrie
                                {
                                    Id = (int)timeEntry.id,
                                    TaskId = issueId,
                                    TaskUserId = user.Id,
                                    DateEntrie = DateOnly.Parse((string)timeEntry.spent_on),
                                    Duration = (int)(hours * 3600),
                                });
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"Erro na requisição ao Redmine: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar entradas de tempo: {ex.Message}");
            }

            return entries;
        }

        public async Task<List<Activity>> GetActivitiesAsync()
        {
            var activities = new List<Activity>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var user = UserDataManager.LoadUserData();

                    client.DefaultRequestHeaders.Add("X-Redmine-API-Key", user.ApiKey);
                    var response = await client.GetAsync($"{baseUrl}/enumerations/time_entry_activities.json");

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<dynamic>(json);

                        foreach (var activity in data.time_entry_activities)
                        {
                            activities.Add(new Activity
                            {
                                Id = (int)activity.id,
                                Name = (string)activity.name
                            });
                        }
                    }
                    else
                    {
                        throw new Exception($"Erro ao obter atividades: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return activities;
        }
    }
}
