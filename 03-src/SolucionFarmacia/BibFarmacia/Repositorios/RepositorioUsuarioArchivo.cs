using System;
using System.Collections.Generic;
using BibFarmacia.Clases;

namespace BibFarmacia.Repositorios
{
    /// <summary>
    /// Responsabilidad única: leer usuarios desde un archivo de texto y
    /// construir los objetos correspondientes.
    /// Extrae de ServicioUsuario la responsabilidad de persistencia/carga (SRP).
    /// </summary>
    public class RepositorioUsuarioArchivo
    {
        /// <summary>
        /// Lee el archivo indicado por <paramref name="ruta"/> y retorna la lista
        /// de usuarios parseados. Devuelve el mensaje de resultado idéntico al
        /// comportamiento anterior de ServicioUsuario.Cargar.
        /// </summary>
        /// <param name="ruta">Ruta del archivo de usuarios.</param>
        /// <param name="usuarios">Lista de usuarios cargados (salida).</param>
        /// <returns>Mensaje de resultado: "Usuarios cargados" o descripción del error.</returns>
        public string Cargar(string ruta, out List<Usuario> usuarios)
        {
            usuarios = new List<Usuario>();

            try
            {
                if (!File.Exists(ruta))
                {
                    return "Archivo no encontrado";
                }

                string[] lineas = File.ReadAllLines(ruta);

                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split(';');

                    Usuario usuario =
                        new Usuario(
                            datos[0],
                            datos[1],
                            datos[2],
                            datos[3],
                            datos[4],
                            datos[5]);

                    usuarios.Add(usuario);
                }

                return "Usuarios cargados";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
