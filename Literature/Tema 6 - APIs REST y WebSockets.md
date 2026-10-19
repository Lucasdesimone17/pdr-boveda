---
date: 2026-08-26
type: literature
tags:
  - literature
  - pdr
  - tema-6
  - rest
  - webapi
  - websockets
ai-first: true
source: "Sources/TEORICO/07_REST/"
source_extra:
  - "Sources/ESTUDIO_PREVIO/TEMA_06_APIS_REST/CHEATSHEET.md"
confidence: stated
semanas: "15-16"
frecuencia_parcial: "11/16 (69%)"
---

# Tema 6 - APIs REST y WebSockets

## For future agent
Nota de tema 6 del temario vigente (semanas 15-16): REST como estilo arquitectónico, ASP.NET Core Web API, arquitectura en capas, inyección de dependencias, y WebSockets como alternativa full duplex. Frecuencia histórica de parcial: **11 de 16 letras (69%)**. Lo evaluable: recursos, URI, verbos, códigos de estado, idempotencia y justificación arquitectónica. Fuentes: `REST APIs 2025.pdf` y `WebSockets.pdf`, más documentación de Microsoft para Web API, capas y DI.

## Source
`Sources/TEORICO/07_REST/` - `REST APIs 2025.md`, `WebSockets.md`.

## Key Claims

### REST, lo esencial
- **API** = contrato: operaciones más la descripción de qué hacen. **REST** = estilo de arquitectura para APIs de sistemas distribuidos.
- Roy Fielding, tesis de doctorado, **2000**. REST = **Re**presentational **S**tate **T**ransfer.
- **Recurso** = unidad de información con estado, identificada por una URI. **RESTful** = arquitectura que cumple todas las restricciones.
- Propiedades buscadas: simplicidad, escalabilidad, modificabilidad, performance, portabilidad, fiabilidad.
- Se apoya en estándares: HTTP 1.1 (RFC 2616), URI (RFC 3986), MIME (RFC 2045/2046).

### Las 9 restricciones
Cliente-Servidor · Stateless · Cacheable · Uniform Interface · Sistema en capas · Identificación de recursos · Manipulación mediante representación · Mensajes auto-descriptivos · HATEOAS. **Code on demand es la única opcional** (dato de examen).

Las 4 sub-restricciones de Uniform Interface: identificación de recursos, mensajes auto-descriptivos, manipulación mediante representación y HATEOAS.

### Las 8 falacias de la programación en redes
La red es confiable · la latencia es cero · el ancho de banda es infinito · la red es segura · la topología no cambia · hay un único administrador · el costo de transporte es cero · la red es homogénea.

### HTTP: verbos e idempotencia
| Verbo | Acción | Idempotente |
|---|---|---|
| GET | Obtener recurso | Sí |
| POST | Crear recurso | **No** |
| PUT | Actualizar completo | Sí |
| PATCH | Actualizar parcial | Sí |
| DELETE | Borrar | Sí |

Códigos: 1xx informativos, 2xx éxito, 3xx redirección, 4xx error del cliente, 5xx error del servidor.

### Diseño de URI
| Regla | Correcto | Incorrecto |
|---|---|---|
| Sustantivos, no verbos | `/students` | `/getStudent` |
| Recurso único en el path | `/teachers/pepeBotella` | - |
| Filtros como query params | `/students?lastName=Botella&active=true` | - |
| Asociaciones navegables | `/teachers/pepeBotella/courses` | - |

Combinación verbo + recurso: `/students` → GET lista, POST crea; `/students/99851` → GET uno, PUT/PATCH actualiza uno, DELETE borra uno.

### Versionado, 4 estrategias
URI path (`/v1/students`) · query string (`?version=1.0`) · header propio (`Api-Version: 1.0`) · header de aceptación (`Accept: application/json; version=1.0`, o tipo de contexto `application/version.model.v1+json`).

### Alternativas al request/response clásico
| Alternativa | Dirección | Nota |
|---|---|---|
| Short polling | Cliente pregunta seguido | Respuesta inmediata |
| Long polling | Cliente pregunta y espera | El servidor responde cuando hay datos o vence el timeout |
| SSE | Servidor a cliente | Unidireccional, soportado por navegadores, análogo al server streaming de gRPC |
| WebSocket | Bidireccional | Full duplex, **no sigue REST** |
| Webhook | Servidor a servidor | "HTTP callback", evita polling |

