using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ISMAU.DATA
{
    /// <summary>
    /// This class represents a humidity sensor
    /// </summary>
    public class HumiditySensor : BoundedSensor<float>
    {
        /// <summary>
        /// Holds the wats measured from the sensor
        /// </summary>
        public float Humidity { get => Data; set => Data = value; }

        /// <summary>
        /// Constructs the object with the given data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rangeBoundaries"></param>
        public HumiditySensor(SensorData data, RangeBoundaries<float> rangeBoundaries)
            : base(data, rangeBoundaries)
        {
            Humidity = -1f;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public HumiditySensor()
            : base()
        {
            Humidity = -1f;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="sensor"></param>
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
