---
name: liskov-substitution
description: Evalúa código contra el principio LSP de SOLID, detectando sustitución insegura entre superclases y subclases, y proponiendo interfaces, composición o agregación cuando la herencia está forzada.
argument-hint: El código, diseño o cambio a revisar para validar LSP y sugerir refactorizaciones.
---

# Liskov Substitution

Usa este skill cuando necesites revisar herencia, polimorfismo o contratos de subtipado bajo el principio de sustitución de Liskov (LSP).

## Objetivo

Determinar si una subclase puede sustituir a su superclase sin romper el comportamiento esperado por los clientes del tipo base. Cuando la herencia esté forzada o sea frágil, prioriza sugerir:

- Nuevas estructuras basadas en interfaces.
- Relaciones todo-partes mediante composición.
- Relaciones todo-partes mediante agregación.

## Señales de alerta

Sospecha una violación de LSP si observas alguno de estos patrones:

- La subclase acepta parámetros más restringidos que la superclase.
- La subclase devuelve un tipo menos específico o incompatible con el contrato base.
- La subclase introduce excepciones no esperadas por el tipo base.
- La subclase exige más precondiciones que la superclase.
- La subclase debilita las postcondiciones del contrato base.
- La subclase rompe invariantes establecidas por la superclase.
- La subclase modifica campos privados heredados de forma que altera el estado interno esperado de la superclase.
- El comportamiento de la subclase obliga al cliente a conocer su tipo concreto para evitar fallos.

## Protocolo de evaluación

Evalúa cada jerarquía de herencia con este checklist:

1. Los tipos de parámetros en un método de una subclase deben coincidir o ser más abstractos que los tipos de parámetros en el método de la superclase.
2. El tipo de retorno en un método de una subclase debe coincidir o ser un subtipo del tipo de retorno en el método de la superclase.
3. Un método en una subclase no debe generar tipos de excepciones que no se espera que genere el método base.
4. Una subclase no debe reforzar las condiciones previas.
5. Una subclase no debe debilitar las condiciones posteriores.
6. Las invariantes de una superclase deben conservarse.
7. Una subclase no debe cambiar los valores de los campos privados de la superclase.

## Cómo evaluar

1. Identifica el contrato público de la superclase o interfaz base.
2. Compara cada método sobrescrito con el comportamiento esperado del tipo base.
3. Revisa si el cliente puede usar la subclase exactamente igual que el tipo base.
4. Busca dependencias implícitas en estados, validaciones o excepciones específicas.
5. Si la jerarquía obliga a condicionar el uso con `instanceof`, `switch` por tipo o validaciones especiales, trata el diseño como un candidato fuerte a refactorización.

## Qué proponer cuando LSP se rompe

Cuando detectes una violación, sugiere una de estas salidas según el caso:

- Extraer una interfaz más pequeña y estable para el comportamiento realmente compartido.
- Reemplazar herencia por composición cuando el objeto usa capacidades delegadas en lugar de especialización real.
- Usar agregación cuando la relación entre objetos sea de colaboración y no de identidad de tipo.
- Separar responsabilidades para que cada tipo tenga un contrato claro y mínimo.
- Dividir una jerarquía en múltiples contratos si una sola superclase está imponiendo demasiadas reglas.

## Formato de respuesta recomendado

Cuando hagas una evaluación, responde con esta estructura:

- Veredicto: cumple, incumple o es ambiguo.
- Evidencia: qué contrato se rompe o qué parte del diseño lo sugiere.
- Impacto: por qué eso impide la sustitución segura.
- Recomendación: interfaz, composición, agregación u otro rediseño concreto.

## Ejemplos de uso

- Revisar una jerarquía de clases para validar si una subclase respeta el contrato de la base.
- Detectar métodos sobrescritos que estrechan parámetros, amplían excepciones o alteran invariantes.
- Proponer rediseño cuando una herencia existe solo para reutilizar código.
- Evaluar si una abstracción debe convertirse en una interfaz con implementación delegada.

## Criterio práctico

Si el código solo hereda estructura pero no cumple el contrato del tipo base, no lo consideres un buen uso de LSP. En ese caso, la prioridad es rediseñar el modelo para que la sustitución sea segura y explícita.
