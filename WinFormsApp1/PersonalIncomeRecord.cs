using System;

namespace WinFormsApp1
{
    public class PersonalIncomeRecord
    {
        public int Id { get; set; }
        
        public string PersonName { get; set; }
        
        public decimal Amount { get; set; }
        
        public string RecordType { get; set; }
        
        public string Remark { get; set; }
        
        public DateTime RecordTime { get; set; }
        
        public DateTime CreateTime { get; set; }

        public PersonalIncomeRecord()
        {
            PersonName = string.Empty;
            RecordType = "收入";
            Remark = string.Empty;
            RecordTime = DateTime.Now;
            CreateTime = DateTime.Now;
        }
    }
}
