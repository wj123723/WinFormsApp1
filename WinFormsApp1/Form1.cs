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

        public Form1()
        {
            InitializeComponent();
            _currentPersonInfo = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 加载数据到DataGridView
            _dataManager = new personalDataManager();
            LoadPersonData();

            // 添加搜索文本框的事件处理程序
            txtSearch.TextChanged += TxtSearch_TextChanged;

            // 添加薪资管理按钮
            AddSalaryManagementButton();
        }

        private void AddSalaryManagementButton()
        {
            // 创建薪资管理按钮
            Button btnSalaryManagement = new Button
            {
                Text = "薪资管理",
                Size = new Size(100, 40),
                Location = new Point(10, 10), // 使用固定位置，确保可见
                BackColor = Color.LightBlue,
                ForeColor = Color.Black,
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold)
            };
            
            // 添加点击事件
            btnSalaryManagement.Click += (sender, e) =>
            {
                try
                {
                    // 打开薪资管理窗体
                    SalaryForm salaryForm = new SalaryForm();
                    salaryForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("打开薪资管理窗体失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            
            // 将按钮添加到窗体并调用BringToFront方法确保在最上层
            this.Controls.Add(btnSalaryManagement);
            btnSalaryManagement.BringToFront(); // 作为方法调用而不是属性设置
            
            // 调试信息
            Console.WriteLine($"薪资管理按钮已添加，位置: {btnSalaryManagement.Location}");
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

                // 更新状态栏信息
                toolStripStatusLabel1.Text = $"就绪 - {personList.Count} 条记录";
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
                PhoneNumber = txtPhone.Text.Trim()
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
                    p.BankCardNumber.Contains(searchText)
                ).ToList();

                // 更新DataGridView
                UpdateDataGridView(filteredList);
                // 更新状态栏信息
                toolStripStatusLabel1.Text = $"就绪 - {filteredList.Count} 条记录";
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
            // 创建文件选择对话框
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "选择Excel文件",
                Filter = "Excel文件 (*.xlsx)|*.xlsx|所有文件 (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Multiselect = false
            };

            // 显示对话框并检查用户是否点击了确定
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 显示导入中的提示
                    toolStripStatusLabel1.Text = "正在导入数据...";
                    Application.DoEvents();

                    // 调用DataManager的导入方法
                    string result = _dataManager.ImportFromExcel(openFileDialog.FileName);

                    // 重新加载数据
                    LoadPersonData();

                    // 显示导入结果
                    MessageBox.Show(result, "导入结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    toolStripStatusLabel1.Text = $"就绪 - {dgvPersonInfo.Rows.Count} 条记录";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导入失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    toolStripStatusLabel1.Text = $"就绪 - {dgvPersonInfo.Rows.Count} 条记录";
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgvPersonInfo.SelectedRows.Count == 0)
            {
                MessageBox.Show("请至少选择一行数据进行导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel文件|*.xlsx";
            saveFileDialog.Title = "保存Excel文件";
            saveFileDialog.FileName = "人员信息导出_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                // 收集选中的人员信息
                List<PersonInfo> selectedPersons = new List<PersonInfo>();
                foreach (DataGridViewRow row in dgvPersonInfo.SelectedRows)
                {
                    if (row.DataBoundItem is PersonInfo person)
                    {
                        selectedPersons.Add(person);
                    }
                }

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
