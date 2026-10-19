# Análisis de frecuencia de parciales — PDR

**Actualizado:** 17/08/2026  
**Estado:** procesado a partir del material histórico local.

## Alcance y método

- Se revisaron las **16 letras de parcial** disponibles entre 2019 y 2025.
- El archivo identificado expresamente como examen del 09/10/2024 se excluyó de la frecuencia de parciales.
- La frecuencia mide **presencia temática por documento**, no cantidad de puntos. Un tema cuenta una vez aunque tenga varios ejercicios.
- La clasificación se hizo leyendo las consignas; sirve para priorizar estudio, no para predecir una letra futura.

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

Docker tiene menor frecuencia total porque aparece en el material más reciente. En 2025 ganó peso: los dos parciales del 30/06 piden explicar un Dockerfile y el nocturno del 01/12 pide interpretar un `docker-compose.yml`.

## Formatos que se repiten

1. **Comparaciones conceptuales:** Thread vs Task, concurrencia vs paralelismo, socket vs RPC, MOM vs comunicación directa.
2. **Lectura o corrección de código:** carreras, `lock`, captura de variables, `async/await`, `WhenAll`, cancelación y operaciones CPU-bound/I/O-bound.
3. **Sockets multi-cliente:** diseñar protocolo, framing, concurrencia, envío/recepción y cierre correcto.
4. **Arquitectura distribuida:** elegir normalmente tres tecnologías entre Sockets, MOM, gRPC y REST; justificar cada elección y definir contratos concretos (`.proto`, colas/exchanges, endpoints o protocolo).
5. **Infraestructura reciente:** Dockerfile, imágenes/contenedores, volúmenes, redes y Compose.

## Prioridad práctica de estudio

1. Resolver una arquitectura completa combinando tres tecnologías.
2. Sockets: protocolo, framing y servidor multi-cliente.
3. Threads: `lock`, `Join`, carreras y captura de variables.
4. Task/async: `WhenAll`, `WhenAny`, cancelación y CPU-bound vs I/O-bound.
5. RabbitMQ: exchange, routing key, colas y acknowledgements.
6. gRPC: contrato `.proto`, unary y streaming.
7. REST: recursos, verbos, códigos HTTP e idempotencia.
8. Docker: Dockerfile multi-stage, Compose, redes, volúmenes y healthchecks.

## Advertencias

- Esta tabla no asigna peso en puntos: las letras cambian entre instancias.
- Web Services/SOAP no se fuerza dentro de la clasificación porque pertenece al mapa anterior de ocho unidades, no al temario vigente de Aulas.
- El histórico orienta; el material y las pautas 2026 tienen prioridad cuando el docente los publique.
