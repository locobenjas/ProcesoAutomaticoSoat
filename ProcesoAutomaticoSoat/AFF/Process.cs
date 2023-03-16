using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace GrandesCuentas.AFF
{
    public class Process
    {
        public int Status()
        {
            int status = 0;

            string query = string.Format("SELECT Estado FROM GREAT_PROC_CAB WHERE FECHAPROCESO BETWEEN '{0}' AND '{1}'", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.AddDays(1).ToString("yyyyMMdd"));

            using (SqlConnection cn = new SqlConnection(Settings.Conexion(DB.AFF_SQL)))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;

                    cn.Open();

                    SqlDataReader process = cmd.ExecuteReader();

                    if (process.HasRows)
                    {
                        while (process.Read())
                        {
                            status = Convert.ToInt32(process["Estado"]);
                        }
                    }
                };
            }

            return status;
        }

        public void Insert_CAB()
        {
            string query = "INSERT INTO GREAT_PROC_CAB VALUES(GETDATE(), 1)";

            using (SqlConnection cn = new SqlConnection(Settings.Conexion(DB.AFF_SQL)))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;

                    cn.Open();

                    cmd.ExecuteNonQuery();
                };
            }
        }

        public void Update_CAB()
        {
            string query = string.Format("UPDATE GREAT_PROC_CAB SET Estado = 2 WHERE (FechaProceso BETWEEN '{0}' AND '{1}') AND Estado = 1", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.AddDays(1).ToString("yyyyMMdd"));

            using (SqlConnection cn = new SqlConnection(Settings.Conexion(DB.AFF_SQL)))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;

                    cn.Open();

                    cmd.ExecuteNonQuery();
                };
            }
        }
    }
}