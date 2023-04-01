using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace EnvioNotificacionPASoat
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Timer timer = new Timer(12000);//1800000
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            Console.WriteLine("Presione cualquier tecla para salir.");
            Console.ReadKey();
        }

        static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TEDEF.NotificacionProcess notificacion = new TEDEF.NotificacionProcess();
            string rpta = notificacion.EnviarNotificacion();
            Console.WriteLine(rpta);
        }
    }
}
