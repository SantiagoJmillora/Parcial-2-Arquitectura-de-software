using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;
using BibFarmacia.Validadores;

namespace BibFarmacia.Aspectos
{
    /// <summary>
    /// Fachada estática de validación.
    /// Refactorización OCP: delega las reglas concretas a ValidadorCliente y
    /// ValidadorProducto (implementaciones de IValidadorCliente e IValidadorProducto).
    /// Para agregar nuevas reglas de validación se crean nuevas implementaciones
    /// de las interfaces sin modificar esta clase.
    /// Los contratos públicos (ValidarCliente, ValidarProducto) y los resultados
    /// devueltos son exactamente los mismos que antes.
    /// </summary>
    public static class AspectoValidacion
    {
        private static readonly IValidadorCliente _validadorCliente =
            new ValidadorCliente();

        private static readonly IValidadorProducto _validadorProducto =
            new ValidadorProducto();

        public static string ValidarCliente(
            Cliente cliente)
        {
            return _validadorCliente.Validar(cliente);
        }

        public static string ValidarProducto(
            Producto producto)
        {
            return _validadorProducto.Validar(producto);
        }
    }
}