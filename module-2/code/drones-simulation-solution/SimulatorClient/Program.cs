using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var simulator = new Simulator();
            var _cancellationTokenSource = new CancellationTokenSource();
            var task = simulator.RunAsync(_cancellationTokenSource.Token);

            AppDomain.CurrentDomain.ProcessExit += (object? sender, EventArgs e) =>
            {
                _cancellationTokenSource.Cancel();
            };

            await task;
        }
    }
}