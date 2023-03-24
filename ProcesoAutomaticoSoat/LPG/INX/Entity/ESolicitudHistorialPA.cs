using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcesoAutomaticoSoat.LPG.INX.Entity
{
    public class ESolicitudHistorialPA
    {
        public int? idSolicitudHistorial { get; set; }
        public int? idSolicitud { get; set; }
        public int? idTipoFlujo { get; set; }
        public int? idUnidadOrganizativa { get; set; }
        public int? idAccion { get; set; }
        public int? idRolOrigen { get; set; }
        public int? idRolDestino { get; set; }
        public int? idBandejaOrigen { get; set; }
        public int? idBandejaDestino { get; set; }
        public string usuarioOrigen { get; set; }
        public string usuarioDestino { get; set; }
        public int? idEstadoDocumento { get; set; }
        public int? idEstadoFlujo { get; set; }
        public string comentario { get; set; }
        public int? idTipoComentario { get; set; }
        public string descTipoComentario { get; set; }
        public DateTime? fechaEfecto { get; set; }
        public DateTime? fechaAnulacion { get; set; }
        public string usuarioCreacion { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public int? idSolicitudHistorialAnt { get; set; }
        public string estadoDocumento { get; set; }
        public DateTime? fechaRecibido { get; set; }
        public DateTime? fechaEnviado { get; set; }
        public string accion { get; set; }
    }
}
