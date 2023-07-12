using System;

namespace ASP.NET_Core依赖注入详解
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string TransientGuid { get; set; }
        public string ScopedGuid { get; set; }
        public string SingletonGuid { get; set; }
        
    }
}
