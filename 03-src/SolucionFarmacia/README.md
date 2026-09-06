# SolucionFarmacia — guía de lectura del código (Reto 2)

> Piensen en esto como la clase de recuperación antes del parcial: no repite lo que ya está en los documentos de `Actividades/`, sino que les explica el código mismo, línea por línea de razonamiento, para que cualquiera del equipo pueda abrir un archivo `.cs` y entender no solo *qué* hace sino *por qué* está escrito así.

## 0. Antes de empezar: ¿de dónde venimos?

Este código es el mismo sistema de farmacia del Reto 1 (SOLID), con una diferencia: en Reto 1 el equipo dejó **construida pero sin conectar** una buena parte de la infraestructura que este reto necesitaba. No es que hiciera falta escribir clases nuevas desde cero — es que existían clases como `ProductoFactory`, `ServicioVentas` o `ServicioNotificacion` que literalmente **nadie llamaba**. Se los pueden imaginar como herramientas nuevas que alguien compró, guardó en la caja de herramientas, y nunca sacó de la caja.

Lo que hizo Reto 2 fue, en resumen: **sacar esas herramientas de la caja y usarlas**, y donde hacía falta una herramienta que no existía (como una forma ordenada de manejar las opciones del menú), construirla siguiendo el mismo estilo del resto del código.

Si quieren el diagnóstico completo de qué estaba mal y por qué, está en `Actividades/Actividad 1/Actividad1_PuntosDolor.docx`. Aquí nos vamos directo al código.

## 1. Cómo está organizado el proyecto

```
03-src/SolucionFarmacia/
├── AppFarmaciaConsola/       ← el programa que se ejecuta (el "main")
│   ├── Program.cs            ← composition root: aquí se arma todo el sistema
│   ├── productos.txt         ← datos de productos (se cargan al iniciar)
│   ├── clientes.txt          ← datos de clientes
│   └── usuarios.txt          ← credenciales de login
└── BibFarmacia/               ← la librería con toda la lógica del negocio
    ├── Clases/                ← Producto, Medicamento, Cliente, Convenio...
    ├── Interfaces/             ← los "contratos" (IVendible, IComandoMenu...)
    ├── Servicios/             ← ServicioProducto, FarmaciaFacade...
    ├── Comandos/               ← NUEVO en Reto 2: una clase por opción de menú
    ├── Factories/              ← ProductoFactory y sus "creadores"
    ├── Eventos/                ← EventoStockMinimo, EventoVencimiento...
    └── Repositorios/           ← quién lee cada archivo .txt
```

Una regla que van a ver repetida en todo el código: **`Program.cs` es el único lugar donde se escribe `new NombreDeClaseConcreta(...)` para las piezas importantes del sistema**. Eso se llama el *composition root* — el único punto donde el programa decide "voy a usar esta implementación específica". Todo lo demás recibe lo que necesita ya construido, por el constructor. Esto no es capricho: es lo que permite que, si mañana quieren cambiar cómo se notifica una alerta, solo tengan que tocar una línea de `Program.cs`, no perseguir el cambio por 10 archivos distintos.

## 2. Los 5 cambios, explicados como si fuera la primera vez que los ven

Cada uno de estos cambios sigue la misma receta: **(a)** un problema concreto que ya existía, **(b)** una idea general de diseño (un "patrón") que resuelve ese tipo de problema en general, y **(c)** cómo se ve esa idea aplicada aquí.

### 2.1. "¿Por qué siempre se carga el mismo tipo de producto, sin importar qué diga el archivo?"

**El problema:** abran `BibFarmacia/Repositorios/RepositorioProductoArchivo.cs` como estaba antes de este reto (pueden verlo con `git log` si quieren el histórico). El método `Cargar` hacía literalmente esto:

```csharp
MedicamentoCapsula medicamento = new MedicamentoCapsula(
    datos[0], decimal.Parse(datos[1]), int.Parse(datos[2]), int.Parse(datos[3]),
    DateTime.Parse(datos[4]), laboratorio, TipoRelleno.Gel);
```

No importa qué tan bien diseñado esté `MedicamentoLiquido` o cualquier otro tipo de producto: **este método siempre construye una cápsula**. Si mañana quieren vender un jarabe, o un cosmético, este método ni se entera.

