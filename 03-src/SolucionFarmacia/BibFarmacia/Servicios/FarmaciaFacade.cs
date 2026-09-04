using System;
using System.Collections.Generic;
using System.Linq;
using BibFarmacia.Clases;

namespace BibFarmacia.Servicios
{
    /// <summary>
    /// Facade (Reto 2, P-02/P-04/P-07). Punto de entrada único para las
    /// operaciones que antes coordinaba Program.cs directamente en cada
    /// case del switch de menú: vender, cargar, buscar, verificar alertas.
    ///
    /// Límite declarado desde Actividad 2/3.3 (para no romper SRP como
    /// advierte el Anexo A): esta clase NO contiene reglas de stock,
    /// descuentos ni presentación — cada método aquí es exactamente la
    /// misma secuencia de llamadas que hacía Program.cs, solo movida de
    /// lugar. RegistrarVenta reproduce el comportamiento original de forma
    /// literal (incluida la ausencia de validación de stock suficiente):
    /// no pasa por ServicioVentas.RegistrarVenta porque esa clase valida
    /// stock insuficiente y por lo tanto cambiaría el comportamiento
    /// observable de una venta que agota o supera el stock disponible.
    /// </summary>
    public class FarmaciaFacade
    {
        private readonly ServicioProducto _servicioProducto;
        private readonly ServicioCliente _servicioCliente;
        private readonly ServicioMovimiento _servicioMovimiento;
        private readonly ServicioVentas _servicioVentas;

        public FarmaciaFacade(
            ServicioProducto servicioProducto,
            ServicioCliente servicioCliente,
            ServicioMovimiento servicioMovimiento,
            ServicioVentas servicioVentas)
        {
            _servicioProducto = servicioProducto;
            _servicioCliente = servicioCliente;
            _servicioMovimiento = servicioMovimiento;
            _servicioVentas = servicioVentas;
        }

        public List<Producto> ObtenerProductos()
        {
            return _servicioProducto.ObtenerProductos();
        }

        public List<Cliente> ObtenerClientes()
        {
            return _servicioCliente.ObtenerClientes();
        }

        public Producto? BuscarProducto(string nombre)
        {
            return ObtenerProductos()
                .FirstOrDefault(p =>
                    p.Nombre.ToLower().Contains(nombre.ToLower()));
        }

        public Cliente? BuscarCliente(string nombre)
        {
            return ObtenerClientes()
                .FirstOrDefault(c =>
                    c.Nombre.ToLower().Contains(nombre.ToLower()));
        }

        /// <summary>
        /// Registra la venta (stock + movimiento, idéntico al comportamiento
        /// original) y calcula el total a cobrar. Si el cliente tiene un
        /// convenio asociado, el total pasa por ServicioVentas.AplicarDescuento
        /// (Strategy, SC-3) — la primera vez que esa ruta se invoca con datos
        /// reales; si no hay cliente o no tiene convenio, el total es el
        /// precio de lista, igual que siempre.
        /// </summary>
        public decimal RegistrarVenta(
            Producto producto,
            Cliente? cliente,
            int cantidad)
        {
            producto.Stock -= cantidad;

            Movimiento venta = new Movimiento(
                DateTime.Now,
                cantidad,
                "Venta",
                producto);

            _servicioMovimiento.RegistrarMovimiento(venta);

            decimal subtotal = producto.Precio * cantidad;

            if (cliente?.Convenio != null)
            {
                return _servicioVentas.AplicarDescuento(
                    subtotal,
                    cliente.Convenio);
            }

            return subtotal;
        }

        public void AcumularPuntos(Cliente cliente, int puntos)
        {
            _servicioCliente.AcumularPuntos(cliente, puntos);
        }

        public void VerificarAlertas()
        {
            _servicioProducto.VerificarStock();
            _servicioProducto.VerificarVencimiento();
        }
    }
}
