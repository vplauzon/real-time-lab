using System;

namespace SimulatorClient
{
    public class DroneEvent
    {
        public string DroneId { get; set; } = string.Empty;

        public DateTime EventTimestamp { get; set; } = DateTime.Now;
        
        public string Device { get; set; } = string.Empty;

        public object? Measurement { get; set; }
    }
}