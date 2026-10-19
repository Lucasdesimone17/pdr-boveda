Fecha: 03/02/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 1 de 12

Puntaje Máximo:     100 Puntos

Ejercicio 1 (40 puntos)

Responda 6 de las siguientes 8 preguntas. Respuestas breves, claras y con ejemplos cuando aplique.

1) Concurrencia vs paralelismo

●  Concurrencia: múltiples tareas progresan “intercaladas” (aunque haya 1 CPU). Ej: servidor

atiende varios sockets alternando esperas de I/O con await.

●  Paralelismo: tareas ejecutan simultáneamente en varios cores. Ej: procesamiento pesado de

imagen en Parallel.For o Task.Run en varias CPU.

●  En un servidor real suele haber concurrencia I/O + paralelismo en CPU cuando hay trabajo

pesado.

2) I/O-bound vs CPU-bound en .NET

●

I/O-bound: espera red/disco/db; usar async/await sin bloquear hilos (ReadAsync, WriteAsync).

●  CPU-bound: cálculos intensivos; usar paralelismo/threads/Task.Run (con cuidado) para no

bloquear el hilo request.

●  Relación: async mejora escalabilidad en I/O; paralelismo mejora throughput en CPU.

3) Idempotencia

●  Operación idempotente: repetirla produce el mismo efecto ﬁnal (ej: PUT /users/5 setea estado).

●

Importante por reintentos: si el cliente reintenta por timeout, no debe duplicar efectos.

●  Ejemplo: POST /payments no siempre idempotente; se resuelve con Idempotency-Key.

4) Polling vs SSE vs WebSockets

●  Short polling: el cliente consulta cada X segundos (simple pero carga al server).

Fecha: 03/02/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 2 de 12

Puntaje Máximo:     100 Puntos

●  Long polling: el server mantiene la request abierta hasta que hay novedades (menos requests,

pero consume conexiones).

●  SSE: unidireccional server→client con HTTP streaming (ideal para eventos en tiempo real

simples).

●  WebSockets: full duplex (client↔server) con conexión persistente (chat, edición colaborativa).

●  Mini-diagrama (texto):

○  Polling: C→S GET status (cada X seg)

○  SSE: C→S GET /events (queda abierto) y S→C events

○  WS: handshake y luego mensajes en ambos sentidos

5) MOM RabbitMQ: manejo de fallos

●  Usar ack solo cuando procesó OK.

●  Ante error: nack/reject con requeue o no.

●  Reintentos con backoff: “retry queue” + TTL y luego vuelve.

●  Usar Dead Letter Exchange/Queue (DLX/DLQ) para mensajes que fallan muchas veces.

●  Registrar y alertar.

6) gRPC vs REST

●  gRPC: binario (protobuf), más eﬁciente; contrato fuerte; streaming; ideal inter-servicios.

●  REST: texto/JSON, fácil para navegadores y debugging; mejor compatibilidad pública.

●  Evolución: protobuf soporta compatibilidad agregando campos; REST requiere versionado

cuidadoso.

Fecha: 03/02/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 3 de 12

Puntaje Máximo:     100 Puntos

7) Race condition + mitigación

●  Race: resultado depende del orden de ejecución de hilos/tareas.

●  Mitigar con lock, Interlocked.Increment, ConcurrentDictionary, colas thread-safe, evitar estado

compartido.

8) ThreadPool y Task

●  ThreadPool = conjunto de hilos reutilizables. Task suele correr ahí.

●  Ventaja Task: scheduling/await, composición, menos overhead, uso eﬁciente del pool.

●  Riesgo: bloquear threads del pool (con .Result/.Wait o I/O sin async) causa starvation.

Fecha: 03/02/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 4 de 12

Puntaje Máximo:     100 Puntos

Ejercicio 2 (15 puntos)

a) Diagrama de secuencia (texto)

Cliente                        Servidor

  |--- POST /jobs ------------->|

  |<-- 202 Accepted + Location -|

  |--- GET /jobs/8841 --------->|

  |<-- 200 RUNNING -------------|

  |--- GET /jobs/8841 --------->|

  |<-- 200 DONE ----------------|

  |--- GET /jobs/8841/result -->|

  |<-- 200 RESULT --------------|

b) Patrón y beneﬁcios/desventajas

●  Patrón: Short Polling

●  Beneﬁcios:

○  El servidor no bloquea request esperando terminar.

○  Aísla cargas largas y permite escalado del worker.

○  El cliente puede consultar estado y resultado.

●  Desventajas:

○  Polling genera tráﬁco extra.

Fecha: 03/02/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 5 de 12

Puntaje Máximo:     100 Puntos

○  Latencia depende del intervalo de polling.

○  Hay que gestionar expiración/limpieza de jobs.

c) Alternativa sin polling (ejemplo con SSE)

●  Cliente:

○  POST /jobs → recibe jobId

