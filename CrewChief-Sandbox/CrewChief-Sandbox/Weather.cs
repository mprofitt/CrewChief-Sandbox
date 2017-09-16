using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrewChief_Sandbox
{
    class Weather
    {
        public int Skies { get; set; } // 0=clear - 1=partly cloudy - 2=mostly cloudy - 3=overcast)
        public float AirTemp { get; set; }
        public float AirPressure { get; set; } // inches of mercury
        public float WindVel { get; set; }
        public float WindDir { get; set; } // radians
        public float RelativeHumidity { get; set; } // percentage
        public float FogLevel { get; set; } // percentage

        public Weather(int Skies, float AirTemp, float AirPressure, float WindVel, float WindDir, float RelativeHumidity, float FodLevel)
        {
            this.Skies = Skies;
            this.AirTemp = AirTemp;
            this.AirPressure = AirPressure;
            this.WindVel = WindVel;
            this.WindDir = WindDir;
            this.RelativeHumidity = RelativeHumidity;
            this.FogLevel = FogLevel;
        }
    }
}
