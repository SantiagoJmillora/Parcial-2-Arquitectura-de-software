using System;
using BibFarmacia.Interfaces;
using BibFarmacia.Servicios;

namespace BibFarmacia.Comandos
{
    /// <summary>
    /// ConcreteCommand: opción 1 del menú (antes case 1 del switch en Program.cs).
    /// </summary>
    public class ComandoVerProductos : IComandoMenu
    {
        private readonly FarmaciaFacade _facade;

        public ComandoVerProductos(FarmaciaFacade facade)
        {
            _facade = facade;
        }

        public void Ejecutar()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(
                "\n===== PRODUCTOS =====");

            Console.ResetColor();

            Console.WriteLine(
                "Nombre\t\tStock\tPrecio");

            Console.WriteLine(
                "-----------------------------------");

            foreach (var producto in _facade.ObtenerProductos())
            {
                Console.WriteLine(
                    $"{producto.Nombre}\t\t" +
                    $"{producto.Stock}\t" +
                    $"{producto.Precio}");
            }
        }
    }
}
