using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace GrandesCuentas.LPG.VT
{
    public class p1_Process
    {
        public void Target()
        {
            using (OracleConnection cn = new OracleConnection(Settings.Conexion(DB.LPG_VT)))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = "INSUDB.REA_CTAS_GRANDES.CTAS_GRANDES";
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    cmd.ExecuteNonQuery();
                };
            }
        }
    }
}