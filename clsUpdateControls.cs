using System;
using System.Windows.Forms;

namespace BurlesonJLogonView
{
    internal class clsUpdateControls
    {
        internal static void UpdateStatusBar(KeyPressEventArgs e, ToolStripStatusLabel tssLabel, bool isValid)
        {
            if (isValid)
            {
                tssLabel.Text = "Valid key.";
                tssLabel.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                tssLabel.Text = "Invalid character.";
                tssLabel.ForeColor = System.Drawing.Color.Red;
            }
        }

        internal static void UpdateStatusBar(ToolStripStatusLabel tssLabel)
        {
            tssLabel.Text = "Password meets complexity requirements.";
            tssLabel.ForeColor = System.Drawing.Color.Green;
        }
    }
}