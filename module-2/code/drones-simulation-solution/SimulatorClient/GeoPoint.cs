using System;
using System.Collections.Generic;

namespace SimulatorClient
{
    public struct GeoPoint
    {
        private readonly double _longitude;
        private readonly double _latitude;

        public GeoPoint(double longitude, double latitude)
        {
            _longitude = longitude;
            _latitude = latitude;
        }

        public double Longitude { get { return _longitude; } }

        public double Latitude { get { return _latitude; } }

        public object ToGeoJsonPoint()
        {
            return new
            {
                Type = "Point",
                Coordinates = new[]
                {
                    _longitude,
                    _latitude
                }
            };
        }

        #region object methods
        public override string ToString()
        {
            return $"{{{Longitude}, {Latitude}}}";
        }
        #endregion
    }
}