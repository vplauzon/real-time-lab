using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class TemperatureDevice : Device
    {
        private const int PERIOD_IN_SECONDS = 20;

        private readonly Random _random = new Random();
        private readonly bool _isExternal;

        public TemperatureDevice(string droneId, bool isExternal)
            : base(droneId)
        {
            _isExternal = isExternal;
        }

        public async override Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(PERIOD_IN_SECONDS), cancellationToken);

                OnNewEvent(new DroneEvent
                {
                    DroneId = DroneId,
                    Device = _isExternal ? "external-temperature" : "internal-temperature",
                    Measurement = (_isExternal ? 18.2 : 46.2) + (_random.NextDouble() * 0.05)
                });
            }
        }
    }
}