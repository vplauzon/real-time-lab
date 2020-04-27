using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public abstract class Device
    {
        public event EventHandler<DroneEvent>? NewEvent;

        public abstract Task RunAsync(CancellationToken cancellationToken);

        protected void OnNewEvent(DroneEvent data)
        {
            NewEvent?.Invoke(this, data);
        }
    }
}