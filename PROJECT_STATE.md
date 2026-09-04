# PROJECT_STATE.md

> Se actualiza al final de cada sesión de trabajo. Refleja el avance REAL, no el planeado — para eso está `CLAUDE.md` (plan/reglas) y `AA Plan_de_trabajo_Reto_2_Patrones.docx` (cronograma).

## Última actualización
2026-09-04 — Auditoría completa del proyecto: corregida una inconsistencia real (tabla de decisión de Actividad 2) y restaurado contenido perdido (bitácora IA, editada fuera de estas sesiones). Las 6 actividades numeradas del enunciado tienen borrador completo, incluido código funcionando.

## Fase actual del plan
**Día 3-4 del cronograma** (según `AA Plan_de_trabajo_Reto_2_Patrones.docx`): código copiado, las 6 actividades del enunciado tienen borrador, SC-3 (Anexo B) implementado y verificado. Queda: revisión del equipo, el PDF de sustentación, y el video.

## Qué existe hoy en este repo
- `AA Reto2_Patrones_Enunciado_y_Rubrica.docx` — enunciado y rúbrica del reto.
- `AA Plan_de_trabajo_Reto_2_Patrones.docx` — cronograma de 4 días y distribución de roles del equipo.
- `CLAUDE.md` — guía de contexto persistente (reglas del reto, resumen del código heredado de Reto 1).
- `PROJECT_STATE.md` — este archivo.
- `03-src/SolucionFarmacia/` — código fuente, con los 5 patrones de Reto 2 ya implementados (ver sección "Implementación de código" abajo). `dotnet build`: 0 errores, 0 warnings.
- `Actividades/Actividad 1/Actividad1_PuntosDolor.docx` — 7 puntos de dolor (P-01 a P-07).
- `Actividades/Actividad 2/Actividad_2_Tabla_Decision_Patrones.docx` — 15 patrones evaluados, 5 adoptados. Columna "Decisión" de la tabla larga corregida (ver Decisiones abajo).
- `Actividades/Actividad 2/Actividad_2.2_Bitacora_Decisiones_IA.docx` — bitácora con **14 registros** (B-01 a B-14, mínimo exigido 10).
- `Actividades/Actividad 3/Diagramas parcial 1/` — diagramas TO-BE del Reto 1, aportados por el equipo como referencia de estilo; desactualizados frente al código actual, no se usaron como fuente de verdad.
- `Actividades/Actividad 3/3.1/` — `Diagrama_3.1_AS-IS_recorte.drawio` + `Diagrama_3.1_TO-BE.drawio` (cada uno con su `.drawio.png`) y `Actividad_3.1_Guia_Diagrama_AS-IS.docx`.
- `Actividades/Actividad 3/Actividad_3.2_Tabla_Cambio_Estructural.docx` y `Actividad_3.3_Fichas_Patrones.docx`.
- `Actividades/Actividad 4/Actividad_4.1_Matriz_Verificacion_SOLID.docx`, `Actividad_4.2_Evidencia_Comportamiento.docx` y `evidencia_transcripts/transcripts_completos.txt` (12 escenarios, entradas + salidas crudas).
- `Actividades/Actividad 5/Actividad_5.1_Registro_Riesgos.docx`.
- `Actividades/Actividad 6/Actividad_6.1_Vista_Negocio.docx` y `Actividad_6.2_Vista_Desarrollo.docx`.

## Progreso por actividad del enunciado

