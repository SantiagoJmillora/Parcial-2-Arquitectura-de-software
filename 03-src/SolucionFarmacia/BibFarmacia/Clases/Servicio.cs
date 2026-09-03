using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Clases
{
    /// <summary>
    /// Clase que representa un servicio ofrecido por la farmacia.
    /// Implementa IVendible para ser vendible como productos.
    /// Ejemplo de OCP: nueva variante vendible sin modificar código existente.
    /// Sigue SRP al tener una responsabilidad única: representar servicios.
    /// Refactorización SRP: se extrae ObtenerInformacion() que retorna un string,
    /// separando la obtención de datos de la responsabilidad de presentación.
    /// MostrarInformacion() se mantiene como adaptador (contrato IVendible preservado).
    /// </summary>
    public class Servicio : IVendible
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int DuracionMinutos { get; set; }
        public string CategoriaServicio { get; set; }

        public Servicio(string nombre,
            decimal precio,
            int duracionMinutos,
            string categoriaServicio)
        {
            Nombre = nombre;
            Precio = precio;
            DuracionMinutos = duracionMinutos;
            CategoriaServicio = categoriaServicio;
        }

        /// <summary>
        /// Retorna la información del servicio como cadena de texto.
        /// Separación SRP: la obtención de la representación textual
        /// es independiente de su visualización en consola.
        /// </summary>
        public string ObtenerInformacion()
        {
            return $"Servicio: {Nombre}\n" +
                   $"Precio: {Precio}\n" +
                   $"Duración: {DuracionMinutos} minutos\n" +
                   $"Categoría: {CategoriaServicio}";
        }

        /// <summary>
        /// Muestra la información del servicio en consola.
        /// Preserva el contrato de IVendible y el comportamiento observable exacto.
        /// Delega la construcción del texto a ObtenerInformacion().
        /// </summary>
        public void MostrarInformacion()
        {
            Console.WriteLine($"Servicio: {Nombre}");
            Console.WriteLine($"Precio: {Precio}");
            Console.WriteLine($"Duración: {DuracionMinutos} minutos");
            Console.WriteLine($"Categoría: {CategoriaServicio}");
        }
    }
}
