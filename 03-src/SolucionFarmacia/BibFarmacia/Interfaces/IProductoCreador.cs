using System;
using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    /// <summary>
    /// Abstracción para la creación de productos.
    /// Permite que el código consumidor dependa de esta abstracción (DIP)
    /// y que nuevos tipos de productos se agreguen sin modificar código
    /// existente (OCP), simplemente implementando esta interfaz.
    /// stockMinimo y fechaVencimiento se reciben como parámetros (no se
    /// hardcodean) para que un cliente como RepositorioProductoArchivo pueda
    /// preservar los valores reales del archivo de origen.
    /// </summary>
    public interface IProductoCreador
    {
        /// <summary>
        /// Crea un producto con los datos comunes proporcionados.
        /// Cada implementación define los valores específicos del tipo
        /// (material, relleno, etc.) que no vienen del archivo.
        /// </summary>
        Producto Crear(
            string nombre,
            decimal precio,
            int stock,
            int stockMinimo,
            DateTime fechaVencimiento,
            Laboratorio laboratorio);
    }
}
