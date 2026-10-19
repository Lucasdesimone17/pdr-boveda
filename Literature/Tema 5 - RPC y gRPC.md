---
date: 2026-08-26
type: literature
tags:
  - literature
  - pdr
  - tema-5
  - rpc
  - grpc
  - protobuf
ai-first: true
source: "Sources/TEORICO/05_RPC_Y_ORBS/RPC.md"
source_extra:
  - "Sources/ESTUDIO_PREVIO/TEMA_05_RPC/CHEATSHEET.md"
confidence: stated
semanas: "12-14"
frecuencia_parcial: "13/16 (81%)"
---

# Tema 5 - RPC y gRPC

## For future agent
Nota de tema 5 del temario vigente (semanas 12-14): RPC como paradigma, su evolución a ORBs, y gRPC como implementación actual. Frecuencia histórica de parcial: **13 de 16 letras (81%)**. Lo evaluable: escribir un contrato `.proto`, distinguir los 4 tipos de servicio, y justificar gRPC frente a REST, MOM o sockets. Fuente: `RPC.pdf`.

## Source
`Sources/TEORICO/05_RPC_Y_ORBS/RPC.md`.

## Key Claims

### RPC, lo esencial
- Origen: **Birrell y Nelson, 1984**. Paradigma **procedural**, mecanismo **síncrono**: el cliente se suspende hasta recibir respuesta.
- Flujo: cliente llama → **client stub** empaqueta → red → **server stub** desempaqueta → ejecución → respuesta por el camino inverso.
- Vive en la capa de **Aplicación**, junto a MOM, ORB, Named Pipes, DCE, SUN y peer-to-peer.
- Vino a reemplazar el uso directo de sockets, named pipes y memoria compartida con protocolos propietarios: exactamente el trabajo manual del [[Tema 2 - Sockets y protocolos]].

### De procedural a orientado a objetos
| Era | Tecnologías |
|---|---|
| Procedural | Courier (Xerox), ONC RPC (Sun), MSRPC (Microsoft) |
| Orientada a objetos | CORBA (OMG), RMI (Java), Remoting (Microsoft) |

### Los 7 retos de RPC/ORB (preguntas oficiales del material)
1. ¿Cómo encuentra el cliente al servidor?
2. ¿Cómo se organizan y arrancan los canales?
3. ¿Qué pasa con la concurrencia de múltiples clientes?
4. ¿Cómo se definen y pasan los parámetros?
5. ¿Cómo se maneja la heterogeneidad de representación?
6. ¿Cómo se manejan las excepciones?
7. (ORB) ¿Cómo consumir objetos remotos de forma transparente?

Cada respuesta del material es una pieza del diseño:
- **Canales**: conexiones múltiples (el canal da la sincronización, pero limita la concurrencia a los puertos disponibles) vs conexión única peer-to-peer (sin problema de canal, pero si no es P2P la sincronización se complica con tablas de bloqueo). La concurrencia la resuelve el programador con monitores, locks y semáforos.
- **Ubicación del servidor**: ruta absoluta tipo URL (Remoting), directorio (RMI, UDDI), o descubrimiento por broadcast (no sirve en WAN).
- **Datos**: serialización (convertir a formato transportable), representación canónica (estructura estándar intermedia, como en CORBA) y marshalling (transformar la representación para transferirla).
- **Excepciones remotas**: se capturan en destino, se serializan, viajan al origen, se deserializan y se vuelven a lanzar como si fueran locales.

### Stub, skeleton e IDL
| Pieza | Lado | Función |
|---|---|---|
| Stub | Cliente | Emula localmente al objeto remoto |
| Skeleton | Servidor | Captura la llamada y ejecuta sobre el objeto real |
| IDL | Ambos | Lenguaje neutral que define el contrato; se compila y genera stub y skeleton |

### MOM vs RPC (postal vs teléfono)
| | MOM | RPC |
|---|---|---|
| Ambos extremos activos | No | Sí |
| Relación de tiempo | Asincrónica | Sincrónica |
| Secuencia | Sin secuencia fija | El servidor debe estar activo antes |
| Equilibrio de carga | Cola FIFO o prioridades | Monitor transaccional |
| Soporte transaccional | La cola participa del commit | Necesita RPC transaccional |
| Filtro de mensajes | Sí | No |
| Desempeño | Lento (salto intermedio) | Veloz |
| Procesamiento asincrónico | Sí | Limitado (hilos manuales) |

