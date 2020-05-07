using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class InternalTemperatureDevice : Device
    {
        const int PERIOD_IN_SECONDS = 20;
        const double TEMPERATURE_BASE = 46.2;
        const double BIAS_SCALE = 1.4;
        const double NOISE_MIN_SCALE = 0.1;
        const double NOISE_MAX_SCALE = 0.3;
        const double SNAP_BIAS_INCREMENT_SCALE = 0.002;
        const double SNAP_BIAS_THRESHOLD = 0.06;

        private readonly double _snapInternalTemperatureLikelihood;
        private readonly Random _random = new Random();
        private readonly double _bias;
        private readonly double _noiseAmplitude;

        public InternalTemperatureDevice(string droneId, double snapInternalTemperatureLikelihood)
            : base(droneId)
        {
            _snapInternalTemperatureLikelihood = snapInternalTemperatureLikelihood;
            _bias = (_random.NextDouble() - 0.5) * BIAS_SCALE;
            _noiseAmplitude = NOISE_MIN_SCALE
                + _random.NextDouble() * (NOISE_MAX_SCALE - NOISE_MIN_SCALE);
        }

        public async override Task RunAsync(CancellationToken cancellationToken)
        {
            var isSnapping = false;
            var snappingBias = (double)0;

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(PERIOD_IN_SECONDS), cancellationToken);

                //  Trial if the device starts to snap
                isSnapping = isSnapping
                    || _random.NextDouble() < _snapInternalTemperatureLikelihood;
                //  If it is snapping, a negative bias will build up until it snaps
                if (isSnapping)
                {
                    snappingBias += _random.NextDouble() * SNAP_BIAS_INCREMENT_SCALE;

                    if (snappingBias > SNAP_BIAS_THRESHOLD)
                    {   //  The device snaps and is out-of-order
                        return;
                    }
                }

                OnNewEvent(new DroneEvent
                {
                    DroneId = DroneId,
                    Device = "internal-temperature",
                    Measurement = TEMPERATURE_BASE
                    + _bias
                    - snappingBias
                    + (_random.NextDouble() * _noiseAmplitude)
                });
            }
        }
    }
}