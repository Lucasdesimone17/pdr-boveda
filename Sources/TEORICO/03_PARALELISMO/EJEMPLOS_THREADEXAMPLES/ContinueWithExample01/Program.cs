using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Starting initial task...");

        // Initial task returns no result (Task)
        Task initialTask = Task.Run(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine("  -> [Task 1] Work completed.");
        });

        // Continuation registered explicitly
        Task continuationTask = initialTask.ContinueWith(antecedent =>
        {
            // 'antecedent' is the completed initialTask
            Console.WriteLine("  -> [Continuation] Triggered after Task 1 finished.");
        });

        await continuationTask;
    }
}