namespace journey_control.Views.Components.Forms
{
    partial class InputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel6 = new Panel();
            panel8 = new Panel();
            panel11 = new Panel();
            txtNum = new TextBox();
            panel10 = new Panel();
            panel9 = new Panel();
            panel7 = new Panel();
            panel13 = new Panel();
            btnCancel = new Button();
            btnOk = new Button();
            panel12 = new Panel();
            panel5 = new Panel();
            panel4 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel6.SuspendLayout();
            panel8.SuspendLayout();
            panel11.SuspendLayout();
            panel7.SuspendLayout();
            panel13.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(384, 132);
            panel1.TabIndex = 0;
            // 
            // panel6
            // 
            panel6.Controls.Add(panel8);
            panel6.Controls.Add(panel7);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(79, 28);
            panel6.Name = "panel6";
            panel6.Size = new Size(226, 80);
            panel6.TabIndex = 4;
            // 
            // panel8
            // 
            panel8.Controls.Add(panel11);
            panel8.Controls.Add(panel10);
            panel8.Controls.Add(panel9);
            panel8.Dock = DockStyle.Fill;
            panel8.Location = new Point(0, 0);
            panel8.Name = "panel8";
            panel8.Size = new Size(226, 29);
            panel8.TabIndex = 1;
            // 
            // panel11
            // 
            panel11.Controls.Add(txtNum);
            panel11.Dock = DockStyle.Fill;
            panel11.Location = new Point(26, 0);
            panel11.Name = "panel11";
            panel11.Size = new Size(174, 29);
            panel11.TabIndex = 2;
            // 
            // txtNum
            // 
            txtNum.BackColor = Color.FromArgb(45, 45, 45);
            txtNum.BorderStyle = BorderStyle.None;
            txtNum.Dock = DockStyle.Bottom;
            txtNum.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtNum.ForeColor = SystemColors.Info;
            txtNum.Location = new Point(0, 7);
            txtNum.MaxLength = 7;
            txtNum.Name = "txtNum";
            txtNum.PlaceholderText = "Insira o número";
            txtNum.Size = new Size(174, 22);
            txtNum.TabIndex = 0;
            txtNum.TextAlign = HorizontalAlignment.Center;
            // 
            // panel10
            // 
            panel10.Dock = DockStyle.Right;
            panel10.Location = new Point(200, 0);
            panel10.Name = "panel10";
            panel10.Size = new Size(26, 29);
            panel10.TabIndex = 1;
            // 
            // panel9
            // 
            panel9.Dock = DockStyle.Left;
            panel9.Location = new Point(0, 0);
            panel9.Name = "panel9";
            panel9.Size = new Size(26, 29);
            panel9.TabIndex = 0;
            // 
            // panel7
            // 
            panel7.Controls.Add(panel13);
            panel7.Controls.Add(panel12);
            panel7.Dock = DockStyle.Bottom;
            panel7.Location = new Point(0, 29);
            panel7.Name = "panel7";
            panel7.Size = new Size(226, 51);
            panel7.TabIndex = 0;
            // 
            // panel13
            // 
            panel13.Controls.Add(btnCancel);
            panel13.Controls.Add(btnOk);
            panel13.Dock = DockStyle.Fill;
            panel13.Location = new Point(0, 20);
            panel13.Name = "panel13";
            panel13.Size = new Size(226, 31);
            panel13.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(45, 45, 45);
            btnCancel.Dock = DockStyle.Right;
            btnCancel.FlatStyle = FlatStyle.Popup;
            btnCancel.ForeColor = SystemColors.ButtonHighlight;
            btnCancel.Location = new Point(135, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(91, 31);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOk
            // 
            btnOk.BackColor = Color.FromArgb(45, 45, 45);
            btnOk.Dock = DockStyle.Left;
            btnOk.FlatStyle = FlatStyle.Popup;
            btnOk.ForeColor = SystemColors.ButtonHighlight;
            btnOk.Location = new Point(0, 0);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(91, 31);
            btnOk.TabIndex = 3;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += btnOk_Click;
            // 
            // panel12
            // 
            panel12.Dock = DockStyle.Top;
            panel12.Location = new Point(0, 0);
            panel12.Name = "panel12";
            panel12.Size = new Size(226, 20);
            panel12.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(79, 108);
            panel5.Name = "panel5";
            panel5.Size = new Size(226, 24);
            panel5.TabIndex = 3;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(79, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(226, 28);
            panel4.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(305, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(79, 132);
            panel3.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(79, 132);
            panel2.TabIndex = 0;
            // 
            // InputForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 23, 23);
            ClientSize = new Size(384, 132);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Adicionar Tarefa";
            panel1.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel7.ResumeLayout(false);
            panel13.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel6;
        private Panel panel8;
        private Panel panel11;
        private TextBox txtNum;
        private Panel panel10;
        private Panel panel9;
        private Panel panel7;
        private Panel panel5;
        private Panel panel4;
        private Panel panel3;
        private Panel panel2;
        private Panel panel13;
        private Panel panel12;
        private Button btnCancel;
        private Button btnOk;
    }
}