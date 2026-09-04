using BibFarmacia.Aspectos;
using BibFarmacia.Clases;
using BibFarmacia.Comandos;
using BibFarmacia.Eventos;
using BibFarmacia.Factories;
using BibFarmacia.Interfaces;
using BibFarmacia.Repositorios;
using BibFarmacia.Servicios;

Console.Title = "Sistema Farmacia";

// ================= COMPOSITION ROOT =================
// Construcción de todos los objetos concretos centralizada aquí.
// Los servicios reciben sus dependencias por inyección de constructor (DIP).

// Eventos (construidos aquí, inyectados en los servicios)
EventoStockMinimo eventoStock =
    new EventoStockMinimo();

EventoVencimiento eventoVencimiento =
    new EventoVencimiento();

EventoPuntos eventoPuntos =
    new EventoPuntos();

EventoMovimiento eventoMovimiento =
    new EventoMovimiento();

// Autenticación (implementación concreta de IAutenticacion)
AspectoAutenticacion autenticacion =
    new AspectoAutenticacion();

// Servicios con dependencias inyectadas
ServicioProducto servicioProducto =
    new ServicioProducto(
        eventoStock,
        eventoVencimiento);

ServicioCliente servicioCliente =
    new ServicioCliente(eventoPuntos);

ServicioUsuario servicioUsuario =
    new ServicioUsuario(autenticacion);

ServicioMovimiento servicioMovimiento =
    new ServicioMovimiento(eventoMovimiento);

// ServicioVentas (Reto 2, P-02/SC-3): antes tenía 0 referencias. Ahora la
// usa FarmaciaFacade.RegistrarVenta para aplicar el descuento de convenio
// (Strategy) cuando el cliente de la venta tiene uno asociado.
ServicioVentas servicioVentas =
    new ServicioVentas();

// Facade (Reto 2, P-02/P-04/P-07): punto de entrada único para las
// operaciones que antes coordinaba este archivo directamente.
FarmaciaFacade facade =
    new FarmaciaFacade(
        servicioProducto,
        servicioCliente,
        servicioMovimiento,
        servicioVentas);

// ================= EVENTOS =================
// Observer (Reto 2, P-03): antes cada evento se suscribía con una lambda que
// repetía Console.ForegroundColor/WriteLine/ResetColor (4 veces). Ahora cada
// evento se suscribe una sola vez a un único ConcreteObserver
// (ServicioNotificacion), que centraliza ese formateo. El color por tipo de
// alerta se preserva pasándolo como parámetro, para no cambiar la salida.

IServicioNotificacion servicioNotificacion =
    new ServicioNotificacion();

servicioProducto.EventoStock.StockMinimo +=
    mensaje =>
        servicioNotificacion.EnviarNotificacion(
            mensaje,
            ConsoleColor.Red);

servicioProducto.EventoVencimiento.Vencimiento +=
    mensaje =>
        servicioNotificacion.EnviarNotificacion(
            mensaje,
            ConsoleColor.Yellow);

servicioCliente.EventoPuntos.PuntosAcumulados +=
    mensaje =>
        servicioNotificacion.EnviarNotificacion(
            mensaje,
            ConsoleColor.Green);

servicioMovimiento.EventoMovimiento
    .MovimientoRegistrado +=
    mensaje =>
        servicioNotificacion.EnviarNotificacion(
            mensaje,
            ConsoleColor.Cyan);

// ================= CARGA TXT =================

Console.ForegroundColor =
    ConsoleColor.DarkGreen;

Console.WriteLine(
    "Cargando información del sistema...\n");

Console.ResetColor();

// H-03: La carga de archivo delega al repositorio especializado (SRP)
RepositorioProductoArchivo repositorioProducto =
    new RepositorioProductoArchivo();

string resultadoProductos =
    repositorioProducto.Cargar(
        "productos.txt",
        out var productosDesdeArchivo);

servicioProducto.CargarProductos(productosDesdeArchivo);

Console.WriteLine(resultadoProductos);

Console.WriteLine(
    servicioCliente.Cargar(
        "clientes.txt"));

// H-06: La carga de usuarios delega al repositorio especializado (SRP)
RepositorioUsuarioArchivo repositorioUsuario =
    new RepositorioUsuarioArchivo();

string resultadoUsuarios =
    repositorioUsuario.Cargar(
        "usuarios.txt",
        out var usuariosDesdeArchivo);

servicioUsuario.CargarUsuarios(usuariosDesdeArchivo);

Console.WriteLine(resultadoUsuarios);

Console.WriteLine();

// ================= LOGIN =================

Console.ForegroundColor =
    ConsoleColor.Blue;

Console.WriteLine(
    "=========== LOGIN ===========");

Console.ResetColor();

Console.Write("Usuario: ");
string user =
    Console.ReadLine()!;

Console.Write("Contraseña: ");
string password =
    Console.ReadLine()!;

bool login =
    servicioUsuario.Login(
        user,
        password);

if (!login)
{
    Console.ForegroundColor =
        ConsoleColor.Red;

    Console.WriteLine(
        "\nAcceso denegado");

    Console.ResetColor();

    return;
}

Console.ForegroundColor =
    ConsoleColor.Green;

Console.WriteLine(
    "\nLogin correcto");

Console.ResetColor();

// ================= ALERTAS =================

servicioProducto.VerificarStock();

servicioProducto.VerificarVencimiento();

// ================= MENÚ =================
// Command (Reto 2, P-02/P-04/P-06/P-07): el switch de 8 casos se reemplaza
// por un diccionario de comandos. Este archivo (Invoker) solo selecciona y
// ejecuta — ya no contiene la lógica de negocio de cada opción.

Dictionary<int, IComandoMenu> comandos =
    new()
    {
        { 1, new ComandoVerProductos(facade) },
        { 2, new ComandoVerClientes(facade) },
        { 3, new ComandoBuscarProducto(facade) },
        { 4, new ComandoRegistrarVenta(facade) },
        { 5, new ComandoAcumularPuntos(facade) },
        { 6, new ComandoVerAlertas(facade) },
        { 7, new ComandoEjecutarDemostracion(facade) },
    };

int opcion = 0;

while (opcion != 8)
{
    Console.ForegroundColor =
        ConsoleColor.Magenta;

    Console.WriteLine("\n==============================");
    Console.WriteLine("      SISTEMA FARMACIA");
    Console.WriteLine("==============================");

    Console.ResetColor();

    Console.WriteLine("1. Ver productos");
    Console.WriteLine("2. Ver clientes");
    Console.WriteLine("3. Buscar producto");
    Console.WriteLine("4. Registrar venta");
    Console.WriteLine("5. Acumular puntos");
    Console.WriteLine("6. Ver alertas");
    Console.WriteLine("7. Ejecutar demostración");
    Console.WriteLine("8. Salir");

    Console.Write("\nSeleccione opción: ");

    opcion =
        int.Parse(Console.ReadLine()!);

    if (opcion == 8)
    {
        Console.ForegroundColor =
            ConsoleColor.Red;

        Console.WriteLine(
            "\nSaliendo del sistema...");

        Console.ResetColor();
    }
    else if (comandos.TryGetValue(opcion, out var comando))
    {
        comando.Ejecutar();
    }
    else
    {
        Console.WriteLine(
            "\nOpción inválida");
    }
}

Console.WriteLine(
    "\nFIN DEL SISTEMA");
