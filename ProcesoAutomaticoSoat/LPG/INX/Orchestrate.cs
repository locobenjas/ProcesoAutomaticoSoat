using System.Collections.Generic;
using GrandesCuentas.AFF;
using System;
using System.Text;
using System.Reflection;
using System.Linq;

namespace GrandesCuentas.LPG.INX
{
    public class Orchestrate : IPrincipal
    {
        public void Transfer()
        {

            ProcessBIlls p1 = new ProcessBIlls();
            //var date = DateTime.Now.ToString("dd/MM/yyyy");
            //var list = p1.SelectBills(date);
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
    }
}