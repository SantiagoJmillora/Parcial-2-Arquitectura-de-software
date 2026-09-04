# PROJECT_STATE.md

> Se actualiza al final de cada sesión de trabajo. Refleja el avance REAL, no el planeado — para eso está `CLAUDE.md` (plan/reglas) y `Plan_de_trabajo_Reto_2_Patrones.docx` (cronograma).

## Última actualización
2026-09-04 — Revisión/corrección de Actividad 1 y 2, bitácora IA (2.2) creada, y diagramas de Actividad 3.1 (AS-IS recorte + TO-BE) creados con draw.io.

## Fase actual del plan
**Día 1-2 en curso** (según `AA Plan_de_trabajo_Reto_2_Patrones.docx`): el equipo copió el código a `03-src/SolucionFarmacia/`, reorganizó los entregables bajo `Actividades/`, y ya tiene borradores de Actividad 1, 2.1, 2.2 y 3.1 revisados/corregidos por Claude.

## Qué existe hoy en este repo
- `AA Reto2_Patrones_Enunciado_y_Rubrica.docx` — enunciado y rúbrica del reto.
- `AA Plan_de_trabajo_Reto_2_Patrones.docx` — cronograma de 4 días y distribución de roles del equipo.
- `CLAUDE.md` — guía de contexto persistente (reglas del reto, resumen del código heredado de Reto 1).
- `PROJECT_STATE.md` — este archivo.
- `03-src/SolucionFarmacia/` — código fuente copiado desde Reto 1 (dos proyectos: `AppFarmaciaConsola`, `BibFarmacia`). Ya incluye piezas más allá de lo documentado en Reto 1: `Repositorios/` (RepositorioProductoArchivo, RepositorioUsuarioArchivo), `Validadores/`, `Aspectos/` (AspectoAutenticacion), `Factories/CreadorCapsula.cs` y `CreadorLiquido.cs` implementando `IProductoCreador` — Factory Method ya está parcialmente construido pero sin usar (ver P-01 abajo).
- `Actividades/Actividad1_PuntosDolor.docx` — 7 puntos de dolor (P-01 a P-07). Revisado y corregido por Claude (ver Decisiones abajo).
- `Actividades/Actividad_2_Tabla_Decision_Patrones.docx` — tabla de decisión con 15 patrones evaluados, 5 adoptados (Factory Method, Facade, Observer, Command, Strategy). Revisada por Claude, sin cambios de contenido (ver pendiente de inconsistencia abajo).
- `Actividades/Actividad_2.2_Bitacora_Decisiones_IA.docx` — bitácora con 11 registros (B-01 a B-11), generada por Claude a partir de la revisión de Actividad 1 y 2 y de las decisiones tomadas en esa sesión (B-07, B-08, B-09 tienen contexto de conversación real; B-08 es un rechazo explícito del equipo).
- `Actividades/Actividad 3/Diagramas parcial 1/` — diagramas TO-BE del Reto 1 (`Diagrama refactorizado.drawio` + PNG), aportados por el equipo como referencia de estilo; sirven de AS-IS de partida para Reto 2 pero están desactualizados frente al código actual (no incluyen Repositorios/Validadores/Aspectos/IProductoCreador).
- `Actividades/Actividad 3/Diagrama_3.1_AS-IS_recorte.drawio` (+`.drawio.png`) y `Diagrama_3.1_TO-BE.drawio` (+`.drawio.png`) — Entregable 3.1, hechos por Claude con la skill de draw.io, basados en el código actual verificado con grep (no en el diagrama viejo de Parcial 1). Recorte: solo las clases que tocan los 5 patrones adoptados.

## Progreso por actividad del enunciado

