---
date: 2026-08-26
type: synthesis
tags:
  - synthesis
  - pdr
  - examen
  - parcial
ai-first: true
confidence: high
sources:
  - "Sources/ESTUDIO_PREVIO/ANALISIS_DE_FRECUENCIA.md"
  - "Sources/ESTUDIO_PREVIO/REPASO_FINAL/README.md"
  - "Sources/PARCIALES/"
---

# Patrones de examen en Programacion de Redes

## For future agent
Nota de síntesis sobre cómo son las evaluaciones de PDR: frecuencia temática histórica, los cinco formatos de pregunta que se repiten, y el orden de prioridad de estudio que se deriva de eso. Base: **16 letras de parcial entre 2019 y 2025**, clasificadas leyendo las consignas (análisis del corpus previo, as of 2026-08-17). Sirve para priorizar, **no para predecir** una letra futura. En la bóveda hay además 21 exámenes y 21 soluciones convertidos en `Sources/PARCIALES/`.

## Frecuencia histórica

| Tema | Apariciones | Frecuencia |
|---|---:|---:|
| Threads y sincronización | 15/16 | 94% |
| Sockets y protocolos | 14/16 | 88% |
| Task, async/await y paralelismo | 14/16 | 88% |
| MOM / RabbitMQ | 13/16 | 81% |
| RPC / gRPC | 13/16 | 81% |
| REST | 11/16 | 69% |
| Docker / Compose | 4/16 | 25% |

Método: presencia temática por documento, no puntaje. Un tema cuenta una vez aunque tenga varios ejercicios. El examen identificado como del 09/10/2024 se excluyó del conteo de parciales.

**Docker está subrepresentado por antigüedad, no por peso real**: aparece solo en el material reciente y en 2025 ganó espacio (los dos parciales del 30/06/2025 piden explicar un Dockerfile, y el nocturno del 01/12/2025 pide interpretar un `docker-compose.yml`). Tratarlo como tema en alza.

## Los cinco formatos que se repiten

1. **Comparaciones conceptuales.** `Thread` vs `Task`, concurrencia vs paralelismo, socket vs RPC, MOM vs comunicación directa. Se responden con la tabla del material, no con prosa.
2. **Lectura o corrección de código.** Carreras, `lock` mal puesto, captura de variable en un `for`, `async`/`await`, `WhenAll`, cancelación, CPU-bound vs I/O-bound.
3. **Sockets multi-cliente.** Diseñar el protocolo, framing, concurrencia, envío y recepción completos, cierre correcto.
4. **Arquitectura distribuida.** Normalmente elegir **tres tecnologías** entre Sockets, MOM, gRPC y REST, justificar cada elección y definir los contratos concretos (`.proto`, colas y exchanges, endpoints, o el protocolo propio). Es el ejercicio de mayor peso: ver [[Elegir tecnologia de comunicacion]].
5. **Infraestructura reciente.** Dockerfile, imágenes vs contenedores, volúmenes, redes, Compose.

## Prioridad de estudio que se deriva

1. Resolver una arquitectura completa combinando tres tecnologías.
2. Sockets: protocolo, framing y servidor multi-cliente.
3. Threads: `lock`, `Join`, carreras y captura de variables.
4. Task y async: `WhenAll`, `WhenAny`, cancelación, CPU-bound vs I/O-bound.
5. RabbitMQ: exchange, routing key, colas y acknowledgements.
6. gRPC: contrato `.proto`, unary y streaming.
7. REST: recursos, verbos, códigos e idempotencia.
8. Docker: Dockerfile multi-stage, Compose, redes, volúmenes, healthchecks.

Nótese que el orden **no** es el orden del temario: arranca por lo integrador y termina por lo reciente.

## Patrones de código que tienen que salir de memoria
- `Thread.Start()` + `Join()`.
- `lock` cubriendo el check **y** la modificación.
- Copia local de la variable capturada dentro de un `for`.
- `Task.WhenAll` y `Task.WhenAny`.
- Cancelación cooperativa con `CancellationToken`.
- Servidor de sockets multi-cliente con framing explícito.
- Un contrato gRPC y un endpoint REST coherentes con el caso planteado.

## Advertencias del propio análisis
- La tabla de frecuencias no asigna peso en puntos: las letras cambian entre instancias.
- Web Services / SOAP no entra en la clasificación porque pertenece al programa anterior de 8 unidades: [[Referencia - Web Services del programa anterior]].
- El histórico orienta; el material y las pautas 2026 mandan cuando la docente los publique.

## Connections
- Método de estudio: [[Plan de estudio y repaso PDR]]. Trampas concretas: [[Errores caros en Programacion de Redes]].
- Ejercicio de arquitectura: [[Elegir tecnologia de comunicacion]].
- Calendario y puntajes: [[Programacion de Redes 2026]] · [[Mapa del curso Programacion de Redes]].
