using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Task 1: Produces an integer result
        Task<int> fetchNumberTask = Task.Run(() =>
        {
            Console.WriteLine("  -> [Task 1] Fetching raw value...");
            Thread.Sleep(1000);
            return 42; // Returns int
        });

        // Task 2 (Continuation): Takes Task 1's result, processes it, and returns a string
        Task<string> processResultTask = fetchNumberTask.ContinueWith(antecedent =>
        {
            // Extract the result produced by Task 1
            int rawValue = antecedent.Result; 
            
            Console.WriteLine($"  -> [Task 2 Continuation] Processing received parameter: {rawValue}");
            return $"FINAL OUTPUT: Processed value is {rawValue * 10}";
        });

        // Await the final continuation task to get the processed string
        string finalResult = await processResultTask;
        Console.WriteLine($"[Main] Output: {finalResult}");
    }
}