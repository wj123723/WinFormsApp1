using System;
using System.Text.RegularExpressions;

namespace WinFormsApp1
{
    public static class ValidationHelper
    {
        public static bool ValidateIdCardNumber(string idCardNumber)
        {
            if (string.IsNullOrWhiteSpace(idCardNumber))
                return false;

            // 简化的18位身份证号码验证
            if (idCardNumber.Length != 18)
                return false;

            // 检查前17位是否为数字
            for (int i = 0; i < 17; i++)
            {
                if (!char.IsDigit(idCardNumber[i]))
                    return false;
            }

            // 检查第18位是否为数字或X/x
            char lastChar = idCardNumber[17];
            if (!char.IsDigit(lastChar) && lastChar != 'X' && lastChar != 'x')
                return false;

            return true;
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // 简化的手机号码验证
            if (phoneNumber.Length != 11)
                return false;

            // 检查是否以1开头且全部为数字
            if (phoneNumber[0] != '1')
                return false;

            for (int i = 1; i < phoneNumber.Length; i++)
            {
                if (!char.IsDigit(phoneNumber[i]))
                    return false;
            }

            return true;
        }

        public static bool ValidateBankCardNumber(string bankCardNumber)
        {
            if (string.IsNullOrWhiteSpace(bankCardNumber))
                return false;

            // 移除空格
            bankCardNumber = bankCardNumber.Replace(" ", "");

            // 检查长度和是否全部为数字
            if (bankCardNumber.Length < 13 || bankCardNumber.Length > 19)
                return false;

            foreach (char c in bankCardNumber)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        public static bool ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            // 检查姓名长度
            if (name.Length < 2 || name.Length > 50)
                return false;

            return true;
        }
    }
}