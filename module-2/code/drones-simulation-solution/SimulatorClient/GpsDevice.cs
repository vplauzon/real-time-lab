using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class GpsDevice : Device
    {
        private readonly Func<GeoPoint> _locationFunction;

        public GpsDevice(string droneId, Func<GeoPoint> locationFunction)
            : base(droneId)
        {
            _locationFunction = locationFunction;
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
                    Measurement = _locationFunction().ToGeoJsonPoint()
                });
            }
        }
    }
}