namespace BibFarmacia.Interfaces
{
    /// <summary>
    /// Command (Reto 2, P-02/P-04/P-06/P-07). Cada opción del menú de
    /// Program.cs pasa de ser un case dentro de un switch de 8 casos a una
    /// clase con una sola responsabilidad. El Invoker (Program.cs) solo
    /// selecciona el comando y llama Ejecutar().
    /// </summary>
    public interface IComandoMenu
    {
        void Ejecutar();
    }
}
