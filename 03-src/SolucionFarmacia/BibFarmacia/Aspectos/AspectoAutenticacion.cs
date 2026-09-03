using System.Collections.Generic;
using System.Linq;
using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Aspectos
{
    /// <summary>
    /// Implementación de autenticación de usuarios.
    /// Refactorización DIP: convertida de clase estática a instancia que
    /// implementa IAutenticacion, permitiendo que ServicioUsuario dependa
    /// de la abstracción y no de la implementación concreta.
    /// La lógica de autenticación es exactamente la misma que antes:
    ///   usuarios.Any(u => u.UserName == user && u.Password == password)
    /// </summary>
    public class AspectoAutenticacion : IAutenticacion
    {
        public bool Login(
            List<Usuario> usuarios,
            string user,
            string password)
        {
            return usuarios.Any(u =>
                u.UserName == user &&
                u.Password == password);
        }
    }
}