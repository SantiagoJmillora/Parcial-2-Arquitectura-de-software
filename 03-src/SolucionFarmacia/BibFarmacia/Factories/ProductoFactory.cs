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
            int stockMinimo,
            DateTime fechaVencimiento,
            Laboratorio laboratorio)
        {
            return (MedicamentoCapsula)_creadorCapsula.Crear(
                nombre, precio, stock, stockMinimo, fechaVencimiento, laboratorio);
        }

        public static MedicamentoLiquido CrearLiquido(
            string nombre,
            decimal precio,
            int stock,
            int stockMinimo,
            DateTime fechaVencimiento,
            Laboratorio laboratorio)
        {
            return (MedicamentoLiquido)_creadorLiquido.Crear(
                nombre, precio, stock, stockMinimo, fechaVencimiento, laboratorio);
        }

        /// <summary>
        /// Punto único de despacho por tipo (P-01): decide qué IProductoCreador usar
        /// según el texto leído del archivo, en vez de que el llamador conozca las
        /// clases concretas. Agregar un tipo nuevo (ej. "cosmetico") es una entrada
        /// más en este switch y un IProductoCreador nuevo — no toca al llamador.
        /// Si el archivo no trae columna de tipo (formato actual), se preserva el
        /// comportamiento histórico: cápsula.
        /// </summary>
        public static Producto CrearPorTipo(
            string? tipo,
            string nombre,
            decimal precio,
            int stock,
            int stockMinimo,
            DateTime fechaVencimiento,
            Laboratorio laboratorio)
        {
            return (tipo?.Trim().ToLowerInvariant()) switch
            {
                "liquido" => CrearLiquido(nombre, precio, stock, stockMinimo, fechaVencimiento, laboratorio),
                _ => CrearCapsula(nombre, precio, stock, stockMinimo, fechaVencimiento, laboratorio),
            };
        }
    }
}