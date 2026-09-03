using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Validadores
{
    /// <summary>
    /// Implementación de IValidadorProducto con exactamente las mismas reglas
    /// que tenía AspectoValidacion.ValidarProducto:
    ///   1. Precio debe ser > 0        → "Precio inválido"
    ///   2. Stock no puede ser < 0     → "Stock inválido"
    ///   3. Si pasa todas las reglas   → "Producto válido"
    /// Las reglas, mensajes y orden son idénticos al original.
    /// </summary>
    public class ValidadorProducto : IValidadorProducto
    {
        public string Validar(Producto producto)
        {
            if (producto.Precio <= 0)
            {
                return "Precio inválido";
            }

            if (producto.Stock < 0)
            {
                return "Stock inválido";
            }

            return "Producto válido";
        }
    }
}
