using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WinFormsApp1
{
    public class DataMigration
    {
        private readonly string _personFilePath;
        private readonly string _salaryFilePath;
        private readonly JsonSerializerOptions _jsonOptions;

        public DataMigration()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolderPath = Path.Combine(appDataPath, "PersonInfoApp");
            
            Directory.CreateDirectory(appFolderPath);
            
            _personFilePath = Path.Combine(appFolderPath, "person_info.json");
            _salaryFilePath = Path.Combine(appFolderPath, "salary_info.json");
            
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        public string ExportData(string exportFilePath)
        {
            try
            {
                var exportData = new MigrationData
                {
                    ExportTime = DateTime.Now,
                    Version = "1.0"
                };

                if (File.Exists(_personFilePath))
                {
                    string personJson = File.ReadAllText(_personFilePath);
                    exportData.PersonInfoList = JsonSerializer.Deserialize<List<PersonInfo>>(personJson, _jsonOptions) 
                        ?? new List<PersonInfo>();
                }
                else
                {
                    exportData.PersonInfoList = new List<PersonInfo>();
                }

                if (File.Exists(_salaryFilePath))
                {
                    string salaryJson = File.ReadAllText(_salaryFilePath);
                    exportData.SalaryInfoList = JsonSerializer.Deserialize<List<SalaryInfo>>(salaryJson, _jsonOptions) 
                        ?? new List<SalaryInfo>();
                }
                else
                {
                    exportData.SalaryInfoList = new List<SalaryInfo>();
                }

                string exportJson = JsonSerializer.Serialize(exportData, _jsonOptions);
                File.WriteAllText(exportFilePath, exportJson);

                return $"数据导出成功！\n导出时间：{exportData.ExportTime:yyyy-MM-dd HH:mm:ss}\n" +
                       $"个人信息：{exportData.PersonInfoList.Count} 条\n" +
                       $"薪资信息：{exportData.SalaryInfoList.Count} 条\n" +
                       $"导出文件：{exportFilePath}";
            }
            catch (Exception ex)
            {
                throw new Exception($"导出数据失败：{ex.Message}", ex);
            }
        }

        public string ImportData(string importFilePath, bool overwriteExisting)
        {
            try
            {
                if (!File.Exists(importFilePath))
                {
                    throw new Exception("导入文件不存在！");
                }

                string importJson = File.ReadAllText(importFilePath);
                var importData = JsonSerializer.Deserialize<MigrationData>(importJson, _jsonOptions);

                if (importData == null)
                {
                    throw new Exception("导入文件格式无效！");
                }

                int personImported = 0;
                int salaryImported = 0;
                int personSkipped = 0;
                int salarySkipped = 0;

                if (overwriteExisting)
                {
                    if (importData.PersonInfoList != null)
                    {
                        string personJson = JsonSerializer.Serialize(importData.PersonInfoList, _jsonOptions);
                        File.WriteAllText(_personFilePath, personJson);
                        personImported = importData.PersonInfoList.Count;
                    }

                    if (importData.SalaryInfoList != null)
                    {
                        string salaryJson = JsonSerializer.Serialize(importData.SalaryInfoList, _jsonOptions);
                        File.WriteAllText(_salaryFilePath, salaryJson);
                        salaryImported = importData.SalaryInfoList.Count;
                    }
                }
                else
                {
                    List<PersonInfo> existingPersons = new List<PersonInfo>();
                    if (File.Exists(_personFilePath))
                    {
                        string personJson = File.ReadAllText(_personFilePath);
                        existingPersons = JsonSerializer.Deserialize<List<PersonInfo>>(personJson, _jsonOptions) 
                            ?? new List<PersonInfo>();
                    }

                    if (importData.PersonInfoList != null)
                    {
                        foreach (var person in importData.PersonInfoList)
                        {
                            bool exists = existingPersons.Exists(p => 
                                p.Name == person.Name && 
                                p.IdCardNumber == person.IdCardNumber);
                            
                            if (!exists)
                            {
                                existingPersons.Add(person);
                                personImported++;
                            }
                            else
                            {
                                personSkipped++;
                            }
                        }
                        
                        string personJson = JsonSerializer.Serialize(existingPersons, _jsonOptions);
                        File.WriteAllText(_personFilePath, personJson);
                    }

                    List<SalaryInfo> existingSalaries = new List<SalaryInfo>();
                    if (File.Exists(_salaryFilePath))
                    {
                        string salaryJson = File.ReadAllText(_salaryFilePath);
                        existingSalaries = JsonSerializer.Deserialize<List<SalaryInfo>>(salaryJson, _jsonOptions) 
                            ?? new List<SalaryInfo>();
                    }

                    if (importData.SalaryInfoList != null)
                    {
                        foreach (var salary in importData.SalaryInfoList)
                        {
                            bool exists = existingSalaries.Exists(s => 
                                s.Name == salary.Name && 
                                s.Month == salary.Month && 
                                s.SalaryAmount == salary.SalaryAmount &&
                                s.PayrollUnit == salary.PayrollUnit);
                            
                            if (!exists)
                            {
                                existingSalaries.Add(salary);
                                salaryImported++;
                            }
                            else
                            {
                                salarySkipped++;
                            }
                        }
                        
                        string salaryJson = JsonSerializer.Serialize(existingSalaries, _jsonOptions);
                        File.WriteAllText(_salaryFilePath, salaryJson);
                    }
                }

                string result = $"数据导入成功！\n" +
                               $"源文件版本：{importData.Version}\n" +
                               $"导出时间：{importData.ExportTime:yyyy-MM-dd HH:mm:ss}\n\n" +
                               $"个人信息：\n" +
                               $"  导入：{personImported} 条\n" +
                               $"  跳过：{personSkipped} 条\n\n" +
                               $"薪资信息：\n" +
                               $"  导入：{salaryImported} 条\n" +
                               $"  跳过：{salarySkipped} 条";

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"导入数据失败：{ex.Message}", ex);
            }
        }

        public MigrationDataPreview PreviewImportData(string importFilePath)
        {
            try
            {
                if (!File.Exists(importFilePath))
                {
                    throw new Exception("导入文件不存在！");
                }

                string importJson = File.ReadAllText(importFilePath);
                var importData = JsonSerializer.Deserialize<MigrationData>(importJson, _jsonOptions);

                if (importData == null)
                {
                    throw new Exception("导入文件格式无效！");
                }

                return new MigrationDataPreview
                {
                    Version = importData.Version,
                    ExportTime = importData.ExportTime,
                    PersonInfoCount = importData.PersonInfoList?.Count ?? 0,
                    SalaryInfoCount = importData.SalaryInfoList?.Count ?? 0
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"预览数据失败：{ex.Message}", ex);
            }
        }
    }

    public class MigrationData
    {
        public string Version { get; set; } = "1.0";
        public DateTime ExportTime { get; set; }
        public List<PersonInfo> PersonInfoList { get; set; } = new List<PersonInfo>();
        public List<SalaryInfo> SalaryInfoList { get; set; } = new List<SalaryInfo>();
    }

    public class MigrationDataPreview
    {
        public string Version { get; set; } = string.Empty;
        public DateTime ExportTime { get; set; }
        public int PersonInfoCount { get; set; }
        public int SalaryInfoCount { get; set; }
    }
}
