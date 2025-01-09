using journey_control.DTOs;
using journey_control.Helpers.AppData;
using journey_control.Models;
using journey_control.Repositories;
using journey_control.Services;
using System.Text.Json;

namespace journey_control.Views.Components.Forms
{
    public partial class ReleaseEntriesForm : Form
    {
        public bool launchTasks;
        private readonly RedmineService _redmineService = new RedmineService();
        private readonly LocalEntriesRepo _localEntriesRepo = new LocalEntriesRepo();
        private readonly ProjectRepo _projectRepo = new ProjectRepo();
        private List<Activity> _activities = new List<Activity>();
        private List<ProjectIndicator> _projectIndicators = ProjectIndicator.GetDefaultIndicators();
        private List<LocalEntrie> _localEntries = new List<LocalEntrie>();
        private List<Project> _projects = new List<Project>();
        private DateOnly _currentDate;

        public ReleaseEntriesForm(DateOnly currentDate)
        {
            InitializeComponent();
            _currentDate = currentDate;
            gridEntries.EditMode = DataGridViewEditMode.EditOnEnter;
            launchTasks = false;
        }

        public async System.Threading.Tasks.Task LoadDataAsync()
        {
            try
            {
                _activities = await _redmineService.GetActivitiesAsync();
                _localEntries = await _localEntriesRepo.GetAllPerDate(_currentDate);
                _projects = await _projectRepo.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PopulateGrid()
        {
            var user = UserDataManager.LoadUserData();
            gridEntries.Rows.Clear();

            foreach (var entry in _localEntries)
            {
                TimeSpan duration = TimeSpan.FromSeconds(entry.Duration);

                if (duration < TimeSpan.FromMinutes(1))
                    continue;

                var rowIndex = gridEntries.Rows.Add();
                var row = gridEntries.Rows[rowIndex];

                if (row.Cells["colProject"] is DataGridViewComboBoxCell projectCell)
                {
                    projectCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    projectCell.DataSource = _projects.ToList();
                    projectCell.DisplayMember = "Name";
                    projectCell.ValueMember = "Id";
                }

                if (row.Cells["colActivity"] is DataGridViewComboBoxCell activityCell)
                {
                    activityCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    activityCell.DataSource = _activities.ToList();
                    activityCell.DisplayMember = "Name";
                    activityCell.ValueMember = "Id";
                }

                if (row.Cells["colProjectInd"] is DataGridViewComboBoxCell projectIndicatorCell)
                {
                    projectIndicatorCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    projectIndicatorCell.DataSource = _projectIndicators.ToList();
                    projectIndicatorCell.DisplayMember = "Name";
                    projectIndicatorCell.ValueMember = "Value";
                }

                if (row.Cells["colExecPlace"] is DataGridViewComboBoxCell executionPlaceCell)
                {
                    executionPlaceCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    executionPlaceCell.DataSource = new[] { "Fora da Cidade", "Dentro da Cidade", "Interno" };
                }

                if (row.Cells["colOvertime"] is DataGridViewComboBoxCell overtimeCell)
                {
                    overtimeCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    overtimeCell.DataSource = new[] { "Sim", "Não" };
                }

                row.Cells["colDate"].Value = entry.DateEntrie.ToString("dd/MM/yyyy");
                row.Cells["colDuration"].Value = duration.ToString(@"hh\:mm");
                row.Cells["colTaskTitle"].Value = entry.Task.Title;
                row.Cells["colId"].Value = entry.Id;

                if (entry.TaskId == "Estudo")
                {
                    foreach (DataGridViewCell cell in row.Cells)
                        cell.ReadOnly = true;

                    row.Cells["colTaskNum"].Value = "Estudo";

                    if (row.Cells["colOk"] is DataGridViewCheckBoxCell checkBoxCell)
                        checkBoxCell.Value = true;
                }
                else if (int.TryParse(entry.TaskId, out _))
                {
                    row.Cells["colTaskNum"].Value = entry.TaskId;

                    if (row.Cells["colProject"] is DataGridViewComboBoxCell)
                        row.Cells["colProject"].Value = entry.Task.Project;
                }
                else
                {
                    row.Cells["colTaskNum"].Value = "Customizada";
                    row.Cells["colComment"].Value = entry.TaskId;
                    row.Cells["colComment"].ReadOnly = true;

                    if (row.Cells["colProject"] is DataGridViewComboBoxCell)
                        row.Cells["colProject"].Value = user.ProjectId;
                }
            }

            UpdateLabelQtd();
        }

        private void ValidateRow(DataGridViewRow row)
        {
            if (row == null)
                return;

            if (row.Cells["colTaskNum"].Value?.ToString() == "Estudo")
                return;

            bool isProjectSelected = row.Cells["colProject"] is DataGridViewComboBoxCell projectCell
                                    && projectCell.Value != null;

            bool isActivitySelected = row.Cells["colActivity"] is DataGridViewComboBoxCell activityCell
                                    && activityCell.Value != null;

            bool isProjectIndicatorSelected = row.Cells["colProjectInd"] is DataGridViewComboBoxCell projectIndicatorCell
                                            && projectIndicatorCell.Value != null;

            if (row.Cells["colOk"] is DataGridViewCheckBoxCell checkBoxCell)
                checkBoxCell.Value = isProjectSelected && isActivitySelected && isProjectIndicatorSelected;

            UpdateLabelQtd();
        }

        private void UpdateLabelQtd()
        {
            int totalRows = gridEntries.Rows.Count;
            int checkedRows = gridEntries.Rows
                .Cast<DataGridViewRow>()
                .Count(r => (bool?)r.Cells["colOk"].Value == true);

            txtQtd.Text = $"{checkedRows:00}/{totalRows:00}";
        }

        private void gridEntries_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                ValidateRow(gridEntries.Rows[e.RowIndex]);
            }
        }

