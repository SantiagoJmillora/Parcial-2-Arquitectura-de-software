using System;
using BibFarmacia.Clases;
using BibFarmacia.Enum;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    /// <summary>
    /// Implementación de IProductoCreador para MedicamentoLiquido.
    /// Encapsula los valores específicos predeterminados de líquidos:
    ///   - StockMinimo: 5
    ///   - FechaVencimiento: DateTime.Now.AddMonths(12)
    ///   - MaterialEnvase: MaterialEnvase.Vidrio
    ///   - Mililitros: 120
    /// Estos valores son exactamente los mismos que tenía ProductoFactory.CrearLiquido.
    /// </summary>
    public class CreadorLiquido : IProductoCreador
    {
        public Producto Crear(
            string nombre,
            decimal precio,
            int stock,
            Laboratorio laboratorio)
        {
            return new MedicamentoLiquido(
                nombre,
                precio,
                stock,
                5,
                DateTime.Now.AddMonths(12),
                laboratorio,
                MaterialEnvase.Vidrio,
                120);
        }
    }
}
