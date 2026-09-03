using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Eventos;

namespace BibFarmacia.Servicios
{
    /// <summary>
    /// Responsabilidad: gestionar clientes y coordinar la acumulación de puntos.
    /// Refactorización DIP: EventoPuntos se inyecta por constructor en lugar de
    /// ser instanciado internamente con 'new', moviendo la construcción
    /// concreta al Composition Root (Program.cs).
    /// </summary>
    public class ServicioCliente
    {
        private List<Cliente> clientes;

        public EventoPuntos EventoPuntos;

        /// <summary>
        /// Constructor que recibe EventoPuntos por inyección (DIP).
        /// </summary>
        public ServicioCliente(
            EventoPuntos eventoPuntos)
        {
            clientes = new List<Cliente>();

            EventoPuntos = eventoPuntos;
        }

        public void AgregarCliente(
            Cliente cliente)
        {
            clientes.Add(cliente);
        }

        public List<Cliente> ObtenerClientes()
        {
            return clientes;
        }

        public void AcumularPuntos(
            Cliente cliente,
            int puntos)
        {
            cliente.Puntos += puntos;

            EventoPuntos.Disparar(
                cliente.Nombre,
                puntos);
        }

        public string Cargar(
            string ruta)
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    return "Archivo no encontrado";
                }

                string[] lineas =
                    File.ReadAllLines(ruta);

                foreach (string linea in lineas)
                {
                    string[] datos =
                        linea.Split(';');

                    Cliente cliente =
                        new Cliente(
                            datos[0],
                            datos[1],
                            datos[2],
                            datos[3]);

                    clientes.Add(cliente);
                }

                return "Clientes cargados";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}