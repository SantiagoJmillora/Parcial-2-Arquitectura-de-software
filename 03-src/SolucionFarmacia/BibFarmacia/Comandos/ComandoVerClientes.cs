using System;
using BibFarmacia.Interfaces;
using BibFarmacia.Servicios;

namespace BibFarmacia.Comandos
{
    /// <summary>
    /// ConcreteCommand: opción 2 del menú (antes case 2 del switch en Program.cs).
    /// </summary>
    public class ComandoVerClientes : IComandoMenu
    {
        private readonly FarmaciaFacade _facade;

        public ComandoVerClientes(FarmaciaFacade facade)
        {
            _facade = facade;
        }

        public void Ejecutar()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(
                "\n===== CLIENTES =====");

            Console.ResetColor();

            foreach (var cliente in _facade.ObtenerClientes())
            {
                Console.WriteLine(
                    $"{cliente.Nombre} - " +
                    $"Puntos: {cliente.Puntos}");
            }
        }
    }
}
