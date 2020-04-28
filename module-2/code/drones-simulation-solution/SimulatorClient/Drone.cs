using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class Drone
    {
        private readonly string _droneId = "1.2.22;" + Guid.NewGuid().GetHashCode().ToString("x8");

        public event EventHandler<DroneEvent>? NewEvent;

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var externalTemperatureDevice = new TemperatureDevice(_droneId, true);
            var internalTemperatureDevice = new TemperatureDevice(_droneId, false);
            var devices = new Device[]
            {
                externalTemperatureDevice,
                internalTemperatureDevice
            };

            foreach (var d in devices)
            {   //  Bubble up
                d.NewEvent +=
                    (sender, eventData) => NewEvent?.Invoke(sender, eventData);
            }

            var tasks = from d in devices
                        select d.RunAsync(cancellationToken);

            await Task.WhenAll(tasks);
        }
    }
}