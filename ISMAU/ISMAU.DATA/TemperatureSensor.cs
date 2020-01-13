using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMAU.DATA
{
    public class TemperatureSensor : Sensor
    {
        private const double INVALID_VALUE = -239.0;
        private double degrees;
        private RangeBoundaries<double> Boundaries;

        public double Degrees
        {
            get => degrees;
            set
            {
                if (value >= Boundaries.Min && value <= Boundaries.Max)
                    degrees = value;
                else
                    degrees = INVALID_VALUE;
            }
        }

        public TemperatureSensor(
            string name,
            string description,
            Location location,
            RangeBoundaries<double> acceptableRange,
            float tickOff,
            int pollingInterval = 1000)
            : base(name, description, location, tickOff, pollingInterval)
        {
            Degrees = INVALID_VALUE;
            Boundaries = acceptableRange;
        }

        public override void GetData()
        {
            throw new NotImplementedException();
        }
    }
}
