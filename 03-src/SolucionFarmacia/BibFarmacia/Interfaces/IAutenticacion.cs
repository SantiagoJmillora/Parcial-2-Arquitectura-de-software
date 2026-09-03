using System.Collections.Generic;
using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    /// <summary>
    /// Abstracción de la autenticación de usuarios.
    /// Permite que ServicioUsuario dependa de esta abstracción en lugar
    /// de la implementación estática concreta AspectoAutenticacion (DIP).
    /// </summary>
    public interface IAutenticacion
    {
        bool Login(
            List<Usuario> usuarios,
            string user,
            string password);
    }
}
