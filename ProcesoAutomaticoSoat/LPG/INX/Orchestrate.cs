using System.Collections.Generic;
using GrandesCuentas.AFF;
using System;
using System.Text;
using System.Reflection;
using System.Linq;
using ProcesoAutomaticoSoat.LPG.INX.Entity;
using System.Data;

namespace GrandesCuentas.LPG.INX
{
    public class Orchestrate : IPrincipal
    {
        public void Transfer()
        {

            ProcessBIlls p1 = new ProcessBIlls();
            string rules = p1.GetFiltersLevel2(2);
            //var bills = p1.ValidateBills(rules);
            List<Factura> mockFacturas = new List<Factura>()
            {
                new Factura(){idLote = 2971,nroLote="0022641", iafas="40005", ruc="20100207941", CodIpress="00013723",serie ="0F206", numero="00112681", userCode="I0006750", motivo="Error siniestro", estado=0},
                new Factura(){idLote = 2971,nroLote="0022641", iafas="40005", ruc="20100207941", CodIpress="00013723",serie ="0F207", numero="00112682", userCode="I0006750", motivo="Error ruc", estado=0}, 
                new Factura(){idLote = 2971,nroLote="0022641", iafas="40005", ruc="20100207941", CodIpress="00013723",serie ="0F208", numero="00112683", userCode="I0006750", motivo="",movimiento=2 ,estado=1}, 
                new Factura(){idLote = 2971,nroLote="0022641", iafas="40005", ruc="20100207941", CodIpress="00013723",serie ="0F209", numero="00112683", userCode="I0006750", motivo="",movimiento=2 ,estado=1}, 
                new Factura(){idLote = 2971,nroLote="0022641", iafas="40005", ruc="20100207941", CodIpress="00013723",serie ="0F210", numero="00112683", userCode="I0006750", motivo="",movimiento=2 ,estado=1}, 
                new Factura(){idLote = 2971,nroLote="0022641", iafas="40005", ruc="20100207941", CodIpress="00013723",serie ="0F211", numero="00112683", userCode="I0006750", motivo="",movimiento=2 ,estado=1}, 
                new Factura(){idLote = 2971,nroLote="0022641", iafas="40005", ruc="20100207941", CodIpress="00013723",serie ="0F212", numero="00112683", userCode="I0006750", motivo="",movimiento=2 ,estado=1}, 
                new Factura(){idLote = 2971,nroLote="0022641", iafas="40005", ruc="20100207941", CodIpress="00013723",serie ="0F213", numero="00112683", userCode="I0006750", motivo="",movimiento=2 ,estado=1}, 
                new Factura(){idLote = 2971,nroLote="0022641", iafas="40005", ruc="20100207941", CodIpress="00013723",serie ="0F214", numero="00112683", userCode="I0006750", motivo="",movimiento=2 ,estado=1}, 
                new Factura(){idLote = 2971,nroLote="0022641", iafas="40005", ruc="20100207941", CodIpress="00013723",serie ="0F215", numero="00112683", userCode="I0006750", motivo="",movimiento=2 ,estado=1},
            };

            var configInicial = p1.GetParamsLiquidacion();

            var facturaFormat = mockFacturas.Select(f => { 
                f.tipo += configInicial[0].SCONDITION_PARAM;
                f.idTabla += configInicial[1].SCONDITION_PARAM;
                f.tipoCampo += configInicial[2].SCONDITION_PARAM;                
                f.campo += configInicial[3].SCONDITION_PARAM;
                f.documento += f.serie.Substring(1) + "-" + f.numero.PadLeft(7,'0');
                return f; 
            }).ToList();
            var xmlPagadas = DataToXML<Factura>(facturaFormat, new Factura());
            var rpta = p1.LiquidarFacturas(xmlPagadas);
            //proceso yhon
            ProcessDerivacionBandeja oProcessDerivacionBandeja = new ProcessDerivacionBandeja();

            foreach (Factura fac in mockFacturas)
            {
                if (fac.movimiento == 0) {//OBS
                    //DERIVAR A SU BANDEJA DEL USUARIO QUE PERTENECE A LA OFICINA
                    fac.oficinaFisica = "LIMA";
                    string[] usuariobandeja = oProcessDerivacionBandeja.ObtenerUsuarioBandeja(fac.oficinaFisica);
                    int rptaDeriv = ProcesoEnviar(fac.ruc, fac.serie, fac.numero, usuariobandeja[0], Convert.ToInt32(usuariobandeja[1])); 
                }else{
                    int rptaDeriv = ProcesoEnviar(fac.ruc, fac.serie, fac.numero); 
                }
            }
        }

        private string DataToXML<T>(List<T> datos, T obj)
        {
            StringBuilder rpta = new StringBuilder();
            PropertyInfo[] propiedades = obj.GetType().GetProperties();
            foreach (var dato in datos)
            {
                rpta.Append("<");
                rpta.Append(typeof(T).Name);
                rpta.Append(">");
                foreach (var propiedad in propiedades)
                {
                    if(propiedad.GetValue(dato, null)!=null)
                    {
                        rpta.Append("<");
                        rpta.Append(propiedad.Name);
                        rpta.Append(">");
                        rpta.Append(propiedad.GetValue(dato, null));
                        rpta.Append("</");
                        rpta.Append(propiedad.Name);
                        rpta.Append(">");
                    }
                    
                }
                rpta.Append("</");
                rpta.Append(typeof(T).Name);
                rpta.Append(">");
            }

            return "<datos>" + rpta.ToString() + "</datos>";
        }

