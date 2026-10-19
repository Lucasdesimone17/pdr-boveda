---
date: 2026-08-26
type: methodology
tags:
  - methodology
  - pdr
  - estudio
ai-first: true
confidence: high
sources:
  - "Sources/ESTUDIO_PREVIO/REPASO_FINAL/README.md"
  - "Sources/ESTUDIO_PREVIO/ANALISIS_DE_FRECUENCIA.md"
  - "Sources/ESTUDIO_PREVIO/00_INDICE.md"
---

# Plan de estudio y repaso PDR

## For future agent
Protocolo de estudio para Programación de Redes: en qué orden repasar, qué usar de cada carpeta de la bóveda, y cómo practicar contra las 16 letras históricas. Deriva del repaso final y del análisis de frecuencia del corpus previo. Usar junto a [[Patrones de examen en Programacion de Redes]], que dice **qué** se pregunta; esta nota dice **cómo** prepararlo.

## Orden de repaso recomendado

1. Threads y sincronización - [[Tema 1 - Programacion multihilo]]
2. Task, async/await y paralelismo - [[Tema 3 - Asincronia y paralelismo]]
3. Sockets y protocolos - [[Tema 2 - Sockets y protocolos]]
4. MOM / RabbitMQ - [[Tema 4 - Colas de mensajes y MOM]]
5. RPC / gRPC - [[Tema 5 - RPC y gRPC]]
6. REST, Web API, capas e inyección de dependencias - [[Tema 6 - APIs REST y WebSockets]]
7. Docker y Compose como complemento - [[Complementario - Docker y Cloud]]
8. Introducción y conceptos transversales - [[Complementario - Introduccion y Streams]]

Los temas 1 y 3 van juntos y primero porque son la base de todo lo demás y suman la frecuencia más alta. Sockets va tercero, no segundo, porque se estudia mejor cuando la concurrencia ya está firme.

## Qué usar de cada lugar de la bóveda

| Necesidad | Dónde |
|---|---|
| Entender un tema por primera vez | La nota `Literature/Tema N - ...` |
| Repaso rápido antes del parcial | `Sources/ESTUDIO_PREVIO/TEMA_*/CHEATSHEET.md` |
| Autoevaluación | `Sources/ESTUDIO_PREVIO/TEMA_*/PREGUNTAS.md` |
| Profundizar con la fuente oficial | `Sources/TEORICO/...` (el teórico convertido) |
| Practicar como en el parcial | `Sources/PARCIALES/` (21 letras y 21 soluciones) |
| Decidir tecnologías en un ejercicio de arquitectura | [[Elegir tecnologia de comunicacion]] |
| Revisar antes de entregar | [[Errores caros en Programacion de Redes]] |

## Cómo practicar con las letras históricas

1. Elegir una letra de `Sources/PARCIALES/*/Letras/` **sin abrir la solución**.
2. Resolverla escribiendo código y contratos de verdad, no descripciones.
3. Comparar contra la solución correspondiente en `Soluciones/` o `Solución/`.
4. Anotar cada diferencia como un ítem concreto a repasar, no como "me equivoqué".

Prioridad de práctica: primero un ejercicio de arquitectura completo (tres tecnologías con contratos), después sockets multi-cliente, después lectura y corrección de código concurrente.

## Ejercicio de arquitectura: el esqueleto de seis pasos

1. Componentes y responsabilidades.
2. Cada enlace con su tecnología.
3. Justificación por requisitos, no por gusto.
4. El contrato real: endpoints y verbos, `.proto`, exchange/routing/cola, o formato del protocolo propio.
5. Concurrencia, errores, reintentos y cierre.
6. Despliegue si aplica: Dockerfile, red, variables, volúmenes, Compose.

Los pasos 4 y 5 son los que se olvidan y los que separan la nota.

## Ritmo sugerido para el semestre 2026

Alineado al calendario de [[Programacion de Redes 2026]] (obligatorios el 28/09, 29/10 y 23/11; parcial el 30/11):

- **Semanas 1-3** (temas 1): threads con las tres guías más el Práctico 1. Lectura del Obligatorio 1 desde el 26/08.
- **Semanas 4-6** (tema 2): sockets, con framing implementado a mano al menos una vez.
- **Semanas 7-8** (tema 3): async, resolviendo el ejercicio del desayuno en sus cuatro variantes.
- **Semanas 9-11** (tema 4): RabbitMQ levantado en Docker, con los cuatro tipos de exchange probados.
- **Semanas 12-14** (tema 5): gRPC con un `.proto` propio, unary y streaming.
- **Semanas 15-16** (tema 6): Web API en capas con DI.
- **Semana previa al parcial**: dos letras históricas completas cronometradas, más el repaso de [[Errores caros en Programacion de Redes]].

## Regla de oro
Los cheatsheets sirven para **recordar**, no para aprender. Lo que se aprende escribiendo código (framing, `lock`, `WhenAll`, un `.proto`) no se recupera leyendo una tabla la noche anterior.

## Connections
- [[Patrones de examen en Programacion de Redes]] · [[Elegir tecnologia de comunicacion]] · [[Errores caros en Programacion de Redes]]
- [[Mapa del curso Programacion de Redes]] · [[Programacion de Redes 2026]] · [[Huecos y contradicciones del material PDR]]
