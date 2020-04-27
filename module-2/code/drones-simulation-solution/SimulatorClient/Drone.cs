using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class Drone
    {
        public event EventHandler<DroneEvent>? NewEvent;

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var temperatureDevice = new TemperatureDevice();
            var devices = new Device[] { temperatureDevice };

            foreach(var d in devices)
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