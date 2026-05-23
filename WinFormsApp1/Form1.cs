using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        // 数据管理器实例
        private personalDataManager _dataManager;
        // 当前选中的人物信息（用于编辑操作）
        private PersonInfo _currentPersonInfo;
        // 搜索相关字段
        private List<PersonInfo> personList;
        private bool isEditMode = false;
        // 模板文件路径
        private string _templateFilePath = string.Empty;

        public Form1()
        {
            InitializeComponent();
            _currentPersonInfo = null;
            
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

        private void Form1_Load(object sender, EventArgs e)
        {
            _dataManager = new personalDataManager();
            LoadPersonData();

            _dataManager.SyncSalaryToIncomeRecords();

            txtSearch.TextChanged += TxtSearch_TextChanged;

            btnImportExcel.Enabled = false;

            dgvPersonInfo.CellValueChanged += dgvPersonInfo_CellValueChanged;
            dgvPersonInfo.CurrentCellDirtyStateChanged += dgvPersonInfo_CurrentCellDirtyStateChanged;
            dgvPersonInfo.CellMouseClick += dgvPersonInfo_CellMouseClick;
            dgvPersonInfo.RowPrePaint += dgvPersonInfo_RowPrePaint;
            dgvPersonInfo.CellDoubleClick += dgvPersonInfo_CellDoubleClick;

            AddSalaryManagementButton();
        }

        private void dgvPersonInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPersonInfo.Rows[e.RowIndex];
                if (row.DataBoundItem is PersonInfo person)
                {
                    using (PersonalIncomeForm incomeForm = new PersonalIncomeForm(person.Name))
                    {
                        incomeForm.ShowDialog();
                    }
                }
            }
        }

        private void dgvPersonInfo_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPersonInfo.Rows[e.RowIndex];
                if (row.DataBoundItem is PersonInfo person)
                {
                    if (person.IsSelected)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = dgvPersonInfo.DefaultCellStyle.BackColor;
                    }
                }
            }
        }

        private void dgvPersonInfo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPersonInfo.Rows[e.RowIndex];
                if (row.DataBoundItem is PersonInfo person)
                {
                    person.IsSelected = !person.IsSelected;
                    dgvPersonInfo.InvalidateRow(e.RowIndex);
                    UpdateSelectedCount();
                }
            }
        }

        private void dgvPersonInfo_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvPersonInfo.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvPersonInfo.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvPersonInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                UpdateSelectedCount();
            }
        }

        private void UpdateSelectedCount()
        {
            int selectedCount = 0;
            foreach (DataGridViewRow row in dgvPersonInfo.Rows)
            {
                if (row.DataBoundItem is PersonInfo person && person.IsSelected)
                {
                    selectedCount++;
                }
            }
            
            int totalCount = dgvPersonInfo.Rows.Count;
            toolStripStatusLabel1.Text = $"就绪 - 总计 {totalCount} 条记录，已选中 {selectedCount} 条";
        }

        private void AddSalaryManagementButton()
        {
            Button btnSalaryManagement = new Button
            {
                Text = "薪资管理",
                Size = new Size(100, 40),
                Location = new Point(10, 10),
                BackColor = Color.LightBlue,
                ForeColor = Color.Black,
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };
            
            btnSalaryManagement.Click += (sender, e) =>
            {
                try
                {
                    SalaryForm salaryForm = new SalaryForm();
                    salaryForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("打开薪资管理窗体失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            
            this.Controls.Add(btnSalaryManagement);
            btnSalaryManagement.BringToFront();
            
            Button btnDataMigration = new Button
            {
                Text = "数据迁移",
                Size = new Size(100, 40),
                Location = new Point(120, 10),
                BackColor = Color.LightGreen,
                ForeColor = Color.Black,
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };
            
            btnDataMigration.Click += (sender, e) =>
            {
                using (Form migrationForm = new Form())
                {
                    migrationForm.Text = "数据迁移同步";
                    migrationForm.Size = new Size(500, 300);
                    migrationForm.StartPosition = FormStartPosition.CenterParent;
                    migrationForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    migrationForm.MaximizeBox = false;
                    migrationForm.MinimizeBox = false;

                    Button btnExport = new Button
                    {
                        Text = "导出数据",
                        Location = new Point(50, 50),
                        Size = new Size(180, 40),
                        Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold)
                    };

                    Button btnImport = new Button
                    {
                        Text = "导入数据",
                        Location = new Point(260, 50),
                        Size = new Size(180, 40),
                        Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold)
                    };

                    Label lblDescription = new Label
                    {
                        Text = "数据迁移功能说明：\n\n" +
                               "• 导出数据：将所有数据（个人信息+薪资信息）导出到一个文件\n" +
                               "• 导入数据：从导出文件中恢复数据到本机\n" +
                               "• 支持覆盖导入或合并导入两种模式\n" +
                               "• 可用于多台电脑之间的数据同步",
                        Location = new Point(50, 120),
                        Size = new Size(390, 120),
                        Font = new Font("Microsoft YaHei UI", 9F)
                    };

                    btnExport.Click += (s, args) =>
                    {
                        try
                        {
                            SaveFileDialog saveDialog = new SaveFileDialog
                            {
                                Filter = "数据迁移文件|*.wddata",
                                Title = "导出数据",
                                FileName = $"WorkData_{DateTime.Now:yyyyMMdd_HHmmss}.wddata",
                                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                            };

                            if (saveDialog.ShowDialog() == DialogResult.OK)
                            {
                                DataMigration migration = new DataMigration();
                                string result = migration.ExportData(saveDialog.FileName);
                                MessageBox.Show(result, "导出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                migrationForm.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "导出失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };

                    btnImport.Click += (s, args) =>
                    {
                        try
                        {
                            OpenFileDialog openDialog = new OpenFileDialog
                            {
                                Filter = "数据迁移文件|*.wddata",
                                Title = "导入数据",
                                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                            };

                            if (openDialog.ShowDialog() == DialogResult.OK)
                            {
                                DataMigration migration = new DataMigration();
                                var preview = migration.PreviewImportData(openDialog.FileName);

                                string previewMessage = $"即将导入的数据：\n\n" +
                                                       $"文件版本：{preview.Version}\n" +
                                                       $"导出时间：{preview.ExportTime:yyyy-MM-dd HH:mm:ss}\n" +
                                                       $"个人信息：{preview.PersonInfoCount} 条\n" +
                                                       $"薪资信息：{preview.SalaryInfoCount} 条\n\n" +
                                                       $"请选择导入方式：";

                                DialogResult result = MessageBox.Show(
                                    previewMessage + "\n\n点击\"是\"覆盖现有数据\n点击\"否\"合并到现有数据\n点击\"取消\"放弃导入",
                                    "导入预览",
                                    MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                {
                                    string importResult = migration.ImportData(openDialog.FileName, true);
                                    MessageBox.Show(importResult, "导入成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    
                                    LoadPersonData();
                                    migrationForm.Close();
                                }
                                else if (result == DialogResult.No)
                                {
                                    string importResult = migration.ImportData(openDialog.FileName, false);
                                    MessageBox.Show(importResult, "导入成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    
                                    LoadPersonData();
                                    migrationForm.Close();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "导入失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };

                    migrationForm.Controls.Add(btnExport);
                    migrationForm.Controls.Add(btnImport);
                    migrationForm.Controls.Add(lblDescription);

                    migrationForm.ShowDialog();
                }
            };
            
            this.Controls.Add(btnDataMigration);
            btnDataMigration.BringToFront();
            
            Console.WriteLine($"薪资管理按钮已添加，位置: {btnSalaryManagement.Location}");
            Console.WriteLine($"数据迁移按钮已添加，位置: {btnDataMigration.Location}");
        }

        /// <summary>
        /// 加载人物数据到DataGridView
        /// </summary>
        private void LoadPersonData()
        {
            try
            {
                personList = _dataManager.LoadData();
                UpdateDataGridView(personList);

                UpdateSelectedCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 验证输入数据
        /// </summary>
        /// <returns>验证是否通过</returns>
        private bool ValidateInput()
        {
            // 验证姓名
            string name = txtName.Text.Trim();
            if (!ValidationHelper.ValidateName(name))
            {
                MessageBox.Show("请输入有效的姓名（2-20个字符，支持中英文）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }

            // 验证身份证号
            string idCard = txtIdCard.Text.Trim();
            if (!ValidationHelper.ValidateIdCardNumber(idCard))
            {
                MessageBox.Show("请输入有效的18位身份证号码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtIdCard.Focus();
                return false;
            }

            // 验证银行卡号
            string bankCard = txtBankCard.Text.Trim();
            if (!ValidationHelper.ValidateBankCardNumber(bankCard))
            {
                MessageBox.Show("请输入有效的银行卡号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBankCard.Focus();
                return false;
            }

            // 验证电话号码
            string phone = txtPhone.Text.Trim();
            if (!ValidationHelper.ValidatePhoneNumber(phone))
            {
                MessageBox.Show("请输入有效的手机号码（11位数字）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhone.Focus();
                return false;
            }

            // 验证性别
            string gender = txtGender.Text.Trim();
            if (string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("请输入性别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGender.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 从界面获取人物信息
        /// </summary>
        /// <returns>人物信息对象</returns>
        private PersonInfo GetPersonInfoFromUI()
        {
            return new PersonInfo
            {
                Name = txtName.Text.Trim(),
                IdCardNumber = txtIdCard.Text.Trim(),
                BankCardNumber = txtBankCard.Text.Trim(),
                PhoneNumber = txtPhone.Text.Trim(),
                Gender = txtGender.Text.Trim(),
                BankName = txtBankName.Text.Trim()
            };
        }

        /// <summary>
        /// 将人物信息显示到界面
        /// </summary>
        /// <param name="personInfo">人物信息对象</param>
        private void DisplayPersonInfoToUI(PersonInfo personInfo)
        {
            if (personInfo != null)
            {
                txtName.Text = personInfo.Name;
                txtIdCard.Text = personInfo.IdCardNumber;
                txtBankCard.Text = personInfo.BankCardNumber;
                txtPhone.Text = personInfo.PhoneNumber;
                txtGender.Text = personInfo.Gender ?? string.Empty;
                txtBankName.Text = personInfo.BankName ?? string.Empty;
                _currentPersonInfo = personInfo;
            }
        }

        /// <summary>
        /// 清空界面输入
        /// </summary>
        private void ClearInput()
        {
            txtName.Clear();
            txtIdCard.Clear();
            txtBankCard.Clear();
            txtPhone.Clear();
            txtGender.Clear();
            txtBankName.Clear();
            txtGender.Clear();
            txtBankName.Clear();
            _currentPersonInfo = null;
            isEditMode = false;
            txtName.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                PersonInfo personInfo = GetPersonInfoFromUI();
                _dataManager.AddPerson(personInfo);

                MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInput();
                LoadPersonData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_currentPersonInfo == null)
            {
                MessageBox.Show("请先选择要编辑的记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
                return;

            try
            {
                PersonInfo newPersonInfo = GetPersonInfoFromUI();
                _dataManager.UpdatePerson(_currentPersonInfo, newPersonInfo);

                MessageBox.Show("更新成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInput();
                LoadPersonData();
                isEditMode = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"更新失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_currentPersonInfo == null)
            {
                MessageBox.Show("请先选择要删除的记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("确定要删除这条记录吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _dataManager.DeletePerson(_currentPersonInfo);

                    MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInput();
                    LoadPersonData();
                    isEditMode = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 保存所有数据（这里实际上是冗余的，因为添加、编辑、删除操作都会自动保存）
            try
            {
                // 重新加载数据以确保获取最新数据
                List<PersonInfo> updatedPersonList = _dataManager.LoadData();
                _dataManager.SaveData(updatedPersonList);

                MessageBox.Show("数据已保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPersonInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPersonInfo.Rows[e.RowIndex];
                if (row.DataBoundItem is PersonInfo personInfo)
                {
                    DisplayPersonInfoToUI(personInfo);
                    isEditMode = true;
                }
            }
        }

        /// <summary>
        /// 搜索功能实现
        /// </summary>
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                // 如果搜索框为空，显示所有数据
                LoadPersonData();
            }
            else
            {
                // 根据搜索文本过滤数据
                var filteredList = personList.Where(p =>
                    p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    p.IdCardNumber.Contains(searchText) ||
                    p.PhoneNumber.Contains(searchText) ||
                    p.BankCardNumber.Contains(searchText) ||
                    p.Gender.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    p.BankName.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                UpdateDataGridView(filteredList);
                UpdateSelectedCount();
            }
        }

        /// <summary>
        /// 更新DataGridView数据
        /// </summary>
        /// <param name="data">要显示的数据列表</param>
        private void UpdateDataGridView(List<PersonInfo> data)
        {
            // 按名字升序排序，按拼音字母顺序显示
            var sortedData = data.OrderBy(p => p.Name).ToList();

            dgvPersonInfo.DataSource = null;
            dgvPersonInfo.DataSource = sortedData;
        }

        /// <summary>
        /// Excel导入按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_templateFilePath) || !File.Exists(_templateFilePath))
            {
                MessageBox.Show("模板文件不存在，请先生成模板！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                toolStripStatusLabel1.Text = "正在导入数据...";
                Application.DoEvents();

                string result = _dataManager.ImportFromExcel(_templateFilePath);

                LoadPersonData();

                MessageBox.Show(result, "导入结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateSelectedCount();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            List<PersonInfo> selectedPersons = new List<PersonInfo>();
            foreach (DataGridViewRow row in dgvPersonInfo.Rows)
            {
                if (row.DataBoundItem is PersonInfo person && person.IsSelected)
                {
                    selectedPersons.Add(person);
                }
            }

            if (selectedPersons.Count == 0)
            {
                MessageBox.Show("请至少勾选一条数据进行导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel文件|*.xlsx";
            saveFileDialog.Title = "保存Excel文件";
            saveFileDialog.FileName = "人员信息导出_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                bool result = _dataManager.ExportToExcel(selectedPersons, filePath);
                if (result)
                {
                    MessageBox.Show($"成功导出{selectedPersons.Count}条数据到Excel文件", "导出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 询问是否打开文件
                    DialogResult dr = MessageBox.Show("是否立即打开导出的Excel文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(filePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("无法打开文件: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("导出失败，请检查并重试", "导出失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGenerateTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel文件|*.xlsx";
                saveFileDialog.Title = "保存模板文件";
                saveFileDialog.FileName = "个人信息模板.xlsx";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    
                    GenerateExcelTemplate(filePath);
                    
                    _templateFilePath = filePath;
                    btnImportExcel.Enabled = true;
                    
                    MessageBox.Show("模板文件已生成！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
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

        private void GenerateExcelTemplate(string filePath)
        {
            try
            {
                NPOI.XSSF.UserModel.XSSFWorkbook workbook = new NPOI.XSSF.UserModel.XSSFWorkbook();
                
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("个人信息模板");
                
                NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("姓名");
                headerRow.CreateCell(1).SetCellValue("身份证号");
                headerRow.CreateCell(2).SetCellValue("银行卡号");
                headerRow.CreateCell(3).SetCellValue("电话号码");
                headerRow.CreateCell(4).SetCellValue("性别");
                headerRow.CreateCell(5).SetCellValue("银行名称");
                
                NPOI.SS.UserModel.ICellStyle headerStyle = workbook.CreateCellStyle();
                NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                headerStyle.SetFont(font);
                headerStyle.FillForegroundColor = NPOI.SS.UserModel.IndexedColors.Grey25Percent.Index;
                headerStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
                
                for (int i = 0; i < 6; i++)
                {
                    headerRow.GetCell(i).CellStyle = headerStyle;
                }
                
                sheet.SetColumnWidth(0, 15 * 256);
                sheet.SetColumnWidth(1, 22 * 256);
                sheet.SetColumnWidth(2, 22 * 256);
                sheet.SetColumnWidth(3, 15 * 256);
                sheet.SetColumnWidth(4, 8 * 256);
                sheet.SetColumnWidth(5, 20 * 256);
                
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fileStream, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("生成Excel模板失败: " + ex.Message, ex);
            }
        }

        private void btnCheckInfo_Click(object sender, EventArgs e)
        {
            if (personList == null || personList.Count == 0)
            {
                MessageBox.Show("没有个人信息数据可供检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var person in personList)
            {
                person.IdCardNumber = person.IdCardNumber.ToUpper();
            }

            var duplicateIdCards = personList.GroupBy(p => p.IdCardNumber)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            var duplicateBankCards = personList.GroupBy(p => p.BankCardNumber)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            var duplicatePhones = personList.GroupBy(p => p.PhoneNumber)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            List<string> errorMessages = new List<string>();
            int errorCount = 0;
            int checkedCount = 0;

            foreach (var person in personList)
            {
                checkedCount++;
                List<string> personErrors = new List<string>();

                if (!ValidationHelper.ValidateIdCardNumber(person.IdCardNumber))
                {
                    personErrors.Add($"身份证号码格式错误（应为18位）: {person.IdCardNumber}");
                }
                else if (duplicateIdCards.Contains(person.IdCardNumber))
                {
                    personErrors.Add($"身份证号码重复: {person.IdCardNumber}");
                }

                if (!ValidationHelper.ValidateBankCardNumber(person.BankCardNumber))
                {
                    personErrors.Add($"银行卡号码格式错误（应为13-19位数字）: {person.BankCardNumber}");
                }
                else if (duplicateBankCards.Contains(person.BankCardNumber))
                {
                    personErrors.Add($"银行卡号码重复: {person.BankCardNumber}");
                }

                if (!ValidationHelper.ValidatePhoneNumber(person.PhoneNumber))
                {
                    personErrors.Add($"电话号码格式错误（应为11位且以1开头）: {person.PhoneNumber}");
                }
                else if (duplicatePhones.Contains(person.PhoneNumber))
                {
                    personErrors.Add($"电话号码重复: {person.PhoneNumber}");
                }

                if (personErrors.Count > 0)
                {
                    errorCount++;
                    errorMessages.Add($"【{person.Name}】:\n  {string.Join("\n  ", personErrors)}");
                }
            }

            if (errorCount == 0)
            {
                MessageBox.Show($"检查完成！\n共检查 {checkedCount} 条记录，全部格式正确且无重复。", "检查结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string resultMessage = $"检查完成！\n共检查 {checkedCount} 条记录，发现 {errorCount} 条记录有误：\n\n";
                resultMessage += string.Join("\n\n", errorMessages);
                
                MessageBox.Show(resultMessage, "检查结果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
