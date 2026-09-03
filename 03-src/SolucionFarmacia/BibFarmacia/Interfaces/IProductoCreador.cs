using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    /// <summary>
    /// Abstracción para la creación de productos.
    /// Permite que el código consumidor dependa de esta abstracción (DIP)
    /// y que nuevos tipos de productos se agreguen sin modificar código
    /// existente (OCP), simplemente implementando esta interfaz.
    /// </summary>
    public interface IProductoCreador
    {
        /// <summary>
        /// Crea un producto con los datos comunes proporcionados.
        /// Cada implementación define los valores específicos del tipo
        /// (stockMinimo predeterminado, fechaVencimiento predeterminada,
        /// material, relleno, etc.).
        /// </summary>
        Producto Crear(
            string nombre,
            decimal precio,
            int stock,
            Laboratorio laboratorio);
    }
}
