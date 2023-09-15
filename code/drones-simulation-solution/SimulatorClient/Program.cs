using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    class Program
    {
        static public async Task Main(string[] args)
        {
            var configuration = new SimulatorConfiguration();
            var simulator = new Simulator(configuration);
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