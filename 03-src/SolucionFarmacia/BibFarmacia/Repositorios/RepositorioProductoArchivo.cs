using System;
using System.Collections.Generic;
using BibFarmacia.Clases;
using BibFarmacia.Enum;

namespace BibFarmacia.Repositorios
{
    /// <summary>
    /// Responsabilidad única: leer productos desde un archivo de texto y
    /// construir los objetos correspondientes.
    /// Extrae de ServicioProducto la responsabilidad de persistencia/carga (SRP).
    /// Al ser una clase separada, ServicioProducto deja de depender de
    /// File.ReadAllLines ni de constructores concretos de MedicamentoCapsula (DIP).
    /// </summary>
    public class RepositorioProductoArchivo
    {
        /// <summary>
        /// Lee el archivo indicado por <paramref name="ruta"/> y retorna la lista
        /// de productos parseados. Devuelve el mensaje de resultado idéntico al
        /// comportamiento anterior de ServicioProducto.CargarDesdeArchivo.
        /// </summary>
        /// <param name="ruta">Ruta del archivo de productos.</param>
        /// <param name="productos">Lista de productos cargados (salida).</param>
        /// <returns>Mensaje de resultado: "Productos cargados" o descripción del error.</returns>
        public string Cargar(string ruta, out List<Producto> productos)
        {
            productos = new List<Producto>();

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

                    Laboratorio laboratorio =
                        new Laboratorio(
                            datos[5],
                            "Medellin",
                            "4444444");

                    MedicamentoCapsula medicamento =
                        new MedicamentoCapsula(
                            datos[0],
                            decimal.Parse(datos[1]),
                            int.Parse(datos[2]),
                            int.Parse(datos[3]),
                            DateTime.Parse(datos[4]),
                            laboratorio,
                            TipoRelleno.Gel);

                    productos.Add(medicamento);
                }

                return "Productos cargados";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
