﻿using System;
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

        public static double KmPerLongitudeDegree { get; } = 111.32;
        
        public static double KmPerLatitudeDegree { get; } = 110.57;

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
    }
}