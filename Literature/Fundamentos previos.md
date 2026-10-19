---
date: 2026-08-26
type: literature
tags:
  - literature
  - pdr
  - fundamentos
  - nivelacion
ai-first: true
source: "Sources/ESTUDIO_PREVIO/TEMA_00_FUNDAMENTOS_PREVIOS/RESUMEN.md"
confidence: medium
oficial: false
---

# Fundamentos previos

## For future agent
Nota de nivelacion, **no oficial**. El material fuente es un resumen generado por un asistente (marcado `[Complementario - Claude]` en el original), no un PDF de Aulas: cubre lo que la materia da por sabido desde la primera clase (programa/proceso/hilo, condicion de carrera, IP, puertos, TCP/UDP, sockets). Usarlo para nivelar vocabulario, **no** como fuente citable en un parcial. Lo oficial equivalente esta en [[Tema 1 - Programacion multihilo]] y [[Tema 2 - Sockets y protocolos]].

## Source
`Sources/ESTUDIO_PREVIO/TEMA_00_FUNDAMENTOS_PREVIOS/RESUMEN.md`. Origen declarado en el propio archivo: no sale de ningun PDF de Aulas.

## Key Claims

### Programa, proceso e hilo
| Concepto | Que es |
|---|---|
| Programa | Archivo en disco con instrucciones; todavia no corre |
| Proceso | Programa en ejecucion, con memoria y recursos propios, aislado de otros procesos |
| Hilo | Linea de ejecucion dentro de un proceso; todos los hilos del proceso comparten memoria y recursos |

Que los hilos compartan memoria es lo que los hace rapidos y, al mismo tiempo, la causa de todos los problemas del Tema 1: de ahi que existan `lock`, semaforos y monitores.

### Concurrencia vs paralelismo (formulacion de Rob Pike, tambien citada en el material oficial del Tema 3)
- **Concurrencia**: manejar varias tareas en el mismo periodo, intercalandolas. Es una forma de **estructurar** el problema.
- **Paralelismo**: ejecutar varias tareas literalmente al mismo tiempo, en nucleos distintos. Es una forma de **ejecutar**.
- Con un solo nucleo se puede tener concurrencia sin paralelismo.

### Condicion de carrera
Dos o mas hilos acceden y modifican el mismo dato compartido sin coordinacion. El ejemplo canonico es el saldo bancario: "depositar" no es atomico, son leer, sumar y escribir; si dos hilos intercalan esos pasos, un deposito se pierde.

### Resto del alcance declarado
Sincronizacion, IP, puertos, TCP/UDP y sockets, como nivelacion previa a los temas 1 y 2. El archivo trae checkpoints de autoevaluacion al final de cada seccion.

## Methodology
Explicacion por analogias (la cocina, la cuenta bancaria) con checkpoints. Formato de repaso, no de teorico.

## Critique
- **No es fuente oficial.** Todo lo que se responda en un parcial deberia poder rastrearse al teorico o a las guias, no a esta nota. El propio archivo lo advierte.
- Utilidad real: leerlo una vez antes de la primera clase para reconocer vocabulario. Despues queda redundante con los temas 1, 2 y 3.

## Connections
- Nivela para [[Tema 1 - Programacion multihilo]], [[Tema 2 - Sockets y protocolos]] y [[Tema 3 - Asincronia y paralelismo]].
- Version oficial de los mismos conceptos: [[Complementario - Introduccion y Streams]].
- Estado del material: [[Huecos y contradicciones del material PDR]].
