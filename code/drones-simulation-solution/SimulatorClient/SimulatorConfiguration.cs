using System;

namespace SimulatorClient
{
    internal class SimulatorConfiguration
    {
        public SimulatorConfiguration()
        {
            AppInsightsKey = ConfigurationString("APP_INSIGHTS_KEY");
            EventHubConnectionString = ConfigurationString("EVENT_HUB_CONN_STRING");
            GatewayCount = ConfigurationInteger("GATEWAY_COUNT", 20);
            DronePerGateway = ConfigurationInteger("DRONE_PER_GATEWAY", 20);
            SnapInternalTemperatureLikelihood = ConfigurationDouble("SNAP_INTERNAL_TEMPERATURE_LIKELIHOOD", 0);
        }

        public string AppInsightsKey { get; set; }

        public string EventHubConnectionString { get; set; }

        public int GatewayCount { get; set; }
        
        public int DronePerGateway { get; set; }
        
        public double SnapInternalTemperatureLikelihood { get; set; }

        #region Configuration key methods
        private string ConfigurationString(string key, string? defaultValue = null)
        {
            var value = Environment.GetEnvironmentVariable(key);

            if (string.IsNullOrWhiteSpace(value))
            {
                if (defaultValue == null)
                {
                    throw new ArgumentNullException("Environment variable missing", key);
                }
                else
                {
                    return defaultValue;
                }
            }
            else
            {
                return value;
            }
        }

        private int ConfigurationInteger(string key, int? defaultValue = null)
        {
            var text = Environment.GetEnvironmentVariable(key);

            if (string.IsNullOrWhiteSpace(text))
            {
                if (defaultValue == null)
                {
                    throw new ArgumentNullException("Environment variable missing", key);
                }
                else
                {
                    return defaultValue.Value;
                }
            }
            else
            {
                int value;

                if (!int.TryParse(text, out value))
                {
                    throw new ArgumentException("Env Var isn't an integer", key);
                }
                else
                {
                    return value;
                }
            }
        }

        private double ConfigurationDouble(string key, double? defaultValue = null)
        {
            var text = Environment.GetEnvironmentVariable(key);

            if (string.IsNullOrWhiteSpace(text))
            {
                if (defaultValue == null)
                {
                    throw new ArgumentNullException("Environment variable missing", key);
                }
                else
                {
                    return defaultValue.Value;
                }
            }
            else
            {
                double value;

                if (!double.TryParse(text, out value))
                {
                    throw new ArgumentException("Env Var isn't a double", key);
                }
                else
                {
                    return value;
                }
            }
        }
        #endregion
    }
}