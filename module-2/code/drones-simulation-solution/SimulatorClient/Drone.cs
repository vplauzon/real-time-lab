using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public class Drone
    {
        #region Inner Types
        private class Trajectory
        {
            public Trajectory(
                GeoPoint originLocation,
                GeoPoint destinationLocation,
                DateTime startTime)
            {
                OriginLocation = originLocation;
                DestinationLocation = destinationLocation;
                StartTime = startTime;
            }

            public GeoPoint OriginLocation { get; }

            public GeoPoint DestinationLocation { get; }

            public DateTime StartTime { get; }

            public GeoPoint GetCurrentLocation()
            {
                return OriginLocation;
            }
        }
        #endregion

        private const double MAX_DISTANCE_FROM_GATEWAY = 15;

        private readonly string _droneId =
            "1.2.22;" + Guid.NewGuid().GetHashCode().ToString("x8");
        private readonly Random _random = new Random();
        private readonly GeoPoint _gatewayLocation;
        private Trajectory _trajectory;

        public event EventHandler<DroneEvent>? NewEvent;

        public Drone(GeoPoint gatewayLocation)
        {
            _gatewayLocation = gatewayLocation;
            //  Dummy data to avoid null value
            _trajectory = new Trajectory(RandomPoint(), RandomPoint(), DateTime.Now);
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var externalTemperatureDevice = new TemperatureDevice(_droneId, true);
            var internalTemperatureDevice = new TemperatureDevice(_droneId, false);
            var gpsDevice = new GpsDevice(_droneId, GetCurrentLocation);
            var devices = new Device[]
            {
                externalTemperatureDevice,
                internalTemperatureDevice,
                gpsDevice
            };

            foreach (var d in devices)
            {   //  Bubble up
                d.NewEvent +=
                    (sender, eventData) => NewEvent?.Invoke(sender, eventData);
            }

            var deviceTasks = from d in devices
                              select d.RunAsync(cancellationToken);
            var moveTask = MoveAsync(cancellationToken);

            await Task.WhenAll(deviceTasks.Prepend(moveTask));
        }

        private Task MoveAsync(CancellationToken cancellationToken)
        {
            _trajectory = new Trajectory(_gatewayLocation, RandomPoint(), DateTime.Now);

            while (!cancellationToken.IsCancellationRequested)
            {
                _trajectory = new Trajectory(
                    _trajectory.DestinationLocation,
                    RandomPoint(),
                    DateTime.Now);
            }

            return Task.CompletedTask;
        }

        private GeoPoint RandomPoint()
        {
            //  x, y are on a [-1, 1]^2 square
            var x = 2 * _random.NextDouble() - 1;
            var y = 2 * _random.NextDouble() - 1;
            //  Let's test if the point is on a radius=1 circle
            var radius = x * x + y * y;

            if (radius <= 1)
            {
                var point = new GeoPoint(
                    x * MAX_DISTANCE_FROM_GATEWAY / GeoPoint.KmPerLongitudeDegree,
                    y * MAX_DISTANCE_FROM_GATEWAY / GeoPoint.KmPerLatitudeDegree);

                return point;
            }
            else
            {   //  Point is outside the circle, let's retry
                return RandomPoint();
            }
        }

        private GeoPoint GetCurrentLocation()
        {
            return _trajectory.GetCurrentLocation();
        }
    }
}