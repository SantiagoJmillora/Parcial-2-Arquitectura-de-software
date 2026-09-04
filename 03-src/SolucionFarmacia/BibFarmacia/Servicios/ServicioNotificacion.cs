using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    /// <summary>
    /// ConcreteObserver del Observer de P-03. Centraliza el formateo que antes
    /// estaba duplicado 4 veces en Program.cs (Console.ForegroundColor / WriteLine
    /// / ResetColor). Reproduce exactamente esa misma secuencia — sin prefijo
    /// adicional — para no cambiar la salida observable.
    /// </summary>
    public class ServicioNotificacion : IServicioNotificacion
    {
        public void EnviarNotificacion(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(mensaje);

            Console.ResetColor();
        }
    }
}