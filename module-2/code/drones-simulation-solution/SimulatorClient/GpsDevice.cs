using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class GpsDevice : Device
    {
        private readonly GeoPoint _gatewayLocation;

        public GpsDevice(string droneId, GeoPoint gatewayLocation)
            : base(droneId)
        {
            _gatewayLocation = gatewayLocation;
        }

        public async override Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);

                OnNewEvent(new DroneEvent
                {
                    DroneId = DroneId,
                    Device = "GPS",
                    Measurement = _gatewayLocation.ToGeoJsonPoint()
                });
            }
        }
    }
}