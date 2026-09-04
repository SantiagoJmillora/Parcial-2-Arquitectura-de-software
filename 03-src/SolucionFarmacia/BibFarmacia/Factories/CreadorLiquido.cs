using System;
using BibFarmacia.Clases;
using BibFarmacia.Enum;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    /// <summary>
    /// Implementación de IProductoCreador para MedicamentoLiquido.
    /// stockMinimo y fechaVencimiento vienen del llamador. MaterialEnvase y
    /// Mililitros no existen todavía en el formato de productos.txt, así que
    /// esta clase mantiene los valores predeterminados que ya tenía
    /// ProductoFactory.CrearLiquido (Vidrio, 120 ml).
    /// </summary>
    public class CreadorLiquido : IProductoCreador
    {
        public Producto Crear(
            string nombre,
            decimal precio,
            int stock,
            int stockMinimo,
            DateTime fechaVencimiento,
            Laboratorio laboratorio)
        {
            return new MedicamentoLiquido(
                nombre,
                precio,
                stock,
                stockMinimo,
                fechaVencimiento,
                laboratorio,
                MaterialEnvase.Vidrio,
                120);
        }
    }
}
