namespace SimulatorClient
{
    public class GatewayMessage
    {
        public string GatewayId { get; set; } = "";

        public DroneEvent[] Events { get; set; } = new DroneEvent[0];
    }
}