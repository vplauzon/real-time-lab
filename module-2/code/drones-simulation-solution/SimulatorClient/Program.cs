using System;
using System.Threading.Tasks;

namespace SimulatorClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var simulator = new Simulator();

            await RunDaemonAsync(simulator);
        }

        private static async Task RunDaemonAsync(IDaemon daemon)
        {
            var task = daemon.RunAsync();

            AppDomain.CurrentDomain.ProcessExit += async (object? sender, EventArgs e) =>
            {
                daemon.Stop();

                await task;
            };

            await task;
        }
    }
}