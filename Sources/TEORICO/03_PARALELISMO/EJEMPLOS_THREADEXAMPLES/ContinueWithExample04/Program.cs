using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Task 1 produces a result
        int rawValue = await FetchNumberAsync();

        // --- CONTINUATION BOUNDARY ---
        // 'rawValue' is automatically captured and passed down to the remaining code
        string finalResult = await ProcessNumberAsync(rawValue);

        Console.WriteLine($"[Main] Output: {finalResult}");
    }

    static Task<int> FetchNumberAsync()
    {
        return Task.Run(() =>
        {
            Console.WriteLine("  -> [Task 1] Fetching raw value...");
            Thread.Sleep(1000);
            return 42;
        });
    }

    static Task<string> ProcessNumberAsync(int number)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"  -> [Task 2] Processing received parameter: {number}");
            return $"FINAL OUTPUT: Processed value is {number * 10}";
        });
    }
}