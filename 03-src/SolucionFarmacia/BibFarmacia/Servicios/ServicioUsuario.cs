using System.Collections.Generic;
using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    /// <summary>
    /// Responsabilidad: gestionar usuarios y orquestar la autenticación.
    /// Refactorización SRP: la carga desde archivo se separó a RepositorioUsuarioArchivo.
    /// Refactorización DIP: depende de IAutenticacion (abstracción) inyectada por
    /// constructor, en lugar de la clase estática AspectoAutenticacion (detalle).
    /// </summary>
    public class ServicioUsuario
    {
        private List<Usuario> usuarios;
        private readonly IAutenticacion _autenticacion;

        /// <summary>
        /// Constructor que recibe la implementación de autenticación por inyección (DIP).
        /// </summary>
        public ServicioUsuario(IAutenticacion autenticacion)
        {
            usuarios = new List<Usuario>();
            _autenticacion = autenticacion;
        }

        public void AgregarUsuario(
            Usuario usuario)
        {
            usuarios.Add(usuario);
        }

        public bool Login(
            string user,
            string password)
        {
            return _autenticacion.Login(
                usuarios,
                user,
                password);
        }

        /// <summary>
        /// Carga la lista de usuarios ya procesada por el repositorio.
        /// </summary>
        public void CargarUsuarios(
            IEnumerable<Usuario> usuariosACargar)
        {
            foreach (var usuario in usuariosACargar)
            {
                usuarios.Add(usuario);
            }
        }
    }
}