using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

class ThreadPoolWebReader
{
    // Reuse a single HttpClient instance across thread pool tasks
    private static readonly HttpClient HttpClient = new HttpClient();

    static void Main()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("  ThreadPool Web Reader Demonstration");
        Console.WriteLine("==================================================\n");

        List<string> urls = new List<string>
        {
            "https://httpbin.org/delay/1",
            "https://httpbin.org/status/200",
            "https://httpbin.org/html",
            "https://httpbin.org/user-agent",
            "https://httpbin.org/ip",
            "https://invalid-domain-name-test.org" // Intentional failure URL
        };

        // Track completed work items so the main thread knows when all threads finish
        using (CountdownEvent countdown = new CountdownEvent(urls.Count))
        {
            Console.WriteLine($"[Main Thread] Queueing {urls.Count} URLs to the ThreadPool...\n");

            foreach (string url in urls)
            {
                // Prepare state bundle containing both the target URL and the sync handle
                WorkState state = new WorkState
                {
                    Url = url,
                    Countdown = countdown
                };

                // Queue work item to the ThreadPool
                ThreadPool.QueueUserWorkItem(FetchWebPage, state);
            }

            Console.WriteLine("[Main Thread] All items queued. Waiting for completion...\n");

            // Block the main thread until CountdownEvent reaches 0
            countdown.Wait();
        }

        Console.WriteLine("\n[Main Thread] All web fetch operations completed. Exiting.");
    }

    /// <summary>
    /// Callback executed by a ThreadPool thread.
    /// </summary>
    private static void FetchWebPage(object stateInfo)
    {
        WorkState state = (WorkState)stateInfo;
        int threadId = Thread.CurrentThread.ManagedThreadId;

        try
        {
            Console.WriteLine($"  -> [Thread #{threadId}] Starting fetch for: {state.Url}");

            // Fetch web page synchronously on this thread pool thread
            HttpResponseMessage response = HttpClient.GetAsync(state.Url).Result;
            string content = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine($"  <- [Thread #{threadId}] Success ({response.StatusCode}): {state.Url} [{content.Length} bytes]");
        }
        catch (Exception ex)
        {
            // Unwrap AggregateException from synchronous task execution if necessary
            Exception actualEx = ex is AggregateException aggEx ? aggEx.InnerException : ex;
            Console.WriteLine($"  !! [Thread #{threadId}] ERROR fetching {state.Url}: {actualEx.Message}");
        }
        finally
        {
            // Always signal the countdown event, even if an exception occurs
            state.Countdown.Signal();
        }
    }

    /// <summary>
    /// Helper class to pass multiple arguments into QueueUserWorkItem state object.
    /// </summary>
    private class WorkState
    {
        public string Url { get; set; }
        public CountdownEvent Countdown { get; set; }
    }
}