Tarea de larga duración (diseño oficial): `POST` → `202` con `Location` → GET repetido a la Status API (`200`) → GET a la Resource API por el resultado.

### WebSockets
- RFC **6455**. Lo inicia el **cliente**. Comunicación **full duplex**; el servidor puede enviar sin request previo.
- Eventos: `Open` → `Message` (en ambos sentidos) → `Error` → `Close`.

| | REST | WebSockets |
|---|---|---|
| Protocolo | HTTP/HTTPS | WS/WSS |
| Duplex | Semi duplex | Full duplex |
| Conexión | Corta | Indefinida |
| Latencia | Alta | Baja |
| Usos | Carga inicial, CRUD | Tiempo real, chats, juegos |

Pros: tiempo real bidireccional, soporte nativo en navegadores, menos sobrecarga que HTTP, multiplataforma. Contras: implementación más compleja, más consumo de recursos, escalado horizontal difícil.

```javascript
const webSocket = new WebSocket("ws://localhost:5000/ws");
webSocket.addEventListener("open", () => {});
webSocket.addEventListener("message", (event) => {});
webSocket.send(message);
```

### ASP.NET Core Web API, capas y DI
```csharp
builder.Services.AddControllers();
builder.Services.AddScoped<IProductService, ProductService>();
app.MapControllers();
```

```csharp
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    public ProductsController(IProductService service) => _service = service;

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> Get(int id)
    {
        var value = await _service.GetById(id);
        return value is null ? NotFound() : Ok(value);
    }
}
```

Capas: API/Presentación (HTTP, rutas, DTO, códigos) → Aplicación (casos de uso) → Dominio (entidades y reglas) → Infraestructura (persistencia, clientes externos). El flujo es `Controller → Service → Repository`, conectado por DI.

| Registro | Duración |
|---|---|
| `AddTransient` | Nueva instancia por resolución |
| `AddScoped` | Una por request HTTP |
| `AddSingleton` | Una para toda la aplicación |

Reglas: preferir inyección por constructor, no crear servicios ni repositorios con `new` dentro del controller, y no capturar un `Scoped` desde un `Singleton`.

Respuestas típicas: `Ok(value)` 200 · `CreatedAtAction(...)` 201 + Location · `NoContent()` 204 · `BadRequest(...)` 400 · `NotFound()` 404 · `Conflict(...)` 409.

### Microservicios
Capacidad de negocio autónoma con despliegue independiente; contrato estable (URI, verbos, DTO, códigos); datos propios por servicio, evitando tablas compartidas como acoplamiento; y elección de mecanismo por enlace: REST para request/response, RabbitMQ para eventos y trabajo asíncrono, gRPC para contrato interno tipado, WebSocket para tiempo real. Diseñar para timeouts, fallas parciales, reintentos e idempotencia.

## Methodology
Dos teóricos (REST y WebSockets) más documentación oficial de Microsoft para la parte de implementación. La parte conceptual (restricciones, falacias) es de Fielding; la parte práctica es .NET puro.

## Critique
- HATEOAS se lista como restricción obligatoria pero no aparece en ningún ejemplo de código: es la brecha habitual entre REST de libro y "REST" de la industria.
- La tabla REST vs WebSockets dice "REST es lento" sin matizar: lo caro es abrir conexión por request, no el estilo.
- Seguridad: el programa histórico incluía "Seguridad" dentro de REST, y el material práctico no la desarrolla más allá de mencionar TLS y el header `Authorization`. Ver [[Huecos y contradicciones del material PDR]].

## Connections
- Comparación directa: [[Tema 5 - RPC y gRPC]]. Alternativa desacoplada: [[Tema 4 - Colas de mensajes y MOM]].
- Criterio de elección por enlace: [[Elegir tecnologia de comunicacion]].
- `async`/`await` en los controllers: [[Tema 3 - Asincronia y paralelismo]].
- Despliegue: [[Complementario - Docker y Cloud]]. Antecedente SOAP: [[Referencia - Web Services del programa anterior]].
