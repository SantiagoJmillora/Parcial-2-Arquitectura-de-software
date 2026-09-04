using System;
using BibFarmacia.Interfaces;
using BibFarmacia.Servicios;

namespace BibFarmacia.Comandos
{
    /// <summary>
    /// ConcreteCommand: opción 6 del menú (antes case 6 del switch en Program.cs).
    /// </summary>
    public class ComandoVerAlertas : IComandoMenu
    {
        private readonly FarmaciaFacade _facade;

        public ComandoVerAlertas(FarmaciaFacade facade)
        {
            _facade = facade;
        }

        public void Ejecutar()
        {
            Console.WriteLine(
                "\nVerificando alertas...");

            _facade.VerificarAlertas();
        }
    }
}
