using System;
using System.Threading.Tasks;

class SimpleAsyncExceptionDemo
{
    static async Task Main()
    {
        Console.WriteLine("[Main] Starting parent process...");

        try
        {
            // do not Await the asynchronous task. 
            var t1 = InnerTaskAsync();
            Console.WriteLine($"[Main] Starts doing some work.");
            await Task.Delay(500); // Simulate asynchronous work on Main
            Console.WriteLine($"[Main] Finished doing some work. Now see how t1 did.");
            await t1;
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"\n[Main CATCH BLOCK] Caught exception from inner task:");
            Console.WriteLine($"  -> Type:    {ex.GetType().Name}");
            Console.WriteLine($"  -> Message: {ex.Message}");
            Console.WriteLine($"  -> Stack Trace:\n{ex.StackTrace}");
        }

        Console.WriteLine("\n[Main] Program completed cleanly after handling the exception.");
    }

    static async Task InnerTaskAsync()
    {
        Console.WriteLine("  -> [Inner Task] Running background work...");
        await Task.Delay(500); // Simulate asynchronous work (e.g., API or database call)

        Console.WriteLine("  -> [Inner Task] Something went wrong! Throwing exception...");
        
        // This exception will be captured inside the Task object returned by InnerTaskAsync
        throw new InvalidOperationException("Failed to process data inside the inner task.");
        
        Console.WriteLine("  -> [Inner Task] Never go to finish...");
    }
}