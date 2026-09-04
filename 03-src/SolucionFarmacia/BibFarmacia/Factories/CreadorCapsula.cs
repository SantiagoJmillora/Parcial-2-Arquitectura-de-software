using System;
using BibFarmacia.Clases;
using BibFarmacia.Enum;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    /// <summary>
    /// Implementación de IProductoCreador para MedicamentoCapsula.
    /// stockMinimo y fechaVencimiento vienen del llamador (ej. el archivo de
    /// productos); el único valor específico de cápsulas que esta clase
    /// decide es TipoRelleno.Gel, igual que hacía RepositorioProductoArchivo
    /// antes de conectarse a la fábrica.
    /// </summary>
    public class CreadorCapsula : IProductoCreador
    {
        public Producto Crear(
            string nombre,
            decimal precio,
            int stock,
            int stockMinimo,
            DateTime fechaVencimiento,
            Laboratorio laboratorio)
        {
            return new MedicamentoCapsula(
                nombre,
                precio,
                stock,
                stockMinimo,
                fechaVencimiento,
                laboratorio,
                TipoRelleno.Gel);
        }
    }
}
