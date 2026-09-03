using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    /// <summary>
    /// Abstracción de validación para clientes.
    /// Permite que nuevas reglas de validación se agreguen implementando
    /// esta interfaz sin modificar AspectoValidacion (OCP).
    /// </summary>
    public interface IValidadorCliente
    {
        string Validar(Cliente cliente);
    }
}
