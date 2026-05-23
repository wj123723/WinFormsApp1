using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WinFormsApp1
{
    /// <summary>
    /// 人物信息类，用于存储个人基本信息
    /// </summary>
    public class PersonInfo
    {
        [JsonIgnore]
        public bool IsSelected { get; set; } = false;

        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [Required(ErrorMessage = "身份证号码不能为空")]
        public string IdCardNumber { get; set; }

        /// <summary>
        /// 银行卡号码
        /// </summary>
        [Required(ErrorMessage = "银行卡号码不能为空")]
        public string BankCardNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别不能为空")]
        public string Gender { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        [Required(ErrorMessage = "银行名称不能为空")]
        public string BankName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Required(ErrorMessage = "电话号码不能为空")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 重写ToString方法，返回人物信息的字符串表示
        /// </summary>
        /// <returns>人物信息字符串</returns>
        public override string ToString()
        {
            return $"姓名: {Name}, 身份证: {IdCardNumber}, 银行卡: {BankCardNumber}, 电话: {PhoneNumber}, 性别: {Gender}, 银行名称: {BankName}";
        }
    }
}