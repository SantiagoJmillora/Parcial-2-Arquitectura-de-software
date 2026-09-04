using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibFarmacia.Interfaces
{
    /// <summary>
    /// Observer (Reto 2, P-03): rol de Observer. ConcreteObserver = ServicioNotificacion.
    /// Recibe el color junto con el mensaje porque cada Evento* (Subject) usaba un
    /// ConsoleColor distinto en su suscripción inline original; centralizar el
    /// formateo sin este parámetro habría cambiado la salida observable.
    /// </summary>
    public interface IServicioNotificacion
    {
        void EnviarNotificacion(string mensaje, ConsoleColor color);
    }
}
