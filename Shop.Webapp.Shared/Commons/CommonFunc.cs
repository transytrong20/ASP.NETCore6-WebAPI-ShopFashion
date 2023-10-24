using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text.RegularExpressions;
using System.Text;

namespace Shop.Webapp.Shared.Commons
{
    public static class CommonFunc
    {
        private static readonly Random random = new Random();
        private static string UsernameRegex = @"^[a-z][a-z0-9]{0,30}$";
        private static string CodeRegex = @"^[a-z0-9\-]{8,100}$";
        private static string PhoneRegex = @"(0[3|5|7|8|9])+([0-9]{8})";

        public static int GetRandomNumber(int min = 1111111, int max = 99999999)
        {
            return random.Next(min, max);
        }

        public static string ToCode(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            value = value.Replace("đ", "d").Trim().TrimStart().TrimEnd();

            string normalized = value.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalized)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            Encoding nonunicode = Encoding.GetEncoding(20127);
            Encoding unicode = Encoding.Unicode;

            byte[] nonunicodeBytes = Encoding.Convert(unicode, nonunicode, unicode.GetBytes(sb.ToString()));
            char[] nonunicodeChars = new char[nonunicode.GetCharCount(nonunicodeBytes, 0, nonunicodeBytes.Length)];
            nonunicode.GetChars(nonunicodeBytes, 0, nonunicodeBytes.Length, nonunicodeChars, 0);

            var result = new string(nonunicodeChars).Replace(" ", "-").Replace("--", "-");

            result = Regex.Replace(result, @"\(+.*?\)", "");

            return result + GetRandomNumber();
        }

        public static bool ValidateCode(this string code)
        {
            if (string.IsNullOrEmpty(code))
                return false;

            if (code.Contains(' '))
                return false;

            return Regex.IsMatch(code, CodeRegex);
        }

        public static bool ValidateUsername(this string username)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            if (username.Contains(' '))
                return false;

            if (username != username.ToLower())
                return false;

            return Regex.IsMatch(username, UsernameRegex) || ValidateEmail(username) || ValidatePhoneNumber(username);
        }

        public static bool ValidateEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            var trimmedEmail = email.Trim();
            if (email.EndsWith(".")) { return false; }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidatePhoneNumber(this string phonenumber)
        {
            if (string.IsNullOrEmpty(phonenumber))
                return false;
            if (phonenumber.Length > 10)
                return false;

            return Regex.IsMatch(phonenumber, PhoneRegex);
        }

        public static string ValidatePassword(this string password)
        {
            if (string.IsNullOrEmpty(password))
                return "Mật khẩu không được để trống";
            if (password.Length < 6)
            {
                return "Mật khẩu phải chứa ít nhất 6 ký tự!";
            }
            if (!(password.Any(char.IsNumber) // chứa số
                && password.Any(char.IsLower) // chứa chữ thường
                && password.Any(char.IsUpper) // chứa chữ hoa
                && Regex.Replace(password, "[^a-zA-Z0-9]", String.Empty) != password)) // chứa ký tự đặc biệt
            {
                return "Mật khẩu phải chứa ít nhất 1 chữ số, 1 chữ in hoa, 1 chữ thường và 1 ký tự đặc biệt!";
            }
            return null;
        }

        public static string GetPasswordHash(this string password, string hashCode)
        {
            var slt = Encoding.UTF8.GetBytes(hashCode);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: slt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
