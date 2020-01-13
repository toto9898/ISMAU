using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMAU.DATA
{
    public class HumiditySensor : Sensor
    {
        private const double INVALID_VALUE = -1.0;
        private double humidity;
		private RangeBoundaries<float> Boundaries;

		public double Humidity
        {
            get => humidity;
            set
            {
                if (value >= Boundaries.Min && value <= Boundaries.Max)
                    humidity = value;
                else
                    humidity = INVALID_VALUE;
            }
        }

        public HumiditySensor(
            string name,
            string description,
            Location location,
            RangeBoundaries<float> acceptableRange,
            float tickOff,
            int pollingInterval = 1000)
            : base(name, description, location, tickOff, pollingInterval)
        {
            Humidity = INVALID_VALUE;
            Boundaries = acceptableRange;
        }

        public override void GetData()
        {
            throw new NotImplementedException();
        }
    }
}