        private void gridEntries_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (!gridEntries.IsCurrentCellDirty)
                return;

            gridEntries.CommitEdit(DataGridViewDataErrorContexts.Commit);

            int rowIndex = gridEntries.CurrentCell?.RowIndex ?? -1;
            if (rowIndex < 0)
                return;

            ValidateRow(gridEntries.Rows[rowIndex]);
        }

        private void gridEntries_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var cell = gridEntries[e.ColumnIndex, e.RowIndex];
                if (cell is DataGridViewComboBoxCell)
                {
                    gridEntries.BeginEdit(true);
                }
            }
        }

        private async void btnReleaseTasks_Click(object sender, EventArgs e)
        {
            bool anyRowReady = gridEntries.Rows
             .Cast<DataGridViewRow>()
             .Any(row => (bool?)row.Cells["colOk"].Value == true);

            if (!anyRowReady)
            {
                MessageBox.Show(
                    "É necessário ao menos um registro revisado para prosseguir.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            launchTasks = true;

            this.Close();
        }

        public async System.Threading.Tasks.Task LaunchTasks()
        {
            var user = UserDataManager.LoadUserData();

            foreach (DataGridViewRow row in gridEntries.Rows)
            {
                if ((bool?)row.Cells["colOk"].Value != true)
                    continue;

                await ReleaseHoursForRowAsync(row, user.ApiKey);
            }

            await RemoveLaunchedLocalEntriesAsync();
        }

        private async System.Threading.Tasks.Task ReleaseHoursForRowAsync(DataGridViewRow row, string apiKey)
        {
            string taskNum = row.Cells["colTaskNum"].Value?.ToString();
            string dateText = row.Cells["colDate"].Value?.ToString();
            string durationHhMm = row.Cells["colDuration"].Value?.ToString();
            string projectIdStr = row.Cells["colProject"].Value?.ToString();
            string activityIdStr = row.Cells["colActivity"].Value?.ToString();
            string comment = row.Cells["colComment"].Value?.ToString() ?? "";
            string projInd = row.Cells["colProjectInd"].Value?.ToString();
            string execPlace = row.Cells["colExecPlace"].Value?.ToString();
            string overtime = row.Cells["colOvertime"].Value?.ToString();

            DateTime date = ParseDate(dateText);

            bool isIssue = int.TryParse(taskNum, out int issueId);

            var existingEntry = await GetExistingTimeEntryAsync(
                apiKey,
                isIssue,
                isIssue ? issueId : (int?)null,
                isIssue ? null : (int?)Convert.ToInt32(projectIdStr),
                isIssue ? null : comment,
                date
            );

            if (existingEntry != null)
            {
                existingEntry.Hours = SumDoubleAndHhMm(existingEntry.Hours, durationHhMm);

                if (!isIssue)
                    existingEntry.Comments = comment;

                existingEntry.ActivityId = Convert.ToInt32(activityIdStr);

                existingEntry.CustomFields = new List<CustomFieldDto>
                {
                    new CustomFieldDto { Id = 143, Value = projInd },
                    new CustomFieldDto { Id = 21,  Value = execPlace },
                    new CustomFieldDto { Id = 117, Value = overtime },
                };

                await UpdateTimeEntryAsync(apiKey, existingEntry);
            }
            else
            {
                double hoursAsDouble = ConvertHhMmToDouble(durationHhMm);

                var newEntry = new TimeEntryDto
                {
                    IssueId = isIssue ? issueId : (int?)null,
                    ProjectId = isIssue ? null : (int?)Convert.ToInt32(projectIdStr),
                    SpentOn = date.ToString("yyyy-MM-dd"),
                    Hours = hoursAsDouble,  
                    ActivityId = Convert.ToInt32(activityIdStr),
                    Comments = isIssue ? "" : comment,
                    CustomFields = new List<CustomFieldDto>
                    {
                        new CustomFieldDto { Id = 143, Value = projInd },
                        new CustomFieldDto { Id = 21,  Value = execPlace },
                        new CustomFieldDto { Id = 117, Value = overtime },
                    }
                };

                await CreateTimeEntryAsync(apiKey, newEntry);
            }
        }

        private async Task<TimeEntryDto> GetExistingTimeEntryAsync(
            string apiKey,
            bool isIssue,
            int? issueId,
            int? projectId,
            string customTaskId,
            DateTime date
        )
        {
            var user = UserDataManager.LoadUserData();
            int userId = user.Id;

            string baseUrl = "https://redmine.questor.com.br/time_entries.json?limit=100";
            string dateParam = date.ToString("yyyy-MM-dd");
            baseUrl += $"&user_id={userId}&spent_on={dateParam}";

            if (isIssue && issueId.HasValue)
            {
                baseUrl += $"&issue_id={issueId.Value}";
            }
            else if (!isIssue && projectId.HasValue)
            {
                baseUrl += $"&project_id={projectId.Value}";
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Redmine-API-Key", apiKey);

                var response = await client.GetAsync(baseUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TimeEntriesResult>(json);

                if (result.TimeEntries == null || !result.TimeEntries.Any())
                    return null;

                if (isIssue)
                {
                    return result.TimeEntries.FirstOrDefault();
                }
                else
                {
                    return result.TimeEntries
                        .FirstOrDefault(te => te.Comments?.Equals(customTaskId, StringComparison.OrdinalIgnoreCase) == true);
                }
            }
        }

        private async System.Threading.Tasks.Task UpdateTimeEntryAsync(string apiKey, TimeEntryDto dto)
        {
            string url = $"https://redmine.questor.com.br/time_entries/{dto.Id}.json";

            var body = new
            {
                time_entry = new
                {
                    hours = dto.Hours, 
                    comments = dto.Comments,
                    activity_id = dto.ActivityId,
                    custom_fields = dto.CustomFields
                        .Select(cf => new { id = cf.Id, value = cf.Value })
                        .ToArray()
                }
            };

            var jsonBody = JsonSerializer.Serialize(body);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Redmine-API-Key", apiKey);

                var request = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
        }

        private async System.Threading.Tasks.Task CreateTimeEntryAsync(string apiKey, TimeEntryDto dto)
        {
            string url = "https://redmine.questor.com.br/time_entries.json";

            var body = new
            {
                time_entry = new
                {
                    project_id = dto.ProjectId,
                    issue_id = dto.IssueId,
                    spent_on = dto.SpentOn,
                    hours = dto.Hours,
                    activity_id = dto.ActivityId,
                    comments = dto.Comments,
                    custom_fields = dto.CustomFields
                        .Select(cf => new { id = cf.Id, value = cf.Value })
                        .ToArray()
                }
            };

            var jsonBody = JsonSerializer.Serialize(body);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Redmine-API-Key", apiKey);

                var response = await client.PostAsync(
                    url,
                    new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json")
                );
                response.EnsureSuccessStatusCode();
            }
        }

        private double SumDoubleAndHhMm(double existingHours, string hhmm)
        {
            int existingSeconds = (int)(existingHours * 3600);
            int newSeconds = ConvertHhMmToSeconds(hhmm);

            int totalSeconds = existingSeconds + newSeconds;
            return totalSeconds / 3600.0; 
        }

      
        private double ConvertHhMmToDouble(string hhmm)
        {
            int seconds = ConvertHhMmToSeconds(hhmm);
            return seconds / 3600.0;
        }

        private int ConvertHhMmToSeconds(string hhmm)
        {
            if (TimeSpan.TryParse(hhmm, out var ts))
                return (int)ts.TotalSeconds;
            return 0;
        }

        private string ConvertSecondsToHhMm(int totalSeconds)
        {
            var ts = TimeSpan.FromSeconds(totalSeconds);
            return ts.ToString(@"hh\:mm");
        }

        private DateTime ParseDate(string ddMMyyyy)
        {
            DateTime.TryParse(ddMMyyyy, out var dt);
            return dt;
        }

        private async System.Threading.Tasks.Task RemoveLaunchedLocalEntriesAsync()
        {
            foreach (DataGridViewRow row in gridEntries.Rows)
            {
                if ((bool?)row.Cells["colOk"].Value != true)
                    continue;

                await _localEntriesRepo.Delete((int)row.Cells["colId"].Value);
            }
        }
    }
}
