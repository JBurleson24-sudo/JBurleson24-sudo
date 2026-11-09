using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurlesonJLogonView
{
    public static class clsCurrentUser
    {
        public static bool IsLoggedIn { get; set; } = false;

        public static int PersonID { get; set; }

        public static string UserName { get; set; } = "";

        public static void Logout()
        {
            IsLoggedIn = false;
            PersonID = 0;
            UserName = "";
        }
    }
}