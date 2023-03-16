using System.Data;
//using IBM.Data.Informix;
using System.Data.OleDb;

namespace GrandesCuentas.LPV.INX
{
    public class p1_Process
    {
        public void Target()
        {
            using (OleDbConnection cn = new OleDbConnection(Settings.Conexion(DB.LPV_INX)))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = "execute procedure CTAS_GRANDES()";
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.Text;

                    cn.Open();

                    cmd.ExecuteNonQuery();
                };
            }
        }
    }
}