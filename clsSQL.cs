using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurlesonJLogonView
{
    internal static class clsSQL
    {
        public static DataTable ExecuteSelect(
            SqlConnection conn,
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlCommand cmd =
                new SqlCommand(sql, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(
                        parameters);
                }

                DataTable dt =
                    new DataTable();

                using (SqlDataAdapter da =
                    new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                return dt;
            }
        }

        public static int ExecuteNonQuery(
            SqlConnection conn,
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlCommand cmd =
                new SqlCommand(sql, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(
                        parameters);
                }

                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(
            SqlConnection conn,
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlCommand cmd =
                new SqlCommand(sql, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(
                        parameters);
                }

                return cmd.ExecuteScalar();
            }
        }
    }
}
