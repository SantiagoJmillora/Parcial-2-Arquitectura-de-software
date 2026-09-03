---
name: open-closed
description: Evalúa código contra el principio OCP de SOLID, detectando puntos donde agregar comportamiento obliga a modificar código existente y proponiendo extensiones mediante interfaces, clases abstractas o patrones de diseño.
argument-hint: El código, diseño o cambio a revisar para validar OCP y proponer extensiones seguras.
---

# Open/Closed

Usa este skill cuando necesites revisar si una unidad de código está abierta para extensión y cerrada para modificación.

## Objetivo

Determinar si el diseño permite añadir nuevas variantes, reglas o comportamientos sin editar el código ya existente. Cuando detectes rigidez o crecimiento por modificaciones repetidas, prioriza sugerir:

- Nuevas estructuras con interfaces.
- Clases abstractas cuando exista un esqueleto común con pasos extensibles.
- Strategy para comportamiento intercambiable.
- Template Method para flujos con pasos variables.
- Factory para desacoplar creación de implementación concreta.
- Decorator para añadir responsabilidades sin tocar la clase base.
- Visitor para operar sobre estructuras estables con nuevas operaciones.

## Señales de alerta

Sospecha una violación de OCP si observas alguno de estos patrones:

- Crecimiento de cadenas de `if/else` o `switch` que discriminan por tipo concreto, `instanceof`, `getClass()` o enums de comportamiento.
- Dependencia directa de tipos concretos cuando el comportamiento variable podría expresarse mediante una interfaz o clase abstracta.
- Reglas de negocio o algoritmos variables que solo pueden ampliarse editando la clase actual.
- Campos o métodos que cambian con frecuencia y aparecen señalados por nombres sugestivos, comentarios como `// TODO agregar caso`, o lógica repetida con pequeñas variaciones.
- Clases marcadas como `final` o con constructores privados sin justificación cuando su responsabilidad representa comportamiento potencialmente variable.
- Métodos con múltiples responsabilidades condicionales que revelan que la clase actúa como punto único de modificación para varios casos de uso.

## Protocolo de evaluación

Evalúa cada unidad con este checklist:

1. Un método no debe contener cadenas de `if/else` o `switch` que discriminen por tipo concreto (`instanceof`, `getClass()`, enums de comportamiento) para decidir lógica de negocio.
2. Una clase no debe depender de tipos concretos de otras clases cuando el comportamiento variable pueda expresarse mediante una interfaz o clase abstracta.
3. Una clase que representa una regla de negocio o algoritmo variable debe exponer un punto de extensión (interfaz, método abstracto, o inyección de estrategia) en lugar de requerir edición directa para añadir casos nuevos.
4. Los campos o métodos que cambian con frecuencia deben señalarse como candidatos a extracción mediante polimorfismo cuando aparezcan como variaciones repetidas, TODOs de nuevos casos o lógica duplicada con ajustes menores.
5. Una clase no debe marcarse como `final` ni tener constructores privados sin justificación cuando su responsabilidad sea representar un comportamiento potencialmente variable.
6. Los métodos no deben tener múltiples responsabilidades condicionales que sugieran que la clase actúa como punto único de modificación para distintos casos de uso.
7. Toda sugerencia de refactor debe proponer una estructura concreta: nombre de la interfaz o clase abstracta, métodos públicos, y cómo se conectan las implementaciones existentes.

## Cómo evaluar

1. Identifica el punto donde hoy se añade un nuevo caso, variante o comportamiento.
2. Verifica si el cambio exigiría editar la clase actual, el método central o una cadena condicional existente.
3. Determina si el comportamiento variable puede extraerse a una abstracción estable.
4. Revisa si hay una jerarquía o estrategia implícita que hoy está codificada como condicional.
5. Comprueba si el diseño está mezclando creación, decisión y ejecución en la misma unidad.

## Qué proponer cuando OCP se rompe

Cuando detectes una violación, sugiere una salida concreta según el caso:

- Extraer una interfaz como `PaymentMethod`, `DiscountRule`, `ReportFormatter` o el nombre equivalente del dominio, con métodos que representen el comportamiento variable.
- Convertir la clase actual en un contexto/orquestador que reciba la abstracción por constructor, setter o fábrica.
- Reemplazar `switch` o `if/else` por una estrategia registrada en un mapa de implementaciones o por inyección de dependencia.
- Introducir una clase abstracta solo si existe un flujo común con pasos variables que pueda modelarse con Template Method.
- Usar Factory cuando el problema principal sea la selección o construcción de variantes concretas.
- Usar Decorator cuando la variación sea aditiva y apile responsabilidades.
- Usar Visitor cuando la estructura de objetos sea estable pero las operaciones nuevas cambien con frecuencia.

## Restricción contra sobreingeniería

No recomiendes extensión por herencia o polimorfismo si el conjunto de variantes es cerrado y estable por diseño, por ejemplo:

- Estados de una máquina de estados finita definida.
- Tipos de una enumeración de dominio inmutable.
- Casos totalmente exhaustivos que nunca crecerán y ya se usan como contrato explícito del dominio.

En esos casos, una cadena condicional o una tabla de despacho puede ser aceptable si el cierre del conjunto está justificado y documentado. Solo propone abstracciones adicionales si hay evidencia real de crecimiento, reutilización o variación futura.

## Formato de respuesta recomendado

Cuando hagas una evaluación, responde con esta estructura:

- Veredicto: cumple, incumple o es ambiguo.
- Evidencia: qué condicional, dependencia concreta o punto rígido obliga a modificar código existente.
- Impacto: por qué eso rompe la extensión sin modificación.
- Recomendación: una estructura concreta con nombre de interfaz o clase abstracta, métodos clave, y cómo conectar las implementaciones existentes.

## Ejemplos de uso

- Revisar un `switch` que agrega nuevos tipos de cálculo cada vez que aparece una nueva variante del negocio.
- Detectar una clase de servicio que depende de varias clases concretas donde bastaría una interfaz común.
- Proponer una estrategia para reemplazar validaciones condicionales repetidas con una jerarquía extensible.
- Rediseñar un flujo de formateo o exportación con Factory, Strategy o Decorator según el tipo de variación.

## Criterio práctico

Si para añadir un caso nuevo debes tocar el mismo método, repetir validaciones o extender una cadena de condiciones, el diseño no está cerrado para modificación. En ese escenario, prioriza extraer una abstracción concreta y conectar las variantes existentes con una implementación intercambiable.