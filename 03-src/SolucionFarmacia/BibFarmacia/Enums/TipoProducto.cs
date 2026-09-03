using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibFarmacia.Enum
{
    /// <summary>
    /// Enumeración que define los tipos de productos disponibles en la farmacia.
    /// Ejemplo de aplicación de OCP (Open/Closed Principle) mediante enums cerrados.
    /// </summary>
    public enum TipoProducto
    {
        Farmaceutico,
        Cosmetico,
        Alimenticio,
        Otro
    }
}
