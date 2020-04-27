using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    internal class Device
    {
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }
    }
}