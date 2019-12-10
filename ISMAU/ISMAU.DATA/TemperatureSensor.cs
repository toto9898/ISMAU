using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMAU.DATA
{
    public class TemperatureSensor : Sensor<double>
    {
        private static double INVALID_VALUE = -239.0;
        private double degrees;

        public double Degrees
        {
            get => degrees;
            set
            {
                if (value >= AcceptableRange.Min && value <= AcceptableRange.Max)
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
            : base(name, description, location, acceptableRange, tickOff, pollingInterval)
        {
            Degrees = INVALID_VALUE;
        }

        public override double GetData()
        {
            throw new NotImplementedException();
        }
    }
}
