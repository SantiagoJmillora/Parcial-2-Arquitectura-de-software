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

                    // Columnas 5 y 6 (nombre de convenio; tasa de descuento)
                    // son opcionales — formato original no las trae, y sin
                    // ellas el cliente queda con Convenio = null, igual que
                    // antes de este cambio (SC-3, Reto 2, Strategy).
                    if (datos.Length > 5 &&
                        !string.IsNullOrWhiteSpace(datos[4]))
                    {
                        // InvariantCulture: en la cultura del sistema (es-CO)
                        // "." es separador de miles, no decimal — parsear
                        // "0.15" sin especificar cultura da 15 en vez de
                        // 0.15 y produce un descuento absurdo.
                        decimal tasa =
                            decimal.Parse(
                                datos[5],
                                System.Globalization.CultureInfo.InvariantCulture);

                        // R-01 (registro de riesgos, Actividad 5.1): una tasa
                        // fuera de [0,1] produciría un total negativo o mayor
                        // que el precio de lista. Se descarta el convenio en
                        // vez de fallar en silencio con un cobro absurdo.
                        if (tasa >= 0m && tasa <= 1m)
                        {
                            cliente.Convenio =
                                new Convenio(
                                    datos[4],
                                    new ServicioDescuento(tasa));
                        }
                        else
                        {
                            Console.WriteLine(
                                $"Advertencia: tasa de descuento invalida " +
                                $"({tasa}) para el cliente {datos[0]}; se " +
                                $"carga sin convenio.");
                        }
                    }

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