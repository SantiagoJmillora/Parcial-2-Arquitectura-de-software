using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibFarmacia.Interfaces;
using BibFarmacia.Clases;

namespace BibFarmacia.Servicios
{
    /// <summary>
    /// Servicio encargado de gestionar ventas de items vendibles (productos y servicios).
    /// Refactorización que aplica SOLID principles:
    /// - DIP: depende de IVendible (abstracción), no de clases concretas
    /// - OCP: extensible a nuevos tipos vendibles sin modificar código existente
    /// - SRP: responsabilidad única en manejar lógica de ventas
    /// </summary>
    public class ServicioVentas
    {
        private List<IVendible> itemsVendibles;

        public ServicioVentas()
        {
            itemsVendibles = new List<IVendible>();
        }

        /// <summary>
        /// Registra un item vendible en el sistema de ventas.
        /// </summary>
        public string AgregarItem(IVendible item)
        {
            try
            {
                if (item == null)
                {
                    return "Item inválido";
                }

                itemsVendibles.Add(item);
                return $"Item '{item.Nombre}' agregado a ventas";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Obtiene la lista de items vendibles disponibles.
        /// </summary>
        public List<IVendible> ObtenerItems()
        {
            return itemsVendibles;
        }

        /// <summary>
        /// Registra una venta de un item a un cliente.
        /// Actualiza stock si es un producto.
        /// </summary>
        public string RegistrarVenta(
            IVendible item,
            Cliente cliente,
            int cantidad)
        {
            try
            {
                if (item == null || cliente == null || cantidad <= 0)
                {
                    return "Parámetros inválidos";
                }

                // Si es un producto, verificar y actualizar stock
                if (item is Producto producto)
                {
                    if (producto.Stock < cantidad)
                    {
                        return "Stock insuficiente";
                    }
                    producto.Stock -= cantidad;
                }

                // Registrar movimiento (salida por venta)
                Movimiento movimiento = new Movimiento(
                    DateTime.Now,
                    cantidad,
                    "Venta",
                    item);

                return $"Venta registrada: {cantidad}x {item.Nombre} al cliente {cliente.Nombre}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Calcula el total de una venta considerando cantidad y cliente.
        /// Puede aplicar descuentos basados en puntos del cliente.
        /// </summary>
        public decimal CalcularTotal(
            IVendible item,
            Cliente cliente,
            int cantidad)
        {
            try
            {
                if (item == null || cliente == null || cantidad <= 0)
                {
                    return 0m;
                }

                decimal subtotal = item.Precio * cantidad;

                // Descuento por puntos del cliente (ejemplo: 1 punto = $100)
                decimal descuentoPuntos = cliente.Puntos > 0
                    ? (cliente.Puntos / 100m) * subtotal
                    : 0m;

                return subtotal - Math.Min(descuentoPuntos, subtotal);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculando total: {ex.Message}");
                return 0m;
            }
        }

        /// <summary>
        /// Aplica un descuento convenio al precio unitario de un item.
        /// Sigue DIP: recibe abstracción Convenio que usa IDescuento internamente.
        /// </summary>
        public decimal AplicarDescuento(
            decimal precio,
            Convenio convenio)
        {
            try
            {
                if (convenio == null || precio <= 0)
                {
                    return precio;
                }

                return convenio.AplicarDescuento(precio);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error aplicando descuento: {ex.Message}");
                return precio;
            }
        }

        /// <summary>
        /// Obtiene un item vendible por nombre.
        /// </summary>
        public IVendible? ObtenerItem(string nombre)
        {
            return itemsVendibles.FirstOrDefault(
                i => i.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }
    }
}
