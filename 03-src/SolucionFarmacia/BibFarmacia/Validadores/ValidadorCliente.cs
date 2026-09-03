using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Validadores
{
    /// <summary>
    /// Implementación de IValidadorCliente con exactamente las mismas reglas
    /// que tenía AspectoValidacion.ValidarCliente:
    ///   1. Nombre no puede ser nulo ni vacío → "Nombre inválido"
    ///   2. Cédula debe tener longitud >= 3   → "Cédula inválida"
    ///   3. Si pasa todas las reglas          → "Cliente válido"
    /// Las reglas, mensajes y orden son idénticos al original.
    /// </summary>
    public class ValidadorCliente : IValidadorCliente
    {
        public string Validar(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(
                cliente.Nombre))
            {
                return "Nombre inválido";
            }

            if (cliente.Cedula.Length < 3)
            {
                return "Cédula inválida";
            }

            return "Cliente válido";
        }
    }
}
