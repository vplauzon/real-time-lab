using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class InternalTemperatureDevice : Device
    {
        private readonly Random _random = new Random();
        private readonly double _bias;
        private readonly double _noiseAmplitude;

        public InternalTemperatureDevice(string droneId)
            : base(droneId)
        {
            _bias = (_random.NextDouble() - 0.5) * 0.02;
            _noiseAmplitude = _random.NextDouble() + 1;
        }

        public async override Task RunAsync(CancellationToken cancellationToken)
        {
            const int PERIOD_IN_SECONDS = 20;
            const double SNAP_LIKELIHOOD_PER_PERIOD = 0.03;

            var isSnapping = false;
            var snappingBias = (double)0;

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(PERIOD_IN_SECONDS), cancellationToken);

                //  Trial if the device starts to snap
                isSnapping = isSnapping
                    || _random.NextDouble() < SNAP_LIKELIHOOD_PER_PERIOD;
                //  If it is snapping, a negative bias will build up until it snaps
                if (isSnapping)
                {
                    snappingBias -= _random.NextDouble() * 0.002;

                    if (snappingBias < -0.03)
                    {   //  The device snaps and is out-of-order
                        return;
                    }
                }

                OnNewEvent(new DroneEvent
                {
                    DroneId = DroneId,
                    Device = "internal-temperature",
                    Measurement = 46.2
                    + _bias
                    + snappingBias
                    + (_random.NextDouble() * _noiseAmplitude)
                });
            }
        }
    }
}