| # | Actividad | Estado | Notas |
|---|-----------|--------|-------|
| 1 | Puntos de dolor (P-xx) | Hecho (borrador corregido) | 7 puntos, ≥5 requeridos. P-01/02/03 verificados contra código real (grep). P-06 marcado **"no se interviene"**. Falta que el equipo decida cuáles de los 5 finales cuentan como "sin IA" (mínimo 3 lo exigen). |
| 2.1 | Tabla de decisión de patrones | Hecho | 15 patrones evaluados (mínimo 6 con ≥2 por familia), 5 adoptados, 10 descartados con justificación técnica. Columna "Decisión" de la tabla larga (antes decía "Pendiente" en las 15 filas) corregida para que coincida con la tabla-resumen. |
| 2.2 | Bitácora de decisiones IA | Hecho | 14 registros (mínimo 10). B-11 se había perdido en una edición del archivo hecha fuera de estas sesiones — restaurado. B-12/13/14 documentan la elección de SC-3, el bug de cultura (decimal.Parse) y la corrección de R-01. |
| 3.1 | Diagramas TO-BE (2 diagramas) | Hecho | Recorte centrado en las clases que tocan los 5 patrones adoptados. AS-IS marca en rojo/naranja/gris qué se retira, cambia o está sin usar; TO-BE colorea gris=sin cambios, naranja=cambia, verde=nueva. Layout no-uniforme a pedido del usuario. Incluye guía de lectura para compañeros/profesor. |
| 3.2 | Tabla de cambio estructural | Hecho | 13 elementos (E-01 a E-13). Ningún "Sale" literal — nada se borra. P-06 excluido a propósito. |
| 3.3 | Fichas por patrón adoptado | Hecho | 5 fichas (1 página c/u, 7 campos). Alternativas evaluadas incluyen "no hacer nada" en todos los casos, tomadas de descartes reales de Actividad 2. |
| 4.1 | Matriz de verificación SOLID | Hecho | 5×5 celdas: 13 Refuerza, 7 Tensionado pero compensado, 5 Neutro, 0 Roto. Los 5 "errores típicos" del enunciado verificados contra el diseño real. |
| 4.2 | Evidencia de comportamiento preservado | Hecho | **12 escenarios** (9 ejecutados antes/después con `dotnet build`, 3 verificados por comparación directa de código fuente por un problema de build en la copia aislada). Factory Method/Observer/Command/Facade: comportamiento 100% preservado. Strategy = SC-3, activado, cambio de comportamiento autorizado y documentado. |
| 5 | Registro de riesgos | Hecho | 5 riesgos (mínimo 3). R-01 y R-02 anclados a bugs reales encontrados en esta implementación; R-01 corregido en código. |
| 6 | Dos vistas (negocio / desarrollo) | Hecho | Vista de negocio escaneada contra la lista de términos prohibidos del enunciado — 0 coincidencias. Vista de desarrollo con la guía de "dónde tocar" (6 filas, mínimo 5, cubre las 3 solicitudes del Anexo B). |
| — | Código fuente + 5 patrones implementados | **Hecho** | `03-src/SolucionFarmacia/`. |
| — | Solicitud de Anexo B (distinta a Reto 1) | **Hecho: SC-3** | Convenios/descuentos activados de verdad en el flujo de venta. |
| — | README.md explicando el código | Hecho | Ver `03-src/SolucionFarmacia/README.md`. |
| — | Guion del video | Hecho | Ver `Guion_video.docx` (ubicación: raíz del repo). |
| — | Documento de sustentación (PDF ≤15 páginas) | **Hecho** | `Documento_Sustentacion.pdf` (+ `.docx` editable), raíz del repo. **13 páginas**, paginado ("Página X de 13"), con índice real (campo TOC de Word). Contiene, en el orden del enunciado, las 6 actividades condensadas — es la referencia rápida para la sustentación, no un compilado de los documentos completos (esos siguen en `Actividades/`). |
| — | Video (20 min) | No iniciado | Fuera de lo que Claude puede producir — requiere a los 4 integrantes en cámara. |

## Implementación de código (Actividad 4.2, 2026-09-04)
Los 5 patrones adoptados ya están en `03-src/SolucionFarmacia`, no solo diseñados:
- **Factory Method**: `IProductoCreador`/`CreadorCapsula`/`CreadorLiquido` reciben `stockMinimo`/`fechaVencimiento` como parámetros (antes los hardcodeaban) para no perder los valores reales del archivo. `ProductoFactory.CrearPorTipo` despacha según una 7ma columna "tipo" opcional. `RepositorioProductoArchivo` ya no hace `new MedicamentoCapsula(...)` directo.
- **Observer**: `IServicioNotificacion.EnviarNotificacion` recibe `(mensaje, ConsoleColor color)` — reproduce las 3 líneas (ForegroundColor/WriteLine/ResetColor) que antes estaban duplicadas 4 veces inline en Program.cs.
- **Command**: `IComandoMenu` + 7 `ComandoXxx` en `BibFarmacia/Comandos/`. Program.cs pasó de un switch de 8 casos a un `Dictionary<int, IComandoMenu>`.
- **Facade**: `FarmaciaFacade` (nueva) coordina ServicioProducto/ServicioCliente/ServicioMovimiento/ServicioVentas. `RegistrarVenta` reproduce la lógica original de stock/movimiento tal cual (sin el chequeo de "stock insuficiente" que sí tiene `ServicioVentas`, para no cambiar comportamiento sin autorización).
- **Strategy = SC-3 (Anexo B), ACTIVADO.** En Reto 1 se dejó infraestructura para las 3 solicitudes (SC-1/2/3), pero solo SC-2 (servicios) quedó operativa — SC-1 y SC-3 eran, en la práctica, P-01 y P-02. El equipo eligió SC-3. Cambios: `Cliente.Convenio` (nueva propiedad, null por defecto), `ServicioCliente.Cargar` acepta 2 columnas opcionales (nombre convenio, tasa, validada en rango [0,1]), `FarmaciaFacade.RegistrarVenta` recibe `Cliente?` y aplica el descuento real vía `ServicioVentas.AplicarDescuento → Convenio → IDescuento`, `ComandoRegistrarVenta` pregunta "Nombre cliente" (opcional). `clientes.txt`: Carlos tiene un convenio de demo (15%), las otras 9 líneas intactas.

