using journey_control.Enums;
using journey_control.Helpers.AppData;
using journey_control.Helpers.DataSync;
using journey_control.Models;
using journey_control.Repositories;
using journey_control.Services;
using journey_control.Views.Components.Cards;
using journey_control.Views.Components.Forms;
using Task = System.Threading.Tasks.Task;

namespace journey_control.Views
{
    public partial class MainForm : Form
    {
        public Models.Version Version { get; set; }
        public User User { get; set; }

        private readonly TaskRepo _taskRepo = new TaskRepo();
        private readonly VersionRepo _versionRepo = new VersionRepo();
        private ICollection<Models.Task> tasks;
        private DateTime currentDate;

        public MainForm()
        {
            InitializeComponent();

            currentDate = DateTime.UtcNow;
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

        private async Task LoadTasksAsync()
        {
            ShowLoading(true);

            try
            {
                var newTasks = await _taskRepo.GetAllPerUserAndDate(currentDate);

                if (!tasks?.SequenceEqual(newTasks) ?? true)
                {
                    tasks = newTasks;

                    pnlTaskList.Controls.Clear();

                    foreach (var task in tasks)
                        AddTaskCard(task);

                    pnlTaskList.PerformLayout();
                    pnlTaskList.Refresh();
                }

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

        private void AddTaskCard(Models.Task task)
        {
            var taskCard = new TaskCard
            {
                TaskNumber = task.Id.ToString(),
                TaskTitle = task.Title,
                TaskSize = $"Tamanho estimado: {GetDescrSize(task.Size)}",
                TimerText = "00:00:00",
                Width = 300,
                Height = 150
            };

            int cardWidth = (pnlTaskList.ClientSize.Width - 40) / 2;
            taskCard.Width = cardWidth;
            taskCard.Height = 200;

            pnlTaskList.Controls.Add(taskCard);
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
                    await NavigateToDateAsync(DateTime.SpecifyKind(ev.Start, DateTimeKind.Utc));
                    calendarForm.Close();
                };

                calendarForm.Controls.Add(calendar);
                calendarForm.ShowDialog();
            }
        }

        private async Task NavigateToDateAsync(DateTime newDate)
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

        private void FilterTasksByDate(DateTime date)
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
            using (var inputForm = new InputForm())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    var taskNumber = inputForm.InputText;

                    if (int.TryParse(taskNumber, out int taskId))
                    {
                        ShowLoading(true);

                        try
                        {
                            var redmineService = new RedmineService();
                            var issue = await redmineService.GetIssueAsync(taskId);

                            if (issue != null)
                            {
                                // Adiciona ou atualiza a tarefa no banco
                                await _taskRepo.AddTaskFromIssueAsync(issue);

                                // Verifica se a data atual está no intervalo
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
                            MessageBox.Show($"Erro ao adicionar tarefa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            ShowLoading(false);
                        }
                    }
                    else
                    {
                        MessageBox.Show("O número da tarefa informado é inválido. Por favor, insira um número válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

    }
}
