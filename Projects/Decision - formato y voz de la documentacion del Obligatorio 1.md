---
date: 2026-09-27
type: decision
tags:
  - pdr
  - obligatorio-1
  - documentacion
  - decision
ai-first: true
confidence: stated
decidido_por: usuario
---

# Decision - formato y voz de la documentacion del Obligatorio 1

## For future agent
Decisiones del usuario del **2026-09-27** sobre como se escribe y se presenta la documentacion del Obligatorio 1. Valen tambien para los obligatorios 2 y 3 salvo que el usuario diga otra cosa. La skill `documentacion-entrega` (`[ruta local]`) aplica todo esto.

## Decisiones
1. **Formato de la entrega de DA2 del usuario** (`[ruta local]`), que tuvo mejor nota que la de otro grupo de DAPP2 usada como comparacion: portada ORT con integrantes `Nombre - numero`, docente, ano y link al repo; indice automatico; encabezado `EDISON — Documentación Obligatorio 1 Programación de Redes — 2026` y pie `Louro · Poli · [integrante]  Página N` en gris; texto serif justificado (Georgia, porque la Merriweather de DA2 no esta instalada); tablas simples sin color.
2. **Voz impersonal y compacta**, como la de DA2 ("se dividio", "el servidor responde"), sin primera persona, y sin el tono robotico de tablas para todo. El texto pasa por la skill `humanizer` sin inventar ni perder datos.
3. **Solo lo que pide la letra.** Se sacaron el resumen, la declaracion de autoria, las referencias, la tabla de responsabilidades por integrante y el plan de pruebas. Motivo, en palabras del usuario: "si la letra no la pide sacala".
4. **No inflar.** La entrega de DA2 llego a 67 paginas y el usuario la considero demasiado larga. Objetivo: 20 a 30 paginas; el Obligatorio 1 quedo en 22.
5. **Diagramas UML estilo Astah antes que tablas**, a partir de la devolucion de DA2 (80% en diseno y documentacion): diagrama de paquetes con los namespaces reales, diagramas de clases para los patrones y diagrama de despliegue, cada uno al lado del texto que explica.
6. **El uso de IA lo redacta el grupo** con sus palabras. La letra exige citar herramientas y contexto de uso, asi que el punto no se elimina. Queda un `[COMPLETAR]` hasta que se escriba.
7. **La documentacion no se commitea al arbol del repo.** Se entrega por Gestion (zip con el PDF) y se adjunta al Release de GitHub.
8. **Recorte a 17 paginas (as of 2026-09-27, noche).** Unos companeros le dijeron al usuario que la docu "estaba muy salada" y que PDR no pide una documentacion tan elaborada como DA2. Claude recomendo no recortar fuerte, porque la letra dice que la documentacion es "muy importante (tanto como la aplicacion)" y "Completitud y calidad de la documentacion" es criterio de correccion. El usuario pidio igual recortar lo que no pide la letra o se va de mambo. Resultado: de 21 a 17 paginas; se conservan todas las decisiones de protocolo, errores y concurrencia y lo necesario para probar en el laboratorio. Despues el usuario pidio sacar el diagrama de clases del patron State, que repetia el de estados de la reserva: quedan seis diagramas, en 17 paginas (respaldo en `Entrega Obligatorio 1\_respaldo\diagrama-3-patron-state-quitado`). Criterio para PDR a futuro: entre 15 y 20 paginas, sin repetir una idea en dos secciones y sin detalle fino (peores casos en KB, clases auxiliares, validaciones en setters). Respaldo de la version de 21 paginas: `Entrega Obligatorio 1\_respaldo\2026-09-27-antes-de-recortar-v2`.

## Connections
- [[Estado del Obligatorio 1 - 2026-09-27]] · [[2026-09-27 - Obligatorio 1 EDISON]]
- [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] · [[Decision - sin comentarios en el codigo del Obligatorio 1]]
