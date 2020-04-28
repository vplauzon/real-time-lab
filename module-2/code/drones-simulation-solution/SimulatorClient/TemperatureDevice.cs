using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class TemperatureDevice : Device
    {
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
                await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);

                OnNewEvent(new DroneEvent
                {
                    DroneId = DroneId,
                    Device = _isExternal ? "external-temperature" : "internal-temperature",
                    Measurement = _isExternal ? 18.2 : 46.2
                });
            }
        }
    }
}