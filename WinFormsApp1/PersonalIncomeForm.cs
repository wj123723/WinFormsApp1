using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace WinFormsApp1
{
    public class PersonalIncomeForm : Form
    {
        private Label lblPersonName;
        private TextBox txtPersonName;
        private Label lblAmount;
        private TextBox txtAmount;
        private Label lblRemark;
        private TextBox txtRemark;
        private Label lblRecordType;
        private ComboBox cmbRecordType;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private DataGridView dgvRecords;
        private Label lblTotalIncome;
        private Label lblTotalExpense;
        private Label lblRemaining;

        private string _personName;
        private personalDataManager _dataManager;
        private List<PersonalIncomeRecord> _records;
        private PersonalIncomeRecord _selectedRecord;

        public PersonalIncomeForm(string personName)
        {
            _personName = personName;
            _dataManager = new personalDataManager();
            InitializeComponent();
            LoadRecords();
        }

        private void InitializeComponent()
        {
            this.Text = $"个人收入管理 - {_personName}";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int y = 20;

            lblPersonName = new Label
            {
                Text = "姓名：",
                Location = new Point(20, y),
                Size = new Size(80, 25)
            };
            this.Controls.Add(lblPersonName);

            txtPersonName = new TextBox
            {
                Text = _personName,
                Location = new Point(110, y),
                Size = new Size(200, 25),
                ReadOnly = true
            };
            this.Controls.Add(txtPersonName);

            y += 35;

            lblRecordType = new Label
            {
                Text = "记录类型：",
                Location = new Point(20, y),
                Size = new Size(80, 25)
            };
            this.Controls.Add(lblRecordType);

            cmbRecordType = new ComboBox
            {
                Location = new Point(110, y),
                Size = new Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRecordType.Items.AddRange(new object[] { "收入", "支取" });
            cmbRecordType.SelectedIndex = 0;
            this.Controls.Add(cmbRecordType);

            y += 35;

            lblAmount = new Label
            {
                Text = "金额：",
                Location = new Point(20, y),
                Size = new Size(80, 25)
            };
            this.Controls.Add(lblAmount);

            txtAmount = new TextBox
            {
                Location = new Point(110, y),
                Size = new Size(200, 25)
            };
            this.Controls.Add(txtAmount);

            y += 35;

            lblRemark = new Label
            {
                Text = "备注：",
                Location = new Point(20, y),
                Size = new Size(80, 25)
            };
            this.Controls.Add(lblRemark);

            txtRemark = new TextBox
            {
                Location = new Point(110, y),
                Size = new Size(400, 25)
            };
            this.Controls.Add(txtRemark);

            y += 35;

            btnAdd = new Button
            {
                Text = "添加记录",
                Location = new Point(110, y),
                Size = new Size(100, 30),
                BackColor = Color.LightGreen
            };
            btnAdd.Click += BtnAdd_Click;
            this.Controls.Add(btnAdd);

            btnUpdate = new Button
            {
                Text = "修改记录",
                Location = new Point(220, y),
                Size = new Size(100, 30),
                BackColor = Color.LightBlue
            };
            btnUpdate.Click += BtnUpdate_Click;
            this.Controls.Add(btnUpdate);

            btnDelete = new Button
            {
                Text = "删除记录",
                Location = new Point(330, y),
                Size = new Size(100, 30),
                BackColor = Color.LightCoral
            };
            btnDelete.Click += BtnDelete_Click;
            this.Controls.Add(btnDelete);

            y += 45;

            dgvRecords = new DataGridView
            {
                Location = new Point(20, y),
                Size = new Size(840, 350),
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", FillWeight = 50 });
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "RecordType", HeaderText = "类型", FillWeight = 80 });
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "Amount", HeaderText = "金额", FillWeight = 100 });
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "Remark", HeaderText = "备注", FillWeight = 200 });
            dgvRecords.Columns.Add(new DataGridViewTextBoxColumn { Name = "RecordTime", HeaderText = "记录时间", FillWeight = 150 });
            dgvRecords.SelectionChanged += DgvRecords_SelectionChanged;
            this.Controls.Add(dgvRecords);

            y += 360;

            lblTotalIncome = new Label
            {
                Location = new Point(20, y),
                Size = new Size(250, 25),
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                ForeColor = Color.Green
            };
            this.Controls.Add(lblTotalIncome);

            lblTotalExpense = new Label
            {
                Location = new Point(280, y),
                Size = new Size(250, 25),
                Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                ForeColor = Color.Red
            };
            this.Controls.Add(lblTotalExpense);

            y += 30;

            lblRemaining = new Label
            {
                Location = new Point(20, y),
                Size = new Size(300, 25),
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                ForeColor = Color.Blue
            };
            this.Controls.Add(lblRemaining);
        }

        private void LoadRecords()
        {
            _records = _dataManager.GetIncomeRecordsByPerson(_personName);
            RefreshDataGridView();
            UpdateTotals();
        }

        private void RefreshDataGridView()
        {
            dgvRecords.Rows.Clear();
            foreach (var record in _records)
            {
                dgvRecords.Rows.Add(
                    record.Id,
                    record.RecordType,
                    record.Amount.ToString("N0"),
                    record.Remark,
                    record.RecordTime.ToString("yyyy-MM-dd HH:mm:ss")
                );
            }
        }

        private void UpdateTotals()
        {
            decimal totalIncome = _records.Where(r => r.RecordType == "收入").Sum(r => r.Amount);
            decimal totalExpense = _records.Where(r => r.RecordType == "支取").Sum(r => r.Amount);
            decimal remaining = totalIncome - totalExpense;

            lblTotalIncome.Text = $"收入总金额：{totalIncome:N0} 元";
            lblTotalExpense.Text = $"支出总金额：{totalExpense:N0} 元";
            lblRemaining.Text = $"剩余总金额：{remaining:N0} 元";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("请输入金额！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("请输入有效的金额（大于0的数字）！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var record = new PersonalIncomeRecord
            {
                PersonName = _personName,
                Amount = amount,
                RecordType = cmbRecordType.SelectedItem.ToString(),
                Remark = txtRemark.Text,
                RecordTime = DateTime.Now,
                CreateTime = DateTime.Now
            };

            try
            {
                _dataManager.AddIncomeRecord(record);
                MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedRecord == null)
            {
                MessageBox.Show("请先选择要修改的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("请输入金额！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("请输入有效的金额（大于0的数字）！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedRecord.Amount = amount;
            _selectedRecord.RecordType = cmbRecordType.SelectedItem.ToString();
            _selectedRecord.Remark = txtRemark.Text;

            try
            {
                _dataManager.UpdateIncomeRecord(_selectedRecord);
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadRecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedRecord == null)
            {
                MessageBox.Show("请先选择要删除的记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"确定要删除这条记录吗？\n类型：{_selectedRecord.RecordType}\n金额：{_selectedRecord.Amount:N0} 元",
                "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _dataManager.DeleteIncomeRecord(_selectedRecord.Id);
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadRecords();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DgvRecords_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRecords.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvRecords.SelectedRows[0].Cells["Id"].Value);
                _selectedRecord = _records.FirstOrDefault(r => r.Id == id);

                if (_selectedRecord != null)
                {
                    txtAmount.Text = _selectedRecord.Amount.ToString();
                    cmbRecordType.SelectedItem = _selectedRecord.RecordType;
                    txtRemark.Text = _selectedRecord.Remark;
                }
            }
            else
            {
                _selectedRecord = null;
            }
        }

        private void ClearInputs()
        {
            txtAmount.Clear();
            txtRemark.Clear();
            cmbRecordType.SelectedIndex = 0;
            _selectedRecord = null;
        }
    }
}
