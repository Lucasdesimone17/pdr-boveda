using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {

        Task<int> initialTask = Task.Run(() =>
        {
            // Simulate conditional error
            throw new InvalidOperationException("Database connection lost!");
            return 100;
        });

        // Runs ONLY if initialTask succeeds
        initialTask.ContinueWith(t => { Console.WriteLine($"Success! Result: {t.Result}"); },
            TaskContinuationOptions.OnlyOnRanToCompletion);

        // Runs ONLY if initialTask throws an exception
        initialTask.ContinueWith(
            t => { Console.WriteLine($"Error caught in continuation: {t.Exception.InnerException.Message}"); },
            TaskContinuationOptions.OnlyOnFaulted);
    }
}