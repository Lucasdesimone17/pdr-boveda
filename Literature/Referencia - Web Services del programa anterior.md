---
date: 2026-08-26
type: literature
tags:
  - literature
  - pdr
  - web-services
  - referencia
ai-first: true
source: "Sources/ESTUDIO_PREVIO/TEMA_06_WEB_SERVICES/RESUMEN.md"
confidence: stated
status: sin-material
---

# Referencia - Web Services del programa anterior

## For future agent
Nota de estado, no de contenido. "Web Services" es la unidad 6 del programa historico de 8 unidades y **no existe en el temario vigente de 6 temas**. Ademas **no hay material oficial**: la carpeta `TEORICO/06_WEB_SERVICES/` esta vacia, y ni SOAP ni WSDL aparecen en ninguna fuente descargada (auditoria del corpus previo, as of 2026-08-12). Esta nota existe para que nadie confunda "sin material" con "ya cubierto". No completar este tema por inferencia.

## Que pide el programa historico
Cita textual del PDF de programa: *"Web Services: Conceptos y caracteristicas. Implementacion. Integracion en arquitecturas distribuidas."* Nada mas concreto que eso.

## Por que esta vacio
- No hay PDF, guia ni diapositiva de Aulas sobre Web Services en el material local.
- Una version anterior del resumen del corpus previo tenia contenido de **Sockets** (modelo TCP/IP, clase `Socket`, TCP vs UDP, `TcpClient`/`TcpListener`) tomado de `0.RESUMEN-STUDOCU.pdf`. Se saco: no es especifico de Web Services y ya esta cubierto con fuente oficial en [[Tema 2 - Sockets y protocolos]]. Dejarlo etiquetado como "Web Services" daba la falsa impresion de cobertura.

## Que haria falta para desbloquearlo
1. Que la docente publique material especifico (lo esperable: SOAP, WSDL, quizas UDDI).
2. Colocarlo en `TEORICO/06_WEB_SERVICES/`.
3. Recien entonces escribir el resumen con fuente oficial primero.

## Pendiente de verificar
- Si "Web Services" en este curso significa estrictamente SOAP/WSDL, o si se cubre con otro enfoque (HTTP APIs en general). Sin material oficial no hay forma de saberlo.
- La relacion formal entre esta unidad y la seccion de Sockets: el programa no la explicita.

## Lo mas cercano que si existe
El contrato tipado y la generacion de codigo desde un IDL, que era el corazon de SOAP/WSDL, aparecen en el curso vigente via `.proto` y gRPC: [[Tema 5 - RPC y gRPC]]. La linea de tiempo del material lo dice: `RPC/Messaging (1970s)` -> `COM/DCOM/CORBA/RMI (1990s)` -> `XMLHTTP/REST/SOAP (2000s)` -> `GraphQL (2010s)` -> `gRPC (2020s)`.

## Connections
- [[Mapa del curso Programacion de Redes]] · [[Huecos y contradicciones del material PDR]] · [[Tema 5 - RPC y gRPC]] · [[Tema 6 - APIs REST y WebSockets]]
