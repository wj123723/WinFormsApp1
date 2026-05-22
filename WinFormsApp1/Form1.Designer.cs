namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label7 = new Label();
            label8 = new Label();
            txtName = new TextBox();
            txtIdCard = new TextBox();
            txtBankCard = new TextBox();
            txtPhone = new TextBox();
            txtGender = new TextBox();
            txtBankName = new TextBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            dgvPersonInfo = new DataGridView();
            NameColumn = new DataGridViewTextBoxColumn();
            GenderColumn = new DataGridViewTextBoxColumn();
            IdCardColumn = new DataGridViewTextBoxColumn();
            BankNameColumn = new DataGridViewTextBoxColumn();
            BankCardColumn = new DataGridViewTextBoxColumn();
            PhoneColumn = new DataGridViewTextBoxColumn();
            CreatedTimeColumn = new DataGridViewTextBoxColumn();
            LastModifiedColumn = new DataGridViewTextBoxColumn();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            btnSave = new Button();
            btnImportExcel = new Button();
            panel1 = new Panel();
            label5 = new Label();
            btnExportExcel = new Button();
            btnGenerateTemplate = new Button();
            btnCheckInfo = new Button();
            panel2 = new Panel();
            txtSearch = new TextBox();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvPersonInfo).BeginInit();
            statusStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 42);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(52, 17);
            label1.TabIndex = 0;
            label1.Text = "姓  名：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 75);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 1;
            label2.Text = "身份证号：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 108);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 2;
            label3.Text = "银行卡号：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 141);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(68, 17);
            label4.TabIndex = 3;
            label4.Text = "电话号码：";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 174);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(52, 17);
            label7.TabIndex = 20;
            label7.Text = "性  别：";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 207);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(68, 17);
            label8.TabIndex = 21;
            label8.Text = "银行名称：";
            // 
            // txtName
            // 
            txtName.Location = new Point(89, 39);
            txtName.Margin = new Padding(2, 3, 2, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(219, 23);
            txtName.TabIndex = 4;
            // 
            // txtIdCard
            // 
            txtIdCard.Location = new Point(89, 72);
            txtIdCard.Margin = new Padding(2, 3, 2, 3);
            txtIdCard.Name = "txtIdCard";
            txtIdCard.Size = new Size(219, 23);
            txtIdCard.TabIndex = 5;
            // 
            // txtBankCard
            // 
            txtBankCard.Location = new Point(89, 105);
            txtBankCard.Margin = new Padding(2, 3, 2, 3);
            txtBankCard.Name = "txtBankCard";
            txtBankCard.Size = new Size(219, 23);
            txtBankCard.TabIndex = 6;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(89, 138);
            txtPhone.Margin = new Padding(2, 3, 2, 3);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(219, 23);
            txtPhone.TabIndex = 7;
            // 
            // txtGender
            // 
            txtGender.Location = new Point(89, 171);
            txtGender.Margin = new Padding(2, 3, 2, 3);
            txtGender.Name = "txtGender";
            txtGender.Size = new Size(219, 23);
            txtGender.TabIndex = 22;
            // 
            // txtBankName
            // 
            txtBankName.Location = new Point(89, 204);
            txtBankName.Margin = new Padding(2, 3, 2, 3);
            txtBankName.Name = "txtBankName";
            txtBankName.Size = new Size(219, 23);
            txtBankName.TabIndex = 23;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(344, 39);
            btnAdd.Margin = new Padding(2, 3, 2, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(62, 30);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "添加";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(344, 75);
            btnEdit.Margin = new Padding(2, 3, 2, 3);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(62, 30);
            btnEdit.TabIndex = 9;
            btnEdit.Text = "编辑";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(344, 111);
            btnDelete.Margin = new Padding(2, 3, 2, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(62, 30);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "删除";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(344, 147);
            btnClear.Margin = new Padding(2, 3, 2, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(62, 30);
            btnClear.TabIndex = 11;
            btnClear.Text = "清空";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // dgvPersonInfo
            // 
            dgvPersonInfo.AllowUserToAddRows = false;
            dgvPersonInfo.AllowUserToDeleteRows = false;
            dgvPersonInfo.AllowUserToOrderColumns = true;
            dgvPersonInfo.AllowUserToResizeRows = false;
            dgvPersonInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvPersonInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvPersonInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPersonInfo.Columns.AddRange(new DataGridViewColumn[] { NameColumn, GenderColumn, IdCardColumn, BankNameColumn, BankCardColumn, PhoneColumn, CreatedTimeColumn, LastModifiedColumn });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvPersonInfo.DefaultCellStyle = dataGridViewCellStyle2;
            dgvPersonInfo.Location = new Point(9, 280);
            dgvPersonInfo.Margin = new Padding(2, 3, 2, 3);
            dgvPersonInfo.Name = "dgvPersonInfo";
            dgvPersonInfo.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Microsoft YaHei UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvPersonInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvPersonInfo.RowHeadersWidth = 51;
            dgvPersonInfo.RowTemplate.Height = 29;
            dgvPersonInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPersonInfo.Size = new Size(1275, 506);
            dgvPersonInfo.TabIndex = 12;
            dgvPersonInfo.CellClick += dgvPersonInfo_CellClick;
            // 
            // NameColumn
            // 
            NameColumn.DataPropertyName = "Name";
            NameColumn.HeaderText = "姓名";
            NameColumn.MinimumWidth = 6;
            NameColumn.Name = "NameColumn";
            NameColumn.ReadOnly = true;
            NameColumn.Width = 120;
            // 
            // GenderColumn
            // 
            GenderColumn.DataPropertyName = "Gender";
            GenderColumn.HeaderText = "性别";
            GenderColumn.MinimumWidth = 6;
            GenderColumn.Name = "GenderColumn";
            GenderColumn.ReadOnly = true;
            GenderColumn.Width = 80;
            // 
            // IdCardColumn
            // 
            IdCardColumn.DataPropertyName = "IdCardNumber";
            IdCardColumn.HeaderText = "身份证号";
            IdCardColumn.MinimumWidth = 6;
            IdCardColumn.Name = "IdCardColumn";
            IdCardColumn.ReadOnly = true;
            IdCardColumn.Width = 200;
            // 
            // BankNameColumn
            // 
            BankNameColumn.DataPropertyName = "BankName";
            BankNameColumn.HeaderText = "银行名称";
            BankNameColumn.MinimumWidth = 6;
            BankNameColumn.Name = "BankNameColumn";
            BankNameColumn.ReadOnly = true;
            BankNameColumn.Width = 150;
            // 
            // BankCardColumn
            // 
            BankCardColumn.DataPropertyName = "BankCardNumber";
            BankCardColumn.HeaderText = "银行卡号";
            BankCardColumn.MinimumWidth = 6;
            BankCardColumn.Name = "BankCardColumn";
            BankCardColumn.ReadOnly = true;
            BankCardColumn.Width = 200;
            // 
            // PhoneColumn
            // 
            PhoneColumn.DataPropertyName = "PhoneNumber";
            PhoneColumn.HeaderText = "电话号码";
            PhoneColumn.MinimumWidth = 6;
            PhoneColumn.Name = "PhoneColumn";
            PhoneColumn.ReadOnly = true;
            PhoneColumn.Width = 150;
            // 
            // CreatedTimeColumn
            // 
            CreatedTimeColumn.DataPropertyName = "CreatedTime";
            CreatedTimeColumn.HeaderText = "创建时间";
            CreatedTimeColumn.MinimumWidth = 6;
            CreatedTimeColumn.Name = "CreatedTimeColumn";
            CreatedTimeColumn.ReadOnly = true;
            CreatedTimeColumn.Width = 150;
            // 
            // LastModifiedColumn
            // 
            LastModifiedColumn.DataPropertyName = "LastModifiedTime";
            LastModifiedColumn.HeaderText = "最后修改时间";
            LastModifiedColumn.MinimumWidth = 6;
            LastModifiedColumn.Name = "LastModifiedColumn";
            LastModifiedColumn.ReadOnly = true;
            LastModifiedColumn.Width = 150;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 793);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 11, 0);
            statusStrip1.Size = new Size(1294, 22);
            statusStrip1.TabIndex = 13;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(92, 17);
            toolStripStatusLabel1.Text = "就绪 - 0 条记录";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(419, 39);
            btnSave.Margin = new Padding(2, 3, 2, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(135, 30);
            btnSave.TabIndex = 14;
            btnSave.Text = "保存";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnImportExcel
            // 
            btnImportExcel.Location = new Point(419, 75);
            btnImportExcel.Margin = new Padding(2, 3, 2, 3);
            btnImportExcel.Name = "btnImportExcel";
            btnImportExcel.Size = new Size(135, 30);
            btnImportExcel.TabIndex = 15;
            btnImportExcel.Text = "个人信息导入Excel";
            btnImportExcel.UseVisualStyleBackColor = true;
            btnImportExcel.Click += btnImportExcel_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(txtBankName);
            panel1.Controls.Add(txtGender);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(btnImportExcel);
            panel1.Controls.Add(btnExportExcel);
            panel1.Controls.Add(btnGenerateTemplate);
            panel1.Controls.Add(btnCheckInfo);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnClear);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(txtName);
            panel1.Controls.Add(txtPhone);
            panel1.Controls.Add(txtIdCard);
            panel1.Controls.Add(txtBankCard);
            panel1.Location = new Point(9, 12);
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1276, 230);
            panel1.TabIndex = 15;
            panel1.Paint += panel1_Paint;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 10.8F, FontStyle.Bold);
            label5.Location = new Point(344, 0);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(99, 19);
            label5.TabIndex = 16;
            label5.Text = "个人信息录入";
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(419, 111);
            btnExportExcel.Margin = new Padding(2, 3, 2, 3);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(135, 30);
            btnExportExcel.TabIndex = 16;
            btnExportExcel.Text = "个人信息导出Excel";
            btnExportExcel.UseVisualStyleBackColor = true;
            btnExportExcel.Click += btnExportExcel_Click;
            btnGenerateTemplate.Location = new Point(419, 147);
            btnGenerateTemplate.Margin = new Padding(2, 3, 2, 3);
            btnGenerateTemplate.Name = "btnGenerateTemplate";
            btnGenerateTemplate.Size = new Size(135, 30);
            btnGenerateTemplate.TabIndex = 17;
            btnGenerateTemplate.Text = "生成模板";
            btnGenerateTemplate.UseVisualStyleBackColor = true;
            btnGenerateTemplate.Click += btnGenerateTemplate_Click;
            btnCheckInfo.Location = new Point(419, 183);
            btnCheckInfo.Margin = new Padding(2, 3, 2, 3);
            btnCheckInfo.Name = "btnCheckInfo";
            btnCheckInfo.Size = new Size(135, 30);
            btnCheckInfo.TabIndex = 18;
            btnCheckInfo.Text = "检查信息";
            btnCheckInfo.UseVisualStyleBackColor = true;
            btnCheckInfo.Click += btnCheckInfo_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(txtSearch);
            panel2.Controls.Add(label6);
            panel2.Location = new Point(9, 248);
            panel2.Margin = new Padding(2, 3, 2, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(1276, 26);
            panel2.TabIndex = 16;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(60, 0);
            txtSearch.Margin = new Padding(2, 3, 2, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(156, 23);
            txtSearch.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 3);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(44, 17);
            label6.TabIndex = 0;
            label6.Text = "搜索：";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1294, 815);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Controls.Add(dgvPersonInfo);
            Font = new Font("Microsoft YaHei UI", 9F);
            Margin = new Padding(2, 3, 2, 3);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "个人信息管理系统";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPersonInfo).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtIdCard;
        private System.Windows.Forms.TextBox txtBankCard;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.DataGridView dgvPersonInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenderColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCardColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankCardColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhoneColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedTimeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastModifiedColumn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnImportExcel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGenerateTemplate;
        private System.Windows.Forms.Button btnCheckInfo;
    }
}
