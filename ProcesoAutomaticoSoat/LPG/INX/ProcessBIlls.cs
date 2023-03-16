

namespace GrandesCuentas.LPG.INX
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Data.OleDb;
    using Oracle.ManagedDataAccess.Client;
    public class ProcessBIlls
    {
        public List<Factura> SelectBills(string date)
        {
            List<Factura> list = new List<Factura>();
            Factura oFactura;
            using (OleDbConnection cn = new OleDbConnection(Settings.Conexion(DB.LPG_INX)))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = string.Format("execute procedure sp_rea_proc_aut_4('{0}')",date);  //execute procedure sp_rea_proc_aut_4('2023-03-02')
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.Text;

                    cn.Open();

                    OleDbDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        try
                        {
                            while (dr.Read())
                            {
                                oFactura = new Factura();
                                oFactura.nroLote = (!dr.IsDBNull(0)) ? dr.GetString(0) : "";
                                oFactura.ruc = (!dr.IsDBNull(1)) ? dr.GetString(1) : "";
                                oFactura.serie = (!dr.IsDBNull(2)) ? dr.GetString(2) : "";
                                oFactura.numero = (!dr.IsDBNull(3)) ? dr.GetString(3) : "";
                                oFactura.fechaEmision = (!dr.IsDBNull(4)) ? dr.GetDateTime(4) : (DateTime?)null;
                                oFactura.monto = (!dr.IsDBNull(5)) ? dr.GetDecimal(5) : (decimal?)null;
                                oFactura.moneda = (!dr.IsDBNull(6)) ? dr.GetInt16(6) : (short?)null;
                                oFactura.diagnostico = (!dr.IsDBNull(7)) ? dr.GetString(7) : "";
                                oFactura.siniestro = (!dr.IsDBNull(8)) ? dr.GetInt32(8) : (int?)null;
                                oFactura.cartaGarantia = (!dr.IsDBNull(9)) ? dr.GetInt32(9) : (int?)null;
                                oFactura.filtroPA = (!dr.IsDBNull(10)) ? dr.GetString(10) : "";
                                list.Add(oFactura);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.Read();
                        }
                    }
                };
            }

            return list;
        }
        public string GetFiltersLevel2(int type)
        {
            string rpta = "";
            using (OracleConnection cn = new OracleConnection(Settings.Conexion(DB.LPG_VT)))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = "TEDEF.SP_GET_RULES_VALIDATION";
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_FILTERTYPE", OracleDbType.Int32).Value = type;
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    cn.Open();

                    OracleDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            rpta += (!dr.IsDBNull(0)) ? dr.GetString(0) : "";
                        }
                    }


                };
            }
            return rpta;
        }
        public List<Factura> ValidateBills(string rules)
        {
            List<Factura> list = new List<Factura>();
            Factura oFactura;
            using (OleDbConnection cn = new OleDbConnection(Settings.Conexion(DB.LPG_INX)))
            {
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = string.Format("execute procedure sp_rea_proc_aut_5('{0}')", rules); 
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.Text;

                    cn.Open();

                    OleDbDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        try
                        {
                            while (dr.Read())
                            {
                                oFactura = new Factura();
                                oFactura.nroLote = (!dr.IsDBNull(0)) ? dr.GetString(0) : "";
                                oFactura.ruc = (!dr.IsDBNull(1)) ? dr.GetString(1) : "";
                                oFactura.serie = (!dr.IsDBNull(2)) ? dr.GetString(2) : "";
                                oFactura.numero = (!dr.IsDBNull(3)) ? dr.GetString(3) : "";
                                oFactura.estado = (!dr.IsDBNull(4)) ? dr.GetInt16(4) : (short?)null;
                                list.Add(oFactura);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.Read();
                        }
                    }
                };
            }

            return list;
        }
        public List<ConfiguracionInicial> GetParamsLiquidacion()
        {
            ConfiguracionInicial oConfiguracionInicial = null;
            List<ConfiguracionInicial> lstConfigInicial = new List<ConfiguracionInicial>();
            using (OracleConnection cn = new OracleConnection(Settings.Conexion(DB.LPG_VT)))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = "TEDEF.SP_TEDEF_GET_CONFIG_LIQUI_PA";
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("RC1", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    cn.Open();

                    OracleDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            oConfiguracionInicial = new ConfiguracionInicial();
                            if (!dr.IsDBNull(dr.GetOrdinal("PARAMETRO"))) oConfiguracionInicial.SPARAM_CONFIG = Convert.ToString(dr["PARAMETRO"]);
                            if (!dr.IsDBNull(dr.GetOrdinal("CONDITION"))) oConfiguracionInicial.SCONDITION_PARAM = Convert.ToString(dr["CONDITION"]);
                            lstConfigInicial.Add(oConfiguracionInicial);
                        }
                    }


                };
            }
            return lstConfigInicial;
        }

        public string LiquidarFacturas(string xml)
        {
            string rpta = "";
            using (OracleConnection cn = new OracleConnection(Settings.Conexion(DB.LPG_VT)))
            {
                cn.Open();
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = "TEDEF.SP_TEDEF_LOAD_BILLS";
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter clobParam = new OracleParameter("PXML_FACTURAS", OracleDbType.Clob);
                    clobParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(clobParam).Value = xml;
                    cmd.ExecuteNonQuery();
                    rpta = "OK";
                };
            }
            return rpta;
        }
    }
}