**Verificación:** 12 escenarios (`Actividades/Actividad 4/evidencia_transcripts/transcripts_completos.txt`). Los que no tocan la venta: idénticos byte a byte antes/después. Los 2 que venden sin convenio: difieren solo en la nueva pregunta de cliente. C-11 confirma el descuento real (Carlos, $30.000 → $25.500). C-09/C-10/C-12 (rutas de error) verificados por comparación de código fuente — la copia aislada del código original tuvo un problema de build no relacionado con el contenido (ver nota en el propio documento 4.2).

**Bugs reales encontrados y corregidos durante la verificación** (vale la pena que el equipo los tenga presentes para la sustentación — son evidencia real, no solo de implementación):
1. `decimal.Parse("0.15")` sin especificar cultura, en la cultura del sistema (es-CO, donde "." es separador de miles), parseaba como 15 en vez de 0,15 → descuento de 1500% (-$420.000 en vez de $25.500). Corregido con `CultureInfo.InvariantCulture`.
2. Al documentar R-01 en el registro de riesgos (tasa fuera de [0,1] → cobro absurdo), se corrigió también en código: `ServicioCliente.Cargar` valida el rango antes de asignar el `Convenio`.

## Decisiones tomadas hasta ahora
- **P-06 = "no se interviene"** (Actividad 1). Falta de manejo de errores en 3 parseos sin validar en `Program.cs`. Arreglarlo cambiaría comportamiento observable sin autorización del Anexo B. **Se mantiene consistente en todo el proyecto**: P-06 no aparece como resuelto en ningún patrón, matriz SOLID, riesgo o vista.
- **SC-3 (convenios/descuentos/crédito) = la solicitud de cambio de Reto 2**, distinta de SC-2 (servicios), que ya funcionaba en Reto 1. Ver bitácora B-12.
- Ningún otro punto de dolor (P-01 a P-05, P-07) está marcado "no se interviene": los 5 patrones adoptados sí los atacan.

## Próximos pasos
**Todos los entregables que Claude puede producir están hechos.** Solo quedan cosas que requieren a las 4 personas:
1. El equipo revisa en persona todo lo generado, decide cuáles de los 5 puntos finales de Actividad 1 cuentan como "sin IA", y repasa la bitácora (14 registros) antes de la sustentación — varios (B-07/08/09/12/13/14) tienen contexto real y pueden salir elegidos al azar.
2. Presentar la vista de negocio (`Actividad_6.1`) a alguien no técnico y anotar qué entendió — prueba de validez exigida por el enunciado, debe quedar evidenciada en el video.
3. Revisar `Documento_Sustentacion.docx`/`.pdf` (13 páginas) antes de entregar: completar el nombre de los 4 integrantes en la portada (dice "completar con los 4 integrantes").
4. Grabar el video de 20 minutos — ver `Guion_video.docx` para el reparto de tiempo entre los 4 integrantes. Fuera de lo que Claude puede producir.
5. Si un teammate edita un .docx directamente en Word, avisar en la siguiente sesión — ya pasó una vez (bitácora IA) y se perdió contenido (restaurado, ver arriba) hasta que se detectó en esta auditoría.

## Bloqueos / pendientes de información
- Ninguno. Todo lo que dependía de una decisión del equipo (Anexo B) está resuelto.
