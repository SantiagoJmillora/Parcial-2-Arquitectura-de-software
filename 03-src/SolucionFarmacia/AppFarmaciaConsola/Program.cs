using BibFarmacia.Aspectos;
using BibFarmacia.Clases;
using BibFarmacia.Eventos;
using BibFarmacia.Factories;
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

// ================= EVENTOS =================

servicioProducto.EventoStock.StockMinimo +=
    mensaje =>
    {
        Console.ForegroundColor =
            ConsoleColor.Red;

        Console.WriteLine(mensaje);

        Console.ResetColor();
    };

servicioProducto.EventoVencimiento.Vencimiento +=
    mensaje =>
    {
        Console.ForegroundColor =
            ConsoleColor.Yellow;

        Console.WriteLine(mensaje);

        Console.ResetColor();
    };

servicioCliente.EventoPuntos.PuntosAcumulados +=
    mensaje =>
    {
        Console.ForegroundColor =
            ConsoleColor.Green;

        Console.WriteLine(mensaje);

        Console.ResetColor();
    };

servicioMovimiento.EventoMovimiento
    .MovimientoRegistrado +=
    mensaje =>
    {
        Console.ForegroundColor =
            ConsoleColor.Cyan;

        Console.WriteLine(mensaje);

        Console.ResetColor();
    };

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

    switch (opcion)
    {
        case 1:

            Console.ForegroundColor =
                ConsoleColor.Cyan;

            Console.WriteLine(
                "\n===== PRODUCTOS =====");

            Console.ResetColor();

            Console.WriteLine(
                "Nombre\t\tStock\tPrecio");

            Console.WriteLine(
                "-----------------------------------");

            foreach (var producto in
                servicioProducto.ObtenerProductos())
            {
                Console.WriteLine(
                    $"{producto.Nombre}\t\t" +
                    $"{producto.Stock}\t" +
                    $"{producto.Precio}");
            }

            break;

        case 2:

            Console.ForegroundColor =
                ConsoleColor.Green;

            Console.WriteLine(
                "\n===== CLIENTES =====");

            Console.ResetColor();

            foreach (var cliente in
                servicioCliente.ObtenerClientes())
            {
                Console.WriteLine(
                    $"{cliente.Nombre} - " +
                    $"Puntos: {cliente.Puntos}");
            }

            break;

        case 3:

            Console.Write(
                "\nIngrese nombre producto: ");

            string nombre =
                Console.ReadLine()!;

            var productoBuscado =
                servicioProducto
                .ObtenerProductos()
                .FirstOrDefault(p =>
                    p.Nombre.ToLower()
                    .Contains(nombre.ToLower()));

            if (productoBuscado != null)
            {
                Console.WriteLine(
                    $"\nProducto: " +
                    $"{productoBuscado.Nombre}");

                Console.WriteLine(
                    $"Precio: " +
                    $"{productoBuscado.Precio}");

                Console.WriteLine(
                    $"Stock: " +
                    $"{productoBuscado.Stock}");
            }
            else
            {
                Console.WriteLine(
                    "\nProducto no encontrado");
            }

            break;

        case 4:

            Console.Write(
                "\nNombre producto: ");

            string nombreVenta =
                Console.ReadLine()!;

            var productoVenta =
                servicioProducto
                .ObtenerProductos()
                .FirstOrDefault(p =>
                    p.Nombre.ToLower()
                    .Contains(
                        nombreVenta.ToLower()));

            if (productoVenta != null)
            {
                Console.Write(
                    "Cantidad: ");

                int cantidad =
                    int.Parse(
                        Console.ReadLine()!);

                productoVenta.Stock -=
                    cantidad;

                Movimiento venta =
                    new Movimiento(
                        DateTime.Now,
                        cantidad,
                        "Venta",
                        productoVenta);

                servicioMovimiento
                    .RegistrarMovimiento(
                        venta);

                Console.WriteLine(
                    "\nVenta registrada");
            }
            else
            {
                Console.WriteLine(
                    "\nProducto no encontrado");
            }

            break;

        case 5:

            Console.Write(
                "\nNombre cliente: ");

            string nombreCliente =
                Console.ReadLine()!;

            var clientePuntos =
                servicioCliente
                .ObtenerClientes()
                .FirstOrDefault(c =>
                    c.Nombre.ToLower()
                    .Contains(
                        nombreCliente.ToLower()));

            if (clientePuntos != null)
            {
                Console.Write(
                    "Puntos: ");

                int puntos =
                    int.Parse(
                        Console.ReadLine()!);

                servicioCliente
                    .AcumularPuntos(
                        clientePuntos,
                        puntos);
            }
            else
            {
                Console.WriteLine(
                    "\nCliente no encontrado");
            }

            break;

        case 6:

            Console.WriteLine(
                "\nVerificando alertas...");

            servicioProducto
                .VerificarStock();

            servicioProducto
                .VerificarVencimiento();

            break;

        case 7:

            EjecutarDemostracion(
                servicioProducto,
                servicioCliente,
                servicioMovimiento);

            break;

        case 8:

            Console.ForegroundColor =
                ConsoleColor.Red;

            Console.WriteLine(
                "\nSaliendo del sistema...");

            Console.ResetColor();

            break;

        default:

            Console.WriteLine(
                "\nOpción inválida");

            break;
    }
}

