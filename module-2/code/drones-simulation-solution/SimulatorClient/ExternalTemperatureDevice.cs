using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class ExternalTemperatureDevice : Device
    {
        private const int PERIOD_IN_SECONDS = 60;

        private readonly Random _random = new Random();

        public ExternalTemperatureDevice(string droneId)
            : base(droneId)
        {
        }

        public async override Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(PERIOD_IN_SECONDS), cancellationToken);

                OnNewEvent(new DroneEvent
                {
                    DroneId = DroneId,
                    Device = "external-temperature",
                    Measurement = 18.2 + (_random.NextDouble() * 0.05)
                });
            }
        }
    }
}