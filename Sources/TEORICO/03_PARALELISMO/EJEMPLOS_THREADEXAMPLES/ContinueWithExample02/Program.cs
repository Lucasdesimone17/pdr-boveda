using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{

    static async Task Main()
    {
        Console.WriteLine("Starting initial task...");

        // The await keyword yields execution back to the caller
        await Task.Run(() =>
        {
            Console.WriteLine("  -> [Task 1] Task Started.");
            Thread.Sleep(1000);
            Console.WriteLine("  -> [Task 1] Work completed.");
        });

        // --- CONTINUATION BOUNDARY ---
        // Everything below this line is scheduled as a continuation of the task above
        Console.WriteLine("  -> [Continuation] Triggered after Task 1 finished.");
    }
}