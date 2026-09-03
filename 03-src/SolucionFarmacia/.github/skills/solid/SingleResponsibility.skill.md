---
name: single-responsibility
description: Evalúa código contra el principio SRP de SOLID, detectando clases con más de una responsabilidad y proponiendo extracción, delegación, Facade, Strategy o separación por capas.
argument-hint: El código, clase o módulo a revisar para validar SRP y sugerir una descomposición clara.
---

# Single Responsibility

Usa este skill cuando necesites revisar si una clase, módulo o componente tiene una sola razón principal para cambiar.

## Objetivo

Determinar si una unidad concentra responsabilidades que deberían separarse. Cuando detectes baja cohesión o acoplamiento funcional excesivo, prioriza sugerir:

- Extracción de clases para separar capacidades independientes.
- Delegación para mover comportamiento a colaboradores especializados.
- Facade para simplificar el acceso a varios subsistemas sin concentrar lógica de negocio.
- Strategy cuando existan variantes de una misma decisión o algoritmo.
- Separación explícita por capas cuando la clase mezcle negocio, persistencia, presentación o integración.

## Señales de alerta

Sospecha una violación de SRP si observas alguno de estos patrones:

- Métodos que operan sobre subconjuntos disjuntos de los campos de la clase, sugiriendo baja cohesión interna.
- Mezcla de capas dentro de la misma clase, como lógica de negocio combinada con persistencia, presentación o logging.
- Alto fan-out, es decir, dependencias hacia múltiples componentes externos no relacionados entre sí.
- Cambios frecuentes concentrados en la misma clase, especialmente cuando una parte del código evoluciona por motivos distintos.
- La clase expone demasiadas acciones heterogéneas que no comparten un motivo de cambio claro.
- La clase coordina demasiadas decisiones distintas y termina funcionando como punto central de modificación.

## Protocolo de evaluación

Evalúa cada unidad con este checklist:

1. Detectar métodos que operan sobre subconjuntos disjuntos de los campos de la clase, como indicio de baja cohesión interna.
2. Detectar mezcla de capas dentro de la misma clase, como lógica de negocio combinada con persistencia, presentación o logging.
3. Detectar alto fan-out, es decir, dependencias hacia múltiples componentes externos no relacionados entre sí, como indicio de acoplamiento excesivo.
4. Detectar posibles múltiples actores o stakeholders que podrían solicitar cambios distintos sobre la misma clase, entendiendo "razón para cambiar" según la definición de Robert C. Martin; este criterio debe tratarse como una heurística inferida por proxy a partir de los dominios de negocio presentes en el código, no como una detección determinística, y debe reportarse con menor certeza que los criterios anteriores.

## Cómo interpretar los hallazgos

1. Reporta cohesión interna y fan-out como señales independientes.
2. No dupliques el mismo hallazgo bajo dos nombres distintos: si una dependencia múltiple no implica realmente más de una responsabilidad, repórtala solo como fan-out; si el problema es que distintos métodos tocan distintos subconjuntos de estado, repórtalo solo como cohesión.
3. Cuando una clase cambie por varias causas, identifica cuál es la causa dominante y cuáles son secundarias.
4. Marca explícitamente menor certeza cuando el hallazgo provenga de stakeholders inferidos por proxy y no de evidencia estructural directa.

## Cómo evaluar

1. Identifica el propósito aparente de la clase a partir de sus métodos, dependencias y campos.
2. Agrupa los métodos por el estado que manipulan y por el tipo de trabajo que realizan.
3. Revisa si la clase cruza límites de capa o mezcla responsabilidades de orquestación con trabajo especializado.
4. Cuenta y clasifica sus dependencias externas para distinguir fan-out alto de simple coordinación normal.
5. Busca indicios de cambio por diferentes dominios de negocio para estimar razones de cambio potenciales.

## Qué proponer cuando SRP se rompe

Cuando detectes una violación, sugiere una salida concreta según el caso:

- Extraer una clase especializada para cada responsabilidad cohesiva, por ejemplo `InvoiceCalculator`, `InvoiceRepository` o `InvoicePresenter`.
- Introducir un colaborador delegado y convertir la clase actual en orquestador ligero.
- Usar una Facade cuando la clase solo esté ocultando la complejidad de varios subsistemas, sin mezclar lógica de negocio propia.
- Aplicar Strategy para variar reglas, cálculos o políticas sin seguir creciendo en la misma clase.
- Separar explícitamente capas, dejando dominio, persistencia, presentación e integración en componentes distintos.

## Formato de respuesta recomendado

Cuando hagas una evaluación, responde con esta estructura:

- Veredicto: cumple, incumple o es ambiguo.
- Hallazgo de cohesión: qué métodos o campos quedan agrupados de forma disjunta.
- Hallazgo de capas o fan-out: qué mezcla de responsabilidades o dependencias externas evidencia el problema.
- Hallazgo de stakeholders: qué razones de cambio podrían existir, indicando que es una inferencia de menor certeza.
- Recomendación: una estructura concreta con nombres sugeridos de clases, responsabilidades y cómo se conectan.

## Ejemplos de uso

- Detectar una clase de servicio que calcula, persiste y además formatea salida para pantalla.
- Separar un componente que manipula campos distintos en grupos de métodos sin relación clara.
- Reducir una clase con demasiadas dependencias externas a una Facade con delegación interna.
- Proponer Strategy para una política que cambia frecuentemente dentro de una misma clase.

## Criterio práctico

Si una clase tiene que cambiar por más de un motivo claramente distinto, o si su estado y sus métodos no apuntan a una única responsabilidad estable, conviene dividirla. Si el hallazgo solo sugiere coordinación entre subsistemas, prioriza delegación y Facade antes que concentrar más lógica en la misma unidad.