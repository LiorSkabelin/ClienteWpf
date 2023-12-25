using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClienteWpf
{

    public class ValidBirthYear : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int year = int.Parse(value.ToString());
                if (year < 1950)
                {
                    return new ValidationResult(false, "Too old");
                }
                if (year > DateTime.Today.Year)
                {
                    return new ValidationResult(false, "No way!");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }

            return ValidationResult.ValidResult;
        }

    }

    public class ValidName : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string fisrtname = value.ToString();
                if (fisrtname.Length < Min)
                {
                    return new ValidationResult(false, "name to short");
                }
                if (fisrtname.Length > Max)
                {
                    return new ValidationResult(false, "name to longt");
                }
                if (!Char.IsLetter(fisrtname[0]))
                {
                    return new ValidationResult(false, "name must withe letter");
                }
                if (fisrtname.IndexOf(" ") != -1)
                {
                    return new ValidationResult(false, "name must not include more them one space");
                }
                for (int i = 0; i < fisrtname.Length; i++)
                {
                    if (!(Char.IsLetter(fisrtname[i]) || Char.IsWhiteSpace(fisrtname[i])))
                    {
                        if (i > 1 && (fisrtname[i]).Equals(" ") && fisrtname[i - 1].Equals(" "))
                        {
                            return new ValidationResult(false, "name must not include more them one space at time");
                        }
                        return new ValidationResult(false, "Onaly letters or spasce");
                    }
                }
            }
            catch (Exception ex)
            {

                return new ValidationResult(false, "name now valid" + ex.Message);
            }
            return ValidationResult.ValidResult;

        }
    }

    public class Validemail : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string email = value as string;
                Regex emailRegex = new Regex("([A-Za-z0-9]+[._-])*[A-Za-z0-9]+@[A-Za-z0-9-]+(\\.[A-Z|a-z]{2,})+");
                Match match = emailRegex.Match(email);

                if (!match.Success)
                {
                    return new ValidationResult(false, "not in the right format");
                }

            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidPassword : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool big, small, digit, sym;
            big = small = digit = sym = false;
            string password = value.ToString();
            string simbol = "#$^&*_!@";
            try
            {
                if (password.Length < 6)
                {
                    return new ValidationResult(false, "Passworsd to short");
                }
                if (password.Length > 16)
                {
                    return new ValidationResult(false, "Passworsd to long");
                }
                for (int i = 0; i < password.Length; i++)
                {
                    if (!Char.IsLetterOrDigit(password[i]) && simbol.IndexOf(password[i]) == -1)
                    {
                        return new ValidationResult(false, "password can contain letters, digits and " + simbol);
                    }
                    if (Char.IsUpper(password[i])) big = true;
                    if (Char.IsLower(password[i])) small = true;
                    if (Char.IsDigit(password[i])) digit = true;
                    if (simbol.IndexOf(password[i]) != -1) sym = true;

                }
                if (!big && small && digit && sym)
                {
                    throw new Exception("A password must contain at last an uppercase letter a lowercase a number and special character");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "useremail IILEGAL" + ex);
            }
            return ValidationResult.ValidResult;

        }
    }
}
