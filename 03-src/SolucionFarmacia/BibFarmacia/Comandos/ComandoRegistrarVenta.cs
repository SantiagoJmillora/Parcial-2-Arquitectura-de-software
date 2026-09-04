using System;
using BibFarmacia.Interfaces;
using BibFarmacia.Servicios;

namespace BibFarmacia.Comandos
{
    /// <summary>
    /// ConcreteCommand: opción 4 del menú (antes case 4 del switch en Program.cs).
    /// Delega en FarmaciaFacade — no decide reglas de negocio propias.
    ///
    /// SC-3 (Reto 2, Anexo B — solicitud de cambio elegida por el equipo):
    /// se agrega el paso "Nombre cliente" (opcional, Enter para omitir) para
    /// poder aplicar el descuento de convenio real. Es el único cambio de
    /// comportamiento observable autorizado en todo Reto 2 — ver
    /// Actividad_4.2_Evidencia_Comportamiento.docx, adenda SC-3.
    /// </summary>
    public class ComandoRegistrarVenta : IComandoMenu
    {
        private readonly FarmaciaFacade _facade;

        public ComandoRegistrarVenta(FarmaciaFacade facade)
        {
            _facade = facade;
        }

        public void Ejecutar()
        {
            Console.Write(
                "\nNombre producto: ");

            string nombreVenta =
                Console.ReadLine()!;

            var productoVenta =
                _facade.BuscarProducto(nombreVenta);

            if (productoVenta != null)
            {
                Console.Write(
                    "Cantidad: ");

                int cantidad =
                    int.Parse(
                        Console.ReadLine()!);

                Console.Write(
                    "Nombre cliente (Enter para omitir): ");

                string nombreCliente =
                    Console.ReadLine()!;

                var cliente =
                    string.IsNullOrWhiteSpace(nombreCliente)
                        ? null
                        : _facade.BuscarCliente(nombreCliente);

                decimal total =
                    _facade.RegistrarVenta(
                        productoVenta,
                        cliente,
                        cantidad);

                Console.WriteLine(
                    "\nVenta registrada");

                // Solo se agrega una línea de salida cuando el descuento
                // realmente aplica — una venta sin cliente o con un cliente
                // sin convenio imprime exactamente lo mismo que antes de
                // SC-3 ("Venta registrada" y nada más).
                if (cliente?.Convenio != null)
                {
                    Console.WriteLine(
                        $"Total con descuento de convenio " +
                        $"({cliente.Convenio.Nombre}): {total}");
                }
            }
            else
            {
                Console.WriteLine(
                    "\nProducto no encontrado");
            }
        }
    }
}
