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

        public SalaryForm()
        {
            InitializeComponent();
            _salaryDataManager = new SalaryDataManager();
        }

        private void InitializeComponent()
        {
            this.dgvSalary = new System.Windows.Forms.DataGridView();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.txtNameFilter = new System.Windows.Forms.TextBox();
            this.txtUnitFilter = new System.Windows.Forms.TextBox();
            this.txtMonthFilter = new System.Windows.Forms.TextBox();
            this.datePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.datePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalary)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSalary
            // 
            this.dgvSalary.AllowUserToAddRows = false;
            this.dgvSalary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSalary.Location = new System.Drawing.Point(0, 0);
            this.dgvSalary.Name = "dgvSalary";
            this.dgvSalary.Size = new System.Drawing.Size(800, 400);
            this.dgvSalary.TabIndex = 0;
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Location = new System.Drawing.Point(12, 12);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(100, 30);
            this.btnImportExcel.TabIndex = 1;
            this.btnImportExcel.Text = "导入Excel";
            this.btnImportExcel.UseVisualStyleBackColor = true;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(118, 12);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(100, 30);
            this.btnExportExcel.TabIndex = 2;
            this.btnExportExcel.Text = "导出Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(664, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 30);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.Location = new System.Drawing.Point(730, 12);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(60, 30);
            this.btnResetFilter.TabIndex = 4;
            this.btnResetFilter.Text = "重置";
            this.btnResetFilter.UseVisualStyleBackColor = true;
            this.btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);
            // 
            // txtNameFilter
            // 
            this.txtNameFilter.Location = new System.Drawing.Point(224, 17);
            this.txtNameFilter.Name = "txtNameFilter";
            this.txtNameFilter.Size = new System.Drawing.Size(100, 20);
            this.txtNameFilter.TabIndex = 5;
            // 
            // txtUnitFilter
            // 
            this.txtUnitFilter.Location = new System.Drawing.Point(374, 17);
            this.txtUnitFilter.Name = "txtUnitFilter";
            this.txtUnitFilter.Size = new System.Drawing.Size(100, 20);
            this.txtUnitFilter.TabIndex = 6;
            // 
            // txtMonthFilter
            // 
            this.txtMonthFilter.Location = new System.Drawing.Point(524, 17);
            this.txtMonthFilter.Name = "txtMonthFilter";
            this.txtMonthFilter.Size = new System.Drawing.Size(70, 20);
            this.txtMonthFilter.TabIndex = 7;
            // 
            // datePickerStartDate
            // 
            this.datePickerStartDate.Location = new System.Drawing.Point(224, 43);
            this.datePickerStartDate.Name = "datePickerStartDate";
            this.datePickerStartDate.Size = new System.Drawing.Size(150, 20);
            this.datePickerStartDate.TabIndex = 8;
            this.datePickerStartDate.Checked = false;
            // 
            // datePickerEndDate
            // 
            this.datePickerEndDate.Location = new System.Drawing.Point(424, 43);
            this.datePickerEndDate.Name = "datePickerEndDate";
            this.datePickerEndDate.Size = new System.Drawing.Size(150, 20);
            this.datePickerEndDate.TabIndex = 9;
            this.datePickerEndDate.Checked = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "姓名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(371, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "发放单位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(521, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "月份";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "开始发放时间";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(421, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "结束发放时间";
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
            this.panel1.Controls.Add(this.btnExportExcel);
            this.panel1.Controls.Add(this.btnImportExcel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 70);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvSalary);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 400);
            this.panel2.TabIndex = 16;
            // 
            // SalaryForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 470);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SalaryForm";
            this.Text = "薪资信息管理";
            this.Load += new System.EventHandler(this.SalaryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalary)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void SalaryForm_Load(object sender, EventArgs e)
        {
            // 初始化DataGridView
            InitializeDataGridView();
            // 加载薪资数据
            LoadSalaryData();
        }

        private void InitializeDataGridView()
        {
            // 设置DataGridView的列
            dgvSalary.Columns.Clear();
            
            dgvSalary.AutoGenerateColumns = false;
            dgvSalary.AllowUserToAddRows = false;
            dgvSalary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSalary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            // 添加列
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "姓名", FillWeight = 100 });
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Month", HeaderText = "月份", FillWeight = 80 });
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SalaryAmount", HeaderText = "当月工资金额", FillWeight = 100, DefaultCellStyle = new DataGridViewCellStyle() { Format = "N2" } });
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PayrollUnit", HeaderText = "发放单位", FillWeight = 120 });
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CreateTime", HeaderText = "制表时间", FillWeight = 150, DefaultCellStyle = new DataGridViewCellStyle() { Format = "yyyy-MM-dd HH:mm:ss" } });
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PaymentTime", HeaderText = "发放时间", FillWeight = 120, DefaultCellStyle = new DataGridViewCellStyle() { Format = "yyyy-MM-dd" } });
            // 添加发放总金额列
            dgvSalary.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalSalary", HeaderText = "发放总金额", FillWeight = 120, DefaultCellStyle = new DataGridViewCellStyle() { Format = "N2", BackColor = Color.LightYellow } });
        }

        private void LoadSalaryData()
        {
            try
            {
                var salaryList = _salaryDataManager.LoadSalaryData();
                // 显示薪资数据并计算发放总金额
                DisplaySalaryDataWithTotals(salaryList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载薪资数据失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel文件|*.xlsx";
            openFileDialog.Title = "选择Excel文件";
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string result = _salaryDataManager.ImportSalaryFromExcel(openFileDialog.FileName);
                    MessageBox.Show(result, "导入结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 重新加载数据
                    LoadSalaryData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导入失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
        }
        
        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            // 重置筛选条件
            txtNameFilter.Text = "";
            txtUnitFilter.Text = "";
            txtMonthFilter.Text = "";
            datePickerStartDate.Checked = false;
            datePickerEndDate.Checked = false;
            
            // 重新加载所有数据
            LoadSalaryData();
        }

        private System.Windows.Forms.DataGridView dgvSalary;
        private System.Windows.Forms.Button btnImportExcel;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.TextBox txtNameFilter;
        private System.Windows.Forms.TextBox txtUnitFilter;
        private System.Windows.Forms.TextBox txtMonthFilter;
        private System.Windows.Forms.DateTimePicker datePickerStartDate;
        private System.Windows.Forms.DateTimePicker datePickerEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}