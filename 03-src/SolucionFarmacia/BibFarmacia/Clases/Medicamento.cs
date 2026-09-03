using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibFarmacia.Enum;

namespace BibFarmacia.Clases
{
    /// <summary>
    /// Clase que representa un medicamento, subtipo de Producto.
    /// Adiciona la relación con Laboratorio.
    /// Sigue SRP al tener una responsabilidad única: medicamentos.
    /// </summary>
    public class Medicamento : Producto
    {
        public Laboratorio Laboratorio { get; set; }

        public Medicamento(string nombre,
            decimal precio,
            int stock,
            int stockMinimo,
            DateTime fechaVencimiento,
            Laboratorio laboratorio)
            : base(nombre, precio, stock,
                  stockMinimo, fechaVencimiento, TipoProducto.Farmaceutico)
        {
            Laboratorio = laboratorio;
        }
    }
}