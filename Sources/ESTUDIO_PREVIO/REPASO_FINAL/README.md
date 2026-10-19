# Repaso final — PDR

## Orden recomendado

1. Threads y sincronización.
2. Task, async/await y paralelismo.
3. Sockets y protocolos.
4. MOM / RabbitMQ.
5. RPC / gRPC.
6. REST, Web API, capas e inyección de dependencias.
7. Docker / Compose como complemento.
8. Introducción y conceptos transversales como complemento.

## Elegir tecnología en arquitectura

| Necesidad | Opción habitual | Qué justificar |
|---|---|---|
| CRUD web público | REST | recursos, verbos, códigos e idempotencia |
| Comunicación interna tipada y rápida | gRPC | contrato `.proto`, unary/streaming |
| Trabajo asíncrono y desacoplado | MOM / RabbitMQ | exchange, routing, cola, ack y reintentos |
| Conexión persistente con protocolo propio | Sockets | framing, concurrencia, timeouts y cierre |
| Tiempo real full-duplex con navegador | WebSocket | conexión persistente y flujo bidireccional |

## Esqueleto para responder una arquitectura

1. Nombrar componentes y responsabilidades.
2. Marcar cada enlace y elegir su tecnología.
3. Justificar por requisitos, no por preferencia.
4. Definir el contrato real: endpoints, `.proto`, mensajes/colas o protocolo.
5. Explicar concurrencia, errores, reintentos y cierre.
6. Agregar despliegue: Dockerfile, red, variables, volúmenes y Compose si corresponde.

## Patrones de código que deben salir de memoria

- `Thread.Start()` + `Join()`.
- `lock` cubriendo check + modificación.
- copia local de la variable capturada en un `for`.
- `Task.WhenAll` y `Task.WhenAny`.
- cancelación cooperativa con `CancellationToken`.
- servidor socket multi-cliente con framing explícito.
- contrato gRPC y endpoint REST coherentes con el caso.

## Errores caros

- Usar `Sleep` como sincronización.
- Poner un `lock` alrededor de solo una parte de la operación crítica.
- Suponer que un `Receive` devuelve exactamente un mensaje completo.
- Bloquear con `.Result` o `.Wait()` dentro de un flujo async.
- Confundir `Task` con un hilo dedicado.
- Diseñar MOM sin ack/reintentos o sin aclarar routing.
- Describir REST sin recursos, verbos ni códigos concretos.
- Hacer un Dockerfile que deja herramientas de compilación en la imagen final sin necesidad.

## Estado del temario vigente

Los seis temas publicados actualmente en Aulas están cubiertos. Web Services/SOAP-WSDL y Arquitecturas Cloud pertenecen al mapa anterior de ocho unidades y se conservan como referencia, pero no son huecos del temario vigente.
