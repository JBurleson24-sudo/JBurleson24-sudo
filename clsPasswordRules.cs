using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows.Forms;

namespace BurlesonJLogonView
{
    internal class clsPasswordRules
    {
        internal static void AllowedKeys(KeyPressEventArgs e, ToolStripStatusLabel tssLabel)
        {
            string strAllowedKeys = "!@#$%^&*()";

            if (char.IsLetterOrDigit(e.KeyChar) || char.IsControl(e.KeyChar) ||
                    strAllowedKeys.Contains(e.KeyChar.ToString()))
            {
                e.Handled = false;
                clsUpdateControls.UpdateStatusBar(e, tssLabel, true);
            }
            else
            {
                e.Handled = true;
                clsUpdateControls.UpdateStatusBar(e, tssLabel, false);
            }
        }

        internal static bool HasComplexity(string strPassword)
        {
            if (string.IsNullOrWhiteSpace(strPassword))
                return false;

           
            if (strPassword.Contains(" "))
                return false;

            string strAllowedKeys = "!@#$%^&*()";

            if (strPassword.Length < 8 || strPassword.Length > 20)
                return false;

            if (!strPassword.Any(char.IsUpper))
                return false;

            if (!strPassword.Any(char.IsLower))
                return false;

            if (!strPassword.Any(char.IsDigit))
                return false;

            // must contain at least one valid special char
            if (!strPassword.Any(ch => strAllowedKeys.Contains(ch)))
                return false;

            // ❌ BLOCK ANY INVALID CHARACTERS
            if (!strPassword.All(ch =>
                char.IsLetterOrDigit(ch) || strAllowedKeys.Contains(ch)))
                return false;

            return true;
        }
    }
}