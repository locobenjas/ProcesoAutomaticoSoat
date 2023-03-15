using System.Collections.Generic;
using System.Data;
using GrandesCuentas.AFF;
using Oracle.ManagedDataAccess.Client;

namespace GrandesCuentas.LPV.VT
{
    public class p2_Extraction
    {
        public List<Acoount> Select(int number)
        {
            List<Acoount> list = new List<Acoount>();

            using (OracleConnection cn = new OracleConnection(Settings.Conexion(DB.LPV_VT)))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = "LPVINSUDB.REA_CTAS_GRANDES.CTAS_GRANDES_DATA";
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("NTIPO", OracleDbType.Int32).Value = number;
                    cmd.Parameters.Add("cursor_cuentas", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    cn.Open();

                    OracleDataReader accounts = cmd.ExecuteReader();

                    if (accounts.HasRows)
                    {
                        while (accounts.Read())
                        {
                            list.Add(new Translate().BetweenReaderAndAcoount(accounts));
                        }
                    }
                };
            }

            return list;
        }
    }
}