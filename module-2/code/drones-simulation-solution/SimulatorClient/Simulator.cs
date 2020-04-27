using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    internal class Simulator: IDaemon
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly SimulatorConfiguration _configuration = new SimulatorConfiguration();

        public Simulator()
        {
        }

        async Task IDaemon.RunAsync()
        {
            await Task.Delay(1);
        }

        void IDaemon.Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}