# Cheat Sheet — TEMA 1: Introducción (PDR)

## Evolución histórica
Mainframes (monolítico, sin estándares) → PC (protocolos de red, estandarización) → Sistemas distribuidos (autónomos, cliente-servidor 1 a N).

## Modelos de capas

| Modelo | Lógica en... | Escala |
|---|---|---|
| 2 capas | Cliente y/o servidor | Mala |
| 3 capas | Servidor (cliente liviano) | Buena, modular |
| N capas | Servidor, dividida en N componentes | Muy buena, interoperable |

## Requerimientos de red → unidad relacionada

- Multi-threading → U2 (Threads y Concurrencia)
- Concurrencia (sincronización) → U2 / U3 (Paralelismo)
- Comunicación entre procesos → U4 / U5 (Middleware, RPC/ORB)
- Seguridad (autenticación/autorización) → U7 (REST)
- Servicios orientados a objetos → U6 (Web Services)

## Núcleo vs. Hilo

| | Núcleo | Hilo |
|---|---|---|
| Naturaleza | Físico | Virtual/lógico |
| Ejecuta | 1 proceso a la vez | Depende del SO/hyper-threading |

## Proceso vs. Hilo programático

| | Proceso | Hilo |
|---|---|---|
| Memoria | Aislada por proceso | Compartida entre hilos del mismo proceso |
| Cambio de contexto | Costoso | Rápido |
| Comunicación | Compleja | Directa (comparten memoria) |

## Tipos de hilo

- **Foreground:** bloquea el fin del proceso padre. Ej: guardado de archivos.
- **Background:** no bloquea el fin del proceso padre. Ej: notificaciones, caché.

## Patrón Delegación
Un "gerente" delega la tarea a un "especialista" (otro hilo) para no bloquearse.
```csharp
Thread t = new Thread(objeto.Metodo);
t.Start();
```

## Streams — tabla rápida

| Stream | Uso |
|---|---|
| `FileStream` | Archivos en disco |
| `MemoryStream` | Bloque de RAM como archivo |
| `NetworkStream` | Conexión de red (sockets) |
| `BufferedStream` | Buffer para reducir accesos costosos |
| `CryptoStream` | Cifrado/descifrado automático |
| `PipeStream` | Comunicación entre procesos, misma máquina |

Operaciones base de cualquier stream: **Lectura, Escritura, Búsqueda (Seek)**.
InputStream = entrada · OutputStream = salida.
