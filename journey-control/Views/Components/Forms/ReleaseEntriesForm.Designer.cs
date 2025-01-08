namespace journey_control.Views.Components.Forms
{
    partial class ReleaseEntriesForm
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
            gridEntries = new DataGridView();
            panel4 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            txtName = new Label();
            pnlFooter = new Panel();
            panel12 = new Panel();
            panel21 = new Panel();
            panel13 = new Panel();
            colOk = new DataGridViewCheckBoxColumn();
            colProject = new DataGridViewComboBoxColumn();
            colTaskNum = new DataGridViewTextBoxColumn();
            colTaskTitle = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colDuration = new DataGridViewTextBoxColumn();
            colComment = new DataGridViewTextBoxColumn();
            colActivity = new DataGridViewComboBoxColumn();
            colProjectInd = new DataGridViewComboBoxColumn();
            colExecPlace = new DataGridViewComboBoxColumn();
            colOvertime = new DataGridViewComboBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridEntries).BeginInit();
            panel2.SuspendLayout();
            pnlFooter.SuspendLayout();
            panel12.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(gridEntries);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(pnlFooter);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1442, 562);
            panel1.TabIndex = 0;
            // 
            // gridEntries
            // 
            gridEntries.AllowUserToAddRows = false;
            gridEntries.AllowUserToDeleteRows = false;
            gridEntries.AllowUserToResizeColumns = false;
            gridEntries.AllowUserToResizeRows = false;
            gridEntries.BackgroundColor = Color.FromArgb(28, 23, 23);
            gridEntries.BorderStyle = BorderStyle.None;
            gridEntries.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridEntries.Columns.AddRange(new DataGridViewColumn[] { colOk, colProject, colTaskNum, colTaskTitle, colDate, colDuration, colComment, colActivity, colProjectInd, colExecPlace, colOvertime });
            gridEntries.Dock = DockStyle.Fill;
            gridEntries.GridColor = Color.FromArgb(28, 23, 23);
            gridEntries.Location = new Point(70, 74);
            gridEntries.Name = "gridEntries";
            gridEntries.Size = new Size(1302, 371);
            gridEntries.TabIndex = 6;
            gridEntries.CellValueChanged += gridEntries_CellValueChanged;
            gridEntries.CurrentCellDirtyStateChanged += gridEntries_CurrentCellDirtyStateChanged;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Right;
            panel4.Location = new Point(1372, 74);
            panel4.Name = "panel4";
            panel4.Size = new Size(70, 371);
            panel4.TabIndex = 5;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(0, 74);
            panel3.Name = "panel3";
            panel3.Size = new Size(70, 371);
            panel3.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.Controls.Add(txtName);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1442, 74);
            panel2.TabIndex = 3;
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            txtName.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            txtName.ForeColor = Color.FromArgb(218, 218, 218);
            txtName.Location = new Point(70, 9);
            txtName.Name = "txtName";
            txtName.Size = new Size(1302, 65);
            txtName.TabIndex = 3;
            txtName.Text = "Preencha todas as informações corretamente!";
            txtName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlFooter
            // 
            pnlFooter.Controls.Add(panel12);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 445);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(1442, 117);
            pnlFooter.TabIndex = 2;
            // 
            // panel12
            // 
            panel12.BackColor = Color.FromArgb(39, 39, 39);
            panel12.Controls.Add(panel21);
            panel12.Controls.Add(panel13);
            panel12.Dock = DockStyle.Fill;
            panel12.Location = new Point(0, 0);
            panel12.Name = "panel12";
            panel12.Size = new Size(1442, 117);
            panel12.TabIndex = 0;
            // 
            // panel21
            // 
            panel21.Dock = DockStyle.Right;
            panel21.Location = new Point(1372, 0);
            panel21.Name = "panel21";
            panel21.Size = new Size(70, 117);
            panel21.TabIndex = 3;
            // 
            // panel13
            // 
            panel13.Dock = DockStyle.Left;
            panel13.Location = new Point(0, 0);
            panel13.Name = "panel13";
            panel13.Size = new Size(70, 117);
            panel13.TabIndex = 0;
            // 
            // colOk
            // 
            colOk.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colOk.HeaderText = "";
            colOk.Name = "colOk";
            colOk.ReadOnly = true;
            colOk.Width = 5;
            // 
            // colProject
            // 
            colProject.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colProject.HeaderText = "Projeto";
            colProject.Name = "colProject";
            // 
            // colTaskNum
            // 
            colTaskNum.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colTaskNum.HeaderText = "Tarefa";
            colTaskNum.Name = "colTaskNum";
            colTaskNum.ReadOnly = true;
            colTaskNum.Width = 63;
            // 
            // colTaskTitle
            // 
            colTaskTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colTaskTitle.HeaderText = "Título";
            colTaskTitle.Name = "colTaskTitle";
            colTaskTitle.ReadOnly = true;
            colTaskTitle.Width = 62;
            // 
            // colDate
            // 
            colDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colDate.HeaderText = "Data";
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            colDate.Width = 56;
            // 
            // colDuration
            // 
            colDuration.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colDuration.HeaderText = "Tempo Gasto";
            colDuration.Name = "colDuration";
            colDuration.ReadOnly = true;
            colDuration.Width = 101;
            // 
            // colComment
            // 
            colComment.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colComment.FillWeight = 200F;
            colComment.HeaderText = "Comentário";
            colComment.Name = "colComment";
            // 
            // colActivity
            // 
            colActivity.AutoComplete = false;
            colActivity.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colActivity.HeaderText = "Atividade";
            colActivity.Name = "colActivity";
            // 
            // colProjectInd
            // 
            colProjectInd.AutoComplete = false;
            colProjectInd.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colProjectInd.HeaderText = "Indicativo Projeto";
            colProjectInd.Name = "colProjectInd";
            // 
            // colExecPlace
            // 
            colExecPlace.AutoComplete = false;
            colExecPlace.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colExecPlace.HeaderText = "Local de Execução";
            colExecPlace.Name = "colExecPlace";
            colExecPlace.Width = 99;
            // 
            // colOvertime
            // 
            colOvertime.AutoComplete = false;
            colOvertime.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            colOvertime.HeaderText = "Hora Extra";
            colOvertime.Name = "colOvertime";
            colOvertime.Width = 61;
            // 
            // ReleaseEntriesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 23, 23);
            ClientSize = new Size(1442, 562);
            Controls.Add(panel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ReleaseEntriesForm";
            Text = "Lançamento de Horas Pendentes";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridEntries).EndInit();
            panel2.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            panel12.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel pnlFooter;
        private Panel panel12;
        private Panel panel21;
        private Panel panel13;
        private Panel panel2;
        private DataGridView gridEntries;
        private Panel panel4;
        private Panel panel3;
        private Label txtName;
        private DataGridViewCheckBoxColumn colOk;
        private DataGridViewComboBoxColumn colProject;
        private DataGridViewTextBoxColumn colTaskNum;
        private DataGridViewTextBoxColumn colTaskTitle;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colDuration;
        private DataGridViewTextBoxColumn colComment;
        private DataGridViewComboBoxColumn colActivity;
        private DataGridViewComboBoxColumn colProjectInd;
        private DataGridViewComboBoxColumn colExecPlace;
        private DataGridViewComboBoxColumn colOvertime;
    }
}