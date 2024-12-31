namespace journey_control.Views
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

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
            pnlHeader = new Panel();
            panel10 = new Panel();
            btnAddTask = new Button();
            panel9 = new Panel();
            panel8 = new Panel();
            btnRefreshTasks = new Button();
            panel11 = new Panel();
            panel7 = new Panel();
            panel6 = new Panel();
            btnCalendar = new Button();
            btnNext = new Button();
            lblDate = new Label();
            btnPrev = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            txtTaskSearch = new TextBox();
            panel4 = new Panel();
            pictureBox2 = new PictureBox();
            panel5 = new Panel();
            txtName = new Label();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            pnlFooter = new Panel();
            pnlTaskList = new FlowLayoutPanel();
            pnlPaddingL = new Panel();
            pnlPaddingR = new Panel();
            pnlLoading = new Components.Panels.TransparentPanel();
            pictureBox3 = new PictureBox();
            pnlHeader.SuspendLayout();
            panel10.SuspendLayout();
            panel8.SuspendLayout();
            panel6.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(panel10);
            pnlHeader.Controls.Add(panel9);
            pnlHeader.Controls.Add(panel8);
            pnlHeader.Controls.Add(panel11);
            pnlHeader.Controls.Add(panel7);
            pnlHeader.Controls.Add(panel6);
            pnlHeader.Controls.Add(panel2);
            pnlHeader.Controls.Add(panel3);
            pnlHeader.Controls.Add(panel5);
            pnlHeader.Controls.Add(txtName);
            pnlHeader.Controls.Add(pictureBox1);
            pnlHeader.Controls.Add(panel1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1212, 90);
            pnlHeader.TabIndex = 0;
            // 
            // panel10
            // 
            panel10.Controls.Add(btnAddTask);
            panel10.Dock = DockStyle.Right;
            panel10.Location = new Point(797, 0);
            panel10.Name = "panel10";
            panel10.Size = new Size(182, 90);
            panel10.TabIndex = 9;
            // 
            // btnAddTask
            // 
            btnAddTask.BackColor = Color.Transparent;
            btnAddTask.Cursor = Cursors.Hand;
            btnAddTask.Dock = DockStyle.Fill;
            btnAddTask.FlatAppearance.BorderSize = 0;
            btnAddTask.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnAddTask.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnAddTask.FlatStyle = FlatStyle.Flat;
            btnAddTask.ForeColor = Color.FromArgb(218, 218, 218);
            btnAddTask.Image = Properties.Resources.Resources.icon_add;
            btnAddTask.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddTask.Location = new Point(0, 0);
            btnAddTask.Name = "btnAddTask";
            btnAddTask.Size = new Size(182, 90);
            btnAddTask.TabIndex = 6;
            btnAddTask.Text = "   Adicionar Tarefa";
            btnAddTask.UseVisualStyleBackColor = false;
            btnAddTask.Click += btnAddTask_Click;
            // 
            // panel9
            // 
            panel9.Dock = DockStyle.Right;
            panel9.Location = new Point(979, 0);
            panel9.Name = "panel9";
            panel9.Size = new Size(22, 90);
            panel9.TabIndex = 8;
            // 
            // panel8
            // 
            panel8.Controls.Add(btnRefreshTasks);
            panel8.Dock = DockStyle.Right;
            panel8.Location = new Point(1001, 0);
            panel8.Name = "panel8";
            panel8.Size = new Size(182, 90);
            panel8.TabIndex = 7;
            // 
            // btnRefreshTasks
            // 
            btnRefreshTasks.BackColor = Color.Transparent;
            btnRefreshTasks.Cursor = Cursors.Hand;
            btnRefreshTasks.Dock = DockStyle.Fill;
            btnRefreshTasks.FlatAppearance.BorderSize = 0;
            btnRefreshTasks.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnRefreshTasks.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnRefreshTasks.FlatStyle = FlatStyle.Flat;
            btnRefreshTasks.ForeColor = Color.FromArgb(218, 218, 218);
            btnRefreshTasks.Image = Properties.Resources.Resources.icon_refresh;
            btnRefreshTasks.ImageAlign = ContentAlignment.MiddleLeft;
            btnRefreshTasks.Location = new Point(0, 0);
            btnRefreshTasks.Name = "btnRefreshTasks";
            btnRefreshTasks.Size = new Size(182, 90);
            btnRefreshTasks.TabIndex = 7;
            btnRefreshTasks.Text = "   Atualizar Tarefas";
            btnRefreshTasks.UseVisualStyleBackColor = false;
            btnRefreshTasks.Click += btnRefreshTasks_Click;
            // 
            // panel11
            // 
            panel11.Dock = DockStyle.Right;
            panel11.Location = new Point(1183, 0);
            panel11.Name = "panel11";
            panel11.Size = new Size(29, 90);
            panel11.TabIndex = 10;
            // 
            // panel7
            // 
            panel7.Dock = DockStyle.Left;
            panel7.Location = new Point(685, 0);
            panel7.Name = "panel7";
            panel7.Size = new Size(22, 90);
            panel7.TabIndex = 6;
            // 
            // panel6
            // 
            panel6.Controls.Add(btnCalendar);
            panel6.Controls.Add(btnNext);
            panel6.Controls.Add(lblDate);
            panel6.Controls.Add(btnPrev);
            panel6.Dock = DockStyle.Left;
            panel6.Location = new Point(464, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(221, 90);
            panel6.TabIndex = 4;
            // 
            // btnCalendar
            // 
            btnCalendar.BackColor = Color.Transparent;
            btnCalendar.Cursor = Cursors.Hand;
            btnCalendar.FlatAppearance.BorderSize = 0;
            btnCalendar.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnCalendar.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnCalendar.FlatStyle = FlatStyle.Flat;
            btnCalendar.Image = Properties.Resources.Resources.icon_calendar;
            btnCalendar.Location = new Point(144, 27);
            btnCalendar.Name = "btnCalendar";
            btnCalendar.Size = new Size(40, 40);
            btnCalendar.TabIndex = 5;
            btnCalendar.UseVisualStyleBackColor = false;
            btnCalendar.Click += btnCalendar_Click;
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.Transparent;
            btnNext.Cursor = Cursors.Hand;
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnNext.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Image = Properties.Resources.Resources.icon_next;
            btnNext.Location = new Point(178, 29);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(40, 40);
            btnNext.TabIndex = 4;
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            // 
            // lblDate
            // 
            lblDate.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblDate.ForeColor = Color.FromArgb(218, 218, 218);
            lblDate.Location = new Point(32, 6);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(118, 82);
            lblDate.TabIndex = 3;
            lblDate.Text = "00/00/0000";
            lblDate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPrev
            // 
            btnPrev.BackColor = Color.Transparent;
            btnPrev.Cursor = Cursors.Hand;
            btnPrev.FlatAppearance.BorderSize = 0;
            btnPrev.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnPrev.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnPrev.FlatStyle = FlatStyle.Flat;
            btnPrev.Image = Properties.Resources.Resources.icon_prev;
            btnPrev.Location = new Point(0, 29);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(40, 40);
            btnPrev.TabIndex = 0;
            btnPrev.UseVisualStyleBackColor = false;
            btnPrev.Click += btnPrev_Click;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(442, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(22, 90);
            panel2.TabIndex = 3;
            // 
            // panel3
            // 
            panel3.Controls.Add(txtTaskSearch);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(pictureBox2);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(258, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(184, 90);
            panel3.TabIndex = 4;
            // 
            // txtTaskSearch
            // 
            txtTaskSearch.BackColor = Color.FromArgb(28, 23, 23);
            txtTaskSearch.BorderStyle = BorderStyle.None;
            txtTaskSearch.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            txtTaskSearch.ForeColor = Color.FromArgb(218, 218, 218);
            txtTaskSearch.Location = new Point(53, 29);
            txtTaskSearch.MaxLength = 7;
            txtTaskSearch.Name = "txtTaskSearch";
            txtTaskSearch.Size = new Size(117, 36);
            txtTaskSearch.TabIndex = 5;
            txtTaskSearch.TextChanged += txtTaskSearch_TextChanged;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(218, 218, 218);
            panel4.Location = new Point(53, 68);
            panel4.Name = "panel4";
            panel4.Size = new Size(117, 2);
            panel4.TabIndex = 2;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox2.Image = Properties.Resources.Resources.icon_search;
            pictureBox2.Location = new Point(0, 25);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(48, 50);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // panel5
            // 
            panel5.Dock = DockStyle.Left;
            panel5.Location = new Point(236, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(22, 90);
            panel5.TabIndex = 5;
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Left;
            txtName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            txtName.ForeColor = Color.FromArgb(218, 218, 218);
            txtName.Location = new Point(109, 0);
            txtName.Name = "txtName";
            txtName.Size = new Size(127, 90);
            txtName.TabIndex = 2;
            txtName.Text = "Nome";
            txtName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Left;
            pictureBox1.Image = Properties.Resources.Resources.icon_user;
            pictureBox1.Location = new Point(58, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(51, 90);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(58, 90);
            panel1.TabIndex = 1;
            // 
            // pnlFooter
            // 
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 578);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(1212, 111);
            pnlFooter.TabIndex = 1;
            // 
            // pnlTaskList
            // 
            pnlTaskList.AutoScroll = true;
            pnlTaskList.Dock = DockStyle.Fill;
            pnlTaskList.Location = new Point(70, 90);
            pnlTaskList.Name = "pnlTaskList";
            pnlTaskList.Size = new Size(1078, 488);
            pnlTaskList.TabIndex = 3;
            pnlTaskList.Resize += pnlTaskList_Resize;
            // 
            // pnlPaddingL
            // 
            pnlPaddingL.Dock = DockStyle.Left;
            pnlPaddingL.Location = new Point(0, 90);
            pnlPaddingL.Name = "pnlPaddingL";
            pnlPaddingL.Size = new Size(70, 488);
            pnlPaddingL.TabIndex = 0;
            // 
            // pnlPaddingR
            // 
            pnlPaddingR.Dock = DockStyle.Right;
            pnlPaddingR.Location = new Point(1148, 90);
            pnlPaddingR.Name = "pnlPaddingR";
            pnlPaddingR.Size = new Size(64, 488);
            pnlPaddingR.TabIndex = 1;
            // 
            // pnlLoading
            // 
            pnlLoading.BackColor = Color.Transparent;
            pnlLoading.BackgroundColor = Color.Black;
            pnlLoading.Controls.Add(pictureBox3);
            pnlLoading.Dock = DockStyle.Fill;
            pnlLoading.Location = new Point(70, 90);
            pnlLoading.Name = "pnlLoading";
            pnlLoading.Opacity = 128;
            pnlLoading.Size = new Size(1078, 488);
            pnlLoading.TabIndex = 0;
            pnlLoading.Visible = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Dock = DockStyle.Fill;
            pictureBox3.Image = Properties.Resources.Resources.icon_loading;
            pictureBox3.Location = new Point(0, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(1078, 488);
            pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 23, 23);
            ClientSize = new Size(1212, 689);
            Controls.Add(pnlLoading);
            Controls.Add(pnlTaskList);
            Controls.Add(pnlPaddingR);
            Controls.Add(pnlPaddingL);
            Controls.Add(pnlHeader);
            Controls.Add(pnlFooter);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            pnlHeader.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlLoading.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Panel pnlFooter;
        private FlowLayoutPanel pnlTaskList;
        private Panel pnlPaddingL;
        private Panel pnlPaddingR;
        private PictureBox pictureBox1;
        private Label txtName;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private PictureBox pictureBox2;
        private Panel panel4;
        private TextBox txtTaskSearch;
        private Panel panel6;
        private Panel panel5;
        private Button btnPrev;
        private Label lblDate;
        private Button btnCalendar;
        private Button btnNext;
        private Components.Panels.TransparentPanel pnlLoading;
        private PictureBox pictureBox3;
        private Panel panel10;
        private Panel panel9;
        private Panel panel8;
        private Panel panel11;
        private Panel panel7;
        private Button btnAddTask;
        private Button btnRefreshTasks;
    }
}