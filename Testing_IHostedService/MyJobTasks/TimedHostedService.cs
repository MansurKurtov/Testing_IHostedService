using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testing_IHostedService.MyJobTasks
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;        
        private Timer? _timer = null;
        private string _taskName;

        private TimeSpan _period;

        public TimedHostedService()
        {
            //Default value:
            _period = TimeSpan.FromSeconds(1);
        }

        public TimedHostedService(string taskName, TimeSpan period)
        {
            _period = period;
            _taskName = taskName;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"{_taskName}  task running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, _period);

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            Console.WriteLine($"Timed Hosted Service's DoWork method is working. Count: {count}, Task name:{_taskName}");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
