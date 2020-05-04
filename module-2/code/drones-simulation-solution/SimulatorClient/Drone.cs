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
        /// <summary>
        /// This implements a s(t) curve, i.e. it returns the coordinate at a given time.
        /// </summary>
        private class Trajectory
        {
            private readonly double _ratioLongitude;
            private readonly double _ratioLatitude;
            private readonly double _maxDistance;

            public Trajectory(
                double speed,
                GeoPoint originLocation,
                GeoPoint destinationLocation,
                DateTime startTime)
            {
                var deltaLongitude = destinationLocation.Longitude - originLocation.Latitude;
                var deltaLatitude = destinationLocation.Latitude - originLocation.Latitude;
                var theta = Math.Atan2(deltaLatitude, deltaLongitude);

                _maxDistance =
                    Math.Sqrt(deltaLongitude * deltaLongitude + deltaLatitude * deltaLatitude);
                _ratioLongitude = Math.Cos(theta);
                _ratioLatitude = Math.Sin(theta);

                Speed = speed;
                OriginLocation = originLocation;
                DestinationLocation = destinationLocation;
                StartTime = startTime;
                Duration = TimeSpan.FromHours(_maxDistance / Speed);
            }

            public double Speed { get; }

            public GeoPoint OriginLocation { get; }

            public GeoPoint DestinationLocation { get; }

            public DateTime StartTime { get; }

            public TimeSpan Duration { get; }

            public GeoPoint GetCurrentLocation()
            {   //  Standardize time in hours, speed in km/h
                var t = DateTime.Now.Subtract(StartTime).TotalHours;
                var s = Speed * t;
                var cappedS = Math.Max(s, _maxDistance);
                var location = new GeoPoint(
                    OriginLocation.Longitude + _ratioLongitude * cappedS,
                    OriginLocation.Latitude + _ratioLatitude * cappedS);

                return location;
            }
        }
        #endregion

        private const double MAX_DISTANCE_FROM_GATEWAY_IN_GEO = 0.005;
        private const double DEFAULT_SPEED_IN_GEO = 0.05;

        private readonly string _droneId =
            "1.2.22;" + Guid.NewGuid().GetHashCode().ToString("x8");
        private readonly Random _random = new Random();
        private readonly GeoPoint _gatewayLocation;
        private readonly double _speed;
        private Trajectory _trajectory;

        public event EventHandler<DroneEvent>? NewEvent;

        public Drone(GeoPoint gatewayLocation)
        {
            _gatewayLocation = gatewayLocation;
            //  Adjust the max speed of the drone
            _speed = DEFAULT_SPEED_IN_GEO - _random.NextDouble() * DEFAULT_SPEED_IN_GEO * 0.1;
            //  Dummy data to avoid null value
            _trajectory = new Trajectory(
                _speed,
                RandomPoint(),
                RandomPoint(),
                DateTime.Now);
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

        private async Task MoveAsync(CancellationToken cancellationToken)
        {
            _trajectory = new Trajectory(
                _speed,
                _gatewayLocation,
                RandomPoint(),
                DateTime.Now);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);

                _trajectory = new Trajectory(
                    _speed,
                    _trajectory.DestinationLocation,
                    RandomPoint(),
                    DateTime.Now);
            }
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
                    x * MAX_DISTANCE_FROM_GATEWAY_IN_GEO,
                    y * MAX_DISTANCE_FROM_GATEWAY_IN_GEO);

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