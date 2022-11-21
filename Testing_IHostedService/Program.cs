using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Testing_IHostedService.MyJobTasks;

namespace Testing_IHostedService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var tasks = new List<TimedHostedService>();
            for (var i = 0; i < 100; i++)
            {
                tasks.Add(new TimedHostedService($"task{i}", TimeSpan.FromSeconds(0.1)));
            }
            foreach (var item in tasks)
            {
                var ct = new CancellationToken();
                await item.StartAsync(ct);
            }
            Console.ReadLine();
        }
    }
}
