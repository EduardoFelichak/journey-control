namespace journey_control.Views.Components.Cards
{
    public partial class TaskCard : UserControl
    {
        public event EventHandler<string> TimerControlClicked;

        public TaskCard()
        {
            InitializeComponent();
        }

        public string TaskNumber
        {
            get => txtTaskNumber.Text;
            set => txtTaskNumber.Text = value;
        }

        public string TaskTitle
        {
            get => txtTitle.Text;
            set => txtTitle.Text = value;
        }

        public string TaskSize
        {
            get => txtSize.Text;
            set => txtSize.Text = value;
        }

        public string TimerText
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        public void ControlButtons(bool enable)
        {
            btnTimerControl.Enabled = enable;
        }

        private void btnTimerControl_Click(object sender, EventArgs e)
        {
            TimerControlClicked?.Invoke(this, TaskNumber);
        }
    }
}
