---
name: dependency-inversion-principle
description: Evalúa código contra el principio DIP de SOLID, detectando dependencias hacia implementaciones concretas y proponiendo abstracciones, interfaces e inyección de dependencias para reducir acoplamiento y mejorar extensibilidad, reutilización y pruebas.
argument-hint: El código, arquitectura o cambio a revisar para validar DIP y proponer una inversión de dependencias.
---

# Dependency Inversion Principle

Usa este skill cuando necesites revisar si los módulos de alto nivel dependen de abstracciones y no de detalles concretos.

## Objetivo

Determinar si el diseño invierte correctamente las dependencias para que el negocio, los casos de uso y los controladores dependan de contratos estables en lugar de implementaciones concretas. Cuando detectes acoplamiento fuerte, prioriza sugerir:

- Interfaces para representar contratos estables.
- Clases abstractas cuando haya comportamiento común con variaciones.
- Inyección de dependencias para recibir colaboraciones desde fuera.
- Factorías cuando la creación concreta sea parte del problema.
- Contenedores de inyección de dependencias cuando existan múltiples servicios y ensamblaje complejo.
- Refactorizaciones que inviertan la dirección de la dependencia hacia abstracciones.

## Señales de alerta

Sospecha una violación de DIP si observas alguno de estos patrones:

- Módulos de alto nivel que dependen directamente de módulos de bajo nivel.
- Abstracciones que dependen de detalles, en lugar de que los detalles dependan de las abstracciones.
- Clases que instancian sus dependencias con `new` dentro de sus métodos o constructores sin justificación.
- Dependencias directas hacia clases concretas cuando bastaría una interfaz o clase abstracta.
- Dependencias creadas internamente en vez de recibidas por constructor, método o propiedad.
- Acoplamiento fuerte entre componentes mediante referencias directas a implementaciones específicas.
- Módulos de negocio que dependen de infraestructura como bases de datos, sistema de archivos, servicios web, APIs externas o bibliotecas concretas.
- Interfaces que cambian con frecuencia o exponen detalles de implementación en lugar de contratos estables.
- Dependencias ocultas o implícitas como Singletons, variables globales o llamadas estáticas que dificultan la sustitución.
- Clases que conocen demasiados detalles internos de otras clases y, por eso, son frágiles frente a cambios.
- Dependencias cíclicas entre módulos o paquetes.
- Controladores, servicios y casos de uso que referencian implementaciones concretas en lugar de contratos.

## Protocolo de evaluación

Evalúa cada unidad con este checklist:

1. Los módulos de alto nivel no deben depender de módulos de bajo nivel; ambos deben depender de abstracciones.
2. Las abstracciones no deben depender de los detalles; los detalles deben depender de las abstracciones.
3. Detectar cuando una clase instancia directamente sus dependencias utilizando operadores como `new`, recomendando el uso de inyección de dependencias o factorías cuando corresponda.
4. Identificar dependencias directas hacia clases concretas, sugiriendo reemplazarlas por interfaces o clases abstractas.
5. Verificar que las dependencias sean recibidas mediante constructor, métodos o propiedades, evitando su creación interna.
6. Detectar acoplamiento fuerte entre componentes, identificando referencias directas a implementaciones específicas.
7. Comprobar que los módulos de negocio no dependan de detalles de infraestructura, como bases de datos, sistema de archivos, servicios web, APIs externas o bibliotecas concretas.
8. Verificar que las interfaces representen contratos estables, permitiendo sustituir implementaciones sin modificar el código cliente.
9. Detectar dependencias ocultas o implícitas, como Singletons, variables globales o llamadas estáticas que dificultan la sustitución de implementaciones.
10. Identificar cuando una clase conoce demasiados detalles de la implementación de otra clase, incrementando el acoplamiento entre módulos.
11. Comprobar que las dependencias puedan ser reemplazadas fácilmente durante las pruebas, favoreciendo el uso de mocks, stubs o implementaciones alternativas.
12. Detectar dependencias cíclicas entre módulos o paquetes, recomendando la introducción de abstracciones para romper el ciclo.
13. Verificar que los controladores, servicios y casos de uso dependan únicamente de contratos, evitando referencias directas a implementaciones concretas.
14. Evaluar el uso adecuado de la Inyección de Dependencias mediante constructor, setter o interfaces, según el contexto.

