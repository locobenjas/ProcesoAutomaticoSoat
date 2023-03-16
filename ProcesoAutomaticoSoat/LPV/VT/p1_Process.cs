using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace GrandesCuentas.LPV.VT
{
    public class p1_Process
    {
        public void Target()
        {
            using (OracleConnection cn = new OracleConnection(Settings.Conexion(DB.LPV_VT)))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = "LPVINSUDB.REA_CTAS_GRANDES.CTAS_GRANDES";
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    cmd.ExecuteNonQuery();
                };
            }
        }
    }
}