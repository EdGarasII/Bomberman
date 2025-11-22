using System;
using System.Diagnostics;

namespace BombermanGame.Proxies
{
    // PROXY PATTERN - Performance measurement
    public class ProxyPerformanceTest
    {
        public static void RunPerformanceTest()
        {
            Console.WriteLine("=== PROXY PATTERN PERFORMANCE TEST ===\n");
            
            const int iterations = 1000;
            
            // Test direct access
            Console.WriteLine("--- Direct Resource Access ---");
            var stopwatch1 = Stopwatch.StartNew();
            var memoryBefore1 = GC.GetTotalMemory(false);
            
            for (int i = 0; i < iterations; i++)
            {
                var resource = new GameResource($"Resource{i}");
                resource.Load();
            }
            
            stopwatch1.Stop();
            var memoryAfter1 = GC.GetTotalMemory(false);
            var memoryUsed1 = memoryAfter1 - memoryBefore1;
            
            Console.WriteLine($"Time: {stopwatch1.ElapsedMilliseconds} ms");
            Console.WriteLine($"Memory: {memoryUsed1 / 1024} KB\n");
            
            // Test with Lazy Load Proxy
            Console.WriteLine("--- Lazy Load Proxy ---");
            var stopwatch2 = Stopwatch.StartNew();
            var memoryBefore2 = GC.GetTotalMemory(false);
            
            for (int i = 0; i < iterations; i++)
            {
                var proxy = new LazyLoadProxy($"Resource{i}");
                // Resource not created until Load() is called
            }
            
            stopwatch2.Stop();
            var memoryAfter2 = GC.GetTotalMemory(false);
            var memoryUsed2 = memoryAfter2 - memoryBefore2;
            
            Console.WriteLine($"Time: {stopwatch2.ElapsedMilliseconds} ms");
            Console.WriteLine($"Memory: {memoryUsed2 / 1024} KB");
            Console.WriteLine($"Memory saved: {(memoryUsed1 - memoryUsed2) / 1024} KB\n");
            
            // Test with Logging Proxy
            Console.WriteLine("--- Logging Proxy (with added functionality) ---");
            var stopwatch3 = Stopwatch.StartNew();
            
            var loggingProxy = new LoggingProxy("TestResource");
            loggingProxy.Load();
            loggingProxy.Load();
            
            stopwatch3.Stop();
            Console.WriteLine($"Time: {stopwatch3.ElapsedMilliseconds} ms");
            Console.WriteLine($"Access count: {loggingProxy.GetAccessCount()}\n");
        }
    }
}

