using System;

namespace SimulatorClient
{
    public class GatewayMessage
    {
        public string GatewayId { get; set; } = string.Empty;

        public Guid MessageId { get; set; } = Guid.NewGuid();
        
        public DateTime MessageTimestamp { get; set; } = DateTime.Now;

        public DroneEvent[] Events { get; set; } = new DroneEvent[0];
    }
}