**La idea general (Factory Method):** en vez de que quien *usa* un objeto decida directamente *cómo construirlo* (con un `new` a lo bruto), le delegan esa decisión a otra pieza especializada — una "fábrica" — que sabe construir cada variante. Quien pide el objeto solo dice "necesito un producto de tipo X", y la fábrica se encarga del resto.

**Cómo se ve aquí:** ya existían `IProductoCreador` (el contrato: "cualquier cosa que sepa construir un producto"), y sus dos implementaciones `CreadorCapsula` y `CreadorLiquido` — pero nadie las llamaba. Lo que hicimos fue conectar el cable que faltaba:

```csharp
// RepositorioProductoArchivo.cs, ahora:
Producto producto = ProductoFactory.CrearPorTipo(
    tipo, datos[0], decimal.Parse(datos[1]), int.Parse(datos[2]),
    int.Parse(datos[3]), DateTime.Parse(datos[4]), laboratorio);
```

Y `ProductoFactory.CrearPorTipo` decide, según el texto que venga en una columna nueva y opcional del archivo (`tipo`), si construir una cápsula o un líquido:

```csharp
// ProductoFactory.cs
public static Producto CrearPorTipo(string? tipo, ...)
{
    return (tipo?.Trim().ToLowerInvariant()) switch
    {
        "liquido" => CrearLiquido(...),
        _ => CrearCapsula(...),   // por defecto: cápsula, igual que antes
    };
}
```

**Detalle importante para que no se asusten al leer el diff:** tuvimos que cambiarle la firma a `IProductoCreador.Crear(...)` para que reciba `stockMinimo` y `fechaVencimiento` como parámetros, en vez de que cada creador los tuviera hardcodeados (`CreadorCapsula` ponía siempre `StockMinimo=5` y `FechaVencimiento=Now+6meses`, sin importar lo que dijera el archivo real). Si no hacíamos ese cambio, conectar la fábrica habría *cambiado* los datos de vencimiento de todos los productos — justo lo que no podíamos hacer.

**Por qué el archivo `productos.txt` no cambió:** como esa columna `tipo` es opcional, y el archivo real no la trae, el comportamiento para los datos de hoy es exactamente el mismo de siempre. La ganancia es que *ahora sí se puede* agregar un producto de otro tipo sin tocar el `RepositorioProductoArchivo`.

### 2.2. "¿Por qué el descuento de un convenio nunca se aplicaba?"

**El problema:** `ServicioVentas.AplicarDescuento(precio, convenio)` existía, `Convenio` existía, `IDescuento`/`ServicioDescuento` existían — pero ninguna venta real los llamaba. `Cliente` ni siquiera tenía dónde guardar su convenio.

**La idea general (Strategy):** cuando hay varias formas de calcular algo (en este caso, un descuento) y esa forma puede cambiar según el caso, en vez de meter un montón de `if` dentro del método que vende, cada forma de calcular se encapsula en su propia clase, todas con el mismo contrato. El código que vende no necesita saber *cómo* se calcula el descuento — solo le pide el resultado a quien corresponda.

**Cómo se ve aquí:** esto es, de hecho, la **solicitud de cambio del Anexo B que el equipo decidió implementar en este reto (SC-3: convenios y descuentos)**. Le agregamos a `Cliente` la posibilidad de tener un convenio:

```csharp
// Cliente.cs
public Convenio? Convenio { get; set; }   // null si no tiene
```

Y cuando se registra una venta, si el cliente tiene convenio, el total pasa de verdad por la cadena Strategy:

```csharp
// FarmaciaFacade.cs
if (cliente?.Convenio != null)
{
    return _servicioVentas.AplicarDescuento(subtotal, cliente.Convenio);
}
return subtotal;
```

Prueben venderle algo a "Carlos" (tiene un convenio del 15% cargado en `clientes.txt` para que puedan verlo funcionar) y comparen con vendérselo a cualquier otro cliente.

**Una decisión que tomamos a propósito:** activar este descuento *sí* cambia lo que ve el usuario (antes nadie pagaba menos por convenio, ahora sí) — pero esto es exactamente lo que el reto permite: la única excepción autorizada a "no cambiar el comportamiento" es implementar una de las solicitudes del Anexo B, y esta es la que el equipo eligió.

### 2.3. "¿Por qué había 4 clases de evento casi idénticas, y por qué se repetía el mismo bloque de colores 4 veces?"

**El problema:** en el `Program.cs` viejo, cada uno de los 4 eventos (`EventoStockMinimo`, `EventoVencimiento`, `EventoPuntos`, `EventoMovimiento`) se suscribía con una lambda que repetía, letra por letra, el mismo patrón:

