using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class InternalTemperatureDevice : Device
    {
        private const int PERIOD_IN_SECONDS = 20;

        private readonly Random _random = new Random();

        public InternalTemperatureDevice(string droneId)
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
                    Device = "internal-temperature",
                    Measurement = 46.2 + (_random.NextDouble() * 0.08)
                });
            }
        }
    }
}