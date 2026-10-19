# Cheat Sheet — TEMA 6: APIs REST (PDR)

Fuentes: 🟢 temario vigente de Aulas + `REST APIs 2025.pdf` + `WebSockets.pdf`; 🟠 documentación oficial de Microsoft para Web API, capas, DI y microservicios.

## API y REST — lo esencial

| Concepto | Definición |
|---|---|
| API | Contrato/especificación: operaciones + descripción de qué hacen |
| REST | Estilo de arquitectura para APIs de sistemas distribuidos |
| Creador | Roy Fielding, tesis de doctorado, año 2000 |
| REST = | **Re**presentational **S**tate **T**ransfer |
| Recurso | Unidad de información con estado, identificado por URI |
| RESTful | Arquitectura que cumple todas las restricciones REST |

## Propiedades RESTful

Simplicidad · Escalabilidad · Modificabilidad · Performance · Portabilidad · Fiabilidad

## Las 9 restricciones (tabla oficial)

| Constraint | Propiedades |
|---|---|
| Cliente-Servidor | Simplicidad, escalabilidad, evolucionable |
| Stateless | Escalabilidad, fiabilidad, visibilidad |
| Cacheable | Escalabilidad, performance |
| Uniform Interface | Simplicidad, evolucionable, fácil de leer |
| Sistema en capas | Escalabilidad, seguridad, integración legados |
| Identificación de recursos | Simplicidad, escalabilidad |
| Manipulación mediante representación | Evolucionable |
| Mensajes auto-descriptivos | Requiere stateless+cacheable+uniform+capas |
| HATEOAS | Escalabilidad, fiabilidad, evolucionable, performance |
| Code on demand (ÚNICA OPCIONAL) | Extensibilidad, simplicidad |

## 4 sub-restricciones de Uniform Interface

1. Identificación de recursos
2. Mensajes auto-descriptivos
3. Manipular recursos (mediante representación)
4. HATEOAS

## Las 8 falacias de la programación en redes

1. La red es confiable
2. La latencia es cero
3. Ancho de banda infinito
4. La red es segura
5. Topología no cambia
6. Único administrador
7. Costo de transporte cero
8. La red es homogénea

## Estándares en que se apoya REST

HTTP 1.1 (RFC 2616) · URI (RFC 3986) · MIME (RFC 2045/2046)

## Línea de tiempo APIs distribuidas

`RPC/Messaging/Queuing (1970s)` → `COM/DCOM/CORBA/Java RMI (1990s)` → `XMLHTTP/REST/SOAP (2000s)` → `GraphQL (2010s)` → `gRPC (2020s)`

## HTTP Request

| Parte | Contenido |
|---|---|
| Verb | GET/POST/PUT/PATCH/DELETE |
| Headers | Content-Type, Content-Length, Authorization, Accept, Cookies |
| Content/Body | HTML, CSS, JS, XML, JSON, binarios |

## Verbos HTTP

| Verbo | Acción | Idempotente |
|---|---|---|
| GET | Request Resource | Sí |
| POST | Create Resource | **No** |
| PUT | Update Resource (completo) | Sí |
| PATCH | Update Partial Resource | Sí |
| DELETE | Delete Resource | Sí |

## HTTP Response — status codes

| Rango | Categoría |
|---|---|
| 100-199 | Informational |
| 200-299 | Success |
| 300-399 | Redirection |
| 400-499 | Client Errors |
| 500-599 | Server Errors |

## Diseño de URI

| Regla | Ejemplo correcto | Ejemplo incorrecto |
|---|---|---|
| Sustantivos, no verbos | `/students` | `/getStudent` |
| Query path para recurso único | `/teachers/pepeBotella` | — |
| Query parameters para filtrado | `/students?lastName=Botella&active=true` | — |
| Navegación por asociaciones | `/teachers/pepeBotella/courses` | — |

## Verbos + recursos (tabla oficial)

| Recurso | GET | POST | PUT/PATCH | DELETE |
|---|---|---|---|---|
| `/students` | Lista | Crear | Actualizar varios | - |
| `/students/99851` | Uno | - | Actualizar uno | Borrar uno |

## Versionado de APIs — 4 estrategias

| Estrategia | Ejemplo |
|---|---|
| URI Path | `/v1/students` vs `/v2/students` |
| Query String | `?version=1.0` vs `?version=2.0` |
| Headers propios | `Api-Version: 1.0` |
| Headers de aceptación | `Accept: application/json; version=1.0` |
| Tipos de contexto | `Accept: application/version.model.v1+json` |