○  abre GET /jobs/{id}/events (SSE)

●  Servidor:

○  emite eventos: RUNNING, PROGRESS 30%, DONE, RESULT_READY

●  Beneﬁcio: menos requests repetidas, actualizaciones inmediatas.

Fecha: 03/02/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 6 de 12

Puntaje Máximo:     100 Puntos

Ejercicio 3 (45 puntos)

1 - Pseudocodigo

class TicketServer

{

    TcpListener listener;

    int maxClients = N;

    // Contadores seguros

    int nextClientId = 0;

    int nextTicket = 0;

    // Estado compartido (turnos)

    ConcurrentDictionary<int, string> ticketState = new();

    // Limitar conexiones simultáneas

    SemaphoreSlim slots;

    async Task RunAsync(int port)

    {

        slots = new SemaphoreSlim(maxClients, maxClients);

Fecha: 03/02/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 7 de 12

Puntaje Máximo:     100 Puntos

        listener = new TcpListener(IPAddress.Any, port);

        listener.Start();

        Console.WriteLine("Server started");

        while (true)

        {

            TcpClient client = await listener.AcceptTcpClientAsync();

            // si está lleno, rechazo rápido:

            if (!await slots.WaitAsync(0))

            {

                // opcional: mandar mensaje antes de cerrar

                SafeClose(client);

                Console.WriteLine("Rejected client (max reached)");

                continue;

            }

            _ = HandleClientAsync(client); // ﬁre & forget

        }

    }

Fecha: 03/02/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 8 de 12

Puntaje Máximo:     100 Puntos

    async Task HandleClientAsync(TcpClient client)

    {

        int clientId = Interlocked.Increment(ref nextClientId);

        try

        {

            using NetworkStream stream = client.GetStream();

            using var reader = new StreamReader(stream);

            using var writer = new StreamWriter(stream) { AutoFlush = true };

            await writer.WriteLineAsync($"WELCOME {clientId}");

            Console.WriteLine($"Client connected: {clientId}");

            while (true)

            {

                // ReadLineAsync devuelve null si el cliente cerró

                string line = await reader.ReadLineAsync();

                if (line == null)

                {

                    Console.WriteLine($"Client disconnected: {clientId}");

                    break;

                }

Fecha: 03/02/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 9 de 12

Puntaje Máximo:     100 Puntos

                Console.WriteLine($"[{clientId}] > {line}");

                string[] parts = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 0) continue;

                string cmd = parts[0].ToUpperInvariant();

                if (cmd == "PING")

                {

                    await writer.WriteLineAsync("PONG");

                }

                else if (cmd == "TAKE")

                {

                    int ticket = Interlocked.Increment(ref nextTicket);

                    ticketState[ticket] = "PENDING";

                    await writer.WriteLineAsync($"TICKET {ticket}");

                }

                else if (cmd == "STATUS")

                {

                    if (parts.Length < 2 || !int.TryParse(parts[1], out int t))

                    {

                        await writer.WriteLineAsync("ERROR BAD_REQUEST");

Fecha: 03/02/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 10 de 12

Puntaje Máximo:     100 Puntos

                        continue;

                    }

                    if (ticketState.TryGetValue(t, out string state))

                        await writer.WriteLineAsync($"STATUS {t} {state}");

                    else

                        await writer.WriteLineAsync($"STATUS {t} UNKNOWN");

                }

                else if (cmd == "QUIT")

                {

                    await writer.WriteLineAsync("BYE");

                    break;

                }

                else

                {

                    await writer.WriteLineAsync("ERROR UNKNOWN_COMMAND");

                }

            }

        }

        catch (IOException)

        {

            // típico: desconexión abrupta, timeout, etc.

Fecha: 03/02/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 11 de 12

Puntaje Máximo:     100 Puntos

            Console.WriteLine($"Client {clientId} lost connection (IO error)");

        }

        catch (Exception ex)

        {

            Console.WriteLine($"Client {clientId} error: {ex.Message}");

        }

        ﬁnally

        {

            SafeClose(client);

            slots.Release();

            Console.WriteLine($"Client handler ended: {clientId}");

        }

    }

    void SafeClose(TcpClient client)

    {

        try { client.Close(); } catch { }

    }

}

Fecha: 03/02/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 12 de 12

Puntaje Máximo:     100 Puntos

2 - Decisiones Técnicas

Evitar condiciones de carrera

●  ClientId y TicketNumber se incrementan con Interlocked.Increment (operación atómica).

●  El mapa de turnos usa ConcurrentDictionary para evitar lock manual y errores de sincronización.

Limitar N conexiones

●  Uso SemaphoreSlim(maxClients); al aceptar un cliente intento WaitAsync(0):

○  si hay cupo → atiendo.

○  si no hay cupo → rechazo/cierrro conexión.

●  Alternativa válida: cola de espera o responder “SERVER_BUSY”.

