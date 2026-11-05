using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BurlesonJLogonView
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string step = "Starting Windows Forms";

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                step = "Initializing the database";
                clsDatabase.InitializeDatabase();

                step = "Seeding the database";
                clsDatabase.SeedDatabase();

                step = "Importing inventory";
                clsDatabase.ImportInventory();

                step = "Importing inventory images";
                clsDatabase.ImportInventoryImages();

                step = "Opening the customer window";
                Application.Run(new frmCustomer());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    step + " failed:\n\n" + ex.GetType().Name + ": " + ex.Message,
                    "Video Game Store startup error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
