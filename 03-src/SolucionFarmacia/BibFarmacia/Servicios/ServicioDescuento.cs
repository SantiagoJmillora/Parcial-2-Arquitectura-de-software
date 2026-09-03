using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    /// <summary>
    /// Implementación de IDescuento con tasa configurable por constructor.
    /// Refactorización OCP: la tasa ya no está hardcoded en el cuerpo del método.
    /// La tasa se inyecta desde fuera (Composition Root), lo que permite
    /// crear instancias con distintas tasas sin modificar esta clase.
    /// El Composition Root instancia con exactamente 0.10m (10%) — mismo valor actual.
    /// </summary>
    public class ServicioDescuento : IDescuento
    {
        private readonly decimal _tasa;

        /// <summary>
        /// Constructor que recibe la tasa de descuento a aplicar.
        /// En Program.cs se instancia con 0.10m para preservar el comportamiento actual.
        /// </summary>
        public ServicioDescuento(decimal tasa)
        {
            _tasa = tasa;
        }

        public decimal CalcularDescuento(decimal precio)
        {
            return precio * _tasa;
        }
    }
}