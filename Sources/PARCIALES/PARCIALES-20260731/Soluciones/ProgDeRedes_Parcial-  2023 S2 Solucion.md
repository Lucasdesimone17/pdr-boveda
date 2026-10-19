| Ejercicio |     | 1   |     |     |     |     |     |
| --------- | --- | --- | --- | --- | --- | --- | --- |
Ver Teorico
| Ejercicio |              | 2             |     |       |     |     |     |
| --------- | ------------ | ------------- | --- | ----- | --- | --- | --- |
| Dado      | el siguiente | código:       |     |       |     |     |     |
| static    | void         | Main(string[] |     | args) |     |     |     |
{
|     | Thread                     | t1    | = new        | Thread(() | =>  | LeerArchivo("archivo1.txt")); |             |
| --- | -------------------------- | ----- | ------------ | --------- | --- | ----------------------------- | ----------- |
|     | Thread                     | t2    | = new        | Thread(() | =>  | LeerArchivo("archivo2.txt")); |             |
|     | //                         | PARTE | 1: COMPLETAR |           |     |                               |             |
|     | Console.WriteLine("Termine |       |              |           |     | de leer ambos                 | archivos"); |
}
| static | void | LeerArchivo(string |     |     | filename) |     |     |
| ------ | ---- | ------------------ | --- | --- | --------- | --- | --- |
{
|     | string | contenidoDelArchivo |     |     |     | = File.ReadAllText(filename); |     |
| --- | ------ | ------------------- | --- | --- | --- | ----------------------------- | --- |
Console.WriteLine("Largo del archivo: " + contenidoDelArchivo.Length);
}
1. Agregue el código que falta, en la sección marcada, para que la aplicación funcione
|     | correctamente |     | (puede      | ser una  | o más        | líneas) |     |
| --- | ------------- | --- | ----------- | -------- | ------------ | ------- | --- |
|     | 2. Modificar  | el  | código para | utilizar | async/await. |         |     |
Además, en vez de imprimir el largo del mensaje para cada archivo, imprimir solo el largo
|     | del archivo |     | más largo |     |     |     |     |
| --- | ----------- | --- | --------- | --- | --- | --- | --- |
Nota: Existe la función Task<string> File.ReadAllTextAsync(string path)
Parte 1:

Parte 2:
Ejercicio 3
Ver teorico