```csharp
servicioProducto.EventoStock.StockMinimo += mensaje =>
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(mensaje);
    Console.ResetColor();
};
```
...cuatro veces, cambiando solo el color y el nombre del evento.

**La idea general (Observer):** cuando algo pasa en un lugar del sistema (un evento) y hay una o varias partes interesadas en enterarse, en vez de que cada evento sepa *cómo mostrarse* (mezclando "qué pasó" con "cómo se muestra"), el evento simplemente avisa, y quien está suscrito decide qué hacer con ese aviso. Esto permite que mañana agreguen un segundo suscriptor (por ejemplo, guardar la alerta en un archivo además de mostrarla) sin tocar el evento para nada.

**Cómo se ve aquí:** `ServicioNotificacion` (que ya existía, sin usar) ahora es el único responsable de imprimir con color:

```csharp
// ServicioNotificacion.cs
public void EnviarNotificacion(string mensaje, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(mensaje);
    Console.ResetColor();
}
```

Y `Program.cs` pasa de 4 bloques de 5 líneas cada uno a 4 líneas de una sola línea:

```csharp
servicioProducto.EventoStock.StockMinimo +=
    mensaje => servicioNotificacion.EnviarNotificacion(mensaje, ConsoleColor.Red);
```

El color sigue siendo distinto por tipo de alerta (eso no cambió), pero el "cómo se imprime" vive en un solo lugar.

### 2.4. "¿Por qué el menú era un switch de 8 casos con toda la lógica adentro?"

**El problema:** abran el `Program.cs` viejo y busquen el `switch (opcion)`. Cada `case` tenía entre 10 y 50 líneas mezclando: pedir datos por consola, buscar en una lista, hacer el cálculo, y mostrar el resultado — todo en el mismo bloque, dentro del mismo archivo que también hacía la composición de objetos, el login y la carga de archivos.

**La idea general (Command):** cada acción que el usuario puede pedir se convierte en un objeto propio, con un único método (`Ejecutar()`). El código que maneja el menú deja de saber *qué hace* cada opción — solo sabe que tiene una lista de acciones y que, cuando el usuario elige un número, tiene que ejecutar la acción correspondiente.

**Cómo se ve aquí:** carpeta `BibFarmacia/Comandos/` — una clase por cada opción del menú (`ComandoVerProductos`, `ComandoRegistrarVenta`, etc.), todas implementando `IComandoMenu`:

```csharp
public interface IComandoMenu
{
    void Ejecutar();
}
```

Y `Program.cs` pasó de un `switch` de 200+ líneas a esto:

```csharp
Dictionary<int, IComandoMenu> comandos = new()
{
    { 1, new ComandoVerProductos(facade) },
    { 2, new ComandoVerClientes(facade) },
    // ... una entrada por opción
};
// ...
else if (comandos.TryGetValue(opcion, out var comando))
{
    comando.Ejecutar();
}
```

Si mañana quieren agregar la opción 9, no tocan este archivo casi nada: crean `ComandoOpcionNueva.cs` y agregan una línea al diccionario.

### 2.5. "¿Por qué `Program.cs` llamaba a 4 servicios distintos en cada opción del menú?"

**El problema:** cada `ComandoXxx` (o, antes, cada `case`) necesitaba hablar directamente con `ServicioProducto`, `ServicioCliente`, `ServicioMovimiento` y, ahora, `ServicioVentas` — cuatro objetos distintos que había que pasar de un lado a otro.

**La idea general (Facade):** cuando varias partes del sistema necesitan coordinar los mismos servicios de la misma forma una y otra vez, se agrupa esa coordinación detrás de un único punto de entrada. El resto del sistema le habla a ese único punto, no a los cuatro servicios por separado.

**Cómo se ve aquí:** `BibFarmacia/Servicios/FarmaciaFacade.cs` es nueva. Expone lo que un `ComandoXxx` necesita (`ObtenerProductos`, `BuscarProducto`, `RegistrarVenta`, `VerificarAlertas`...) y por dentro decide a qué servicio real llamar.