## Alternativas a Request/Response clásico

| Alternativa | Dirección | Nota |
|---|---|---|
| Short Polling | Cliente pregunta seguido | Respuesta inmediata |
| Long Polling | Cliente pregunta, espera | Servidor responde cuando hay datos o timeout |
| SSE (Server-Sent Events) | Unidireccional (servidor→cliente) | Soportado por navegadores; similar a gRPC server streaming |
| WebSocket | Bidireccional (full duplex) | No sigue REST |
| Webhook | Servidor→servidor/cliente | "HTTP Callback", evita polling entre servidores |

## Long-running task (polling design oficial)

`POST → HTTP 202/Location → GET repetido a Status API (200) → GET a Resource API (resultado)`

## WebSockets — datos clave

| Dato | Valor |
|---|---|
| RFC | 6455 |
| Quién inicia | El cliente |
| Comunicación | Bidireccional (full duplex) |
| Servidor puede enviar sin request del cliente | Sí |

## WebSocket vs REST (tabla oficial)

| | API REST | WebSockets |
|---|---|---|
| Protocolo | HTTP/HTTPS | WS/WSS |
| Duplex | Semi duplex | Full duplex |
| Conexión | Corta | Indefinida |
| Latencia | Alta | Baja |
| Velocidad | Lenta | Rápida |
| Usos | Carga inicial, CRUD | Tiempo real, chats, juegos |

## Eventos WebSocket

`Open` → `Message` (bidireccional) → `Error` → `Close`

## WebSocket — pros/contras

| Pros | Contras |
|---|---|
| Bidireccional en tiempo real | Implementación compleja |
| Soporte nativo navegadores | Mayor consumo de recursos |
| Menor sobrecarga que HTTP | Difícil escalado horizontal |
| Multiplataforma/multilenguaje | |

## Sintaxis WebSocket (JS)

```javascript
const webSocket = new WebSocket("ws://localhost:5000/ws");
webSocket.addEventListener("open", () => {});
webSocket.addEventListener("message", (event) => {});
webSocket.addEventListener("error", (event) => {});
webSocket.addEventListener("close", (event) => {});
webSocket.send(message);
```

## ASP.NET Core Web API — mínimo

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

## Capas

| Capa | Responsabilidad |
|---|---|
| API/Presentación | HTTP, rutas, DTO y status codes |
| Aplicación | Casos de uso y coordinación |
| Dominio | Entidades y reglas del negocio |
| Infraestructura | Persistencia y clientes externos |

`Controller → Service → Repository`; la implementación concreta se conecta mediante DI.

## Inyección de dependencias

| Registro | Duración |
|---|---|
| `AddTransient` | Nueva instancia cada resolución |
| `AddScoped` | Una instancia por request HTTP |
| `AddSingleton` | Una instancia para toda la aplicación |

- Preferir inyección por constructor.
- No crear repositorios/servicios con `new` dentro del controller.
- No capturar un servicio `Scoped` directamente desde un `Singleton`.

## Microservicios

- Capacidad de negocio autónoma y despliegue independiente.
- Contrato estable: URI, verbos, DTO y códigos.
- Datos propios por servicio; evitar tablas compartidas como acoplamiento.
- REST: request/response; RabbitMQ: eventos/trabajo asíncrono; gRPC: contrato interno tipado; WebSocket: tiempo real.
- Diseñar para timeouts, fallas parciales, reintentos e idempotencia.

## Respuestas Web API típicas

| Caso | Respuesta |
|---|---|
| Lectura | `Ok(value)` → 200 |
| Creación | `CreatedAtAction(...)` → 201 + Location |
| Sin contenido | `NoContent()` → 204 |
| Entrada inválida | `BadRequest(...)` → 400 |
| No existe | `NotFound()` → 404 |
| Conflicto | `Conflict(...)` → 409 |

## Preguntas típicas de parcial

🔵 **Frecuencia histórica:** REST aparece en 11 de 16 letras revisadas. Practicar recursos, URI, verbos, códigos, idempotencia y justificación arquitectónica.

## Estado

Temario vigente completo. Solo queda validar durante el semestre la versión de .NET, las convenciones de proyecto y los ejercicios concretos elegidos por la docente.
