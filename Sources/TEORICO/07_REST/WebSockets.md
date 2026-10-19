WebSockets

Introducción
WebSockets es un protocolo que proporciona un mecanismo para
aplicaciones basadas en navegador que necesitan comunicación
bidireccional con servidores, sin depender de la apertura de múltiples
conexiones HTTP.
RFC 6455
Claves
● El cliente inicia la conexión
● Comunicación bidireccional
● Servidor puede enviar mensajes sin una request del cliente

Pros y Contras
Pros Contras
● Comunicación bidireccional en ● Implementación compleja: Manejo de
tiempo real múltiples conexiones, eventos,
● Soporte nativo por los navegadores reconexiones
● Menor sobrecarga de red que HTTP ● Mayor consumo de recursos
● multiplataforma y multilenguaje ● Dificultad para escalado horizontal

WebSocket VS. HTTP
|                    | API REST    | WebSockets  |
| ------------------ | ----------- | ----------- |
| Protocolo          | HTTP/HTTPS  | WS/WSS      |
| Duplex             | Semi duplex | Full duplex |
| Tiempo de conexión | Corto       | Indefinido  |
| Latencia           | Alta        | Baja        |
| Velocidad          | Lenta       | Rápido      |
Usos Carga inicial, operaciones CRUD,  Apps en tiempo real, chats, juegos
descarga/subida de archivos
Modelo de comunicación Basado en REST Basado en conexión persistente (Eventos/Mensajes)

Casos de uso
● WhatsApp: Envío de mensajes, audios
● Uber: Envío de ubicación en tiempo real,
mensajería
● Figma, Google Docs: Colaboración en vivo
● Github: Notificaciones, mensajes, estado de
issues y pull request

Eventos
● Open: Inicio de conexión
● Message: Envío de mensajes
● Error: Fallo en la conexión
● Close:Cierre de conexión

Ejemplo cliente JavaScript

Ejemplo (Configuración y conexión)

Ejemplo (envío y recepción)