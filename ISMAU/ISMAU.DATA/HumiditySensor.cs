using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMAU.DATA
{
    public class HumiditySensor : Sensor<float>
    {
        private static double INVALID_VALUE = -1.0;
        private double humidity;

        public double Humidity
        {
            get => humidity;
            set
            {
                if (value >= AcceptableRange.Min && value <= AcceptableRange.Max)
                    humidity = value;
                else
                    humidity = INVALID_VALUE;
            }
        }

        public override float GetData()
        {
            throw new NotImplementedException();
        }
    }
}