**La regla que hay que respetar si tocan esta clase:** `FarmaciaFacade` **no debe tomar decisiones de negocio** — nada de calcular descuentos, decidir si hay stock suficiente, ni formatear texto para el usuario. Solo coordina llamadas a los servicios que ya existen. Si empiezan a meterle lógica propia, se vuelve exactamente el mismo problema que teníamos con `Program.cs` (una clase que termina haciendo de todo), solo que con otro nombre. Esto está vigilado como riesgo R-04 en `Actividades/Actividad 5/Actividad_5.1_Registro_Riesgos.docx`.

## 3. Cómo correr el proyecto

```bash
cd 03-src/SolucionFarmacia
dotnet build
cd AppFarmaciaConsola
dotnet run
```

Usuario de prueba: `admin` / contraseña `1234` (ver `usuarios.txt` para los demás).

Para probar el descuento de convenio: en la opción 4 (Registrar venta), cuando pregunte el nombre del cliente, escriban `Carlos`.

## 4. ¿Cómo sabemos que no rompimos nada?

Antes de tocar cualquier línea, se ejecutó el sistema con una serie de entradas por consola y se guardó la salida completa. Después de cada cambio, se volvió a ejecutar exactamente lo mismo y se comparó carácter por carácter. El detalle completo — con las entradas exactas y la salida cruda — está en:

```
Actividades/Actividad 4/Actividad_4.2_Evidencia_Comportamiento.docx
Actividades/Actividad 4/evidencia_transcripts/transcripts_completos.txt
```

Si van a hacer un cambio nuevo, la forma correcta de probarlo es la misma: correr esos mismos escenarios (o unos nuevos que toquen lo que cambiaron) antes y después, y comparar. No basta con "correlo una vez y mirar que no truene".

## 5. Lo que queda pendiente (para no sorprenderse si lo preguntan)

- **P-06** (los `int.Parse` de los menús no validan la entrada — si escriben una letra donde va un número, el programa se cae) sigue sin arreglarse **a propósito**: arreglarlo de verdad cambiaría cómo se comporta el sistema ante una entrada inválida, y eso solo se puede hacer si es parte de una solicitud de cambio autorizada. No lo era.
- **SC-1** (cosméticos/alimentos) y **SC-2** (servicios) tienen la infraestructura lista (`ProductoFactory` ya soporta un tipo nuevo, `Servicio` ya implementa `IVendible`) pero no están conectadas de punta a punta como sí lo está SC-3.
- `ServicioVentas.RegistrarVenta` (con su validación de "stock insuficiente") sigue sin un llamador real — `FarmaciaFacade` resta el stock directamente, igual que hacía `Program.cs` antes, para no cambiar ese comportamiento sin autorización.

Para el resto de la historia — puntos de dolor completos, por qué se descartaron otros patrones, la matriz de principios, los riesgos y las dos vistas para audiencias distintas — todo está en `Actividades/`, una carpeta por actividad del enunciado.

## 6. Auditoría SOLID posterior — revisión ítem por ítem

Después de esta refactorización se hizo una segunda auditoría SOLID independiente (`Auditoria/Auditoria_exacta_violaciones_SOLID_Farmacia.docx`) que listó 12 puntos. Aquí está la revisión de cada uno: si es una **violación real que hay que arreglar** o una **decisión de diseño deliberada** que se sostiene bajo las reglas del reto (comportamiento congelado, prohibido cambiar el estilo arquitectónico, prohibido contenedor de inyección de dependencias, y penalización de −0.3 por cada patrón de sobre‑ingeniería).

