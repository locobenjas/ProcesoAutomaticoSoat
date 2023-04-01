using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace EnvioNotificacionPASoat.TEDEF
{
    public class NotificacionProcess
    {
        public string EnviarNotificacion() {
            string salida = string.Empty;
            List<Notificacion> listNotificaciones = ListarNotificationes();
            DateTime hoy = DateTime.Now;
            DayOfWeek dia = hoy.DayOfWeek;
            string horaminuto = hoy.ToString("HH:mm");

            foreach (var n in listNotificaciones) {
                Dictionary<string, int> dicSemana = new Dictionary<string, int>();
                dicSemana.Add("Monday", 1);
                dicSemana.Add("Tuesday", 2);
                dicSemana.Add("Wednesday", 3);
                dicSemana.Add("Thursday", 4);
                dicSemana.Add("Friday", 5);

                int numeroDia = dicSemana[dia.ToString()];

                string[] dias = n.Frecuencia.Split(new string[] { "|" }, StringSplitOptions.None);
                if (HoraActualEsMayor(n.HoraEjecucion, horaminuto) && dias.Contains(numeroDia.ToString()))
                {
                    salida += "Coincide Día, Hora y Minuto \n";
                    bool enviar = EnviarEmail(n.Emails.Split(new string[] { "|" }, StringSplitOptions.None));
                }
                else {
                    salida += "No Coincide \n";
                }
            }

            return salida;
        }

        public bool HoraActualEsMayor(string horaMinutoNotificacion, string horaMinutoActual) {
            string horaNotificacion = horaMinutoNotificacion.Split(new string[] { ":" }, StringSplitOptions.None)[0];
            string minutoNotificacion = horaMinutoNotificacion.Split(new string[] { ":" }, StringSplitOptions.None)[1];
            string horaActual = horaMinutoActual.Split(new string[] { ":" }, StringSplitOptions.None)[0];
            string minutoActual = horaMinutoActual.Split(new string[] { ":" }, StringSplitOptions.None)[1];
            if (int.Parse(horaActual) == int.Parse(horaNotificacion))
            {
                if (int.Parse(minutoActual) >= int.Parse(minutoNotificacion)) {
                    return true;
                }
            }
            return false;
        }

        public bool EnviarEmail(string[] destinatarios) {
            bool enviado = false;
            foreach (string email in destinatarios) { 
                
            }
            return enviado;
        }

        public List<Notificacion> ListarNotificationes()
        {
            List<Notificacion> rpta = new List<Notificacion>();
            using (OracleConnection cn = new OracleConnection(Settings.Conexion(DB.LPG_TD)))
            {
                try
                {
                    DataTable tbl = new DataTable();
                    OracleCommand cmd = new OracleCommand("TEDEF.SP_LISTAR_NOTIFICATIONS_ADDRES", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(tbl);
                    if (tbl.Rows.Count > 0)
                    {
                        DataRow oR = tbl.AsEnumerable().First();
                        rpta.Add(new Notificacion()
                        {
                            IdNotificacion = Convert.ToInt32(oR["NIDNOTIFICACION"].ToString()),
                            Nombre = oR["SNOTIFICACION"].ToString(),
                            Asunto = oR["SASUNTO"].ToString(),
                            CodFiltro = oR["SIDFILTER_PA"].ToString(),
                            NombreFiltro = oR["SFILTER_PA"].ToString(),
                            Frecuencia = oR["SFRECUENCY"].ToString(),
                            HoraEjecucion = oR["SHOUR_EJECUTION"].ToString(),
                            InicioProceso = Convert.ToDateTime(oR["DINIT_PROCESS"].ToString()),
                            FinProceso = Convert.ToDateTime(oR["DEND_PROCESS"].ToString()),
                            IdEstado = Convert.ToInt32(oR["NIDRULE_STATE"].ToString()),
                            Emails = oR["SEMAILS"].ToString()
                        });
                    }
                    return rpta;
                }
                catch (Exception ex)
                {
                    throw new Exception("NotificacionProcess.ListarNotificationes(oficina)", ex);
                }
            }

        }
    }
}
