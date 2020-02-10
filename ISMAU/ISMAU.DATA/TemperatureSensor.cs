using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ISMAU.DATA
{
    public class TemperatureSensor : BoundedSensor<double>
    {
        public double Degrees { get; set; }

        public TemperatureSensor(SensorData data, RangeBoundaries<double> rangeBoundaries)
            : base(data, rangeBoundaries)
        {
            Degrees = -239.0;
        }

        public TemperatureSensor()
            : base()
        {
            Degrees = -239.0;
        }

        public TemperatureSensor(TemperatureSensor sensor)
            : base(sensor)
        {
            Degrees = sensor.Degrees;
        }

        public override void GetData()
        {

        }
        public override void ConvertValueString()
		{
            double temp = 0d;
			double.TryParse(DataAsString, out temp);
            Degrees = temp;
		}
	}
}
