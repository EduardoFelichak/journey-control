using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

#nullable disable

namespace journey_control.Migrations
{
    /// <inheritdoc />
    public partial class SeedProjectsAndVersions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Task.Run(async () =>
            {
                var data = await GetProjectsAndVersionsFromApi();

                foreach (var project in data)
                {
                    migrationBuilder.Sql($@"
                        INSERT INTO projects (""Id"", ""Name"")
                        VALUES ({project.Id}, '{project.Name.Replace("'", "''")}')
                        ON CONFLICT (""Id"") DO NOTHING;
                    ");

                    foreach (var version in project.Versions)
                    {
                        migrationBuilder.Sql($@"
                            INSERT INTO versions (""Id"", ""Name"", ""StartDate"", ""DueDate"", ""ProjectId"")
                            VALUES ({version.Id}, '{version.Name.Replace("'", "''")}', 
                                    '{version.StartDate:yyyy-MM-dd}', 
                                    '{version.DueDate:yyyy-MM-dd}', 
                                    {project.Id})
                            ON CONFLICT (""Id"", ""ProjectId"") DO NOTHING;
                        ");
                    }
                }
            }).GetAwaiter().GetResult();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM versions");
            migrationBuilder.Sql("DELETE FROM projects");
        }

        private async Task<List<ProjectModel>> GetProjectsAndVersionsFromApi()
        {
            var userApiKey = "API_KEY";
            var baseUrl = "https://redmine.questor.com.br";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Redmine-API-Key", userApiKey);

                var projects = new List<ProjectModel>();
                int offset = 0;
                const int limit = 100;
                bool hasMore = true;

                while (hasMore)
                {
                    HttpResponseMessage response = await client.GetAsync($"{baseUrl}/projects.json?offset={offset}&limit={limit}");
                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Erro ao buscar projetos: {response.StatusCode}");

                    string projectJson = await response.Content.ReadAsStringAsync();
                    var projectData = JsonConvert.DeserializeObject<dynamic>(projectJson);

                    foreach (var project in projectData.projects)
                    {
                        var projectId = (int)project.id;
                        var projectName = (string)project.name;

                        var versions = await GetVersionsFromApi(client, baseUrl, projectId);

                        projects.Add(new ProjectModel
                        {
                            Id = projectId,
                            Name = projectName,
                            Versions = versions
                        });
                    }

                    offset += limit;
                    hasMore = projectData.total_count > offset;
                }

                return projects;
            }
        }

        private async Task<List<VersionModel>> GetVersionsFromApi(HttpClient client, string baseUrl, int projectId)
        {
            var versions = new List<VersionModel>();

            HttpResponseMessage response = await client.GetAsync($"{baseUrl}/projects/{projectId}/versions.json");
            if (response.IsSuccessStatusCode)
            {
                string versionJson = await response.Content.ReadAsStringAsync();
                var versionData = JsonConvert.DeserializeObject<dynamic>(versionJson);

                foreach (var version in versionData.versions)
                {
                    DateOnly? startDate = null;
                    DateOnly? dueDate = null;

                    var customFields = version.custom_fields as IEnumerable<dynamic>;
                    if (customFields != null)
                    {
                        var startDateField = customFields.FirstOrDefault(field => (string)field.name == "Data Início");
                        if (startDateField != null && !string.IsNullOrEmpty((string)startDateField.value))
                        {
                            startDate = DateOnly.FromDateTime(DateTime.Parse((string)startDateField.value).ToUniversalTime());
                        }

                        var dueDateField = customFields.FirstOrDefault(field => (string)field.name == "Data de Produção");
                        if (dueDateField != null && !string.IsNullOrEmpty((string)dueDateField.value))
                        {
                            dueDate = DateOnly.FromDateTime(DateTime.Parse((string)dueDateField.value).ToUniversalTime());
                        }
                    }

                    if (startDate.HasValue && dueDate.HasValue)
                    {
                        versions.Add(new VersionModel
                        {
                            Id = (int)version.id,
                            Name = (string)version.name,
                            StartDate = startDate.Value,
                            DueDate = dueDate.Value
                        });
                    }
                }
            }
            else
            {
                Console.WriteLine($"Erro ao buscar versões para o projeto {projectId}: {response.StatusCode}");
            }

            return versions;
        }


        private class ProjectModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<VersionModel> Versions { get; set; }
        }

        private class VersionModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateOnly StartDate { get; set; }
            public DateOnly DueDate { get; set; }
        }
    }
}
