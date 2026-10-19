using System;
using System.IO;
using System.Text;

class FileStreamTextMenu
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            ShowMenu();

            Console.Write("Select an option: ");
            string? option = Console.ReadLine();

            Console.Clear();

            try
            {
                switch (option)
                {
                    case "1":
                        CreateNewFile();
                        break;

                    case "2":
                        CreateFile();
                        break;

                    case "3":
                        OpenFile();
                        break;

                    case "4":
                        OpenOrCreateFile();
                        break;

                    case "5":
                        TruncateFile();
                        break;

                    case "6":
                        AppendToFile();
                        break;

                    case "7":
                        WriteLinesToFile();
                        break;

                    case "8":
                        AppendLinesToExistingFile();
                        break;

                    case "9":
                        OverwriteExistingFile();
                        break;

                    case "10":
                        SelectTextFile();
                        break;

                    case "11":
                        ShowFileContent();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"ERROR: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("==============================================");
        Console.WriteLine("          FileStream Console Example");
        Console.WriteLine("==============================================");
        Console.WriteLine();
        Console.WriteLine("FileMode examples");
        Console.WriteLine("-----------------");
        Console.WriteLine(" 1. FileMode.CreateNew");
        Console.WriteLine(" 2. FileMode.Create");
        Console.WriteLine(" 3. FileMode.Open");
        Console.WriteLine(" 4. FileMode.OpenOrCreate");
        Console.WriteLine(" 5. FileMode.Truncate");
        Console.WriteLine(" 6. FileMode.Append");
        Console.WriteLine();
        Console.WriteLine("File operations");
        Console.WriteLine("----------------");
        Console.WriteLine(" 7. Write lines to a file");
        Console.WriteLine(" 8. Append lines to an existing .txt file");
        Console.WriteLine(" 9. Overwrite part of an existing file");
        Console.WriteLine("10. Select a .txt file");
        Console.WriteLine("11. Show file contents");
        Console.WriteLine();
        Console.WriteLine(" 0. Exit");
        Console.WriteLine();
    }

    // ---------------------------------------------------------
    // Helper methods
    // ---------------------------------------------------------

    static string GetFileName()
    {
        Console.Write("Enter file name: ");
        string? fileName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("A file name is required.");

        // Add .txt if the user did not specify an extension.
        if (string.IsNullOrEmpty(Path.GetExtension(fileName)))
        {
            fileName += ".txt";
        }

        return fileName;
    }
    static string SelectTextFile()
    {
        string currentDirectory = Directory.GetCurrentDirectory();

        string[] files = Directory.GetFiles(
            currentDirectory,
            "*.txt");

        if (files.Length == 0)
        {
            Console.WriteLine("No .txt files were found.");
            return "";
        }

        Console.WriteLine("Available .txt files:");
        Console.WriteLine();

        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine(
                $"{i + 1}. {Path.GetFileName(files[i])}");
        }

        Console.WriteLine();
        Console.Write("Select a file: ");

        if (!int.TryParse(Console.ReadLine(), out int number) ||
            number < 1 ||
            number > files.Length)
        {
            throw new ArgumentException("Invalid file selection.");
        }

        return files[number - 1];
    }

    static void WriteLines(FileStream stream)
    {
        Console.WriteLine();
        Console.WriteLine("Enter lines to write.");
        Console.WriteLine("Enter an empty line to finish.");
        Console.WriteLine();

        while (true)
        {
            Console.Write("> ");
            string? line = Console.ReadLine();

            if (string.IsNullOrEmpty(line))
                break;

            byte[] data = Encoding.UTF8.GetBytes(line + Environment.NewLine);

            stream.Write(data, 0, data.Length);
        }
    }

    static void DisplayFile(string fileName)
    {
        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine($"File: {fileName}");
        Console.WriteLine("----------------------------------------------");

        using FileStream stream = new FileStream(
            fileName,
            FileMode.Open,
            FileAccess.Read);

        using StreamReader reader = new StreamReader(stream);

        string? line;

        while ((line = reader.ReadLine()) != null)
        {
            Console.WriteLine(line);
        }

        Console.WriteLine("----------------------------------------------");
    }

    // ---------------------------------------------------------
    // FileMode.CreateNew
    // ---------------------------------------------------------

    static void CreateNewFile()
    {
        Console.WriteLine("FILEMODE.CREATENEW");
        Console.WriteLine("------------------");
        Console.WriteLine();
        Console.WriteLine(
            "Creates a new file. An exception is generated");
        Console.WriteLine(
            "if the file already exists.");
        Console.WriteLine();

        string fileName = GetFileName();

        using FileStream stream = new FileStream(
            fileName,
            FileMode.CreateNew,
            FileAccess.Write);

        Console.WriteLine();
        Console.WriteLine("File created successfully.");

        WriteLines(stream);
    }

    // ---------------------------------------------------------
    // FileMode.Create
    // ---------------------------------------------------------

    static void CreateFile()
    {
        Console.WriteLine("FILEMODE.CREATE");
        Console.WriteLine("---------------");
        Console.WriteLine();
        Console.WriteLine(
            "Creates a new file.");
        Console.WriteLine(
            "If the file already exists, its contents are replaced.");
        Console.WriteLine();

        string fileName = GetFileName();

        using FileStream stream = new FileStream(
            fileName,
            FileMode.Create,
            FileAccess.Write);

        WriteLines(stream);

        Console.WriteLine();
        Console.WriteLine("File created/written successfully.");
    }

    // ---------------------------------------------------------
    // FileMode.Open
    // ---------------------------------------------------------

    static void OpenFile()
    {
        Console.WriteLine("FILEMODE.OPEN");
        Console.WriteLine("--------------");
        Console.WriteLine();
        Console.WriteLine(
            "Opens an existing file.");
        Console.WriteLine(
            "An exception is generated if it does not exist.");
        Console.WriteLine();

        string fileName = SelectTextFile();

        if (string.IsNullOrEmpty(fileName))
            return;

        using FileStream stream = new FileStream(
            fileName,
            FileMode.Open,
            FileAccess.Read);

        using StreamReader reader = new StreamReader(stream);

        Console.WriteLine();
        Console.WriteLine("File contents:");
        Console.WriteLine();

        while (reader.ReadLine() is string line)
        {
            Console.WriteLine(line);
        }
    }

    // ---------------------------------------------------------
    // FileMode.OpenOrCreate
    // ---------------------------------------------------------

    static void OpenOrCreateFile()
    {
        Console.WriteLine("FILEMODE.OPENORCREATE");
        Console.WriteLine("--------------------");
        Console.WriteLine();
        Console.WriteLine(
            "Opens an existing file.");
        Console.WriteLine(
            "If it does not exist, creates it.");
        Console.WriteLine();

        string fileName = GetFileName();

        using FileStream stream = new FileStream(
            fileName,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite);

        Console.WriteLine();

        if (stream.Length == 0)
        {
            Console.WriteLine("The file is empty.");
            WriteLines(stream);
        }
        else
        {
            Console.WriteLine(
                $"File opened. Size: {stream.Length} bytes.");

            stream.Position = stream.Length;

            Console.WriteLine();
            Console.WriteLine(
                "The position was moved to the end of the file.");

            WriteLines(stream);
        }
    }

    // ---------------------------------------------------------
    // FileMode.Truncate
    // ---------------------------------------------------------

    static void TruncateFile()
    {
        Console.WriteLine("FILEMODE.TRUNCATE");
        Console.WriteLine("-----------------");
        Console.WriteLine();
        Console.WriteLine(
            "Opens an existing file and sets its length to zero.");
        Console.WriteLine(
            "The file must already exist.");
        Console.WriteLine();

        string fileName = SelectTextFile();

        if (string.IsNullOrEmpty(fileName))
            return;

        Console.WriteLine();
        Console.WriteLine(
            "WARNING: All existing contents will be removed.");
        Console.Write("Continue? (y/n): ");

        string? answer = Console.ReadLine();

        if (answer?.ToLower() != "y")
        {
            Console.WriteLine("Operation cancelled.");
            return;
        }

        using FileStream stream = new FileStream(
            fileName,
            FileMode.Truncate,
            FileAccess.Write);

        Console.WriteLine();
        Console.WriteLine("File has been truncated.");

        WriteLines(stream);
    }

    // ---------------------------------------------------------
    // FileMode.Append
    // ---------------------------------------------------------

    static void AppendToFile()
    {
        Console.WriteLine("FILEMODE.APPEND");
        Console.WriteLine("---------------");
        Console.WriteLine();
        Console.WriteLine(
            "Opens an existing file or creates it.");
        Console.WriteLine(
            "All writes are performed at the end of the file.");
        Console.WriteLine();

        string fileName = GetFileName();

        using FileStream stream = new FileStream(
            fileName,
            FileMode.Append,
            FileAccess.Write);

        WriteLines(stream);

        Console.WriteLine();
        Console.WriteLine("Lines appended successfully.");
    }

    // ---------------------------------------------------------
    // Write lines
    // ---------------------------------------------------------

    static void WriteLinesToFile()
    {
        Console.WriteLine("WRITE LINES");
        Console.WriteLine("-----------");
        Console.WriteLine();

        string fileName = GetFileName();

        using FileStream stream = new FileStream(
            fileName,
            FileMode.Create,
            FileAccess.Write);

        WriteLines(stream);
    }

    // ---------------------------------------------------------
    // Append lines to existing file
    // ---------------------------------------------------------

    static void AppendLinesToExistingFile()
    {
        Console.WriteLine("APPEND LINES");
        Console.WriteLine("------------");
        Console.WriteLine();

        string fileName = SelectTextFile();

        if (string.IsNullOrEmpty(fileName))
            return;

        using FileStream stream = new FileStream(
            fileName,
            FileMode.Append,
            FileAccess.Write);

        WriteLines(stream);

        Console.WriteLine();
        Console.WriteLine("Lines appended.");
    }

    // ---------------------------------------------------------
    // Overwrite part of existing file
    // ---------------------------------------------------------

    static void OverwriteExistingFile()
    {
        Console.WriteLine("OVERWRITE PART OF FILE");
        Console.WriteLine("----------------------");
        Console.WriteLine();
        Console.WriteLine(
            "This example opens the file with FileMode.Open");
        Console.WriteLine(
            "and writes bytes at a selected position.");
        Console.WriteLine();

        string fileName = SelectTextFile();

        if (string.IsNullOrEmpty(fileName))
            return;

        using FileStream stream = new FileStream(
            fileName,
            FileMode.Open,
            FileAccess.ReadWrite);

        Console.WriteLine(
            $"File size: {stream.Length} bytes.");

        Console.WriteLine();
        Console.Write("Position in bytes: ");

        if (!long.TryParse(Console.ReadLine(), out long position))
        {
            Console.WriteLine("Invalid position.");
            return;
        }

        if (position < 0 || position > stream.Length)
        {
            Console.WriteLine("Position is outside the file.");
            return;
        }

        Console.Write("Text to write: ");
        string? text = Console.ReadLine();

        if (text == null)
            return;

        byte[] data = Encoding.UTF8.GetBytes(text);

        stream.Position = position;

        stream.Write(data, 0, data.Length);

        Console.WriteLine();
        Console.WriteLine(
            "Data written at the selected position.");

        Console.WriteLine(
            "Notice that this does not insert bytes.");
        Console.WriteLine(
            "Existing bytes are overwritten.");
    }

    // ---------------------------------------------------------
    // Select a text file
    // ---------------------------------------------------------

    static void SelectTextFileExample()
    {
        string fileName = SelectTextFile();

        if (!string.IsNullOrEmpty(fileName))
        {
            Console.WriteLine();
            Console.WriteLine($"Selected: {fileName}");
        }
    }

    // ---------------------------------------------------------
    // Show file contents
    // ---------------------------------------------------------

    static void ShowFileContent()
    {
        Console.WriteLine("SHOW FILE CONTENT");
        Console.WriteLine("-----------------");
        Console.WriteLine();

        string fileName = SelectTextFile();

        if (string.IsNullOrEmpty(fileName))
            return;

        DisplayFile(fileName);
    }
}