### gRPC
- Google, **2015**. Transporte **HTTP/2**, serialización **Protocol Buffers** (binario), seguridad TLS, IDL estricto (`.proto`), streaming en 4 variantes.
- HTTP/2 aporta: streams multiplexados sobre una sola conexión, server push, formato binario y headers comprimidos en un bloque.
- Protobuf vs JSON: binario vs texto, no estandarizado vs estandarizado, poco legible vs legible, no self-describing vs self-describing.

| Característica | gRPC | REST |
|---|---|---|
| Protocolo | HTTP/2 | HTTP/1.1 |
| Carga útil | Protobuf binario | JSON texto |
| Contrato | Estricto (`.proto`) | Flexible (OpenAPI opcional) |
| Generación de código | Integrada | Terceros (Swagger) |
| Streaming | Bidireccional | Solo cliente a servidor |
| Soporte de navegador | Limitado (gRPC-Web) | Sí |

### Los 4 tipos de servicio
| Tipo | Cliente | Servidor |
|---|---|---|
| Unario | 1 request | 1 response |
| Server streaming | 1 request | stream de responses |
| Client streaming | stream de requests | 1 response |
| Bidireccional | stream | stream |

### Sintaxis `.proto`
```proto
syntax = "proto3";

message Person {
  required string name = 1;
  required int32 id = 2;
  optional string email = 3;
  enum PhoneType { MOBILE = 0; HOME = 1; WORK = 2; }
  message PhoneNumber {
    required string number = 1;
    optional PhoneType type = 2 [default = HOME];
  }
  repeated PhoneNumber phone = 4;
}

service ChatService {
  rpc chat (stream ChatMessage) returns (stream ChatMessage);
}
```

### Implementación en .NET
- Paquete de servidor `Grpc.AspNetCore`; la URL se expone vía `applicationUrl` en `launchSettings.json`.
- El código generado del `.proto` trae `// DO NOT EDIT!`: no se toca a mano.
- La clase de servicio hereda de la base autogenerada (`Greeter.GreeterBase`).
- Abrir el endpoint directo en el navegador **falla**: hace falta un cliente gRPC.

### Ventajas y desventajas
Ventajas: performance, resuelve el problema histórico de RPC sin HTTP, acoplamiento bajo, multiplataforma y multilenguaje real. Desventajas: definición estricta, menos difundido.

## Methodology
Un teórico que hace historia primero (RPC, ORBs, los 7 retos) y recién después presenta gRPC como la respuesta moderna a esos mismos retos. Leerlo en ese orden es lo que hace que las preguntas de justificación tengan respuesta.

## Critique
- El ejemplo `.proto` usa `required`/`optional` con `syntax = "proto3"`, y `required` fue eliminado en proto3. El ejemplo es de estilo proto2. Erratum del material, con riesgo de repetirlo en un parcial.
- No trata deadlines/timeouts ni interceptores de gRPC, que son el equivalente práctico de "manejar fallas parciales".
- La tabla gRPC vs REST fija REST a HTTP/1.1, cuando REST también corre sobre HTTP/2. La comparación es de implementaciones típicas, no de límites del estilo.

## Dicho en clase

### 2026-10-08 ([[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]])
- Se mostro un **"RPC" construido sobre RabbitMQ**: un `RpcClient` con `CallAsync` que devuelve una promesa, y un correlation id para vincular la respuesta. Para quien llama parece una funcion local que devuelve un futuro. Por eso se llama *Remote Procedure Call*. (stated)
- El docente aclaro que **"todavia no vimos RPC"**: *"la idea es parecida, pero es otra cosa"*. El tema propio viene despues. (stated)
- Cancelar una llamada remota necesita **un mensaje de cancelacion** que dispare otro token del lado del servidor. Quedo sin implementar en el ejemplo. (stated)

## Connections
- Contrapunto asíncrono: [[Tema 4 - Colas de mensajes y MOM]]. Contrapunto sobre HTTP: [[Tema 6 - APIs REST y WebSockets]].
- Lo que gRPC evita hacer a mano: [[Tema 2 - Sockets y protocolos]]. Criterio de elección: [[Elegir tecnologia de comunicacion]].
- Antecedente del programa anterior sobre contratos y WSDL: [[Referencia - Web Services del programa anterior]].
- Formato de examen: [[Patrones de examen en Programacion de Redes]].
