using System.Collections.Generic;
using GrandesCuentas.AFF;

namespace GrandesCuentas.LPG.VT
{
    public class Orchestrate : IPrincipal
    {
        public void Transfer()
        {
            p1_Process p1 = new p1_Process();
            p2_Extraction p2 = new p2_Extraction();
            p3_Extraction p3 = new p3_Extraction();

            p3_Load p4 = new p3_Load();

            p1.Target();

            //Polizas Individuales y Multilocalidad - Facturacion != "Por certificado"
            List<Acoount> accounts = p2.Select(1);
            p4.Transfer(accounts);

            //Polizas Colectivas - Facturacion = "Por certificado"
            List<Acoount> accounts2 = p3.Select(2);
            p4.Transfer(accounts2);
        }
    }
}