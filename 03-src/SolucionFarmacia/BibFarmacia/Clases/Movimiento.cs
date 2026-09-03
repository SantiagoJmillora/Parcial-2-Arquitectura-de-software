using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Clases
{
    /// <summary>
    /// Clase que registra movimientos de items vendibles (productos o servicios).
    /// Refactorizada para depender de IVendible en lugar de Producto concreto.
    /// Aplica DIP (Dependency Inversion Principle): depende de abstracción, no de detalles.
    /// </summary>
    public class Movimiento
    {
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; }
        public IVendible Vendible { get; set; }

        public Movimiento(DateTime fecha,
            int cantidad,
            string tipo,
            IVendible vendible)
        {
            Fecha = fecha;
            Cantidad = cantidad;
            Tipo = tipo;
            Vendible = vendible;
        }
    }
}