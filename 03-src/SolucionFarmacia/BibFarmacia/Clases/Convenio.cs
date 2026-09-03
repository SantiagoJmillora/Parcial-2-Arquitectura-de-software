using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Clases
{
    /// <summary>
    /// Clase que representa un convenio con una política de descuento.
    /// Encapsula la lógica de descuento de manera limpia.
    /// Sigue SRP: responsabilidad única de manejar convenios y sus descuentos.
    /// Implementa DIP: depende de IDescuento (abstracción).
    /// </summary>
    public class Convenio
    {
        public string Nombre { get; set; }
        private IDescuento _politicaDescuento;

        public Convenio(string nombre, IDescuento politicaDescuento)
        {
            Nombre = nombre;
            _politicaDescuento = politicaDescuento;
        }

        public decimal CalcularDescuento(decimal precio)
        {
            return _politicaDescuento.CalcularDescuento(precio);
        }

        public decimal AplicarDescuento(decimal precio)
        {
            decimal descuento = _politicaDescuento.CalcularDescuento(precio);
            return precio - descuento;
        }
    }
}
