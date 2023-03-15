using System;
using System.Collections.Generic;
using System.Threading;

namespace GrandesCuentas
{
    class Program
    {
        static void Main(string[] args)
        {
            var threads = new List<Thread>();
            AFF.Process proc = new AFF.Process();
            //AFF.p3_Load affSQL = new AFF.p3_Load();
            //LPG.VT.Orchestrate lpgVT = new LPG.VT.Orchestrate();
            //LPV.VT.Orchestrate lpvVT = new LPV.VT.Orchestrate();
            LPG.INX.Orchestrate lpgINX = new LPG.INX.Orchestrate();
            //LPV.INX.Orchestrate lpvINX = new LPV.INX.Orchestrate();
            //LPE.INX.Orchestrate lpeINX = new LPE.INX.Orchestrate();
            
            try
            {
                //AFF SQL
                Console.WriteLine("Paso 1: AFF SQL / Valida Proceso");
                //switch (proc.Status())
                //{
                //    case 1: throw new Exception(string.Format("El proceso ({0}) se ejecutó previamente con errores", DateTime.Now.ToString("dd/MM/yyyy")));
                //    case 2: throw new Exception(string.Format("El proceso ({0}) se ejecutó previamente de forma correcta", DateTime.Now.ToString("dd/MM/yyyy")));
                //}

                Console.WriteLine("Paso 2: AFF SQL / Inicio Proceso");
                //proc.Insert_CAB();
            
                //AFF SQL
                Console.WriteLine("Paso 3: AFF SQL / Delete TBL_TMP");
                //affSQL.Delete_TBL_TMP();

                //LPG VT
                Console.WriteLine("Paso 4: LPG VT / Transfer");
                //threads.Add(Execute(lpgVT));

                //LPG INX
                Console.WriteLine("Paso 5: LPG INX / Transfer");
                //threads.Add(Execute(lpgINX));
                lpgINX.Transfer();

                //LPV VT
                Console.WriteLine("Paso 6: LPV VT / Transfer");
                //threads.Add(Execute(lpvVT));

                //LPV INX
                Console.WriteLine("Paso 7: LPV INX / Transfer");
                //threads.Add(Execute(lpvINX));

                //LPE INX
                Console.WriteLine("Paso 8: LPE INX / Transfer");
                //threads.Add(Execute(lpeINX));

                //Espera a que terminen todos los hilos
                //foreach (Thread t in threads)
                //    t.Join();

                //AFF SQL
                Console.WriteLine("Paso 9: AFF SQL / Insert TBL_FIN");
                //affSQL.Insert_TBL_FIN();

                Console.WriteLine("Paso 10: AFF SQL / Update CAB");
                proc.Update_CAB();

                //Console.WriteLine("Fin Proceso");
                //Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
        }

        static Thread Execute(IPrincipal obj)
        {
            ThreadStart delegado = new ThreadStart(obj.Transfer);
            Thread hilo = new Thread(delegado);
            hilo.Start();

            return hilo;
        }
    }
}