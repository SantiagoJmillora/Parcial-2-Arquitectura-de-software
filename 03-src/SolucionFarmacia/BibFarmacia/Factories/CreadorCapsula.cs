using System;
using BibFarmacia.Clases;
using BibFarmacia.Enum;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    /// <summary>
    /// Implementación de IProductoCreador para MedicamentoCapsula.
    /// Encapsula los valores específicos predeterminados de cápsulas:
    ///   - StockMinimo: 5
    ///   - FechaVencimiento: DateTime.Now.AddMonths(6)
    ///   - TipoRelleno: TipoRelleno.Gel
    /// Estos valores son exactamente los mismos que tenía ProductoFactory.CrearCapsula.
    /// </summary>
    public class CreadorCapsula : IProductoCreador
    {
        public Producto Crear(
            string nombre,
            decimal precio,
            int stock,
            Laboratorio laboratorio)
        {
            return new MedicamentoCapsula(
                nombre,
                precio,
                stock,
                5,
                DateTime.Now.AddMonths(6),
                laboratorio,
                TipoRelleno.Gel);
        }
    }
}
