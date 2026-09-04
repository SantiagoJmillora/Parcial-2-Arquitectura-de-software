using System;
using System.Linq;
using BibFarmacia.Interfaces;
using BibFarmacia.Servicios;

namespace BibFarmacia.Comandos
{
    /// <summary>
    /// ConcreteCommand: opción 7 del menú (antes el método local
    /// EjecutarDemostracion en Program.cs). Misma secuencia de pasos y
    /// mismos mensajes, ahora orquestados a través de FarmaciaFacade.
    /// </summary>
    public class ComandoEjecutarDemostracion : IComandoMenu
    {
        private readonly FarmaciaFacade _facade;

        public ComandoEjecutarDemostracion(FarmaciaFacade facade)
        {
            _facade = facade;
        }

        public void Ejecutar()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(
                "\n===== DEMOSTRACIÓN DE USOS CLAVE =====");

            Console.ResetColor();

            var productos =
                _facade.ObtenerProductos();
            var clientes =
                _facade.ObtenerClientes();

            Console.WriteLine(
                $"Productos cargados: {productos.Count}");
            Console.WriteLine(
                $"Clientes cargados: {clientes.Count}");

            if (productos.Any())
            {
                var producto = productos.First();

                Console.WriteLine(
                    $"\nProducto de demostración: {producto.Nombre}");
                Console.WriteLine(
                    $"Precio: {producto.Precio}");
                Console.WriteLine(
                    $"Stock actual: {producto.Stock}");

                Console.WriteLine(
                    "\nBuscando el mismo producto por nombre...");

                var busqueda =
                    _facade.BuscarProducto(producto.Nombre);

                if (busqueda != null)
                {
                    Console.WriteLine(
                        $"Producto encontrado: {busqueda.Nombre}");
                }

                if (producto.Stock > 0)
                {
                    Console.WriteLine(
                        "\nRegistrando venta de 1 unidad...");

                    _facade.RegistrarVenta(producto, null, 1);

                    Console.WriteLine(
                        "Venta registrada en el sistema.");
                    Console.WriteLine(
                        $"Stock después de la venta: {producto.Stock}");
                }
                else
                {
                    Console.WriteLine(
                        "No hay stock suficiente para la venta de demostración.");
                }
            }
            else
            {
                Console.WriteLine(
                    "\nNo hay productos cargados para la demostración.");
            }

            if (clientes.Any())
            {
                var cliente = clientes.First();

                Console.WriteLine(
                    $"\nCliente de demostración: {cliente.Nombre}");
                Console.WriteLine(
                    $"Puntos actuales: {cliente.Puntos}");

                Console.WriteLine(
                    "Acumulando 50 puntos al cliente...");

                _facade.AcumularPuntos(
                    cliente,
                    50);

                Console.WriteLine(
                    $"Puntos después de la demostración: {cliente.Puntos}");
            }
            else
            {
                Console.WriteLine(
                    "\nNo hay clientes cargados para la demostración.");
            }

            Console.WriteLine(
                "\nVerificando alertas de stock y vencimiento...");

            _facade.VerificarAlertas();

            Console.WriteLine(
                "\nDemostración completada. Revise los mensajes anteriores para ver los eventos y resultados.");
        }
    }
}
