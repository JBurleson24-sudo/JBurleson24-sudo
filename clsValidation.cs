using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BurlesonJLogonView
{
    internal static class clsValidation
    {
        public static bool ValidUsername(string username)
        {
            return Regex.IsMatch(
                username,
                @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z0-9]{8,20}$");
        }
        internal static bool ValidName(string v)
        {
            if (string.IsNullOrWhiteSpace(v))
            {
                return false;
            }

            return Regex.IsMatch(
                v.Trim(),
                @"^[A-Za-z][A-Za-z\s\.\-']*$");
        }
        public static bool ValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Contains(" "))
                return false;

            return Regex.IsMatch(
                password,
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[()!@#$%^&*])[A-Za-z\d()!@#$%^&*]{8,20}$");
        }

        public static bool ValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            if (email.Length < 6 || email.Length > 50)
            {
                return false;
            }

            return Regex.IsMatch(
                email,
                @"^(?!.*\.\.)[A-Za-z0-9](?:[A-Za-z0-9._%+-]{0,62}[A-Za-z0-9])?@(?:[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,}$");
        }

        public static bool ValidZip(string zip)
        {
            if (string.IsNullOrWhiteSpace(zip))
            {
                return false;
            }

            return Regex.IsMatch(zip, @"^\d{5}(-\d{4})?$");
        }

        public static bool ValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }

            return Regex.IsMatch(phone, @"^\d{10}$");
        }
        internal static bool ValidState(string text)
        {
            return !string.IsNullOrWhiteSpace(text) &&
                   States.Contains(text.ToUpper().Trim());
        }
        public static bool HasAddress(string address)
        {
            return !string.IsNullOrWhiteSpace(address);
        }

        public static bool HasAnswer(string answer)
        {
            return !string.IsNullOrWhiteSpace(answer);
        }

        private static readonly HashSet<string> States = new HashSet<string>
        {
            "AA","AE","AP",
            "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
            "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
            "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
            "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
            "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
        };
        public static bool ValidCreditCard(string cardNumber)
        {
            cardNumber = cardNumber.Replace("-", "").Replace(" ", "");

            if (cardNumber.Length != 16)
                return false;

            return cardNumber.All(char.IsDigit);
        }
        public static bool ValidCCV(string ccv)
        {
            if (ccv.Length != 3)
                return false;

            return ccv.All(char.IsDigit);
        }
        public static bool ValidExpirationDate(string expiration)
        {
            expiration = expiration.Trim();

            DateTime expDate;

            bool valid =
                DateTime.TryParseExact(
                    expiration,
                    new string[]
                    {
                "MM/yy",
                "MM/yyyy"
                    },
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out expDate);

            if (!valid)
                return false;

           
            expDate = expDate.AddMonths(1).AddDays(-1);

            if (expDate < DateTime.Today)
                return false;

            
            if (expDate > DateTime.Today.AddYears(5))
                return false;

            return true;
        }



    }
}