**Conclusión corta: ninguno de los 12 puntos deja una celda de la matriz SOLID en "Roto".** No se hizo ningún cambio de código a raíz de esta auditoría. 11 de los 12 son decisiones defendibles o tensiones ya declaradas en `Actividad_4.1_Matriz_Verificacion_SOLID.docx`; 1 (el #2) es una tensión real que se deja explícitamente para después de la entrega por riesgo de romper comportamiento.

| # | Punto de la auditoría | Veredicto | ¿Cambió código? |
|---|---|---|---|
| 1 | `ServicioVentas` — `if (item is Producto producto)` | Decisión de diseño (código muerto) | No |
| 2 | `ServicioCliente.Cargar` concentra responsabilidades | **Tensión real — diferida a post‑entrega** | No |
| 3 | `ProductoFactory.CrearPorTipo` — `switch` | Decisión de diseño (ya declarada como tensión compensada) | No |
| 4 | `IProductoCreador` acoplado a `Laboratorio` | Decisión de diseño (la auditoría misma dice que no es violación) | No |
| 5 | `ServicioProducto` depende de `EventoStockMinimo`/`EventoVencimiento` concretos | Decisión de diseño | No |
| 6 | `ServicioCliente` depende de `EventoPuntos` concreto | Decisión de diseño | No |
| 7 | `ServicioMovimiento` depende de `EventoMovimiento` concreto | Decisión de diseño | No |
| 8 | `EventoStockMinimo.Disparar(Producto)` | Decisión de diseño (la auditoría lo marca prioridad BAJA) | No |
| 9 | `EventoVencimiento.Disparar(Producto)` | Decisión de diseño (prioridad BAJA) | No |
| 10 | `Producto.MostrarInformacion()` usa `Console.WriteLine` | Decisión de diseño (ya compensada con `ObtenerInformacion()`) | No |
| 11 | `Servicio.MostrarInformacion()` usa `Console.WriteLine` | Decisión de diseño (igual que #10) | No |
| 12 | `FarmaciaFacade` vs `ServicioVentas` — dos caminos de venta | Decisión de diseño (documentada; el segundo camino es código muerto) | No |

### Detalle del razonamiento

**#1 — `ServicioVentas.RegistrarVenta` hace `if (item is Producto producto)` para restar stock.**
El `is Producto` dentro de un método que recibe `IVendible` es, en abstracto, un olor a OCP/DIP. Pero ese método **no tiene ningún llamador** en todo el proyecto (búsquenlo: solo se usa `ServicioVentas.AplicarDescuento`). El flujo de venta real (`FarmaciaFacade.RegistrarVenta`) no tiene ningún type check. "Arreglarlo" con polimorfismo obligaría a meterle semántica de `Stock` a `IVendible` — que también implementa `Servicio`, que no tiene stock — es decir, tocar la abstracción central para limpiar código que nunca se ejecuta. Se deja como está y documentado. Si el equipo quiere, la jugada segura es *borrar* los métodos sin uso de `ServicioVentas` (`RegistrarVenta`, `CalcularTotal`, `AgregarItem`, `ObtenerItems`, `ObtenerItem`), no reescribirlos — pero eso es opcional y post‑entrega.

**#2 — `ServicioCliente.Cargar` mezcla persistencia + parsing + construcción de dominio + presentación.**
Esta sí es una **tensión SRP real**. `Cargar` hace `File.Exists`, `File.ReadAllLines`, `Split`, `decimal.Parse`, `new Cliente`, `new ServicioDescuento`, `new Convenio` y un `Console.WriteLine` (en la rama de tasa inválida). Lo correcto sería extraer un `RepositorioClienteArchivo`, exactamente como ya se hizo con `RepositorioProductoArchivo` y `RepositorioUsuarioArchivo` en el Reto 1 — `ServicioCliente.Cargar` es el único que quedó sin ese tratamiento. **No se hace ahora** por tres razones: (a) es el mismo método donde vive la carga del convenio de SC‑3, el *único* cambio de comportamiento autorizado del reto, así que moverlo pone en riesgo lo único que no se puede romper; (b) obligaría a re‑ejecutar todos los escenarios de caracterización el día de la entrega; (c) es tarea de "terminar la limpieza SRP del Reto 1", no una decisión de patrón del Reto 2. Queda registrada como tensión conocida (la celda DIP de la matriz 4.1 ya la menciona) y como primera candidata de trabajo post‑entrega. Preservar al moverla: los mensajes `"Clientes cargados"` / `"Archivo no encontrado"` / `ex.Message` y la advertencia de tasa fuera de `[0,1]`.

**#3 — `ProductoFactory.CrearPorTipo` despacha con un `switch`.**
Tensión OCP real y **ya declarada** como "Tensionado pero compensado" en la matriz 4.1: agregar un tipo es una rama nueva en un `switch` de expresión que solo mapea texto → `IProductoCreador`, sin lógica de negocio, con `default` = comportamiento histórico (cápsula). La auditoría pide un "registro/resolución extensible". Para un sistema con **dos** tipos de producto, y con la regla explícita de no sobre‑ingeniería (−0.3) y de no meter contenedores, un registro abierto o resolución por reflexión sería precisamente el tipo de abstracción que el enunciado penaliza. Se mantiene el `switch`.

**#4 — `IProductoCreador.Crear(..., Laboratorio laboratorio)`.**
La auditoría misma dice "no es una violación SOLID inequívoca". Todos los creadores actuales construyen un `Medicamento`, que **requiere** `Laboratorio`. Un creador que no lo necesite (cosmético, alimento) solo existiría si se implementara SC‑1, que **no** es la solicitud de cambio elegida (se eligió SC‑3). Generalizar la firma hoy para un creador que no existe es generalidad especulativa (YAGNI). Se revisa cuando/si se implemente SC‑1.

**#5, #6, #7 — Los servicios dependen de clases `Evento*` concretas, no de interfaces.**
Las dependencias **ya están invertidas en construcción**: se inyectan por constructor y se arman en el composition root (eso fue el DIP del Reto 1). Que sean tipos concretos y no interfaces no las hace sustituibles de todos modos: cada `Evento*` tiene una firma de `Disparar` distinta (`Disparar(Producto)`, `Disparar(string, int)`, `Disparar(string)`) y un solo suscriptor. Una interfaz común `IEvento`/`IPublisher` para cuatro contenedores de delegado de una línea, en una app de consola, colapsaría cuatro cargas de datos distintas en un genérico y arriesgaría el texto exacto de las alertas — abstracción por la abstracción misma, que la auditoría también desaconseja ("no introducir complejidad innecesaria", "solo si se adopta una abstracción común… no es prioritario"). La fila Observer de la matriz 4.1 ya cubre esto: el canal de salida **sí** está detrás de `IServicioNotificacion` (ahí es donde importa para OCP); el lado Subject queda concreto, que es normal en Observer.

**#8, #9 — `EventoStockMinimo`/`EventoVencimiento` conocen `Producto`.**
La auditoría los marca prioridad **BAJA** y "solo junto con una estrategia común de eventos; no aisladamente". Un evento de alerta de inventario que conoce la entidad de inventario es acoplamiento natural: solo `Producto` tiene stock y vencimiento (los `Servicio` no), así que `Producto` es el tipo honesto ahí. Sin cambio.

**#10, #11 — `Producto.MostrarInformacion()` y `Servicio.MostrarInformacion()` escriben en consola.**
El Reto 1 ya hizo la separación correcta: extrajo `ObtenerInformacion()` (devuelve `string`) y dejó `MostrarInformacion()` como adaptador delgado para **no romper el contrato de `IVendible`**. Además, `MostrarInformacion()` **no se llama en ningún punto del flujo** — los menús (opciones 1 y 3) y la demostración arman su propia salida con `Console.WriteLine` inline. Es decir, la "mezcla" SRP vive en un método que es parte de un contrato pero que en ejecución nunca corre. Quitarle el `Console` o cambiar `IVendible` para eliminar el método sería SRP de papel con cero efecto observable y con riesgo sobre la abstracción central. Tensión menor, ya compensada, se acepta.

**#12 — `FarmaciaFacade.RegistrarVenta` y `ServicioVentas.RegistrarVenta` son dos caminos de venta con reglas distintas.**
`ServicioVentas.RegistrarVenta` **no tiene llamador** — funcionalmente hay un solo camino vivo, el de la Facade. La Facade **no delega** en `ServicioVentas.RegistrarVenta` a propósito: ese método tiene un guard de `"Stock insuficiente"` que cambiaría el comportamiento observable de una venta que supera el stock (hoy el sistema deja el stock en negativo sin quejarse — ese es el comportamiento congelado). Está explicado en el XML‑doc de `FarmaciaFacade` y en la sección 5 de este README. La auditoría coincide en que "una Facade puede coordinar" y que no es violación inequívoca. El riesgo R‑04 (Actividad 5.1) ya vigila que la Facade no crezca con lógica de negocio. Limpiar el código muerto de `ServicioVentas` es opcional y post‑entrega.

### Notas de la auditoría que se confirman

- **`Program.cs` no es una God Class.** Hace de composition root y de UI de consola — dos roles legítimos y acotados en una app de este tamaño. Ya no contiene lógica de negocio de cada opción de menú (eso son los `ComandoXxx`).
- **`Cliente.AcumularPuntos(int)` está sin uso** (el flujo real hace `cliente.Puntos += puntos` dentro de `ServicioCliente.AcumularPuntos`), pero se deja: es inofensivo y borrarlo solo por SOLID no aporta.
- **No se afirma que OCP/DIP estén "resueltos del todo".** Siguen en pie el `switch` de la fábrica (#3) y las dependencias concretas a `Evento*` (#5‑#9); están declaradas como tensiones compensadas en la matriz 4.1, no como principios intactos.
