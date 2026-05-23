using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
    
    // 薪资信息管理窗体
    public partial class SalaryForm : Form
    {
        private SalaryDataManager _salaryDataManager;
        private string _templateFilePath = string.Empty;

        public SalaryForm()
        {
            InitializeComponent();
            _salaryDataManager = new SalaryDataManager();
            
            try
            {
                string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.ico");
                if (System.IO.File.Exists(iconPath))
                {
                    this.Icon = new Icon(iconPath);
                }
            }
            catch
            {
            }
        }

        private void InitializeComponent()
        {
            this.dgvSalary = new System.Windows.Forms.DataGridView();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnGenerateTemplate = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.btnClearConditions = new System.Windows.Forms.Button();
            this.txtNameFilter = new System.Windows.Forms.ComboBox();
            this.txtUnitFilter = new System.Windows.Forms.ComboBox();
            this.txtMonthFilter = new System.Windows.Forms.ComboBox();
            this.datePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.datePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalary)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSalary
            // 
            this.dgvSalary.AllowUserToAddRows = false;
            this.dgvSalary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSalary.Location = new System.Drawing.Point(0, 35);
            this.dgvSalary.Name = "dgvSalary";
            this.dgvSalary.Size = new System.Drawing.Size(900, 465);
            this.dgvSalary.TabIndex = 0;
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Location = new System.Drawing.Point(12, 10);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(100, 35);
            this.btnImportExcel.TabIndex = 1;
            this.btnImportExcel.Text = "导入Excel";
            this.btnImportExcel.UseVisualStyleBackColor = true;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(12, 55);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(100, 35);
            this.btnExportExcel.TabIndex = 2;
            this.btnExportExcel.Text = "导出Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            this.btnGenerateTemplate.Location = new System.Drawing.Point(130, 10);
            this.btnGenerateTemplate.Name = "btnGenerateTemplate";
            this.btnGenerateTemplate.Size = new System.Drawing.Size(100, 35);
            this.btnGenerateTemplate.TabIndex = 8;
            this.btnGenerateTemplate.Text = "生成模板";
            this.btnGenerateTemplate.UseVisualStyleBackColor = true;
            this.btnGenerateTemplate.Click += new System.EventHandler(this.btnGenerateTemplate_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(750, 35);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 30);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.Location = new System.Drawing.Point(820, 35);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(60, 30);
            this.btnResetFilter.TabIndex = 4;
            this.btnResetFilter.Text = "重置";
            this.btnResetFilter.UseVisualStyleBackColor = true;
            this.btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);
            this.btnClearConditions.Location = new System.Drawing.Point(130, 55);
            this.btnClearConditions.Name = "btnClearConditions";
            this.btnClearConditions.Size = new System.Drawing.Size(100, 35);
            this.btnClearConditions.TabIndex = 9;
            this.btnClearConditions.Text = "清除条件";
            this.btnClearConditions.UseVisualStyleBackColor = true;
            this.btnClearConditions.Click += new System.EventHandler(this.btnClearConditions_Click);
            // 
            // txtNameFilter
            // 
            this.txtNameFilter.Location = new System.Drawing.Point(250, 35);
            this.txtNameFilter.Name = "txtNameFilter";
            this.txtNameFilter.Size = new System.Drawing.Size(120, 21);
            this.txtNameFilter.TabIndex = 5;
            this.txtNameFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.txtNameFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtNameFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            // 
            // txtUnitFilter
            // 
            this.txtUnitFilter.Location = new System.Drawing.Point(420, 35);
            this.txtUnitFilter.Name = "txtUnitFilter";
            this.txtUnitFilter.Size = new System.Drawing.Size(120, 21);
            this.txtUnitFilter.TabIndex = 6;
            this.txtUnitFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.txtUnitFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtUnitFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            // 
            // txtMonthFilter
            // 
            this.txtMonthFilter.Location = new System.Drawing.Point(600, 35);
            this.txtMonthFilter.Name = "txtMonthFilter";
            this.txtMonthFilter.Size = new System.Drawing.Size(120, 21);
            this.txtMonthFilter.TabIndex = 7;
            this.txtMonthFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.txtMonthFilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtMonthFilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            // 
            // datePickerStartDate
            // 
            this.datePickerStartDate.Location = new System.Drawing.Point(250, 75);
            this.datePickerStartDate.Name = "datePickerStartDate";
            this.datePickerStartDate.Size = new System.Drawing.Size(150, 21);
            this.datePickerStartDate.TabIndex = 8;
            this.datePickerStartDate.Checked = false;
            // 
            // datePickerEndDate
            // 
            this.datePickerEndDate.Location = new System.Drawing.Point(470, 75);
            this.datePickerEndDate.Name = "datePickerEndDate";
            this.datePickerEndDate.Size = new System.Drawing.Size(150, 21);
            this.datePickerEndDate.TabIndex = 9;
            this.datePickerEndDate.Checked = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "姓名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(420, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "发放单位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(600, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "月份";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "开始日期";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(470, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "结束日期";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.datePickerEndDate);
            this.panel1.Controls.Add(this.datePickerStartDate);
            this.panel1.Controls.Add(this.txtMonthFilter);
            this.panel1.Controls.Add(this.txtUnitFilter);
            this.panel1.Controls.Add(this.txtNameFilter);
            this.panel1.Controls.Add(this.btnResetFilter);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnClearConditions);
            this.panel1.Controls.Add(this.btnExportExcel);
            this.panel1.Controls.Add(this.btnImportExcel);
            this.panel1.Controls.Add(this.btnGenerateTemplate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 100);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblTotalAmount);
            this.panel2.Controls.Add(this.dgvSalary);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 500);
            this.panel2.TabIndex = 16;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.BackColor = System.Drawing.Color.LightYellow;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTotalAmount.Location = new System.Drawing.Point(10, 10);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(150, 20);
            this.lblTotalAmount.TabIndex = 17;
            this.lblTotalAmount.Text = "选中记录总金额：0 元";
            // 
            // SalaryForm
            // 
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SalaryForm";
            this.Text = "WorkData - 薪资信息管理";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SalaryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalary)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void SalaryForm_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            LoadSalaryData();
            btnImportExcel.Enabled = false;
        }

        private void InitializeDataGridView()
        {
            dgvSalary.Columns.Clear();
            
            dgvSalary.AutoGenerateColumns = false;
            dgvSalary.AllowUserToAddRows = false;
            dgvSalary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSalary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", DataPropertyName = "Name", HeaderText = "姓名", FillWeight = 100 });
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { Name = "Month", DataPropertyName = "Month", HeaderText = "月份", FillWeight = 80 });
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { Name = "SalaryAmount", DataPropertyName = "SalaryAmount", HeaderText = "当月发放金额", FillWeight = 100, DefaultCellStyle = new DataGridViewCellStyle() { Format = "N0" } });
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { Name = "PayrollUnit", DataPropertyName = "PayrollUnit", HeaderText = "发放单位", FillWeight = 120 });
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { Name = "CreateTime", DataPropertyName = "CreateTime", HeaderText = "制表时间", FillWeight = 150, DefaultCellStyle = new DataGridViewCellStyle() { Format = "yyyy-MM-dd HH:mm:ss" } });
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { Name = "PaymentTime", DataPropertyName = "PaymentTime", HeaderText = "发放时间", FillWeight = 120, DefaultCellStyle = new DataGridViewCellStyle() { Format = "yyyy-MM-dd" } });
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalSalary", DataPropertyName = "TotalSalary", HeaderText = "发放总金额", FillWeight = 120, DefaultCellStyle = new DataGridViewCellStyle() { Format = "N0", BackColor = Color.LightYellow } });
        }

        private void UpdateTotalAmount()
        {
            decimal totalAmount = 0;
            
            foreach (DataGridViewRow row in dgvSalary.Rows)
            {
                if (row.Cells["SalaryAmount"].Value != null)
                {
                    decimal salaryAmount;
                    if (decimal.TryParse(row.Cells["SalaryAmount"].Value.ToString(), out salaryAmount))
                    {
                        totalAmount += salaryAmount;
                    }
                }
            }
            
            lblTotalAmount.Text = $"选中记录总金额：{totalAmount:N0} 元";
        }

        private void LoadSalaryData()
        {
            try
            {
                var salaryList = _salaryDataManager.LoadSalaryData();
                DisplaySalaryDataWithTotals(salaryList);
                PopulateComboBoxes(salaryList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载薪资数据失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateComboBoxes(List<SalaryInfo> salaryList)
        {
            txtNameFilter.Items.Clear();
            txtUnitFilter.Items.Clear();
            txtMonthFilter.Items.Clear();

            txtNameFilter.Items.Add("");
            txtUnitFilter.Items.Add("");
            txtMonthFilter.Items.Add("");

            var names = salaryList.Select(s => s.Name).Distinct().OrderBy(n => n).ToList();
            foreach (var name in names)
            {
                txtNameFilter.Items.Add(name);
            }

            var units = salaryList.Select(s => s.PayrollUnit).Distinct().OrderBy(u => u).ToList();
            foreach (var unit in units)
            {
                txtUnitFilter.Items.Add(unit);
            }

            var months = salaryList.Select(s => s.Month).Distinct().OrderBy(m => m).ToList();
            foreach (var month in months)
            {
                txtMonthFilter.Items.Add(month);
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_templateFilePath) || !File.Exists(_templateFilePath))
            {
                MessageBox.Show("模板文件不存在，请先生成模板！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string result = _salaryDataManager.ImportSalaryFromExcel(_templateFilePath);
                MessageBox.Show(result, "导入结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSalaryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // 获取筛选条件
            string nameFilter = txtNameFilter.Text.Trim();
            string unitFilter = txtUnitFilter.Text.Trim();
            string monthFilter = txtMonthFilter.Text.Trim();
            DateTime? startDate = null;
            DateTime? endDate = null;
            
            // 如果选中了日期，则使用选中的日期
            if (datePickerStartDate.Checked)
                startDate = datePickerStartDate.Value.Date;
            if (datePickerEndDate.Checked)
                endDate = datePickerEndDate.Value.Date;
            
            // 筛选数据
            var filteredData = _salaryDataManager.FilterSalaryData(nameFilter, unitFilter, monthFilter, startDate, endDate);
            
            if (filteredData.Count == 0)
            {
                MessageBox.Show("没有符合条件的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // 计算每个人员在当前查询范围内的发放总金额
            var nameToTotalAmount = filteredData.GroupBy(s => s.Name)
                .ToDictionary(g => g.Key, g => g.Sum(s => s.SalaryAmount));
            
            // 创建带总金额的导出数据列表
            var exportDataWithTotals = filteredData.Select(s => new
            {
                s.Name,
                s.Month,
                s.SalaryAmount,
                s.PayrollUnit,
                CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                PaymentTime = s.PaymentTime.HasValue ? s.PaymentTime.Value.ToString("yyyy-MM-dd") : "",
                TotalSalary = nameToTotalAmount[s.Name]
            }).OrderBy(s => s.Month).ThenBy(s => s.Name).ToList();
            
            // 保存文件对话框
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel文件|*.xlsx";
            saveFileDialog.Title = "保存Excel文件";
            saveFileDialog.FileName = $"薪资数据_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx";
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    bool success = _salaryDataManager.ExportSalaryWithTotalsToExcel(exportDataWithTotals, saveFileDialog.FileName);
                    if (success)
                    {
                        MessageBox.Show("导出成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("导出失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 获取筛选条件
            string nameFilter = txtNameFilter.Text.Trim();
            string unitFilter = txtUnitFilter.Text.Trim();
            string monthFilter = txtMonthFilter.Text.Trim();
            DateTime? startDate = null;
            DateTime? endDate = null;
            
            // 如果选中了日期，则使用选中的日期
            if (datePickerStartDate.Checked)
                startDate = datePickerStartDate.Value.Date;
            if (datePickerEndDate.Checked)
                endDate = datePickerEndDate.Value.Date;
            
            // 筛选数据
            var filteredData = _salaryDataManager.FilterSalaryData(nameFilter, unitFilter, monthFilter, startDate, endDate);
            
            // 显示筛选结果（包含发放总金额）
            DisplaySalaryDataWithTotals(filteredData);
        }

        /// <summary>
        /// 显示薪资数据并计算每个人在当前查询范围内的发放总金额
        /// </summary>
        /// <param name="salaryList">薪资数据列表</param>
        private void DisplaySalaryDataWithTotals(List<SalaryInfo> salaryList)
        {
            // 按姓名分组计算每个人员在当前查询范围内的总金额
            var nameToTotalAmount = salaryList.GroupBy(s => s.Name)
                .ToDictionary(g => g.Key, g => g.Sum(s => s.SalaryAmount));
            
            // 创建带总金额的数据项列表
            var dataWithTotals = salaryList.Select(s => new
            {
                s.Id,
                s.Name,
                s.Month,
                s.SalaryAmount,
                s.PayrollUnit,
                s.CreateTime,
                s.PaymentTime,
                TotalSalary = nameToTotalAmount[s.Name]
            }).OrderBy(s => s.Month).ThenBy(s => s.Name).ToList();
            
            dgvSalary.DataSource = dataWithTotals;
            
            UpdateTotalAmount();
        }
        
        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            txtNameFilter.Text = "";
            txtUnitFilter.Text = "";
            txtMonthFilter.Text = "";
            datePickerStartDate.Checked = false;
            datePickerEndDate.Checked = false;
            
            LoadSalaryData();
        }

        private void btnClearConditions_Click(object sender, EventArgs e)
        {
            txtNameFilter.Text = "";
            txtUnitFilter.Text = "";
            txtMonthFilter.Text = "";
            
            btnSearch_Click(sender, e);
        }

        private void btnGenerateTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel文件|*.xlsx";
                saveFileDialog.Title = "保存薪资模板文件";
                saveFileDialog.FileName = "薪资信息模板.xlsx";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    
                    GenerateSalaryExcelTemplate(filePath);
                    
                    _templateFilePath = filePath;
                    btnImportExcel.Enabled = true;
                    
                    MessageBox.Show("薪资模板文件已生成！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("无法打开文件: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成模板失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateSalaryExcelTemplate(string filePath)
        {
            try
            {
                NPOI.XSSF.UserModel.XSSFWorkbook workbook = new NPOI.XSSF.UserModel.XSSFWorkbook();
                
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("薪资信息模板");
                
                NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("姓名");
                headerRow.CreateCell(1).SetCellValue("月份");
                headerRow.CreateCell(2).SetCellValue("工资金额");
                headerRow.CreateCell(3).SetCellValue("发放单位");
                headerRow.CreateCell(4).SetCellValue("发放时间");
                
                NPOI.SS.UserModel.ICellStyle headerStyle = workbook.CreateCellStyle();
                NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                headerStyle.SetFont(font);
                headerStyle.FillForegroundColor = NPOI.SS.UserModel.IndexedColors.Grey25Percent.Index;
                headerStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
                
                for (int i = 0; i < 5; i++)
                {
                    headerRow.GetCell(i).CellStyle = headerStyle;
                }
                
                sheet.SetColumnWidth(0, 15 * 256);
                sheet.SetColumnWidth(1, 12 * 256);
                sheet.SetColumnWidth(2, 15 * 256);
                sheet.SetColumnWidth(3, 20 * 256);
                sheet.SetColumnWidth(4, 18 * 256);
                
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fileStream, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("生成薪资Excel模板失败: " + ex.Message, ex);
            }
        }

        private System.Windows.Forms.DataGridView dgvSalary;
        private System.Windows.Forms.Button btnImportExcel;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnGenerateTemplate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.Button btnClearConditions;
        private System.Windows.Forms.ComboBox txtNameFilter;
        private System.Windows.Forms.ComboBox txtUnitFilter;
        private System.Windows.Forms.ComboBox txtMonthFilter;
        private System.Windows.Forms.DateTimePicker datePickerStartDate;
        private System.Windows.Forms.DateTimePicker datePickerEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTotalAmount;
    }
}