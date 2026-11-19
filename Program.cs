using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace BurlesonJLogonView
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            string step = "Starting Windows Forms";

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                step = "Initializing the database";
                Stopwatch timer = Stopwatch.StartNew();
                clsDatabase.InitializeDatabase();
                Trace.WriteLine($"InitializeDatabase: {timer.Elapsed.TotalMilliseconds:F2} ms");

                step = "Seeding the database";
                timer.Restart();
                clsDatabase.SeedDatabase();
                Trace.WriteLine($"SeedDatabase: {timer.Elapsed.TotalMilliseconds:F2} ms");

                step = "Importing inventory";
                timer.Restart();
                clsDatabase.ImportInventory();
                Trace.WriteLine($"ImportInventory: {timer.Elapsed.TotalMilliseconds:F2} ms");

                step = "Importing inventory images";
                timer.Restart();
                clsDatabase.ImportInventoryImages();
                timer.Stop();
                Trace.WriteLine($"ImportInventoryImages: {timer.Elapsed.TotalMilliseconds:F2} ms");
                step = "Opening the customer window";
                Application.Run(new frmCustomer());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    step + " failed:\n\n" +
                    ex.GetType().Name + ": " + ex.Message,
                    "Video Game Store startup error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
