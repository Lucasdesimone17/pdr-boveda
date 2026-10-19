using System;
using System.Threading;

class ExceptionBehaviorDemo
{
    static void Main(string[] args)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("  C# Threading & Exception Behavior Demonstration");
        Console.WriteLine("==================================================");
        Console.WriteLine($"[Main Process ID: {Environment.ProcessId}] Started.\n");

        Console.WriteLine("Select a test scenario:");
        Console.WriteLine("1 - Handled Exception on Background Thread");
        Console.WriteLine("2 - Handled Exception on Foreground Thread");
        Console.WriteLine("3 - Unhandled Exception on Background Thread (Crashes Process)");
        Console.WriteLine("4 - Unhandled Exception on Foreground Thread (Crashes Process)");
        Console.WriteLine("5 - Long Thread on Background Thread (Crashes Process)");
        Console.WriteLine("6 - Long Thread on Foreground Thread (Crashes Process)");
        Console.Write("\nEnter choice (1-6): ");

        string input = Console.ReadLine();
        Console.WriteLine();

        switch (input)
        {
            case "1":
                RunHandledException(isBackground: true);
                // Give worker threads time to execute before Main finishes
                Thread.Sleep(2000);
                break;
            case "2":
                RunHandledException(isBackground: false);
                // Give worker threads time to execute before Main finishes
                Thread.Sleep(2000);
                break;
            case "3":
                RunUnhandledException(isBackground: true);
                // Give worker threads time to execute before Main finishes
                Thread.Sleep(2000);
                break;
            case "4":
                RunUnhandledException(isBackground: false);
                // Give worker threads time to execute before Main finishes
                Thread.Sleep(2000);
                break;
            case "5":
                RunLongThread(isBackground: true);
                break;
            case "6":
                RunLongThread(isBackground: false);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                return;
        }
        Console.WriteLine("[Main Thread] Main thread completed successfully.<<<<<<<");
        Console.WriteLine("[Process Status] Application process is still running.");
    }

    /// <summary>
    /// Demonstrates how catching an exception on a worker thread keeps the thread and process safe.
    /// </summary>
    static void RunHandledException(bool isBackground)
    {
        string threadType = isBackground ? "Background" : "Foreground";
        
        Thread thread = new Thread(() =>
        {
            Console.WriteLine($"[{threadType} Thread] Started.");
            
            try
            {
                Console.WriteLine($"[{threadType} Thread] Performing work...");
                Thread.Sleep(500);

                // Simulate an unexpected error
                throw new InvalidOperationException($"Error encountered inside {threadType} Thread.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"[{threadType} Thread] CATCH BLOCK: Handled exception cleanly -> '{ex.Message}'");
            }
            finally
            {
                Console.WriteLine($"[{threadType} Thread] FINALLY BLOCK: Cleanup completed.");
            }

            Console.WriteLine($"[{threadType} Thread] Finished execution normally. >>>>>>>");
        });

        thread.IsBackground = isBackground;
        thread.Start();
    }

    /// <summary>
    /// Demonstrates that an unhandled exception on ANY thread crashes the application.
    /// </summary>
    static void RunUnhandledException(bool isBackground)
    {
        string threadType = isBackground ? "Background" : "Foreground";

        Thread thread = new Thread(() =>
        {
            Console.WriteLine($"[{threadType} Thread] Started.");
            Thread.Sleep(500);

            Console.WriteLine($"[{threadType} Thread] About to throw UNHANDLED exception...");
            
            // This exception will NOT be caught on this thread.
            // It will trigger AppDomain.UnhandledException and crash the process.
            throw new InvalidOperationException($"Unhandled crash in {threadType} Thread!");
            
            Console.WriteLine($"  [{threadType} Thread] Finnished... >>>>>>>");
        });

        thread.IsBackground = isBackground;
        thread.Start();
    }
    
    /// <summary>
    /// Demonstrates what happens to a long runnig thread when the main application ends.
    /// </summary>
    static void RunLongThread(bool isBackground)
    {
        string threadType = isBackground ? "Background" : "Foreground";

        Thread thread = new Thread(() =>
        {
            Console.WriteLine($"  [{threadType} Thread] Started.");
            Thread.Sleep(200);
            Console.WriteLine($"  [{threadType} Thread] Running 1...");
            Thread.Sleep(200);
            Console.WriteLine($"  [{threadType} Thread] Running 2...");
            Thread.Sleep(200);
            Console.WriteLine($"  [{threadType} Thread] Running 3...");
            Thread.Sleep(200);
            Console.WriteLine($"  [{threadType} Thread] Running 4...");
            Thread.Sleep(200);
            Console.WriteLine($"  [{threadType} Thread] Running 5...");
            Thread.Sleep(200);
            Console.WriteLine($"  [{threadType} Thread] Finnished... >>>>>>>");
        });

        thread.IsBackground = isBackground;
        thread.Start();
        Console.WriteLine("[Main Thread] Started.");
        Thread.Sleep(200);
        Console.WriteLine("[Main Thread] Running 3...");
        Thread.Sleep(200);
        Console.WriteLine("[Main Thread] Running 2...");
        Thread.Sleep(200);
        Console.WriteLine("[Main Thread] Running 1...");
        Thread.Sleep(200);
        Console.WriteLine("[Main Thread] Ended...");
    }
}

