using journey_control.Enums;
using journey_control.Helpers.AppData;
using journey_control.Helpers.DataSync;
using journey_control.Models;
using journey_control.Repositories;
using journey_control.Services;
using journey_control.Views.Components.Cards;
using journey_control.Views.Components.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Task = System.Threading.Tasks.Task;

namespace journey_control.Views
{
    public partial class MainForm : Form
    {
        public Models.Version Version { get; set; }
        public Models.User User { get; set; }

        private readonly TaskRepo _taskRepo = new TaskRepo();
        private readonly VersionRepo _versionRepo = new VersionRepo();
        private ICollection<Models.Task> tasks;
        private DateOnly currentDate;

        public MainForm()
        {
            InitializeComponent();

            txtAppVersion.Text = $"Versão: {Application.ProductVersion.Split('+')[0]}";
            currentDate = DateOnly.FromDateTime(DateTime.Today);
            UpdateDateLabel();

            this.Load += MainForm_Load;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            ShowLoading(true);

            try
            {
                User = UserDataManager.LoadUserData();
                if (User == null)
                    throw new Exception("Usuário não encontrado.");

                txtName.Text = User.FirstName;

                UpdateDateLabel();
                await InitializeDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private async Task InitializeDataAsync()
        {
            try
            {
                Version = await _versionRepo.GetVersionPerDate(currentDate);
                if (Version == null)
                    throw new Exception("Nenhuma versão encontrada para a data atual.");

                await TaskSync.Run(currentDate);
                await LoadTasksAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task ControlTotalizers()
        {
            EntriesRepo entriesRepo = new EntriesRepo();
            LocalEntriesRepo localEntriesRepo = new LocalEntriesRepo();

            string releasedTime = TimeSpan.FromSeconds(await entriesRepo.GetRealesedTime(currentDate)).ToString(@"hh\:mm\:ss");
            string beggingTime = TimeSpan.FromSeconds(await localEntriesRepo.GetRealesedTime(currentDate)).ToString(@"hh\:mm\:ss");
            string totalDuration = TimeSpan.FromSeconds(await entriesRepo.GetTotalSpentTimePerDate(currentDate)).ToString(@"hh\:mm\:ss");
            string workDuration = TimeSpan.FromSeconds(await entriesRepo.GetWorkTimeSpentPerDate(currentDate)).ToString(@"hh\:mm\:ss");
            string studyDuration = TimeSpan.FromSeconds(await entriesRepo.GetStudyTimeSpentPerDate(currentDate)).ToString(@"hh\:mm\:ss");

            txtTotalTime.Text = totalDuration;
            txtWorkTime.Text = workDuration;
            txtStudyTime.Text = studyDuration;
            txtBeggingTime.Text = beggingTime;
            txtReleasedTime.Text = releasedTime;
        }

        public async Task LoadTasksAsync()
        {
            ShowLoading(true);

            try
            {
                tasks = await _taskRepo.GetAllPerUserAndDate(currentDate);

                var entriesRepo = new EntriesRepo();
                var localEntriesRepo = new LocalEntriesRepo();

                pnlTaskList.Controls.Clear();

                foreach (var task in tasks)
                {
                    var totalDuration = await entriesRepo.GetTotalTimeByTaskAndDate(task.Id, currentDate) +
                                        await localEntriesRepo.GetTotalTimeByTaskAndDate(task.Id, currentDate);

                    task.Entries = new List<Entrie>
                    {
                        new Entrie
                        {
                            TaskId = task.Id,
                            Duration = totalDuration,
                            DateEntrie = currentDate,
                        }
                    };

                    AddTaskCard(task);
                }

                await ControlTotalizers();

                pnlTaskList.PerformLayout();
                pnlTaskList.Refresh();

                FilterTasksByDate(currentDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar tarefas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private string GetDescrSize(SizeE size)
        {
            if (size == SizeE.N)
                return "Sem tamanho";
            else
                return $"{size}";
        }

        private async void AddTaskCard(Models.Task task)
        {
            var totalDuration = task.Entries?.Where(e => e.DateEntrie == currentDate).Sum(e => e.Duration) ?? 0;
            var formattedDuration = TimeSpan.FromSeconds(totalDuration).ToString(@"hh\:mm\:ss");

            var taskCard = new TaskCard
            {
                TaskNumber = task.Id.ToString(),
                TaskTitle = task.Title,
                TaskSize = $"Tamanho estimado: {GetDescrSize(task.Size)}",
                TimerText = formattedDuration,
                Width = 300,
                Height = 150
            };

            taskCard.ControlButtons(currentDate == DateOnly.FromDateTime(DateTime.Today));
            taskCard.TimerControlClicked += TaskCard_TimerControlClicked;

            int cardWidth = (pnlTaskList.ClientSize.Width - 40) / 2;
            taskCard.Width = cardWidth;
            taskCard.Height = 200;

            pnlTaskList.Controls.Add(taskCard);
        }

        private void TaskCard_TimerControlClicked(object sender, string taskNumber)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskNumber);

            if (task != null)
            {
                var timeControlForm = new TimeControlForm(task);

                Hide();
                timeControlForm.Show();
            }
        }

        private void txtTaskSearch_TextChanged(object sender, EventArgs e)
        {
            foreach (Control control in pnlTaskList.Controls)
            {
                if (control is TaskCard taskCard)
                {
                    taskCard.Visible = taskCard.TaskNumber.Contains(txtTaskSearch.Text);
                }
            }

            pnlTaskList.PerformLayout();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            await NavigateToDateAsync(currentDate.AddDays(-1));
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            await NavigateToDateAsync(currentDate.AddDays(1));
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            using (MonthCalendar calendar = new MonthCalendar())
            {
                calendar.MaxSelectionCount = 1;
                calendar.TodayDate = DateTime.Today;
                calendar.SelectionStart = currentDate.ToDateTime(TimeOnly.MinValue);

                Size calendarSize = calendar.PreferredSize;

                Form calendarForm = new Form
                {
                    Text = "Selecione uma data",
                    Width = calendarSize.Width + 65,
                    Height = calendarSize.Height + 47,
                    StartPosition = FormStartPosition.CenterScreen,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                };

                calendar.Dock = DockStyle.Fill;

                calendar.DateSelected += async (s, ev) =>
                {
                    calendarForm.Close();
                    await NavigateToDateAsync(DateOnly.FromDateTime(ev.Start));
                };

                calendarForm.Controls.Add(calendar);
                calendarForm.ShowDialog();
            }
        }

        private async Task NavigateToDateAsync(DateOnly newDate)
        {
            if (newDate == currentDate)
                return;

            txtTaskSearch.Clear();

            ShowLoading(true);

            try
            {
                currentDate = newDate;
                UpdateDateLabel();

                Version = await _versionRepo.GetVersionPerDate(currentDate);
                await TaskSync.Run(currentDate);
                await LoadTasksAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao navegar para nova data: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void FilterTasksByDate(DateOnly date)
        {
            try
            {
                foreach (Control control in pnlTaskList.Controls)
                {
                    if (control is TaskCard taskCard)
                    {
                        var task = tasks.FirstOrDefault(t => t.Id == taskCard.TaskNumber);
                        if (task != null)
                            taskCard.Visible = date >= task.StartDate && date <= task.DueDate;
                    }
                }

                pnlTaskList.PerformLayout();
                pnlTaskList.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao filtrar tarefas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowLoading(bool isLoading)
        {
            pnlLoading.Visible = isLoading;
            pnlLoading.BringToFront();
            btnCalendar.Enabled = !isLoading;
            btnNext.Enabled = !isLoading;
            btnPrev.Enabled = !isLoading;
            txtTaskSearch.Enabled = !isLoading;
            btnAddTask.Enabled = !isLoading;
            btnRefreshTasks.Enabled = !isLoading;
            btnReleaseTasks.Enabled = !isLoading;

            if (isLoading)
            {
                txtWorkTime.Text = "";
                txtStudyTime.Text = "";
                txtTotalTime.Text = "";
                txtReleasedTime.Text = "";
                txtBeggingTime.Text = "";
            }
        }

        private void pnlTaskList_Resize(object sender, EventArgs e)
        {
            foreach (Control control in pnlTaskList.Controls)
            {
                int cardWidth = (pnlTaskList.ClientSize.Width - 40) / 2;
                control.Width = cardWidth;
                control.Height = 200;
            }
        }

        private void UpdateDateLabel()
        {
            lblDate.Text = currentDate.ToString("dd/MM/yyyy");
        }

        private async void btnRefreshTasks_Click(object sender, EventArgs e)
        {
            ShowLoading(true);

            try
            {
                txtTaskSearch.Text = "";

                await TaskSync.Run(currentDate);
                await LoadTasksAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar tarefas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }

        }

        private async void btnAddTask_Click(object sender, EventArgs e)
        {
            ShowLoading(true);

            try
            {
                using (var inputForm = new InputForm())
                {
                    if (inputForm.ShowDialog() == DialogResult.OK)
                    {
                        var user = UserDataManager.LoadUserData();
                        Models.Version version = await _versionRepo.GetVersionPerDate(currentDate);

                        if (inputForm.isCustom)
                        {
                            var newTask = new Models.Task
                            {
                                Id = inputForm.InputText,
                                Title = inputForm.DescrText,
                                Description = "Tarefa customizada",
                                StartDate = inputForm.StartDate,
                                DueDate = inputForm.DueDate,
                                Size = SizeE.N,
                                Status = "Nenhum",
                                Project = user.ProjectId,
                                UserId = user.Id,
                                VersionId = version.Id,
                                VersionProjectId = version.ProjectId,
                            };

                            await _taskRepo.Add(newTask);

                            if (currentDate < newTask.StartDate || currentDate > newTask.DueDate)
                            {
                                MessageBox.Show(
                                    $"A tarefa foi adicionada, mas está entre as datas: {newTask.StartDate.ToString("dd/MM/yyyy")} - {newTask.DueDate.ToString("dd/MM/yyyy")}.",
                                    "Aviso",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }

                            await LoadTasksAsync();

                            MessageBox.Show("Tarefa customizada adicionada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (int.TryParse(inputForm.InputText, out int taskId))
                            {
                                try
                                {
                                    var redmineService = new RedmineService();
                                    var issue = await redmineService.GetIssueAsync(taskId);

                                    if (issue != null)
                                    {
                                        await _taskRepo.AddTaskFromIssueAsync(issue);

                                        if (currentDate < issue.StartDate || currentDate > issue.DueDate)
                                        {
                                            MessageBox.Show(
                                                $"A tarefa foi adicionada, mas está entre as datas: {issue.StartDate?.ToString("dd/MM/yyyy")} - {issue.DueDate?.ToString("dd/MM/yyyy")}.",
                                                "Aviso",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                                        }

                                        await LoadTasksAsync();
                                        MessageBox.Show("Tarefa adicionada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Tarefa não encontrada no Redmine.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Erro ao buscar a tarefa no Redmine: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("O número da tarefa informado é inválido. Por favor, insira um número válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        await ControlTotalizers();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private async void btnReleaseTasks_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBeggingTime.Text == "00:00:00")
                    throw new Exception("Não existem horas pendentes de lançamento");

                LocalEntriesRepo localEntriesRepo = new LocalEntriesRepo();                
                var localEntries = await localEntriesRepo.GetAllPerDate(currentDate);

                if (!localEntries.Any(x => x.Duration >= 60))
                    throw new Exception("Não há registros pendentes de tempo com pelo menos um minuto de duração");

                var releaseEntriesForm = new ReleaseEntriesForm(currentDate);

                await releaseEntriesForm.LoadDataAsync();
                releaseEntriesForm.PopulateGrid();
                releaseEntriesForm.ShowDialog();

                if (releaseEntriesForm.launchTasks)
                {
                    ShowLoading(true);
                    await releaseEntriesForm.LaunchTasks();
                    await TaskSync.Run(currentDate);
                    await LoadTasksAsync();
                    await ControlTotalizers();
                    ShowLoading(false);
                    MessageBox.Show($"Lançamento de horas realizado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao lançar horas: {ex.Message}.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
