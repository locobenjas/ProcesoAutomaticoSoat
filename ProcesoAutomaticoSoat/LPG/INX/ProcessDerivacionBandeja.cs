using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using ProcesoAutomaticoSoat.LPG.INX.Entity;

namespace GrandesCuentas.LPG.INX
{
    public class ProcessDerivacionBandeja
    {
        public DataTable GetDocumentoCoa(string ruc, string serie, string numerodoc)
        {
            DataTable tbl = new DataTable();
            using (OracleConnection cn = new OracleConnection(Settings.Conexion(DB.LPG_GF)))
            {
                try
                {
                    OracleCommand cmd = new OracleCommand("GESTFLUJO.SP_OBTENER_DOCUMENTOCOA_PA", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_RUC", OracleDbType.Varchar2).Value = ruc;
                    cmd.Parameters.Add("p_SERIE", OracleDbType.Varchar2).Value = serie;
                    cmd.Parameters.Add("p_NUMERODOC", OracleDbType.Varchar2).Value = numerodoc;
                    cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(tbl);
                    return tbl;
                }
                catch (Exception ex)
                {
                    throw new Exception("ProcessDerivacionBandeja.GetDocumentoCoa(ruc, serie, numerodoc)", ex);
                }
            }
        }

        public int RegistrarHistoricoBandeja(ESolicitudHistorialPA oSolicitud)
        {
            using (OracleConnection cn = new OracleConnection(Settings.Conexion(DB.LPG_GF)))
            {
                try
                {
                    OracleCommand cmd = new OracleCommand("PKGMANTENIMIENTO.SP_INS_SolicitudHistorial", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_IdSolicitud", OracleDbType.Int32).Value = oSolicitud.idSolicitud;
                    cmd.Parameters.Add("p_IdTipoFlujo", OracleDbType.Int32).Value = oSolicitud.idTipoFlujo;
                    cmd.Parameters.Add("p_IdUnidadOrganizativa", OracleDbType.Int32).Value = oSolicitud.idUnidadOrganizativa;
                    cmd.Parameters.Add("p_IdAccion", OracleDbType.Int32).Value = oSolicitud.idAccion;
                    cmd.Parameters.Add("p_IdRolOrigen", OracleDbType.Int32).Value = oSolicitud.idRolOrigen;
                    cmd.Parameters.Add("p_IdBandejaOrigen", OracleDbType.Int32).Value = oSolicitud.idBandejaOrigen;
                    cmd.Parameters.Add("p_UsuarioOrigen", OracleDbType.Varchar2).Value = oSolicitud.usuarioOrigen;
                    cmd.Parameters.Add("p_IdRolDestino", OracleDbType.Int32).Value = oSolicitud.idRolDestino;
                    cmd.Parameters.Add("p_IdBandejaDestino", OracleDbType.Int32).Value = oSolicitud.idBandejaDestino;
                    cmd.Parameters.Add("p_UsuarioDestino", OracleDbType.Varchar2).Value = oSolicitud.usuarioDestino;
                    cmd.Parameters.Add("p_IdEstadoDocumento", OracleDbType.Int32).Value = oSolicitud.idEstadoDocumento;
                    cmd.Parameters.Add("p_IdEstadoFlujo", OracleDbType.Int32).Value = oSolicitud.idEstadoFlujo;
                    cmd.Parameters.Add("p_Comentario", OracleDbType.Varchar2).Value = oSolicitud.comentario;
                    cmd.Parameters.Add("p_IdTipoComentario", OracleDbType.Int32).Value = oSolicitud.idTipoComentario;
                    cmd.Parameters.Add("p_FechaEfecto", OracleDbType.Date).Value = DateTime.Now;
                    cmd.Parameters.Add("p_UsuarioCreacion", OracleDbType.Varchar2).Value = oSolicitud.usuarioCreacion;
                    cmd.Parameters.Add("p_FechaCreacion", OracleDbType.Date).Value = DateTime.Now;
                    cmd.Parameters.Add("p_IdSolicitudHistorialAnt", OracleDbType.Int32).Value = oSolicitud.idSolicitudHistorialAnt;
                    cmd.Parameters.Add("p_IdSolicitudHistorial", OracleDbType.Int32, 1).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_numErrNumber", OracleDbType.Int32, 1).Direction = ParameterDirection.Output;
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    long filasAfec = cmd.RowSize;
                    cn.Close();
                    return i;
                }
                catch (Exception ex)
                {
                    cn.Close();
                    throw new Exception("ProcessDerivacionBandeja.RegistrarHistoricoBandeja(ESolicitudHistorialPA obj)", ex);
                }
            }
        }

        public ESolicitudHistorialPA ObtenerHistoricoBandeja(ESolicitudHistorialPA oItem)
        {
            DataTable tblRpta = new DataTable();
            ESolicitudHistorialPA itemResult = new ESolicitudHistorialPA();
            using (OracleConnection cn = new OracleConnection(Settings.Conexion(DB.LPG_GF)))
            {
                try
                {
                    OracleCommand cmd = new OracleCommand("PKGMANTENIMIENTO.SP_GET_SolicitudHistorial", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_IdSolicitudHistorial", OracleDbType.Int32).Value = oItem.idSolicitudHistorial;
                    cmd.Parameters.Add("p_IdSolicitud", OracleDbType.Int32).Value = oItem.idSolicitud;
                    cmd.Parameters.Add("p_IdTipoFlujo", OracleDbType.Int32).Value = oItem.idTipoFlujo;
                    cmd.Parameters.Add("p_curegRegistro", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(tblRpta);

                    if (tblRpta.Rows.Count > 0)
                    {
                        DataRow oR = tblRpta.AsEnumerable().First();

                        if (oR["IdSolicitudHistorial"] != DBNull.Value) itemResult.idSolicitudHistorial = Convert.ToInt32(oR["IdSolicitudHistorial"]);
                        if (oR["IdSolicitud"] != DBNull.Value) itemResult.idSolicitud = Convert.ToInt32(oR["IdSolicitud"]);
                        if (oR["IdTipoFlujo"] != DBNull.Value) itemResult.idTipoFlujo = Convert.ToInt32(oR["IdTipoFlujo"]);
                        if (oR["IdAccion"] != DBNull.Value) itemResult.idAccion = Convert.ToInt32(oR["IdAccion"]);
                        if (oR["IdBandejaOrigen"] != DBNull.Value) itemResult.idBandejaOrigen = Convert.ToInt32(oR["IdBandejaOrigen"]);
                        if (oR["IdBandejaDestino"] != DBNull.Value) itemResult.idBandejaDestino = Convert.ToInt32(oR["IdBandejaDestino"]);
                        if (oR["IdRolOrigen"] != DBNull.Value) itemResult.idRolOrigen = Convert.ToInt32(oR["IdRolOrigen"]);
                        if (oR["IdRolDestino"] != DBNull.Value) itemResult.idRolDestino = Convert.ToInt32(oR["IdRolDestino"]);
                        if (oR["UsuarioOrigen"] != DBNull.Value) itemResult.usuarioOrigen = Convert.ToString(oR["UsuarioOrigen"]);
                        if (oR["UsuarioDestino"] != DBNull.Value) itemResult.usuarioDestino = Convert.ToString(oR["UsuarioDestino"]);
                        if (oR["IdEstadoDocumento"] != DBNull.Value) itemResult.idEstadoDocumento = Convert.ToInt32(oR["IdEstadoDocumento"]);
                        if (oR["IdEstadoFlujo"] != DBNull.Value) itemResult.idEstadoFlujo = Convert.ToInt32(oR["IdEstadoFlujo"]);
                        if (oR["FechaEfecto"] != DBNull.Value) itemResult.fechaEfecto = Convert.ToDateTime(oR["FechaEfecto"]);
                        if (oR["FechaAnulacion"] != DBNull.Value) itemResult.fechaAnulacion = Convert.ToDateTime(oR["FechaAnulacion"]);
                        if (oR["Comentario"] != DBNull.Value) itemResult.comentario = Convert.ToString(oR["Comentario"]);
                        if (oR["IdTipoComentario"] != DBNull.Value) itemResult.idTipoComentario = Convert.ToInt32(oR["IdTipoComentario"]);
                        if (oR["DescTipoComentario"] != DBNull.Value) itemResult.descTipoComentario = Convert.ToString(oR["DescTipoComentario"]);
                        if (oR["UsuarioCreacion"] != DBNull.Value) itemResult.usuarioCreacion = Convert.ToString(oR["UsuarioCreacion"]);
                        if (oR["FechaCreacion"] != DBNull.Value) itemResult.fechaCreacion = Convert.ToDateTime(oR["FechaCreacion"]);
                        if (oR["IdSolicitudHistorialAnt"] != DBNull.Value) itemResult.idSolicitudHistorialAnt = Convert.ToInt32(oR["IdSolicitudHistorialAnt"]);
                    }
                    return itemResult;
                }
                catch (Exception ex)
                {
                    throw new Exception("ProcessDerivacionBandeja.ObtenerHistoricoBandeja(ESolicitudHistorialPA obj)", ex);
                }
            }
        }

        public int EliminarHistoricoBandeja(ESolicitudHistorialPA oItem)
        {
            using (OracleConnection cn = new OracleConnection(Settings.Conexion(DB.LPG_GF)))
            {
                try
                {
                    OracleCommand cmd = new OracleCommand("PKGMANTENIMIENTO.SP_DEL_SolicitudHistorial", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_IdSolicitud", OracleDbType.Int32).Value = oItem.idSolicitud;
                    cmd.Parameters.Add("p_IdTipoFlujo", OracleDbType.Int32).Value = oItem.idTipoFlujo;
                    cmd.Parameters.Add("p_IdUnidadOrganizativa", OracleDbType.Int32).Value = oItem.idUnidadOrganizativa;
                    cmd.Parameters.Add("p_FechaAnulacion", OracleDbType.Date).Value = DateTime.Now;
                    cmd.Parameters.Add("p_numErrNumber", OracleDbType.Int32, 1).Direction = ParameterDirection.Output;

                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cn.Close();
                    return i;
                }
                catch (Exception ex)
                {
                    cn.Close();
                    throw new Exception("ProcessDerivacionBandeja.EliminarHistoricoBandeja(ESolicitudHistorialPA obj)", ex);
                } 
            }
        }

        public string[] ObtenerUsuarioBandeja(string oficina) //string[usuario, bandeja]
        {
            DataTable tbl = new DataTable();
            string[] rpta = new string[2];
            using (OracleConnection cn = new OracleConnection(Settings.Conexion(DB.LPG_TD)))
            {
                try
                {
                    OracleCommand cmd = new OracleCommand("TEDEF.SP_OBTENER_OFIUSUARIO_PA", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_SOFICINA", OracleDbType.Varchar2).Value = oficina;
                    cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(tbl);
                    if (tbl.Rows.Count > 0)
                    {
                        DataRow oR = tbl.AsEnumerable().First();

                        if (oR["SUSUARIO"] != DBNull.Value) rpta[0] = Convert.ToString(oR["SUSUARIO"]);
                        if (oR["NBANDEJA"] != DBNull.Value) rpta[1] = Convert.ToString(oR["NBANDEJA"]);
                    }
                    return rpta;
                }
                catch (Exception ex)
                {
                    throw new Exception("ProcessDerivacionBandeja.ObtenerUsuarioBandeja(oficina)", ex);
                }
            }
        }
    }
}
