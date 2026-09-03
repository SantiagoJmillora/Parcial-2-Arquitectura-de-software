using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibFarmacia.Interfaces;
using BibFarmacia.Enum;

namespace BibFarmacia.Clases
{
    /// <summary>
    /// Clase abstracta que representa un producto vendible en la farmacia.
    /// Implementa IVendible para seguir el DIP (Dependency Inversion Principle).
    /// Separa responsabilidades de producto base vs. tipos específicos (SRP).
    /// Refactorización SRP: se extrae ObtenerInformacion() que retorna un string,
    /// separando la obtención de datos de la responsabilidad de presentación.
    /// MostrarInformacion() se mantiene como adaptador (contrato IVendible preservado).
    /// </summary>
    public abstract class Producto : IVendible
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public TipoProducto Categoria { get; set; }

        protected Producto(string nombre,
            decimal precio,
            int stock,
            int stockMinimo,
            DateTime fechaVencimiento,
            TipoProducto categoria = TipoProducto.Otro)
        {
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            StockMinimo = stockMinimo;
            FechaVencimiento = fechaVencimiento;
            Categoria = categoria;
        }

        /// <summary>
        /// Retorna la información del producto como cadena de texto.
        /// Separación SRP: la obtención de la representación textual
        /// es independiente de su visualización en consola.
        /// </summary>
        public virtual string ObtenerInformacion()
        {
            return $"Producto: {Nombre}\n" +
                   $"Precio: {Precio}\n" +
                   $"Stock: {Stock}\n" +
                   $"Categoría: {Categoria}";
        }

        /// <summary>
        /// Muestra la información del producto en consola.
        /// Preserva el contrato de IVendible y el comportamiento observable exacto.
        /// Delega la construcción del texto a ObtenerInformacion().
        /// </summary>
        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"Producto: {Nombre}");
            Console.WriteLine($"Precio: {Precio}");
            Console.WriteLine($"Stock: {Stock}");
            Console.WriteLine($"Categoría: {Categoria}");
        }
    }
}