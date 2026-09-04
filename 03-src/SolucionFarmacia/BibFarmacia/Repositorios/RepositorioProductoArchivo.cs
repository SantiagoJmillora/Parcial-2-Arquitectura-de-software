using System;
using System.Collections.Generic;
using BibFarmacia.Clases;
using BibFarmacia.Enum;
using BibFarmacia.Factories;

namespace BibFarmacia.Repositorios
{
    /// <summary>
    /// Responsabilidad única: leer productos desde un archivo de texto y
    /// construir los objetos correspondientes.
    /// Extrae de ServicioProducto la responsabilidad de persistencia/carga (SRP).
    /// Al ser una clase separada, ServicioProducto deja de depender de
    /// File.ReadAllLines ni de constructores concretos de MedicamentoCapsula (DIP).
    ///
    /// P-01 (Reto 2, Factory Method): antes construía siempre un
    /// MedicamentoCapsula con TipoRelleno.Gel fijo. Ahora delega en
    /// ProductoFactory.CrearPorTipo, que decide la clase concreta según una
    /// 7ma columna opcional "tipo". El formato actual de productos.txt no
    /// trae esa columna, así que el comportamiento observable para los datos
    /// de hoy es idéntico (siempre cápsula) — la extensibilidad es real pero
    /// no cambia ninguna salida existente.
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

                    // Columna 7 (tipo) es opcional: formato actual no la trae.
                    string? tipo = datos.Length > 6 ? datos[6] : null;

                    Producto producto =
                        ProductoFactory.CrearPorTipo(
                            tipo,
                            datos[0],
                            decimal.Parse(datos[1]),
                            int.Parse(datos[2]),
                            int.Parse(datos[3]),
                            DateTime.Parse(datos[4]),
                            laboratorio);

                    productos.Add(producto);
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
