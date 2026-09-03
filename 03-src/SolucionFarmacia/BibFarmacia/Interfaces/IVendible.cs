using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibFarmacia.Interfaces
{
    /// <summary>
    /// Interfaz que representa un item vendible en la farmacia.
    /// Abstracción común para productos y servicios.
    /// Aplica el principio DIP (Dependency Inversion Principle).
    /// </summary>
    public interface IVendible
    {
        string Nombre { get; set; }
        decimal Precio { get; set; }

        void MostrarInformacion();
    }
}