| # | Actividad | Estado | Notas |
|---|-----------|--------|-------|
| 1 | Puntos de dolor (P-xx) | Hecho (borrador corregido) | 7 puntos, ≥5 requeridos. P-01/02/03 verificados contra código real por Claude (grep confirmó 0 referencias a ProductoFactory/ServicioVentas/Convenio/ServicioNotificacion fuera de sí mismos). Columnas de costo de P-04, P-05 y P-07 reescritas por Claude para medir el problema hoy (archivos/líneas/ocurrencias reales), no el costo de la solución. P-06 marcado como **"no se interviene"** — decisión de Claude, ver justificación abajo. Falta que el equipo revise en persona y decida cuáles de los 5 finales cuentan como "sin IA" (mínimo 3 lo exigen). |
| 2.1 | Tabla de decisión de patrones | Hecho (borrador validado) | 15 patrones evaluados (5 creacional, 4 estructural, 6 comportamiento) — cumple el mínimo de 6 con ≥2 por familia. 5 adoptados: Factory Method (P-01), Facade (P-02, con límite declarado por escrito), Observer (P-03), Command (P-02/04/07), Strategy (P-02). 10 descartados con justificación técnica (Abstract Factory y Singleton citan explícitamente las advertencias del Anexo A). Sin cambios necesarios. |
| 2.2 | Bitácora de decisiones IA | Hecho (borrador) | 11 registros en `Actividad_2.2_Bitacora_Decisiones_IA.docx` (mínimo 10). Incluye B-09 (P-06 = "no se interviene") y B-08 (rechazo explícito del equipo a precisar "Dónde" en P-04–P-07) — buen ejemplo de criterio propio. Falta que el equipo lo revise y pueda defender cualquier registro elegido al azar. |
| 3.1 | Diagramas TO-BE (2 diagramas) | Hecho (borrador) | `Actividades/Actividad 3/Diagrama_3.1_AS-IS_recorte.drawio` (+`.drawio.png`) y `Diagrama_3.1_TO-BE.drawio` (+`.drawio.png`). Recorte centrado solo en las clases que tocan los 5 patrones adoptados (no el sistema completo). AS-IS marca en rojo/naranja/gris qué se retira, cambia o está sin usar; TO-BE colorea gris=sin cambios, naranja=cambia, verde=nueva, con el rol de cada clase en el patrón (stereotype «Rol»). Layout del TO-BE rehecho a pedido del usuario para que no se vea "tan ordenado y secuencial" (arranques de cluster escalonados, pares lado a lado, separaciones irregulares) — mismo contenido, look menos templado. Se añadió `Actividad_3.1_Guia_Diagrama_AS-IS.docx`: explica para compañeros/profesor qué muestra el AS-IS y por qué, sección por sección. |
| 3.2 | Tabla de cambio estructural | Hecho (borrador) | `Actividades/Actividad 3/Actividad_3.2_Tabla_Cambio_Estructural.docx` — 13 elementos (E-01 a E-13) cubriendo los 5 patrones adoptados, con Estado (Sale/Entra/Se transforma), qué hacía antes, qué hace ahora, y quién se reconecta a él. Ningún "Sale" literal — nada se borra, ver nota en el propio doc. P-06 excluido a propósito (no se interviene). |
| 3.3 | Fichas por patrón adoptado | Hecho (borrador) | `Actividades/Actividad 3/Actividad_3.3_Fichas_Patrones.docx` — 5 fichas (Factory Method, Strategy, Observer, Command, Facade), 1 página c/u, con los 7 campos del enunciado. Cada "Alternativas evaluadas" incluye ≥2 opciones + "no hacer nada", tomadas de los descartes reales de Actividad 2 (no inventadas). Cruza referencias con P-xx, bitácora IA (B-01/02/03/06/10) y la tabla 3.2. |
| 4.1 | Matriz de verificación SOLID | Hecho (borrador) | `Actividades/Actividad 4/Actividad_4.1_Matriz_Verificacion_SOLID.docx` — 5 patrones × 5 principios (25 celdas): 13 Refuerza, 7 Tensionado pero compensado, 5 Neutro, 0 Roto. Cada celda no-Neutro tiene evidencia. Incluye verificación explícita de los 5 "errores típicos" del enunciado (fábrica con condicional creciente, Facade que absorbe lógica, Singleton, Template Method, Decorator que cambia contrato) contra el diseño real. |
| 4.2 | Evidencia de comportamiento preservado | Hecho (borrador) | `Actividades/Actividad 4/Actividad_4.2_Evidencia_Comportamiento.docx` + `evidencia_transcripts/transcripts_completos.txt`. **Los 5 patrones ya están implementados en código real** (ver sección nueva abajo) — 8 escenarios ejecutados con `dotnet build` + entradas por stdin, diff antes/después: 8/8 idénticos byte a byte. Strategy queda conectado pero inactivo por defecto (activarlo cambiaría comportamiento observable; depende de si el equipo elige SC-3). |
| 5 | Registro de riesgos | Hecho (borrador) | `Actividades/Actividad 5/Actividad_5.1_Registro_Riesgos.docx` — 5 riesgos (mínimo 3). R-01 (tasa de descuento fuera de rango) y R-02 (parseo decimal sin cultura) están anclados a código real tocado en esta sesión; R-01 además se corrigió en el código (ver abajo) apenas se documentó. R-05 es un riesgo de proceso ligado al criterio de "criterio propio frente a la IA" de la rúbrica (20%). |
| 6 | Dos vistas (negocio / desarrollo) | No iniciado | |
| — | Código fuente copiado a este repo | **Hecho** | En `03-src/SolucionFarmacia/`. |
| — | Elección de solicitud Anexo B (SC-1/2/3, distinta a la de Reto 1) | No decidido todavía | Falta confirmar cuál SC implementó el equipo en Reto 1 para elegir otra. |
| — | Documento de sustentación (PDF ≤15 páginas) | No iniciado | |
| — | Video (20 min) | No iniciado | |

