using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace WinFormsApp1
{
    /// <summary>
    /// 数据管理器类，负责数据的存储、读取和管理
    /// </summary>
    public class personalDataManager
    {
        private readonly string _personFilePath;
        private readonly string _salaryFilePath;
        private readonly JsonSerializerOptions _jsonOptions;

        /// <summary>
        /// 构造函数
        /// </summary>
        public personalDataManager()
        {
            // 设置数据文件路径
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolderPath = Path.Combine(appDataPath, "PersonInfoApp");
            
            // 确保文件夹存在
            Directory.CreateDirectory(appFolderPath);
            
            _personFilePath = Path.Combine(appFolderPath, "person_info.json");
            _salaryFilePath = Path.Combine(appFolderPath, "salary_info.json");
            
            // 配置JSON序列化选项
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        /// <summary>
        /// 保存人物信息列表到文件
        /// </summary>
        /// <param name="personList">人物信息列表</param>
        public void SaveData(List<PersonInfo> personList)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(personList, _jsonOptions);
                File.WriteAllText(_personFilePath, jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception($"保存数据失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 从文件读取人物信息列表
        /// </summary>
        /// <returns>人物信息列表</returns>
        public List<PersonInfo> LoadData()
        {
            try
            {
                if (!File.Exists(_personFilePath))
                {
                    return new List<PersonInfo>();
                }

                string jsonData = File.ReadAllText(_personFilePath);
                var personList = JsonSerializer.Deserialize<List<PersonInfo>>(jsonData, _jsonOptions) ?? new List<PersonInfo>();
                
                // 确保所有PersonInfo对象的Gender和BankName属性都已初始化
                foreach (var person in personList)
                {
                    if (person.Gender == null)
                        person.Gender = string.Empty;
                    if (person.BankName == null)
                        person.BankName = string.Empty;
                }
                
                return personList;
            }
            catch (Exception ex)
            {
                throw new Exception($"读取数据失败: {ex.Message}", ex);
            }
        }
        
        // 薪资信息相关方法
        
        /// <summary>
        /// 保存薪资信息列表到文件
        /// </summary>
        /// <param name="salaryList">薪资信息列表</param>
        public void SaveSalaryData(List<SalaryInfo> salaryList)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(salaryList, _jsonOptions);
                File.WriteAllText(_salaryFilePath, jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception($"保存薪资数据失败: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// 从文件读取薪资信息列表
        /// </summary>
        /// <returns>薪资信息列表</returns>
        public List<SalaryInfo> LoadSalaryData()
        {
            try
            {
                if (!File.Exists(_salaryFilePath))
                {
                    return new List<SalaryInfo>();
                }

                string jsonData = File.ReadAllText(_salaryFilePath);
                return JsonSerializer.Deserialize<List<SalaryInfo>>(jsonData, _jsonOptions) ?? new List<SalaryInfo>();
            }
            catch (Exception ex)
            {
                throw new Exception($"读取薪资数据失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 添加新的人物信息
        /// </summary>
        /// <param name="personInfo">新的人物信息</param>
        public void AddPerson(PersonInfo personInfo)
        {
            var personList = LoadData();
            personInfo.CreatedTime = DateTime.Now;
            personInfo.LastModifiedTime = DateTime.Now;
            personList.Add(personInfo);
            SaveData(personList);
        }

        /// <summary>
        /// 更新人物信息
        /// </summary>
        /// <param name="oldPersonInfo">旧的人物信息（用于查找）</param>
        /// <param name="newPersonInfo">新的人物信息</param>
        public void UpdatePerson(PersonInfo oldPersonInfo, PersonInfo newPersonInfo)
        {
            var personList = LoadData();
            int index = personList.FindIndex(p => 
                p.Name == oldPersonInfo.Name && 
                p.IdCardNumber == oldPersonInfo.IdCardNumber &&
                p.BankCardNumber == oldPersonInfo.BankCardNumber &&
                p.PhoneNumber == oldPersonInfo.PhoneNumber);
            
            if (index >= 0)
            {
                newPersonInfo.CreatedTime = personList[index].CreatedTime; // 保留创建时间
                newPersonInfo.LastModifiedTime = DateTime.Now;
                personList[index] = newPersonInfo;
                SaveData(personList);
            }
        }

        /// <summary>
        /// 删除人物信息
        /// </summary>
        /// <param name="personInfo">要删除的人物信息</param>
        public void DeletePerson(PersonInfo personInfo)
        {
            var personList = LoadData();
            personList.RemoveAll(p => 
                p.Name == personInfo.Name && 
                p.IdCardNumber == personInfo.IdCardNumber &&
                p.BankCardNumber == personInfo.BankCardNumber &&
                p.PhoneNumber == personInfo.PhoneNumber);
            SaveData(personList);
        }

        /// <summary>
        /// 从Excel文件批量导入数据
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns>导入结果信息</returns>
        public string ImportFromExcel(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return "文件不存在";
            }

            try
            {
                var existingData = LoadData();
                int addedCount = 0;
                int updatedCount = 0;
                int errorCount = 0;

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook workbook = new XSSFWorkbook(fileStream);
                    ISheet sheet = workbook.GetSheetAt(0); // 获取第一个工作表

                    // 假设第一行是表头，从第二行开始读取数据
                    for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                    {
                        IRow row = sheet.GetRow(rowIndex);
                        if (row == null) continue;

                        try
                        {
                            // 读取Excel中的数据（假设列顺序为：姓名、身份证号、银行卡号、电话号码、性别、银行名称）
                            string name = GetCellValue(row.GetCell(0));
                            string idCard = GetCellValue(row.GetCell(1));
                            string bankCard = GetCellValue(row.GetCell(2));
                            string phone = GetCellValue(row.GetCell(3));
                            string gender = GetCellValue(row.GetCell(4));
                            string bankName = GetCellValue(row.GetCell(5));

                            // 验证数据
                            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(idCard) || 
                                string.IsNullOrEmpty(bankCard) || string.IsNullOrEmpty(phone) ||
                                string.IsNullOrEmpty(gender))
                            {
                                errorCount++;
                                continue;
                            }

                            // 创建PersonInfo对象
                            PersonInfo newPerson = new PersonInfo
                            {
                                Name = name.Trim(),
                                IdCardNumber = idCard.Trim(),
                                BankCardNumber = bankCard.Trim(),
                                PhoneNumber = phone.Trim(),
                                Gender = gender.Trim(),
                                BankName = bankName.Trim(),
                                CreatedTime = DateTime.Now,
                                LastModifiedTime = DateTime.Now
                            };

                            // 检查是否已存在（根据身份证号判断）
                            int existingIndex = existingData.FindIndex(p => p.IdCardNumber == newPerson.IdCardNumber);
                            if (existingIndex >= 0)
                            {
                                // 更新现有记录
                                newPerson.CreatedTime = existingData[existingIndex].CreatedTime;
                                existingData[existingIndex] = newPerson;
                                updatedCount++;
                            }
                            else
                            {
                                // 添加新记录
                                existingData.Add(newPerson);
                                addedCount++;
                            }
                        }
                        catch (Exception)
                        {
                            errorCount++;
                        }
                    }
                }

                // 保存更新后的数据
                if (addedCount > 0 || updatedCount > 0)
                {
                    SaveData(existingData);
                }

                return $"导入完成：新增 {addedCount} 条，更新 {updatedCount} 条，错误 {errorCount} 条";
            }
            catch (Exception ex)
            {
                return $"导入失败：{ex.Message}";
            }
        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns>单元格的字符串值</returns>
        private string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;

            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Formula:
                    try
                    {
                        return cell.StringCellValue;
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
                default:
                    return string.Empty;
            }
        }
        
        /// <summary>
        /// 将人员信息导出到Excel文件
        /// </summary>
        /// <param name="persons">要导出的人员信息列表</param>
        /// <param name="filePath">导出文件路径</param>
        /// <returns>导出是否成功</returns>
        public bool ExportToExcel(List<PersonInfo> persons, string filePath)
        {
            try
            {
                // 创建工作簿
                XSSFWorkbook workbook = new XSSFWorkbook();
                
                // 创建工作表
                ISheet sheet = workbook.CreateSheet("个人信息");
                
                // 创建标题行
                IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("姓名");
                headerRow.CreateCell(1).SetCellValue("身份证号");
                headerRow.CreateCell(2).SetCellValue("银行卡号");
                headerRow.CreateCell(3).SetCellValue("电话号码");
                headerRow.CreateCell(4).SetCellValue("性别");
                headerRow.CreateCell(5).SetCellValue("银行名称");
                headerRow.CreateCell(6).SetCellValue("创建时间");
                headerRow.CreateCell(7).SetCellValue("最后修改时间");
                
                // 设置标题样式
                ICellStyle headerStyle = workbook.CreateCellStyle();
                IFont font = workbook.CreateFont();
                font.Boldweight = (short)FontBoldWeight.Bold;
                headerStyle.SetFont(font);
                
                for (int i = 0; i < 8; i++)
                {
                    headerRow.GetCell(i).CellStyle = headerStyle;
                    sheet.AutoSizeColumn(i);
                }
                
                // 填充数据
                int rowIndex = 1;
                foreach (var person in persons)
                {
                    IRow row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(person.Name);
                    row.CreateCell(1).SetCellValue(person.IdCardNumber);
                    row.CreateCell(2).SetCellValue(person.BankCardNumber);
                    row.CreateCell(3).SetCellValue(person.PhoneNumber);
                    row.CreateCell(4).SetCellValue(person.Gender);
                    row.CreateCell(5).SetCellValue(person.BankName);
                    row.CreateCell(6).SetCellValue(person.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    row.CreateCell(7).SetCellValue(person.LastModifiedTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                
                // 调整列宽
                sheet.AutoSizeColumn(0);
                sheet.AutoSizeColumn(1);
                sheet.AutoSizeColumn(2);
                sheet.AutoSizeColumn(3);
                sheet.AutoSizeColumn(4);
                sheet.AutoSizeColumn(5);
                sheet.AutoSizeColumn(6);
                sheet.AutoSizeColumn(7);
                
                // 保存文件
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    workbook.Write(fs);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("导出Excel失败: " + ex.Message);
                return false;
            }
        }
    }
    
    /// <summary>
    /// 薪资信息类
    /// </summary>
    public class SalaryInfo
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// 当月工资金额
        /// </summary>
        public decimal SalaryAmount { get; set; }

        /// <summary>
        /// 发放单位
        /// </summary>
        public string PayrollUnit { get; set; }

        /// <summary>
        /// 制表时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 发放时间
        /// </summary>
        public DateTime? PaymentTime { get; set; }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>薪资信息字符串</returns>
        public override string ToString()
        {
            return $"姓名：{Name}, 月份：{Month}, 金额：{SalaryAmount}, 单位：{PayrollUnit}";
        }
    }
    
    /// <summary>
    /// 薪资数据管理器类，负责薪资数据的存储、读取和管理
    /// </summary>
    public class SalaryDataManager
    {
        private readonly string _salaryFilePath;
        private readonly JsonSerializerOptions _jsonOptions;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SalaryDataManager()
        {
            // 设置数据文件路径
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolderPath = Path.Combine(appDataPath, "PersonInfoApp");
            
            // 确保文件夹存在
            Directory.CreateDirectory(appFolderPath);
            
            _salaryFilePath = Path.Combine(appFolderPath, "salary_info.json");
            
            // 配置JSON序列化选项
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        /// <summary>
        /// 保存薪资信息列表到文件
        /// </summary>
        /// <param name="salaryList">薪资信息列表</param>
        public void SaveSalaryData(List<SalaryInfo> salaryList)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(salaryList, _jsonOptions);
                File.WriteAllText(_salaryFilePath, jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception($"保存薪资数据失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 从文件读取薪资信息列表
        /// </summary>
        /// <returns>薪资信息列表</returns>
        public List<SalaryInfo> LoadSalaryData()
        {
            try
            {
                if (!File.Exists(_salaryFilePath))
                {
                    return new List<SalaryInfo>();
                }

                string jsonData = File.ReadAllText(_salaryFilePath);
                return JsonSerializer.Deserialize<List<SalaryInfo>>(jsonData, _jsonOptions) ?? new List<SalaryInfo>();
            }
            catch (Exception ex)
            {
                throw new Exception($"读取薪资数据失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 从Excel文件批量导入薪资数据
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns>导入结果信息</returns>
        public string ImportSalaryFromExcel(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return "文件不存在";
            }

            try
            {
                var existingData = LoadSalaryData();
                int addedCount = 0;
                int updatedCount = 0;
                int errorCount = 0;

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook workbook = new XSSFWorkbook(fileStream);
                    ISheet sheet = workbook.GetSheetAt(0); // 获取第一个工作表

                    // 假设第一行是表头，从第二行开始读取数据
                    for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                    {
                        IRow row = sheet.GetRow(rowIndex);
                        if (row == null) continue;

                        try
                        {
                            // 读取Excel中的数据（假设列顺序为：姓名、月份、工资金额、发放单位、发放时间）
                            string name = GetCellValue(row.GetCell(0));
                            string month = GetCellValue(row.GetCell(1));
                            string salaryAmountStr = GetCellValue(row.GetCell(2));
                            string payrollUnit = GetCellValue(row.GetCell(3));
                            string paymentTimeStr = GetCellValue(row.GetCell(4));

                            // 验证数据
                            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(month) || 
                                string.IsNullOrEmpty(salaryAmountStr) || string.IsNullOrEmpty(payrollUnit))
                            {
                                errorCount++;
                                continue;
                            }

                            // 解析工资金额
                            if (!decimal.TryParse(salaryAmountStr, out decimal salaryAmount))
                            {
                                errorCount++;
                                continue;
                            }

                            // 解析发放时间
                            DateTime? paymentTime = null;
                            if (!string.IsNullOrEmpty(paymentTimeStr))
                            {
                                if (DateTime.TryParse(paymentTimeStr, out DateTime parsedDate))
                                {
                                    paymentTime = parsedDate;
                                }
                            }

                            // 创建SalaryInfo对象
                            SalaryInfo newSalary = new SalaryInfo
                            {
                                Name = name.Trim(),
                                Month = month.Trim(),
                                SalaryAmount = salaryAmount,
                                PayrollUnit = payrollUnit.Trim(),
                                PaymentTime = paymentTime
                            };

                            // 检查是否已存在（根据姓名和月份判断）
                            int existingIndex = existingData.FindIndex(s => 
                                s.Name == newSalary.Name && s.Month == newSalary.Month);
                            if (existingIndex >= 0)
                            {
                                // 更新现有记录
                                newSalary.CreateTime = existingData[existingIndex].CreateTime;
                                newSalary.Id = existingData[existingIndex].Id;
                                existingData[existingIndex] = newSalary;
                                updatedCount++;
                            }
                            else
                            {
                                // 添加新记录
                                existingData.Add(newSalary);
                                addedCount++;
                            }
                        }
                        catch (Exception)
                        {
                            errorCount++;
                        }
                    }
                }

                // 保存更新后的数据
                if (addedCount > 0 || updatedCount > 0)
                {
                    SaveSalaryData(existingData);
                }

                return $"导入完成：新增 {addedCount} 条，更新 {updatedCount} 条，错误 {errorCount} 条";
            }
            catch (Exception ex)
            {
                return $"导入失败：{ex.Message}";
            }
        }

        /// <summary>
        /// 将薪资信息导出到Excel文件（支持筛选条件）
        /// </summary>
        /// <param name="salaries">要导出的薪资信息列表</param>
        /// <param name="filePath">导出文件路径</param>
        /// <returns>导出是否成功</returns>
        public bool ExportSalaryToExcel(List<SalaryInfo> salaries, string filePath)
        {
            try
            {
                // 创建工作簿
                XSSFWorkbook workbook = new XSSFWorkbook();
                
                // 创建工作表
                ISheet sheet = workbook.CreateSheet("薪资信息");
                
                // 创建标题行
                IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("姓名");
                headerRow.CreateCell(1).SetCellValue("月份");
                headerRow.CreateCell(2).SetCellValue("当月工资金额");
                headerRow.CreateCell(3).SetCellValue("发放单位");
                headerRow.CreateCell(4).SetCellValue("制表时间");
                headerRow.CreateCell(5).SetCellValue("发放时间");
                
                // 设置标题样式
                ICellStyle headerStyle = workbook.CreateCellStyle();
                IFont font = workbook.CreateFont();
                font.Boldweight = (short)FontBoldWeight.Bold;
                headerStyle.SetFont(font);
                
                for (int i = 0; i < 6; i++)
                {
                    headerRow.GetCell(i).CellStyle = headerStyle;
                }
                
                // 填充数据
                int rowIndex = 1;
                foreach (var salary in salaries)
                {
                    IRow row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(salary.Name);
                    row.CreateCell(1).SetCellValue(salary.Month);
                    row.CreateCell(2).SetCellValue(Convert.ToDouble(salary.SalaryAmount));
                    row.CreateCell(3).SetCellValue(salary.PayrollUnit);
                    row.CreateCell(4).SetCellValue(salary.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    row.CreateCell(5).SetCellValue(salary.PaymentTime?.ToString("yyyy-MM-dd") ?? "");
                }
                
                // 调整列宽
                for (int i = 0; i < 6; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
                
                // 保存文件
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    workbook.Write(fs);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("导出Excel失败: " + ex.Message);
                return false;
            }
        }
        
        /// <summary>
        /// 将薪资信息导出到Excel文件（包含总金额列）
        /// </summary>
        /// <param name="salariesWithTotals">要导出的带总金额的薪资信息列表</param>
        /// <param name="filePath">导出文件路径</param>
        /// <returns>导出是否成功</returns>
        public bool ExportSalaryWithTotalsToExcel(dynamic salariesWithTotals, string filePath)
        {
            try
            {
                // 创建工作簿
                XSSFWorkbook workbook = new XSSFWorkbook();
                
                // 创建工作表
                ISheet sheet = workbook.CreateSheet("薪资信息");
                
                // 创建标题行
                IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("姓名");
                headerRow.CreateCell(1).SetCellValue("月份");
                headerRow.CreateCell(2).SetCellValue("当月工资金额");
                headerRow.CreateCell(3).SetCellValue("发放单位");
                headerRow.CreateCell(4).SetCellValue("制表时间");
                headerRow.CreateCell(5).SetCellValue("发放时间");
                headerRow.CreateCell(6).SetCellValue("发放总金额");
                
                // 设置标题样式
                ICellStyle headerStyle = workbook.CreateCellStyle();
                IFont font = workbook.CreateFont();
                font.Boldweight = (short)FontBoldWeight.Bold;
                headerStyle.SetFont(font);
                
                for (int i = 0; i < 7; i++)
                {
                    headerRow.GetCell(i).CellStyle = headerStyle;
                }
                
                // 设置总金额的样式（浅黄色背景）
                ICellStyle totalSalaryStyle = workbook.CreateCellStyle();
                totalSalaryStyle.FillForegroundColor = IndexedColors.LightYellow.Index;
                totalSalaryStyle.FillPattern = FillPattern.SolidForeground;
                
                // 设置金额格式
                ICellStyle currencyStyle = workbook.CreateCellStyle();
                currencyStyle.CloneStyleFrom(totalSalaryStyle);
                IDrawing drawing = sheet.CreateDrawingPatriarch();
                IDataFormat format = workbook.CreateDataFormat();
                currencyStyle.DataFormat = format.GetFormat("#,##0");
                
                // 填充数据
                int rowIndex = 1;
                foreach (var item in salariesWithTotals)
                {
                    IRow row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(item.Name);
                    row.CreateCell(1).SetCellValue(item.Month);
                    row.CreateCell(2).SetCellValue(Convert.ToDouble(item.SalaryAmount));
                    row.CreateCell(3).SetCellValue(item.PayrollUnit);
                    row.CreateCell(4).SetCellValue(item.CreateTime);
                    row.CreateCell(5).SetCellValue(item.PaymentTime);
                    
                    // 设置总金额单元格并应用样式
                    ICell totalCell = row.CreateCell(6);
                    totalCell.SetCellValue(Convert.ToDouble(item.TotalSalary));
                    totalCell.CellStyle = currencyStyle;
                }
                
                // 调整列宽
                for (int i = 0; i < 7; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
                
                // 保存文件
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    workbook.Write(fs);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("导出Excel失败: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 根据条件筛选薪资信息
        /// </summary>
        /// <param name="name">姓名筛选条件（可选）</param>
        /// <param name="payrollUnit">发放单位筛选条件（可选）</param>
        /// <param name="month">月份筛选条件（可选）</param>
        /// <param name="startDate">开始发放日期（可选）</param>
        /// <param name="endDate">结束发放日期（可选）</param>
        /// <returns>筛选后的薪资信息列表</returns>
        public List<SalaryInfo> FilterSalaryData(string name = null, string payrollUnit = null, 
            string month = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var allData = LoadSalaryData();
            
            return allData.Where(s => 
                (string.IsNullOrEmpty(name) || s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(payrollUnit) || s.PayrollUnit.Contains(payrollUnit, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(month) || s.Month == month) &&
                (!startDate.HasValue || !s.PaymentTime.HasValue || s.PaymentTime.Value >= startDate.Value) &&
                (!endDate.HasValue || !s.PaymentTime.HasValue || s.PaymentTime.Value <= endDate.Value)
            ).ToList();
        }
        
        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <returns>单元格的字符串值</returns>
        private string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;

            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Formula:
                    try
                    {
                        return cell.StringCellValue;
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
                default:
                    return string.Empty;
            }
        }
    }
}