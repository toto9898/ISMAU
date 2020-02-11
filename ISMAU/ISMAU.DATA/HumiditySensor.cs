using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ISMAU.DATA
{
    public class HumiditySensor : BoundedSensor<float>
    {
        public float Humidity { get => Data; set => Data = value; }

        public HumiditySensor(SensorData data, RangeBoundaries<float> rangeBoundaries)
            : base(data, rangeBoundaries)
        {
            Humidity = -1f;
        }

        public HumiditySensor()
            : base()
        {
            Humidity = -1f;
        }

        public HumiditySensor(HumiditySensor sensor)
            : base(sensor)
        {
            Humidity = sensor.Humidity;
        }

        public override void GetData()
        {

        }

        public override void ConvertValueString()
		{
            float temp = 0f;
			float.TryParse(DataAsString, out temp);
            Humidity = temp;
		}
	}
}
