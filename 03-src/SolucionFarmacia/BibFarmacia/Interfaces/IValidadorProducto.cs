using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    /// <summary>
    /// Abstracción de validación para productos.
    /// Permite que nuevas reglas de validación se agreguen implementando
    /// esta interfaz sin modificar AspectoValidacion (OCP).
    /// </summary>
    public interface IValidadorProducto
    {
        string Validar(Producto producto);
    }
}
