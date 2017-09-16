using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrewChief_Sandbox
{
    class Weather
    {
        public int SessionID { get; set; }
        public string Skies { get; set; }

        private float _airTemp;
        public float AirTemp // Celcius
        {
            get
            {
                return _airTemp;
            }
            set
            {
                Math.Round(_airTemp = AirTemp * 1.8f + 32);
            }
        }

        public float AirPressure { get; set; } // inches of mercury

        private float _windVel;
        public float WindVel  // meters / second
        {
            get
            {
                return _windVel;
            }
            set
            {
                _windVel = (float)Math.Round(WindVel * 2.27273f); // Convert mertes / second to MPH
            }
        }

        public float WindDir { get; set; } // radians
        public int RelativeHumidity { get; set; } // percentage
        public int FogLevel { get; set; } // percentage
    }
}
