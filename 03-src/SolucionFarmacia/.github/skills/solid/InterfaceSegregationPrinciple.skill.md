---
name: interface-segregation-principle
description: Evalúa si un diseño cumple con el principio de segregación de interfaces (ISP) de SOLID. Útil para detectar interfaces demasiado grandes, implementaciones obligadas a soportar métodos innecesarios y contratos poco cohesionados.
---
 
# Interface Segregation Principle (ISP)
 
Use este skill cuando deba revisar interfaces, clases que las implementan, consumidores de contratos o módulos que dependen de operaciones que no utilizan. También es útil para detectar interfaces gordas, métodos vacíos o implementaciones forzadas a soportar responsabilidades irrelevantes.
 
## Objetivo
 
Validar que los clientes dependan únicamente de los métodos que realmente necesitan. Las interfaces deben ser pequeñas, específicas y cohesionadas, de modo que las implementaciones no se vean obligadas a soportar comportamiento innecesario.
 
## Capacidades
 
- Identificar interfaces demasiado grandes o genéricas.
- Detectar implementaciones que dependan de métodos que no utilizan.
- Detectar implementaciones que definen métodos vacíos, excepciones o comportamientos ficticios por obligación.
- Sugerir la segregación de interfaces en contratos más pequeños y específicos.
- Verificar si los consumidores dependen de contratos con funcionalidades no utilizadas.
- Evaluar si una interfaz mezcla varias responsabilidades funcionales.
- Detectar interfaces con demasiados métodos o responsabilidades múltiples.
- Recomendar el uso de interfaces de rol o capacidad cuando corresponda.
- Proponer refactorizaciones como Extract Interface, Split Interface, Replace Fat Interface with Multiple Interfaces y Move Method.
 
## Protocolo de evaluación
 
1. Revisar cada interfaz y evaluar si contiene más métodos de los necesarios para un cliente concreto.
2. Identificar si los clientes están obligados a depender de operaciones que no utilizan.
3. Verificar si las implementaciones deben soportar métodos que no aplican a su comportamiento.
4. Detectar métodos vacíos, excepciones o implementaciones ficticias que indiquen una interfaz demasiado amplia.
5. Evaluar si la interfaz representa un único contrato funcional o mezcla varias responsabilidades.
6. Comprobar si la interfaz contiene métodos opcionales que podrían separarse en contratos independientes.
7. Evaluar si las clases consumidoras dependen de interfaces específicas o si están ligadas a contratos demasiado amplios.
8. Determinar si la interfaz es una fat interface o una interfaz poco cohesionada.
9. Proponer una división en interfaces más pequeñas y reutilizables.
10. Generar recomendaciones de refactorización orientadas a reducir el acoplamiento y mejorar la cohesión.
 
## Requisitos de cumplimiento
 
Los clientes no deben estar obligados a depender de interfaces que no utilizan.
 
Las interfaces deben contener únicamente los métodos necesarios para sus implementaciones o consumidores, evitando contratos excesivamente amplios.
 
Debe detectarse una interfaz con un número elevado de métodos, indicando una posible violación del principio de segregación de interfaces.
 
Debe identificarse cuando una clase implementa métodos vacíos, excepciones o implementaciones ficticias, sugiriendo dividir la interfaz.
 
Debe verificarse que cada implementación utilice todos o la mayoría de los métodos definidos en la interfaz, alertando cuando existan métodos innecesarios.
 
Debe detectarse una interfaz con responsabilidades múltiples, recomendando dividirla en interfaces más pequeñas y cohesionadas.
 
Debe identificarse dependencias innecesarias cuando una clase deba conocer operaciones que nunca invoca.
 
Debe verificarse que cada interfaz represente un único contrato funcional, evitando mezclar responsabilidades de distintos dominios.
 
Debe detectarse una implementación que viole el principio debido a métodos obligatorios que no son aplicables a su comportamiento.
 
Debe comprobarse que las interfaces favorezcan un bajo acoplamiento, permitiendo que diferentes clientes dependan únicamente de los contratos que requieren.
 
Debe identificarse cuando una interfaz contiene métodos opcionales, recomendando dividir dichos métodos en interfaces independientes.
 
Debe detectarse una interfaz gordo o fat interface, es decir, una interfaz demasiado extensa que obliga a múltiples implementaciones a incorporar funcionalidades innecesarias.
 
Debe evaluarse la cohesión funcional de cada interfaz, verificando que todos sus métodos pertenezcan a la misma responsabilidad.
 
Debe verificarse que las clases consumidoras dependan únicamente de interfaces específicas, evitando referencias a contratos con funcionalidades no utilizadas.
 
Debe generarse una recomendación de refactorización con técnicas como Extract Interface, Split Interface, Interface Segregation, Introduce Role Interface, Introduce Capability Interface, Move Method o Replace Fat Interface with Multiple Interfaces.
 
## Criterios de incumplimiento
 
- Un cliente depende de una interfaz que contiene operaciones que nunca usa.
- Una clase implementa métodos vacíos o ficticios solo para cumplir con un contrato amplio.
- La interfaz mezcla responsabilidades distintas y obliga a varias implementaciones a soportar comportamiento irrelevante.
- Un contrato demasiado grande provoca acoplamiento innecesario y reduce la reutilización.
- Un consumidor debe conocer operaciones que no le corresponden.
 
## Explicación de causa
 
Cada incumplimiento debe explicarse claramente indicando:
 
- Qué interfaz contiene métodos innecesarios.
- Qué clases están siendo obligadas a implementar o depender de dichos métodos.
- Por qué esto viola el principio de segregación de interfaces.
- El impacto en el mantenimiento, reutilización y desacoplamiento del sistema.
- Una propuesta concreta de refactorización mostrando cómo dividir la interfaz en contratos más pequeños y especializados.
 
## Recomendaciones de diseño
 
- Favorecer interfaces pequeñas, específicas y cohesionadas.
- Separar contratos por rol o capacidad funcional.
- Evitar que un cliente dependa de operaciones que no necesita.
- Reducir implementaciones ficticias y métodos vacíos.
- Promover contratos reutilizables que mejoren el desacoplamiento del sistema.
 
## Plantilla de salida
 
- Estado: Cumple / No cumple ISP
- Interfaz detectada:
- Clientes o implementaciones afectadas:
- Métodos innecesarios o no aplicables:
- Causa del incumplimiento:
- Impacto en mantenimiento y desacoplamiento:
- Refactorización recomendada:
- Estructura propuesta:
 
## Ejemplo de evaluación
 
Si una interfaz define operaciones de lectura, escritura, envío de correos y generación de reportes, pero solo un cliente necesita lectura y otro necesita escritura, entonces la interfaz es demasiado amplia. La solución recomendada es dividirla en contratos más pequeños como IReadableRepository, IWritableRepository y IReportGenerator.