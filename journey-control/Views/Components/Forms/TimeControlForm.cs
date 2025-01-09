using journey_control.Enums;
using journey_control.Models;
using journey_control.Properties.Resources;
using journey_control.Repositories;

namespace journey_control.Views.Components.Forms
{
    public partial class TimeControlForm : Form
    {
        private Models.Task _task;
        private TimeSpan _currentDuration = TimeSpan.Zero;
        private TimeSpan _remainingTime = TimeSpan.Zero;
        private System.Windows.Forms.Timer _timer;

        private NotifyIcon _trayIcon;
        private ContextMenuStrip _trayMenu;

        public TimeControlForm(Models.Task task)
        {
            InitializeComponent();
            InitializeTray();
            PositionFormInBottomRight();

            _task = task;

            txtTaskNumber.Text = _task.Id;
            txtTitle.Text = _task.Title;

            _timer = new System.Windows.Forms.Timer
            {
                Interval = 1000
            };

            _timer.Tick += Timer_Tick;

            LoadTaskDataAsync();
        }

        private async void LoadTaskDataAsync()
        {
            var entriesRepo = new EntriesRepo();
            var localEntriesRepo = new LocalEntriesRepo();

            var redmineTime = await entriesRepo.GetTotalTimeByTaskAndDate(_task.Id, DateOnly.FromDateTime(DateTime.Now));
            var localTime = await localEntriesRepo.GetTotalTimeByTaskAndDate(_task.Id, DateOnly.FromDateTime(DateTime.Now));

            _currentDuration = TimeSpan.FromSeconds(redmineTime + localTime);

            txtDuration.Text = _currentDuration.ToString(@"hh\:mm\:ss");

            if (_task.Size == SizeE.G)
                _remainingTime = TimeSpan.FromHours(6) - _currentDuration;
            else if (_task.Size == SizeE.M)
                _remainingTime = TimeSpan.FromHours(3) - _currentDuration;
            else if (_task.Size == SizeE.P)
                _remainingTime = TimeSpan.FromHours(1) - _currentDuration;

            txtRemainingTime.Text = _remainingTime.ToString(@"hh\:mm\:ss");

            Play();
        }

        private void PositionFormInBottomRight()
        {
            var screen = Screen.PrimaryScreen.WorkingArea;

            int x = screen.Width - Width;
            int y = screen.Height - Height;

            StartPosition = FormStartPosition.Manual;
            Location = new Point(x, y);
        }

        private void InitializeTray()
        {
            _trayMenu = new ContextMenuStrip();
            _trayMenu.Items.Add("Abrir", null, (s, e) => ShowForm());

            _trayIcon = new NotifyIcon
            {
                Icon = Resources.icon_logo,
                ContextMenuStrip = _trayMenu,
                Text = "Controle de Tempo",
                Visible = true
            };

            _trayIcon.DoubleClick += (s, e) => ShowForm();
        }

        private void ShowForm()
        {
            Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _currentDuration = _currentDuration.Add(TimeSpan.FromSeconds(1));
            txtDuration.Text = _currentDuration.ToString(@"hh\:mm\:ss");

            if (_remainingTime > TimeSpan.Zero)
            {
                _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));
                txtRemainingTime.Text = _remainingTime.ToString(@"hh\:mm\:ss");
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private async void btnHome_Click(object sender, EventArgs e)
        {
            _timer.Stop();

            var entriesRepo = new EntriesRepo();
            var localEntriesRepo = new LocalEntriesRepo();
            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            int redmineTime = await entriesRepo.GetTotalTimeByTaskAndDate(_task.Id, currentDate);
            int localTime = await localEntriesRepo.GetTotalTimeByTaskAndDate(_task.Id, currentDate);
            int totalLaunchedSeconds = redmineTime + localTime;

            int clockSeconds = (int)_currentDuration.TotalSeconds;

            int pendingSeconds = clockSeconds - totalLaunchedSeconds;

            if (pendingSeconds > 0)
            {
                var newEntry = new LocalEntrie
                {
                    TaskId = _task.Id,
                    TaskUserId = _task.UserId,
                    DateEntrie = currentDate,
                    Duration = pendingSeconds,
                };

                await localEntriesRepo.Add(newEntry);
            }

            _trayIcon.Visible = false;

            MainForm mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            if (mainForm == null)
            {
                mainForm = new MainForm();
                mainForm.Show();
            }
            else
            {
                await mainForm.LoadTasksAsync();
                mainForm.Show();
                mainForm.WindowState = FormWindowState.Normal;
            }

            Hide();
        }


        private void TimeControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();

                return;
            }
            base.OnFormClosing(e);
        }

        private void Pause()
        {
            _timer.Stop();

            btnPause.Image = Resources.icon_pause_s;
            btnPause.Enabled = false;

            btnPlay.Image = Resources.icon_play;
            btnPlay.Enabled = true;
        }

        private void Play()
        {
            _timer.Start();

            btnPause.Image = Resources.icon_pause;
            btnPause.Enabled = true;

            btnPlay.Image = Resources.icon_play_s;
            btnPlay.Enabled = false;
        }
    }
}
