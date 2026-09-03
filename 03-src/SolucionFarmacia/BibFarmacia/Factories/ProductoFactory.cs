using BibFarmacia.Clases;
using BibFarmacia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibFarmacia.Factories
{
    /// <summary>
    /// Fábrica de productos. Mantiene los métodos estáticos originales para
    /// compatibilidad, ahora delegando a las implementaciones de IProductoCreador
    /// (CreadorCapsula y CreadorLiquido), que encapsulan los valores específicos
    /// de cada tipo.
    /// Refactorización OCP: para agregar un nuevo tipo de medicamento se crea
    /// una nueva implementación de IProductoCreador sin modificar esta clase.
    /// </summary>
    public static class ProductoFactory
    {
        private static readonly IProductoCreador _creadorCapsula =
            new CreadorCapsula();

        private static readonly IProductoCreador _creadorLiquido =
            new CreadorLiquido();

        public static MedicamentoCapsula CrearCapsula(
            string nombre,
            decimal precio,
            int stock,
            Laboratorio laboratorio)
        {
            return (MedicamentoCapsula)_creadorCapsula.Crear(
                nombre, precio, stock, laboratorio);
        }

        public static MedicamentoLiquido CrearLiquido(
            string nombre,
            decimal precio,
            int stock,
            Laboratorio laboratorio)
        {
            return (MedicamentoLiquido)_creadorLiquido.Crear(
                nombre, precio, stock, laboratorio);
        }
    }
}