## Cómo evaluar

1. Identifica quién es el módulo de alto nivel y qué reglas de negocio contiene.
2. Localiza qué dependencias usa directamente y si son concretas o abstraídas.
3. Revisa cómo se crean esas dependencias: internamente, por parámetro, por propiedad, por fábrica o por contenedor.
4. Observa si la clase depende de infraestructura en lugar de depender de una interfaz de puerto o contrato.
5. Busca Singletons, estados globales, métodos estáticos o ciclos de dependencia.
6. Comprueba si la dependencia puede sustituirse en pruebas sin tocar el código cliente.

## Qué proponer cuando DIP se rompe

Cuando detectes una violación, sugiere una salida concreta según el caso:

- Introduce Interface para extraer un contrato estable.
- Introduce Abstract Class cuando exista comportamiento compartido con variaciones controladas.
- Constructor Injection para recibir dependencias obligatorias.
- Setter Injection para dependencias opcionales o configurables.
- Interface Injection cuando el consumidor deba aceptar una dependencia a través de un contrato de configuración específico.
- Dependency Injection para mover la creación al exterior de la clase.
- Dependency Injection Container cuando el ensamblaje de objetos sea grande y repetitivo.
- Invert Dependency para convertir la clase de alto nivel en dependiente de una abstracción.
- Extract Interface para separar solo las operaciones realmente usadas.
- Replace Concrete Dependency with Abstraction cuando una clase concreta aparezca acoplada sin necesidad.
- Use a Factory when the main issue is object creation rather than the business rule itself.

## Causa, impacto y refactorización esperada

Cuando reportes un incumplimiento, deja claro:

- Qué dependencia concreta fue detectada.
- Por qué esa dependencia viola el principio de inversión de dependencias.
- Qué impacto tiene en mantenimiento, reutilización, pruebas y escalabilidad.
- Qué abstracción propuesta reemplazaría a la implementación concreta.
- Cómo se conectaría la implementación existente con el nuevo contrato.

Ejemplo de formato de recomendación:

- Dependencia detectada: `PedidoService -> SqlConnection`.
- Problema: el caso de uso depende de un detalle de infraestructura.
- Impacto: pruebas más difíciles, acoplamiento alto y cambios costosos de persistencia.
- Refactor: crear `IPedidoRepository`, implementar `SqlPedidoRepository` y recibirlo por `constructor`.

## Restricciones de interpretación

No marques como violación cuando una dependencia concreta sea parte de un límite intencional y estable, por ejemplo un adaptador de infraestructura en la capa externa, siempre que el módulo de alto nivel no dependa directamente de ese detalle.

Tampoco recomiendes una abstracción nueva si solo duplicaría una interfaz sin aportar estabilidad, claridad o sustituibilidad.

## Formato de respuesta recomendado

Cuando hagas una evaluación, responde con esta estructura:

- Veredicto: cumple, incumple o es ambiguo.
- Dependencia detectada: qué clase, módulo o API concreta se encontró.
- Violación: por qué rompe DIP.
- Impacto: efecto sobre mantenimiento, reutilización, pruebas y escalabilidad.
- Recomendación: interfaz, clase abstracta, factoría o técnica de inyección concreta.
- Ejemplo de inversión: cómo quedaría la relación entre alto nivel, abstracción e implementación.

## Ejemplos de uso

- Revisar un servicio que crea por sí mismo repositorios, clientes HTTP o loggers.
- Detectar un controlador acoplado a una base de datos concreta o a un SDK externo.
- Proponer interfaces de puerto para separar casos de uso de infraestructura.
- Sustituir una dependencia concreta por mocks o stubs en pruebas unitarias.
- Romper un ciclo entre módulos con una interfaz intermedia.

## Criterio práctico

Si cambiar una implementación obliga a tocar el código de negocio, el diseño no está invirtiendo dependencias correctamente. En ese escenario, prioriza abstraer el contrato, mover la construcción hacia el exterior y conectar las implementaciones mediante inyección o fábricas.