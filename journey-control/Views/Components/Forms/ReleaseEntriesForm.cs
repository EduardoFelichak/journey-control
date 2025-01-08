using journey_control.Helpers.AppData;
using journey_control.Models;
using journey_control.Repositories;
using journey_control.Services;

namespace journey_control.Views.Components.Forms
{
    public partial class ReleaseEntriesForm : Form
    {
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
        }

        private void gridEntries_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = gridEntries.Rows[e.RowIndex];
                ValidateRow(row);
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
    }
}
