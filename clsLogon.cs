using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurlesonJLogonView
{
    internal static class clsLogon
    {
        public static bool ValidateLogin(
            string username,
            string password,
            out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrWhiteSpace(username))
            {
                errorMessage =
                    "Please enter your username.";

                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                errorMessage =
                    "Please enter your password.";

                return false;
            }

            if (username.Length > 20)
            {
                errorMessage =
                    "Username cannot exceed 20 characters.";

                return false;
            }

            if (password.Length > 20)
            {
                errorMessage =
                    "Password cannot exceed 20 characters.";

                return false;
            }

            return true;
        }
    }
}