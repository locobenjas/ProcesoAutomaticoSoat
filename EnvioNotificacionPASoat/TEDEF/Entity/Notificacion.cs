using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvioNotificacionPASoat.TEDEF
{
    public class Notificacion
    {
        public int IdNotificacion { get; set; }
        public string Nombre { get; set; }
        public string Asunto { get; set; }
        public string CodFiltro { get; set; }
        public string NombreFiltro { get; set; }
        public string Frecuencia { get; set; }
        public string HoraEjecucion { get; set; } //HH:mm
        public DateTime InicioProceso { get; set; }
        public DateTime FinProceso { get; set; }
        public int IdEstado { get; set; }
        public string Emails { get; set; }
    }
}
