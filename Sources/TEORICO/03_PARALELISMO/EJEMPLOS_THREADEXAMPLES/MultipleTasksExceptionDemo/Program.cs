using System;
using System.Threading.Tasks;

class MultipleTasksExceptionDemo
{
    static async Task Main()
    {
        Task task1 = FailTaskAsync("Task 1", "Database timeout");
        Task task2 = FailTaskAsync("Task 2", "Network connection lost");

        Task allTasks = Task.WhenAll(task1, task2);

        try
        {
            await allTasks;
        }
        catch (Exception)
        {
            Console.WriteLine("[Main] At least one task failed!\n");

            // Inspect the AggregateException property on the combined task to see ALL errors
            AggregateException allExceptions = allTasks.Exception;

            if (allExceptions != null)
            {
                foreach (Exception innerEx in allExceptions.InnerExceptions)
                {
                    Console.WriteLine($"  -> Captured: {innerEx.Message}");
                }
            }
        }
    }

    static async Task FailTaskAsync(string taskName, string errorMessage)
    {
        await Task.Delay(300);
        throw new InvalidOperationException($"[{taskName}] {errorMessage}");
    }
}