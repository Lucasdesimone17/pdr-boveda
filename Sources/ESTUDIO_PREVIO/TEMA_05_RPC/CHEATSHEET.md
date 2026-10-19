# Cheat Sheet — TEMA 5: Invocaciones Remotas (RPC/gRPC) (PDR)

Fuente: 🟢 `RPC.pdf` (oficial). Sin prosa, solo tablas/sintaxis de referencia rápida.

## RPC — lo esencial

| Concepto | Detalle |
|---|---|
| Origen | Birrell y Nelson, 1984 |
| Paradigma | Procedural |
| Mecanismo | Síncrono — cliente se suspende hasta recibir respuesta |
| Flujo | Cliente llama → Client Stub empaqueta → red → Server Stub desempaqueta → ejecución → respuesta (camino inverso) |
| Capa (modelo de capas) | Application (junto a MOM, ORB, Named Pipes, DCE, SUN, Peer-to-Peer) |
| Mecanismos previos que reemplazó | Sockets, Named Pipes, Shared Memory + protocolos propietarios |

## Evolución RPC → ORB

| Era | Tecnología | Empresa/Org |
|---|---|---|
| Procedural | Courier | Xerox |
| Procedural | ONC RPC | Sun Microsystems |
| Procedural | MSRPC | Microsoft |
| Orientada a objetos | CORBA | OMG |
| Orientada a objetos | RMI | Java |
| Orientada a objetos | Remoting | Microsoft |

## Retos de RPC/ORB (7 preguntas oficiales)

1. ¿Cómo encuentra el cliente al servidor?
2. ¿Cómo se organizan/arrancan los canales?
3. ¿Qué pasa con concurrencia de múltiples clientes?
4. ¿Cómo se definen/pasan parámetros?
5. ¿Cómo se maneja heterogeneidad de representación?
6. ¿Cómo se manejan excepciones?
7. (ORB) ¿Cómo consumir objetos remotos de forma transparente?

## Canales

| Estrategia | Ventaja | Desventaja |
|---|---|---|
| Conexiones múltiples | Sincronización la da el canal | Limita concurrencia a puertos disponibles |
| Conexión única (Peer to Peer) | Sin problemas de concurrencia a nivel canal | Si no es P2P: sincronización compleja (tablas de bloqueo) |
| Concurrencia | Se resuelve con monitores, locks, semáforos | Responsabilidad del programador |

## Ubicación del servidor

| Enfoque | Ejemplo |
|---|---|
| Ruta absoluta (URL, tipo IPEndPoint) | Remoting |
| Directorio | RMI, UDDI |
| Descubrimiento automático (broadcast) | No sirve en WAN |

## Serialización / Marshalling

| Concepto | Definición corta |
|---|---|
| Serialización | Convertir datos a formato transportable (SOAP, JSON, etc.) |
| Representación canónica | Estructura estándar intermedia (ej. CORBA) |
| Marshalling | Transformar representación de datos para transferirla por red (cita Wikipedia) |

## Excepciones remotas

Se capturan en destino → se serializan → viajan al origen → se deserializan → se re-disparan como si fueran locales.

## Stubs, Skeletons, IDL

| Pieza | Lado | Función |
|---|---|---|
| Stub | Cliente | Emula al objeto remoto localmente |
| Skeleton | Servidor | Captura llamadas del cliente, ejecuta sobre el objeto real |
| IDL | Ambos | Lenguaje neutral que define el contrato; se compila y genera Stub/Skeleton (ej. CORBA) |

## MOM vs RPC

| | MOM (Postal) | RPC (Teléfono) |
|---|---|---|
| Ambos extremos activos | No | Sí |
| Relación de tiempo | Asincrónica | Sincrónica |
| Secuencia | Sin secuencia fija | Servidor debe estar activo antes |
| Estilo | Encolamiento | Devolución de llamadas |
| Equilibrio de carga | Cola FIFO/prioridades | Monitor transaccional |
| Soporte transaccional | Cola participa en commit | Necesita RPC transaccional |
| Filtro de mensajes | Sí | No |
| Desempeño | Lento (salto intermedio) | Veloz |
| Procesamiento asincrónico | Sí (cola + triggers) | Limitado (threads manuales) |

## gRPC — datos clave

| Dato | Valor |
|---|---|
| Creador | Google (2015) |
| Transporte | HTTP/2 |
| Serialización | Protocol Buffers (Protobuf, binario) |
| Seguridad | TLS |
| IDL | Estricto (`.proto`) |
| Streaming | Sí (4 tipos) |

## HTTP/2 vs HTTP/1

| Feature | HTTP/2 |
|---|---|
| Multiplexed Streams | Múltiples request/response en una sola conexión |
| Server Push | Servidor envía recursos sin que el cliente los pida |
| Formato | Binario (vs texto plano) |
| Headers | Comprimidos en un solo bloque |

## Protobuf vs JSON

| | Protobuf | JSON |
|---|---|---|
| Desarrollador | Google | Douglas Crockford |
| Binario | Sí | No |
| Estandarización | No | Sí |
| Legible por humano | En parte | Sí |
| Self-describing | No | Sí |

## gRPC vs REST

| Característica | gRPC | REST |
|---|---|---|
| Protocolo | HTTP/2 (rápido) | HTTP/1.1 (lento) |
| Carga útil | Protobuf (binario) | JSON (texto) |
| Contrato API | Estricto (`.proto`) | Flexible (OpenAPI opcional) |
| Generación código | Integrada | Terceros (Swagger) |
| Seguridad | TLS/SSL | TLS/SSL |
| Streaming | Bidireccional | Solo cliente→servidor |
| Soporte navegador | Limitado (gRPC-Web) | Sí |

## Tipos de servicio gRPC

| Tipo | Cliente | Servidor |
|---|---|---|
| Unario | 1 request | 1 response |
| Server Streaming | 1 request | stream de responses |
| Client Streaming | stream de requests | 1 response |
| Bidireccional | stream | stream |

## Sintaxis .proto (ejemplos oficiales)

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
```

```proto
service ChatService {
  rpc chat (stream ChatMessage) returns (stream ChatMessage);
}
```

## gRPC — ventajas / desventajas

| Ventajas | Desventajas |
|---|---|
| Performance | Definición estricta |
| Resuelve problema histórico de RPC sin HTTP | No tan difundido |
| Acoplamiento bajo | |
| Multiplataforma/multilenguaje real | |

## Implementación .NET — notas rápidas

| Elemento | Detalle |
|---|---|
| Paquete servidor | `Grpc.AspNetCore` |
| URL de exposición | `applicationUrl` en `launchSettings.json` |
| Código generado del `.proto` | `// DO NOT EDIT!` — no se toca a mano |
| Clase de servicio | Hereda de `<Servicio>Base` autogenerada, ej. `Greeter.GreeterBase` |
| Acceso directo por navegador | Falla — se necesita un cliente gRPC |

## Preguntas típicas de parcial

🔵 **Frecuencia histórica:** RPC/gRPC aparece en 13 de 16 letras revisadas. Practicar contratos `.proto`, unary/streaming y justificación frente a REST, MOM o Sockets.