## Implementación de código (Actividad 4.2, 2026-09-04)
Los 5 patrones adoptados ya están en `03-src/SolucionFarmacia`, no solo diseñados:
- **Factory Method**: `IProductoCreador`/`CreadorCapsula`/`CreadorLiquido` ahora reciben `stockMinimo`/`fechaVencimiento` como parámetros (antes los hardcodeaban) para no perder los valores reales del archivo. `ProductoFactory.CrearPorTipo` despacha por diccionario/switch-expression según una 7ma columna "tipo" opcional. `RepositorioProductoArchivo` ya no hace `new MedicamentoCapsula(...)` directo.
- **Observer**: `IServicioNotificacion.EnviarNotificacion` ahora recibe `(mensaje, ConsoleColor color)` — reproduce exactamente las 3 líneas (ForegroundColor/WriteLine/ResetColor) que antes estaban duplicadas 4 veces inline en Program.cs.
- **Command**: `IComandoMenu` + 7 `ComandoXxx` nuevos en `BibFarmacia/Comandos/`. Program.cs pasó de un switch de 8 casos a un `Dictionary<int, IComandoMenu>`.
- **Facade**: `FarmaciaFacade` (nueva) coordina ServicioProducto/ServicioCliente/ServicioMovimiento. `RegistrarVenta` reproduce la lógica original **literal** (sin el chequeo de stock insuficiente que sí tiene `ServicioVentas`) para no cambiar comportamiento — ver nota de Strategy abajo.
- **Strategy = SC-3 (Anexo B), ACTIVADO.** El usuario confirmó que en Reto 1 se dejó infraestructura para las 3 solicitudes (SC-1/2/3), pero solo SC-2 (servicios) quedó operativa — SC-1 y SC-3 eran, en la práctica, P-01 y P-02. El equipo eligió SC-3 (recomendado por Claude: menor riesgo, no toca el formato de productos.txt). Cambios: `Cliente.Convenio` (nueva propiedad, null por defecto), `ServicioCliente.Cargar` acepta 2 columnas opcionales (nombre convenio, tasa), `FarmaciaFacade.RegistrarVenta` ahora recibe `Cliente?` y aplica el descuento real vía `ServicioVentas.AplicarDescuento → Convenio → IDescuento`, `ComandoRegistrarVenta` pregunta "Nombre cliente" (opcional). `clientes.txt`: Carlos tiene un convenio de demo (15%), las otras 9 líneas intactas.

