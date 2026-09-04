using System;
using BibFarmacia.Interfaces;
using BibFarmacia.Servicios;

namespace BibFarmacia.Comandos
{
    /// <summary>
    /// ConcreteCommand: opción 3 del menú (antes case 3 del switch en Program.cs).
    /// </summary>
    public class ComandoBuscarProducto : IComandoMenu
    {
        private readonly FarmaciaFacade _facade;

        public ComandoBuscarProducto(FarmaciaFacade facade)
        {
            _facade = facade;
        }

        public void Ejecutar()
        {
            Console.Write(
                "\nIngrese nombre producto: ");

            string nombre =
                Console.ReadLine()!;

            var productoBuscado =
                _facade.BuscarProducto(nombre);

            if (productoBuscado != null)
            {
                Console.WriteLine(
                    $"\nProducto: " +
                    $"{productoBuscado.Nombre}");

                Console.WriteLine(
                    $"Precio: " +
                    $"{productoBuscado.Precio}");

                Console.WriteLine(
                    $"Stock: " +
                    $"{productoBuscado.Stock}");
            }
            else
            {
                Console.WriteLine(
                    "\nProducto no encontrado");
            }
        }
    }
}
