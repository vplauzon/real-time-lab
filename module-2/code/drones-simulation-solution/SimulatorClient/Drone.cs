using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class Drone
    {
        private readonly string _droneId =
            "1.2.22;" + Guid.NewGuid().GetHashCode().ToString("x8");
        private readonly GeoPoint _gatewayLocation;

        public event EventHandler<DroneEvent>? NewEvent;

        public Drone(GeoPoint gatewayLocation)
        {
            _gatewayLocation = gatewayLocation;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var externalTemperatureDevice = new TemperatureDevice(_droneId, true);
            var internalTemperatureDevice = new TemperatureDevice(_droneId, false);
            var gpsDevice = new GpsDevice(_droneId, GetCurrentLocation);
            var devices = new Device[]
            {
                externalTemperatureDevice,
                internalTemperatureDevice,
                gpsDevice
            };

            foreach (var d in devices)
            {   //  Bubble up
                d.NewEvent +=
                    (sender, eventData) => NewEvent?.Invoke(sender, eventData);
            }

            var deviceTasks = from d in devices
                              select d.RunAsync(cancellationToken);
            var moveTask = MoveAsync();

            await Task.WhenAll(deviceTasks.Prepend(moveTask));
        }

        private Task MoveAsync()
        {
            return Task.CompletedTask;
        }

        private GeoPoint GetCurrentLocation()
        {
            return _gatewayLocation;
        }
    }
}