Verificado con `dotnet build` (0 errores/warnings) y 9 escenarios ejecutados por stdin. 6 idénticos byte a byte antes/después; 2 (C-05/C-08, venta sin convenio) difieren solo en la nueva pregunta de cliente, nada más; 1 nuevo (C-11) confirma el descuento real (Carlos, $30.000 → $25.500). Detalle completo en `Actividades/Actividad 4/Actividad_4.2_Evidencia_Comportamiento.docx` + `evidencia_transcripts/`.

**Bug real encontrado y corregido durante la verificación:** `decimal.Parse("0.15")` sin especificar cultura, en la cultura del sistema (es-CO, donde "." es separador de miles), parseaba como 15 en vez de 0,15 → descuento de 1500% (-$420.000 en vez de $25.500). Corregido con `CultureInfo.InvariantCulture`. Vale la pena que el equipo tenga este caso presente para la sustentación — es evidencia real de verificación, no solo de implementación.

**Segundo fix (Actividad 5, mismo día):** al documentar R-01 en el registro de riesgos (tasa de descuento fuera de [0,1] → total negativo o mayor al precio de lista), se corrigió también en código: `ServicioCliente.Cargar` ahora valida el rango antes de asignar el `Convenio` y avisa por consola si la tasa es inválida, en vez de fallar en silencio. Re-verificado con los 9 escenarios de Actividad 4.2 — sin regresiones (todos idénticos).

## Decisiones tomadas hasta ahora
- **P-06 = "no se interviene"** (Actividad 1, decisión de Claude a pedido explícito del usuario). P-06 es la falta de manejo de errores en 3 parseos sin validar en `Program.cs` (opción de menú, cantidad, puntos — cada uno un `int.Parse(Console.ReadLine()!)` sin try/catch). Argumento: arreglarlo de verdad (capturar la excepción y decidir qué mostrar/hacer) cambia el comportamiento observable actual ante una entrada inválida, y la regla 1 del reto congela ese comportamiento salvo por la solicitud del Anexo B que se implemente — ningún SC del Anexo B depende de esto. El remedio arrastra un riesgo de romper una regla dura del reto a cambio de arreglar 3 líneas: el costo supera el problema. **Esta decisión hay que mantenerla consistente en el resto del proyecto**: P-06 no debe aparecer como punto de dolor atacado por ningún patrón adoptado en Actividad 2/3, ni en la matriz SOLID, ni en las vistas — se documenta pero se deja fuera del alcance de intervención de Reto 2.
- Ningún otro punto de dolor (P-01 a P-05, P-07) está marcado como "no se interviene": los 5 patrones adoptados en Actividad 2 sí los atacan.

## Próximos pasos (según cronograma del plan de trabajo)
1. El equipo revisa en persona todo lo generado (Actividades 1-5) y decide cuáles de los 5 puntos finales de Actividad 1 cuentan como "sin IA"; confirma la decisión de P-06 en la bitácora (Entregable 2.2).
2. Arrancar Actividad 6 (últimas pendientes): dos vistas — negocio (sin jerga técnica) y desarrollo ("dónde tocar", ≥5 filas cubriendo las 3 solicitudes del Anexo B aunque solo SC-3 se haya implementado).
3. Corregir la inconsistencia detectada en `Actividad_2_Tabla_Decision_Patrones.docx` (columna "Decisión" en "Pendiente" en la tabla larga de 15 patrones vs. "Adoptado" en la tabla-resumen).
4. Ensamblar el documento de sustentación final (PDF ≤15 páginas) a partir de todos los entregables ya generados.
5. Grabar el video de 20 minutos (fuera de lo que Claude puede producir — requiere a los 4 integrantes en cámara).

## Bloqueos / pendientes de información
- Ninguno crítico. Pendiente menor: corregir la columna "Decisión" inconsistente en `Actividad_2_Tabla_Decision_Patrones.docx` (ver Próximos pasos).
