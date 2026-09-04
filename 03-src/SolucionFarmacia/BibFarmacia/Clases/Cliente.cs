using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibFarmacia.Clases
{
    /// <summary>
    /// Convenio (Reto 2, P-02/SC-3): un cliente puede tener un convenio
    /// asociado. Es null por defecto — los clientes cargados desde el
    /// formato original de clientes.txt (sin columnas de convenio) se
    /// comportan exactamente igual que antes de este cambio.
    /// </summary>
    public class Cliente : Persona
    {
        public int Puntos { get; set; }
        public Convenio? Convenio { get; set; }

        public Cliente(string nombre, string cedula,
            string telefono, string correo)
            : base(nombre, cedula, telefono, correo)
        {
            Puntos = 0;
        }

        public void AcumularPuntos(int puntos)
        {
            Puntos += puntos;
        }
    }
}
