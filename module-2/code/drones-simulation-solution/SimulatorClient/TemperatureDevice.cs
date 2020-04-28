using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class TemperatureDevice : Device
    {
        public TemperatureDevice(string droneId) : base(droneId)
        {
        }

        public async override Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);

                OnNewEvent(new DroneEvent
                {
                    DroneId = DroneId,
                    Device = "temperature",
                    Measurement = 18.2
                });
            }
        }
    }
}