static void EjecutarDemostracion(
    ServicioProducto servicioProducto,
    ServicioCliente servicioCliente,
    ServicioMovimiento servicioMovimiento)
{
    Console.ForegroundColor =
        ConsoleColor.Blue;

    Console.WriteLine(
        "\n===== DEMOSTRACIÓN DE USOS CLAVE =====");

    Console.ResetColor();

    var productos =
        servicioProducto.ObtenerProductos();
    var clientes =
        servicioCliente.ObtenerClientes();

    Console.WriteLine(
        $"Productos cargados: {productos.Count}");
    Console.WriteLine(
        $"Clientes cargados: {clientes.Count}");

    if (productos.Any())
    {
        var producto = productos.First();

        Console.WriteLine(
            $"\nProducto de demostración: {producto.Nombre}");
        Console.WriteLine(
            $"Precio: {producto.Precio}");
        Console.WriteLine(
            $"Stock actual: {producto.Stock}");

        Console.WriteLine(
            "\nBuscando el mismo producto por nombre...");

        var busqueda =
            productos.FirstOrDefault(p =>
                p.Nombre.ToLower()
                    .Contains(producto.Nombre.ToLower()));

        if (busqueda != null)
        {
            Console.WriteLine(
                $"Producto encontrado: {busqueda.Nombre}");
        }

        if (producto.Stock > 0)
        {
            Console.WriteLine(
                "\nRegistrando venta de 1 unidad...");

            producto.Stock -= 1;

            Movimiento movimiento =
                new Movimiento(
                    DateTime.Now,
                    1,
                    "Venta",
                    producto);

            servicioMovimiento
                .RegistrarMovimiento(movimiento);

            Console.WriteLine(
                "Venta registrada en el sistema.");
            Console.WriteLine(
                $"Stock después de la venta: {producto.Stock}");
        }
        else
        {
            Console.WriteLine(
                "No hay stock suficiente para la venta de demostración.");
        }
    }
    else
    {
        Console.WriteLine(
            "\nNo hay productos cargados para la demostración.");
    }

    if (clientes.Any())
    {
        var cliente = clientes.First();

        Console.WriteLine(
            $"\nCliente de demostración: {cliente.Nombre}");
        Console.WriteLine(
            $"Puntos actuales: {cliente.Puntos}");

        Console.WriteLine(
            "Acumulando 50 puntos al cliente...");

        servicioCliente.AcumularPuntos(
            cliente,
            50);

        Console.WriteLine(
            $"Puntos después de la demostración: {cliente.Puntos}");
    }
    else
    {
        Console.WriteLine(
            "\nNo hay clientes cargados para la demostración.");
    }

    Console.WriteLine(
        "\nVerificando alertas de stock y vencimiento...");

    servicioProducto.VerificarStock();
    servicioProducto.VerificarVencimiento();

    Console.WriteLine(
        "\nDemostración completada. Revise los mensajes anteriores para ver los eventos y resultados.");
}

Console.WriteLine(
    "\nFIN DEL SISTEMA");