using System.Collections.Generic;
using System.Data;
using GrandesCuentas.AFF;
//using IBM.Data.Informix;
using System.Data.OleDb;
using System;

namespace GrandesCuentas.LPV.INX
{
    public class p3_Extraction
    {
        public List<Acoount> Select(int number)
        {
            List<Acoount> list = new List<Acoount>();

            using (OleDbConnection cn = new OleDbConnection(Settings.Conexion(DB.LPV_INX)))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = string.Format("execute procedure ctas_grandes_data({0})", number);  //"execute procedure ctas_grandes_data()";
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.Text;

                    cn.Open();

                    OleDbDataReader accounts = cmd.ExecuteReader();

                    if (accounts.HasRows)
                    {
                        try
                        {
                            while (accounts.Read())
                            {
                                list.Add(new Translate().BetweenReaderAndAcoount(accounts));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message + " -> " + accounts["SCOMPANY"].ToString().Trim() +
                                                           " -> " + accounts["NPOLICY"].ToString().Trim() +
                                                           " -> " + accounts["NCERTIF"].ToString().Trim());
                            Console.Read();
                        }
                    }
                };
            }

            return list;
        }
    }
}