using System;
using BibFarmacia.Interfaces;
using BibFarmacia.Servicios;

namespace BibFarmacia.Comandos
{
    /// <summary>
    /// ConcreteCommand: opción 5 del menú (antes case 5 del switch en Program.cs).
    /// </summary>
    public class ComandoAcumularPuntos : IComandoMenu
    {
        private readonly FarmaciaFacade _facade;

        public ComandoAcumularPuntos(FarmaciaFacade facade)
        {
            _facade = facade;
        }

        public void Ejecutar()
        {
            Console.Write(
                "\nNombre cliente: ");

            string nombreCliente =
                Console.ReadLine()!;

            var clientePuntos =
                _facade.BuscarCliente(nombreCliente);

            if (clientePuntos != null)
            {
                Console.Write(
                    "Puntos: ");

                int puntos =
                    int.Parse(
                        Console.ReadLine()!);

                _facade.AcumularPuntos(
                    clientePuntos,
                    puntos);
            }
            else
            {
                Console.WriteLine(
                    "\nCliente no encontrado");
            }
        }
    }
}
