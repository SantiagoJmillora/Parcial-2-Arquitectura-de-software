using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibFarmacia.Clases;
using BibFarmacia.Eventos;

namespace BibFarmacia.Servicios
{
    /// <summary>
    /// Responsabilidad: registrar y gestionar movimientos de inventario.
    /// Refactorización DIP: EventoMovimiento se inyecta por constructor en lugar
    /// de ser instanciado internamente con 'new', moviendo la construcción
    /// concreta al Composition Root (Program.cs).
    /// </summary>
    public class ServicioMovimiento
    {
        private List<Movimiento> movimientos;

        public EventoMovimiento EventoMovimiento;

        /// <summary>
        /// Constructor que recibe EventoMovimiento por inyección (DIP).
        /// </summary>
        public ServicioMovimiento(
            EventoMovimiento eventoMovimiento)
        {
            movimientos = new List<Movimiento>();

            EventoMovimiento = eventoMovimiento;
        }

        public void RegistrarMovimiento(
            Movimiento movimiento)
        {
            movimientos.Add(movimiento);

            EventoMovimiento.Disparar(
                movimiento.Tipo);
        }

        public List<Movimiento>
            ObtenerMovimientos()
        {
            return movimientos;
        }
    }
}