using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibFarmacia.Clases;
using BibFarmacia.Interfaces;
using BibFarmacia.Eventos;

namespace BibFarmacia.Servicios
{
    /// <summary>
    /// Responsabilidad: gestionar el catálogo de productos y coordinar
    /// las verificaciones de stock y vencimiento.
    /// Refactorización SRP: la carga desde archivo se separó a RepositorioProductoArchivo.
    /// Refactorización DIP: EventoStockMinimo y EventoVencimiento se inyectan
    /// por constructor en lugar de ser instanciados internamente.
    /// </summary>
    public class ServicioProducto
    {
        private readonly List<Producto> productos;

        public EventoStockMinimo EventoStock;
        public EventoVencimiento EventoVencimiento;

        /// <summary>
        /// Constructor que recibe los eventos por inyección (DIP).
        /// La construcción concreta se realiza en el Composition Root (Program.cs).
        /// </summary>
        public ServicioProducto(
            EventoStockMinimo eventoStock,
            EventoVencimiento eventoVencimiento)
        {
            productos = new List<Producto>();

            EventoStock = eventoStock;
            EventoVencimiento = eventoVencimiento;
        }

        public string AgregarProducto(
            Producto producto)
        {
            try
            {
                productos.Add(producto);

                return "Producto agregado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Carga la lista de productos ya procesada por el repositorio.
        /// El mensaje de resultado es producido por el repositorio.
        /// </summary>
        public void CargarProductos(
            IEnumerable<Producto> productosACagar)
        {
            foreach (var producto in productosACagar)
            {
                productos.Add(producto);
            }
        }

        public List<Producto> ObtenerProductos()
        {
            return productos;
        }

        public void VerificarStock()
        {
            foreach (var producto in productos)
            {
                if (producto.Stock <=
                    producto.StockMinimo)
                {
                    EventoStock.Disparar(producto);
                }
            }
        }

        public void VerificarVencimiento()
        {
            foreach (var producto in productos)
            {
                int dias =
                    (producto.FechaVencimiento -
                    DateTime.Now).Days;

                if (dias <= 30)
                {
                    EventoVencimiento
                        .Disparar(producto);
                }
            }
        }
    }
}