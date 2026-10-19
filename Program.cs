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
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            clsDatabase.InitializeDatabase();
            clsDatabase.SeedDatabase();
           
            clsDatabase.ImportInventory();
            clsDatabase.ImportInventoryImages();
           
            Application.Run(new frmCustomer());
        }
    }
}
