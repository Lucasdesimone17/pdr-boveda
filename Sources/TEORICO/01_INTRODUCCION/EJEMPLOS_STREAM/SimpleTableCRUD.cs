using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

class Program
{
    // SizeConst = 20 means that the string occupies 20 characters
    // in the structure, including the terminating '\0'.
    const int MaxStringLength = 20;
    const int MaxTextLength = MaxStringLength - 1;

    const string FilePath = "data.bin";

    // Declaro la estructura binaria del registro
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct Record
    {
        public int ID;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxStringLength)]
        public string Name;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxStringLength)]
        public string LastName;

        public float Salary;
    }

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("       ARCHIVO DE REGISTROS");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Escribir un Registro");
            Console.WriteLine("2. Leer un Registro");
            Console.WriteLine("3. Modificar un Registro");
            Console.WriteLine("4. Listar todos los Registros");
            Console.WriteLine("5. Salir");
            Console.WriteLine("=================================");
            Console.Write("Ingresar una opción: ");

            string choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    WriteRecord();
                    break;

                case "2":
                    ReadRecord();
                    break;

                case "3":
                    ModifyRecord();
                    break;

                case "4":
                    ListRecords();
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine(
                        "Opción Inválida. Por favor inténtelo nuevamente.");
                    break;
            }
        }
    }

    // ------------------------------------------------------------
    // Valida una cadena y la prepara para almacenarla en el record.
    // ------------------------------------------------------------
    static string GetFixedLengthString(string prompt)
    {
        Console.Write(prompt);
        string value = Console.ReadLine();

        if (value == null)
            throw new ArgumentException("El texto no puede ser nulo.");

        if (value.Length > MaxTextLength)
        {
            throw new ArgumentException(
                $"El texto no puede superar {MaxTextLength} caracteres.");
        }

        // Rellenamos hasta SizeConst con '\0'
        return value.PadRight(MaxStringLength, '\0');
    }

    // ------------------------------------------------------------
    // Escribir un nuevo registro
    // ------------------------------------------------------------
    static void WriteRecord()
    {
        try
        {
            Console.Write("Ingrese el ID (entero): ");
            int id = int.Parse(Console.ReadLine());

            string name = GetFixedLengthString(
                $"Ingrese el Nombre (Hasta {MaxTextLength} caracteres): ");

            string lastName = GetFixedLengthString(
                $"Ingrese el Apellido (Hasta {MaxTextLength} caracteres): ");

            Console.Write("Ingrese el Salario (Punto Flotante): ");
            float salary = float.Parse(Console.ReadLine());

            // Guardamos los datos en el registro
            Record record = new Record
            {
                ID = id,
                Name = name,
                LastName = lastName,
                Salary = salary
            };

            // Si el archivo no existe se crea.
            // Si existe se agrega al final.
            using (FileStream fs = new FileStream(
                FilePath,
                FileMode.Append,
                FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // Obtenemos el tamaño del registro
                int size = Marshal.SizeOf(typeof(Record));

                // Creamos un buffer de bytes
                byte[] buffer = new byte[size];

                IntPtr ptr = Marshal.AllocHGlobal(size);

                try
                {
                    // Copiamos la estructura al buffer
                    Marshal.StructureToPtr(record, ptr, true);
                    Marshal.Copy(ptr, buffer, 0, size);
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr);
                }

                // Escribimos el buffer al archivo
                writer.Write(buffer);
            }

            Console.WriteLine(
                "El registro fue escrito satisfactoriamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Error escribiendo el registro: {ex.Message}");
        }
    }

    // ------------------------------------------------------------
    // Leer un registro
    // ------------------------------------------------------------
    static void ReadRecord()
    {
        try
        {
            Console.Write("Ingrese el número de registro a leer: ");
            int recordNumber = int.Parse(Console.ReadLine());

            if (recordNumber < 0)
            {
                Console.WriteLine(
                    "El número de registro no puede ser negativo.");
                return;
            }

            int size = Marshal.SizeOf(typeof(Record));

            using (FileStream fs = new FileStream(
                FilePath,
                FileMode.Open,
                FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // Calculamos la posición del registro
                long position = (long)recordNumber * size;

                // Verificamos que el registro exista
                if (position + size > fs.Length)
                {
                    Console.WriteLine("El registro no existe.");
                    return;
                }

                // Hacemos Seek hasta la posición del registro
                fs.Seek(position, SeekOrigin.Begin);

                // Leemos el registro
                byte[] buffer = reader.ReadBytes(size);

                if (buffer.Length != size)
                {
                    Console.WriteLine(
                        "No se pudo leer el registro completo.");
                    return;
                }

                Record record = BytesToRecord(buffer, size);

                Console.WriteLine(
                    $"Registro {recordNumber}: " +
                    $"ID={record.ID}, " +
                    $"Nombre={CleanString(record.Name)}, " +
                    $"Apellido={CleanString(record.LastName)}, " +
                    $"Salario={record.Salary}");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("El archivo de registros no existe.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Error leyendo el registro: {ex.Message}");
        }
    }

    // ------------------------------------------------------------
    // Modificar un registro existente
    // ------------------------------------------------------------
    static void ModifyRecord()
    {
        try
        {
            Console.Write("Ingrese el número de registro a modificar: ");
            int recordNumber = int.Parse(Console.ReadLine());

            if (recordNumber < 0)
            {
                Console.WriteLine(
                    "El número de registro no puede ser negativo.");
                return;
            }

            int size = Marshal.SizeOf(typeof(Record));

            using (FileStream fs = new FileStream(
                FilePath,
                FileMode.Open,
                FileAccess.ReadWrite))
            using (BinaryReader reader = new BinaryReader(fs))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // Calculamos la posición del registro
                long position = (long)recordNumber * size;

                // Verificamos que el registro exista
                if (position + size > fs.Length)
                {
                    Console.WriteLine("El registro no existe.");
                    return;
                }

                // Nos posicionamos al comienzo del registro
                fs.Seek(position, SeekOrigin.Begin);

                // Leemos el registro existente
                byte[] buffer = reader.ReadBytes(size);

                if (buffer.Length != size)
                {
                    Console.WriteLine(
                        "No se pudo leer el registro completo.");
                    return;
                }

                Record oldRecord = BytesToRecord(buffer, size);

                // Mostramos los datos actuales
                Console.WriteLine();
                Console.WriteLine("Registro actual:");
                Console.WriteLine($"ID       : {oldRecord.ID}");
                Console.WriteLine(
                    $"Nombre   : {CleanString(oldRecord.Name)}");
                Console.WriteLine(
                    $"Apellido : {CleanString(oldRecord.LastName)}");
                Console.WriteLine($"Salario  : {oldRecord.Salary}");

                Console.WriteLine();
                Console.WriteLine("Ingrese los nuevos datos:");

                Console.Write("Nuevo ID: ");
                int id = int.Parse(Console.ReadLine());

                string name = GetFixedLengthString(
                    $"Nuevo Nombre (Hasta {MaxTextLength} caracteres): ");

                string lastName = GetFixedLengthString(
                    $"Nuevo Apellido (Hasta {MaxTextLength} caracteres): ");

                Console.Write("Nuevo Salario: ");
                float salary = float.Parse(Console.ReadLine());

                Record newRecord = new Record
                {
                    ID = id,
                    Name = name,
                    LastName = lastName,
                    Salary = salary
                };

                // Convertimos la nueva estructura a bytes
                byte[] newBuffer = RecordToBytes(newRecord, size);

                // Volvemos al comienzo del registro
                fs.Seek(position, SeekOrigin.Begin);

                // Sobrescribimos el registro existente
                writer.Write(newBuffer);

                Console.WriteLine();
                Console.WriteLine(
                    "El registro fue modificado satisfactoriamente.");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("El archivo de registros no existe.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Error modificando el registro: {ex.Message}");
        }
    }

    // ------------------------------------------------------------
    // Listar todos los registros
    // ------------------------------------------------------------
    static void ListRecords()
    {
        try
        {
            int size = Marshal.SizeOf(typeof(Record));

            using (FileStream fs = new FileStream(
                FilePath,
                FileMode.Open,
                FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                if (fs.Length == 0)
                {
                    Console.WriteLine("El archivo está vacío.");
                    return;
                }

                // Verificamos que el archivo tenga registros completos
                if (fs.Length % size != 0)
                {
                    Console.WriteLine(
                        "Advertencia: el archivo contiene datos incompletos.");
                }

                int recordCount = (int)(fs.Length / size);

                Console.WriteLine(
                    $"Cantidad de registros: {recordCount}");
                Console.WriteLine();

                Console.WriteLine(
                    "{0,-10} {1,-10} {2,-22} {3,-22} {4,12}",
                    "Registro",
                    "ID",
                    "Nombre",
                    "Apellido",
                    "Salario");

                Console.WriteLine(
                    new string('-', 80));

                for (int recordNumber = 0;
                     recordNumber < recordCount;
                     recordNumber++)
                {
                    // El FileStream avanza automáticamente
                    // al leer cada registro.
                    byte[] buffer = reader.ReadBytes(size);

                    if (buffer.Length != size)
                    {
                        Console.WriteLine(
                            $"No se pudo leer el registro {recordNumber}.");
                        break;
                    }

                    Record record = BytesToRecord(buffer, size);

                    Console.WriteLine(
                        "{0,-10} {1,-10} {2,-22} {3,-22} {4,12:F2}",
                        recordNumber,
                        record.ID,
                        CleanString(record.Name),
                        CleanString(record.LastName),
                        record.Salary);
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("El archivo de registros no existe.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Error listando los registros: {ex.Message}");
        }
    }

    // ------------------------------------------------------------
    // Convertir Record -> byte[]
    // ------------------------------------------------------------
    static byte[] RecordToBytes(Record record, int size)
    {
        byte[] buffer = new byte[size];

        IntPtr ptr = Marshal.AllocHGlobal(size);

        try
        {
            Marshal.StructureToPtr(record, ptr, true);
            Marshal.Copy(ptr, buffer, 0, size);
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }

        return buffer;
    }

    // ------------------------------------------------------------
    // Convertir byte[] -> Record
    // ------------------------------------------------------------
    static Record BytesToRecord(byte[] buffer, int size)
    {
        IntPtr ptr = Marshal.AllocHGlobal(size);

        try
        {
            Marshal.Copy(buffer, 0, ptr, size);

            return (Record)Marshal.PtrToStructure(
                ptr,
                typeof(Record));
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    // ------------------------------------------------------------
    // Elimina los '\0' usados para completar las cadenas.
    // ------------------------------------------------------------
    static string CleanString(string value)
    {
        return value.TrimEnd('\0');
    }
}