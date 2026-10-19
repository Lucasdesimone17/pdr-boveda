Fecha: 1/12/2025
Evaluación: Parcial 1
Materia: Programación de redes
Turno: Nocturno

Duración: 2 horas
Uso de Calculadora:    NO
Uso de Material:     NO
Puntaje Máximo:     35 Puntos

Página 1 de 2

Ejercicio 1

A.  ¿En qué se diferencian los procesos de los hilos?¿Para qué sirve el .Start() de Threading?¿Cuál es

la diferencia entre Thread de Foreground y de Background?

B.  Explique qué es la programación asincrónica, qué signiﬁcan los Tasks<genéricos> o Promesas y

cómo se aplican para la devolución de resultados, y por qué es mejor que la programación
basada en Threads. ¿Un programa de usuario (de un hilo) se puede beneﬁciar con la
programación asincrónica?

C.  Explique los diferentes mecanismos de streaming disponibles en gRPC. Dé un ejemplo de

aplicación.

D.  Explique el funcionamiento de gRPC. Explique cómo a partir del archivo de protocolo se genera el

código del lado del cliente y del servidor.

E.  Explique el funcionamiento de MOM. En particular la implementación de RabbitMQ. Describa los
tipos de enrutamiento vistos en clase para AMQP. Explique el funcionamiento de cada uno.
F.  Explique cómo debe ser el uso de los verbos en REST-API. Dé unos ejemplos con listados, altas

bajas y modiﬁcaciones.

G.  Explique cómo funciona la conectividad con WebSockets. Cómo se logra que la comunicación

sea Full-Duplex. Explique un pseudocódigo de cómo se implementa la comunicación Full-Duplex.

Ejercicio 2

# version is now using "compose spec"
# v2 and v3 are now combined!
# docker-compose v1.27+ required

services:
  mom:
    image: rabbitmq:4.2.1-management-alpine
    ports:
      - "15672:15672"
      - "5672:5672"
    networks:
      - front-tier
      - back-tier

  class_server:
    build:
      context: ./class_server
      target: runtimeserv
    depends_on:
      mom:
    volumes:
     - ./class_server/data:/usr/local/app/data
    ports:

Fecha: 1/12/2025
Evaluación: Parcial 1
Materia: Programación de redes
Turno: Nocturno

Duración: 2 horas
Uso de Calculadora:    NO
Uso de Material:     NO
Puntaje Máximo:     35 Puntos

Página 2 de 2

      - "5000:5000"
      - "5001:5001"
    networks:
      - front-tier
      - back-tier

  log-server:
    build:
      context: ./log_server
      target: runtimelog
    depends_on:
      mom:
    volumes:
      - ./log_server/data:/usr/local/app/data
    ports:
      - "8081:80"
      - "127.0.0.1:5002:5002"
    networks:
      - front-tier
      - back-tier

  chatserver:
    build:
      context: ./chatserver
    depends_on:
      class_server:
    ports:
      - "8080:80"
      - "127.0.0.1:5003:5003"
    networks:
      - front-tier
      - back-tier

networks:
  front-tier:
  back-tier:

A.  Explica brevemente qué problema soluciona Docker.
B.  Explique qué ventaja tiene el uso de docker compose)
C.  Describe a grandes rasgos qué define el docker compose de arriba. Y haz un diagrama de la

arquitectura posible.

