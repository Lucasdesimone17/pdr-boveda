---
date: 2026-08-26
type: synthesis
tags:
  - synthesis
  - pdr
  - arquitectura
  - decision
ai-first: true
confidence: high
sources:
  - "[[Tema 2 - Sockets y protocolos]]"
  - "[[Tema 4 - Colas de mensajes y MOM]]"
  - "[[Tema 5 - RPC y gRPC]]"
  - "[[Tema 6 - APIs REST y WebSockets]]"
  - "Sources/ESTUDIO_PREVIO/REPASO_FINAL/README.md"
---

# Elegir tecnologia de comunicacion

## For future agent
Nota de decisión: cómo elegir entre Sockets, MOM/RabbitMQ, gRPC, REST y WebSockets, y qué hay que justificar en cada caso. Es la síntesis que responde el ejercicio más pesado del parcial de Programación de Redes: "diseñe una arquitectura eligiendo tres tecnologías y justifique". Contiene la matriz de decisión, el esqueleto de respuesta y los contratos concretos que hay que definir en cada elección.

## La matriz

| Necesidad | Opción | Qué hay que justificar y definir |
|---|---|---|
| CRUD web público, clientes heterogéneos, navegador | **REST** | Recursos, URI en sustantivos, verbos, códigos de estado, idempotencia, versionado |
| Comunicación interna entre servicios, tipada y rápida | **gRPC** | Contrato `.proto`, unary vs streaming, por qué binario, límite de navegador |
| Trabajo asíncrono, desacoplado, absorber picos | **MOM / RabbitMQ** | Exchange y tipo, routing key, colas, ack, reintentos, dead-letter, idempotencia |
| Conexión persistente con protocolo propio, control total | **Sockets** | Framing, envío y recepción completos, concurrencia, timeouts, cierre |
| Tiempo real full duplex con navegador | **WebSocket** | Conexión persistente, flujo bidireccional, escalado horizontal |

## Los tres ejes que deciden

Cada tecnología se ubica por tres preguntas. Contestarlas es la justificación:

1. **¿Sincrónico o asincrónico?** Si el emisor necesita la respuesta para seguir, es RPC/REST. Si no la necesita, o si el receptor puede estar caído, es MOM. La analogía del material: **RPC es el teléfono, MOM es el correo postal**.
2. **¿Contrato rígido o flexible?** gRPC impone `.proto` compilado; REST acepta cualquier JSON. Rígido da validación y generación de código; flexible da evolución sin recompilar clientes.
3. **¿Quién es el cliente?** Un navegador descarta gRPC puro (solo gRPC-Web) y favorece REST o WebSocket. Otro servicio propio habilita gRPC y sockets.

Un cuarto eje aparece solo cuando aprieta: **¿cuánto control del canal necesito?** Si la respuesta es "todo", sockets; si es "el mínimo", cualquier capa superior.

## Lo que cada opción cuesta

| Opción | Lo que ganás | Lo que pagás |
|---|---|---|
| Sockets | Eficiencia, control total, multiplataforma | Codificación manual, framing propio, difícil agregar operaciones |
| MOM | Desacople, asincronía, ruteo, absorber picos | Un componente más que administrar, salto intermedio, orden no garantizado |
| gRPC | Performance, contrato fuerte, streaming bidireccional | Definición estricta, menos difundido, navegador limitado |
| REST | Ubicuidad, cacheable, simple de consumir | Verboso (JSON texto), semi duplex, sin contrato obligatorio |
| WebSocket | Baja latencia bidireccional | Implementación compleja, más recursos, escalado horizontal difícil |

## Esqueleto para responder una arquitectura

Del repaso final del corpus previo, y es el orden que conviene seguir literalmente:

1. Nombrar componentes y responsabilidades.
2. Marcar **cada enlace** del diagrama y elegir su tecnología (no una sola para todo el sistema).
3. Justificar por requisitos, no por preferencia.
4. Definir el contrato **real**: endpoints y verbos, o el `.proto`, o exchange/routing key/cola, o el formato del protocolo propio.
5. Explicar concurrencia, errores, reintentos y cierre.
6. Agregar despliegue si corresponde: Dockerfile, red, variables de entorno, volúmenes, Compose.

El paso 4 es el que separa una respuesta aprobada de una excelente: nombrar la tecnología sin escribir el contrato es media respuesta.

## Combinación típica

La arquitectura que el material sugiere como patrón (microservicios, Tema 6) usa las cuatro a la vez, cada una en su enlace:

- **REST** hacia afuera, para el cliente público.
- **gRPC** entre servicios internos, con contrato tipado.
- **RabbitMQ** para eventos y trabajo asíncrono entre servicios.
- **WebSocket** para el canal de tiempo real al navegador.
- **Sockets** solo si hay un dispositivo o sistema legado que impone su propio protocolo.

Y en todos los casos: diseñar para timeouts, fallas parciales, reintentos e idempotencia. Las 8 falacias de la programación en redes del Tema 6 son el recordatorio de por qué.

## Connections
- Temas fuente: [[Tema 2 - Sockets y protocolos]] · [[Tema 4 - Colas de mensajes y MOM]] · [[Tema 5 - RPC y gRPC]] · [[Tema 6 - APIs REST y WebSockets]].
- Formato de examen: [[Patrones de examen en Programacion de Redes]]. Trampas: [[Errores caros en Programacion de Redes]].
- Despliegue: [[Complementario - Docker y Cloud]]. Mapa: [[Mapa del curso Programacion de Redes]].
