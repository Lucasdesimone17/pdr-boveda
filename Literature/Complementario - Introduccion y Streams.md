---
date: 2026-08-26
type: literature
tags:
  - literature
  - pdr
  - streams
  - sistemas-distribuidos
  - complementario
ai-first: true
source: "Sources/TEORICO/01_INTRODUCCION/"
source_extra:
  - "Sources/ESTUDIO_PREVIO/TEMA_99_OTROS/COMPLEMENTARIO_INTRODUCCION_Y_STREAMS/CHEATSHEET.md"
confidence: stated
---

# Complementario - Introduccion y Streams

## For future agent
Nota complementaria: la unidad de introduccion del programa historico (sistemas distribuidos, modelos de capas, procesos vs hilos, streams de C#). **No es uno de los 6 temas vigentes**, pero aporta el vocabulario transversal del curso y la tabla de streams, que es la que explica por que `NetworkStream` se usa igual que un `FileStream`. Fuentes: `1-Introduccion-2026.pdf`, `Introduccion.pdf` y `4-Stream.pdf`.

## Source
`Sources/TEORICO/01_INTRODUCCION/` - `1-Introduccion-2026.md`, `1-Introduccion (2026).md` (duplicado), `Introduccion.md`, `4-Stream.md`. Tabla de capas: `Sources/Tabla_comparativa_capas_Google_Docs.md`.

## Key Claims

### Evolucion historica
Mainframes (monolitico, sin estandares) -> PC (protocolos de red, estandarizacion) -> sistemas distribuidos (nodos autonomos, cliente-servidor 1 a N).

### Modelos de capas
| Modelo | Logica en | Escala |
|---|---|---|
| 2 capas | Cliente y/o servidor | Mala |
| 3 capas | Servidor, cliente liviano | Buena, modular |
| N capas | Servidor, dividida en N componentes | Muy buena, interoperable |

La tabla comparativa del curso agrega: complejidad baja/media/alta, mantenimiento dificil/moderado/facil, y caso de uso apps simples / web apps / sistemas empresariales.

### Nucleo vs hilo, proceso vs hilo
| | Nucleo | Hilo |
|---|---|---|
| Naturaleza | Fisico | Virtual o logico |
| Ejecuta | Un proceso a la vez | Depende del SO y del hyper-threading |

| | Proceso | Hilo |
|---|---|---|
| Memoria | Aislada por proceso | Compartida entre hilos del mismo proceso |
| Cambio de contexto | Costoso | Rapido |
| Comunicacion | Compleja | Directa, comparten memoria |

- Hilos **foreground** bloquean el fin del proceso padre (ej. guardado de archivos); **background** no lo bloquean (ej. notificaciones, cache).
- Patron de delegacion: un "gerente" delega a un "especialista" (otro hilo) para no bloquearse: `new Thread(objeto.Metodo).Start()`.

### Streams
| Stream | Uso |
|---|---|
| `FileStream` | Archivos en disco |
| `MemoryStream` | Un bloque de RAM tratado como archivo |
| `NetworkStream` | Conexion de red (sockets) |
| `BufferedStream` | Buffer para reducir accesos costosos |
| `CryptoStream` | Cifrado y descifrado automaticos |
| `PipeStream` | Comunicacion entre procesos en la misma maquina |

Operaciones base de cualquier stream: **lectura, escritura y busqueda (Seek)**. `InputStream` es entrada, `OutputStream` es salida. La abstraccion unica es la razon por la que el codigo de sockets se parece al de archivos.

### Mapa requerimiento a unidad (del programa historico de 8 unidades)
Multi-threading -> U2 · concurrencia y sincronizacion -> U2/U3 · comunicacion entre procesos -> U4/U5 · seguridad (autenticacion/autorizacion) -> U7 REST · servicios orientados a objetos -> U6 Web Services.

## Methodology
Material introductorio del programa historico, mas un teorico dedicado a streams. Hay **dos archivos con el mismo contenido** en la carpeta (`1-Introduccion-2026` y `1-Introduccion (2026)`, ambos de 1584 KB), diferenciados solo por la tilde del nombre.

## Critique
- La numeracion de unidades que usa esta introduccion es la del programa de 8 unidades, no la vigente de 6 temas: leerla sin ese contexto desordena el mapa. Ver [[Mapa del curso Programacion de Redes]].
- Los modelos de capas se comparan de forma muy gruesa ("escala mala/buena/muy buena") sin criterios medibles.
- Nada sobre teorema CAP, consistencia ni tolerancia a fallas, que es lo que uno esperaria de "sistemas distribuidos".

## Connections
- Da vocabulario a [[Tema 1 - Programacion multihilo]] y a [[Tema 2 - Sockets y protocolos]] (`NetworkStream`).
- Capas y N capas reaparecen en [[Tema 6 - APIs REST y WebSockets]].
- Mapa: [[Mapa del curso Programacion de Redes]].