        private int ProcesoEnviar(string ruc, string serie, string numerodoc, string usuario = "", int bandeja = 0)
        {
            ProcessDerivacionBandeja oCDerivacionBandejaPA = new ProcessDerivacionBandeja();
            DataTable dtDocumentos = oCDerivacionBandejaPA.GetDocumentoCoa(ruc, serie, numerodoc);
            ESolicitudHistorialPA EitemSolicitudHistorialOrigen;
            ESolicitudHistorialPA EitemSolicitudHistorialDestino;
            int cantRegistrados = 0;

            foreach (DataRow dr in dtDocumentos.Rows)
            {
                EitemSolicitudHistorialOrigen = new ESolicitudHistorialPA();
                EitemSolicitudHistorialOrigen.idSolicitud = Convert.ToInt32(dr["IdSolicitud"].ToString());
                EitemSolicitudHistorialOrigen.idTipoFlujo = 1;
                EitemSolicitudHistorialOrigen.idUnidadOrganizativa = 1;
                EitemSolicitudHistorialOrigen.idAccion = 3; //ACCION ENVIAR
                EitemSolicitudHistorialOrigen.idBandejaOrigen = 6;//Bandeja del usuario en login TEDEF
                EitemSolicitudHistorialOrigen.idBandejaDestino = bandeja == 0 ? 6 : bandeja;//Bandeja del usuario: U0000001, 6: A. PAGOS
                EitemSolicitudHistorialOrigen.idRolOrigen = 6;
                EitemSolicitudHistorialOrigen.idRolDestino = bandeja == 0 ? 6 : bandeja;
                EitemSolicitudHistorialOrigen.usuarioOrigen = "U0000001"; //Usuario Asistente Pagos
                EitemSolicitudHistorialOrigen.usuarioDestino = usuario == "" ? "U0000002" : usuario;
                EitemSolicitudHistorialOrigen.usuarioCreacion = "PA";

                EitemSolicitudHistorialOrigen.comentario = "PAGO AUTOMATICO CONSOLA";
                EitemSolicitudHistorialOrigen.idTipoComentario = 1;
                EitemSolicitudHistorialOrigen.idEstadoFlujo = 1;
                EitemSolicitudHistorialOrigen.idEstadoDocumento = 1;

                EitemSolicitudHistorialOrigen.idSolicitudHistorial = RegistrarHistorico(null, EitemSolicitudHistorialOrigen);

                EitemSolicitudHistorialDestino = new ESolicitudHistorialPA();
                EitemSolicitudHistorialDestino.idSolicitud = Convert.ToInt32(dr["IdSolicitud"].ToString());
                EitemSolicitudHistorialDestino.idTipoFlujo = 1;
                EitemSolicitudHistorialDestino.idUnidadOrganizativa = 1;
                EitemSolicitudHistorialDestino.idAccion = 3;
                EitemSolicitudHistorialDestino.idBandejaOrigen = 6;
                EitemSolicitudHistorialDestino.idBandejaDestino = null;
                EitemSolicitudHistorialDestino.idRolOrigen = 6;
                EitemSolicitudHistorialDestino.idRolDestino = null;
                EitemSolicitudHistorialDestino.usuarioOrigen = "U0000001";
                EitemSolicitudHistorialDestino.usuarioDestino = null;
                EitemSolicitudHistorialDestino.usuarioCreacion = "U0000000";
                EitemSolicitudHistorialDestino.comentario = null;

                EitemSolicitudHistorialDestino.idEstadoFlujo = 1;
                EitemSolicitudHistorialDestino.idEstadoDocumento = 1;

                cantRegistrados += RegistrarHistorico(EitemSolicitudHistorialOrigen, EitemSolicitudHistorialDestino);
            }
            return cantRegistrados;
        }

        private int RegistrarHistorico(ESolicitudHistorialPA origen, ESolicitudHistorialPA destino)
        {
            ESolicitudHistorialPA oItemAnt = null;
            int iResultado = 0;
            ProcessDerivacionBandeja oProcessDerivacionBandeja = new ProcessDerivacionBandeja();

            try
            {
                if (origen == null)
                {
                    oItemAnt = oProcessDerivacionBandeja.ObtenerHistoricoBandeja(destino);
                }
                if (origen != null)
                {
                    string sOrigen = origen.idRolOrigen != 2 ? origen.usuarioOrigen : string.Empty;
                    if (origen.usuarioDestino != null && origen.usuarioDestino != sOrigen)
                        destino.idSolicitudHistorialAnt = origen.idSolicitudHistorial;
                }
                else
                    destino.idSolicitudHistorialAnt = oItemAnt.idSolicitudHistorialAnt;


                oProcessDerivacionBandeja.EliminarHistoricoBandeja(destino);
                iResultado = oProcessDerivacionBandeja.RegistrarHistoricoBandeja(destino);
                return iResultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Orchestrate.RegistrarHistorico()" + " - " + ex.Message + " - " + ex.InnerException, ex);
            }
        }
    }
}