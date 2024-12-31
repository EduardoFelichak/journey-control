using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace journey_control.Views.Components.Cards
{
    public partial class TaskCard : UserControl
    {
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
    }
}
