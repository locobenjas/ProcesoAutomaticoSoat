namespace GrandesCuentas.LPG.INX
{
    using System;
    public class Factura
    {
        public int idLote { get; set; }
        public string nroLote { get; set; }
        public string ruc { get; set; }        
        public string serie { get; set; }
        public string numero { get; set; }
        public DateTime? fechaEmision{ get; set; }
        public decimal? monto { get; set; }
        public short? moneda { get; set; }
        public string diagnostico { get; set; }
        public int? siniestro { get; set; }
        public int? cartaGarantia { get; set; }
        public string filtroPA { get; set; }
        public short? estado { get; set; }
        public string CodIpress { get; set; }
        public string iafas { get; set; }
        public string documento { get; set; }
        public string userCode { get; set; }
        public string tipo { get; set; }
        public string idTabla { get; set; }
        public string tipoCampo {get;set;}
        public string campo {get;set;}
        public string motivo { get; set; }
        public int movimiento { get; set; }
    }
}
