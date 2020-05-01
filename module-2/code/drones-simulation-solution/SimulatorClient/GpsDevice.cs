using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class GpsDevice : Device
    {
        private readonly double _gatewayLongitude;
        private readonly double _gatewayLatitude;

        public GpsDevice(string droneId, double gatewayLongitude, double gatewayLatitude)
            : base(droneId)
        {
            _gatewayLongitude = gatewayLongitude;
            _gatewayLatitude = gatewayLatitude;
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
                    Measurement = new
                    {
                        Type = "Point",
                        Coordinates = new[]
                        {
                            _gatewayLongitude,
                            _gatewayLatitude
                        }
                    }
                });
            }
        }
    }
}