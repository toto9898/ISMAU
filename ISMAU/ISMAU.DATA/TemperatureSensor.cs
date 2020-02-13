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
    /// This class represents a temperature sensor
    /// </summary>
    public class TemperatureSensor : BoundedSensor<double>
    {
        /// <summary>
        /// Holds the decibels measured from the sensor
        /// </summary>
        public double Degrees { get => Data; set => Data = value; }

        /// <summary>
        /// Constructs the object with the given data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rangeBoundaries"></param>
        public TemperatureSensor(SensorData data, RangeBoundaries<double> rangeBoundaries)
            : base(data, rangeBoundaries)
        {
            Degrees = -239.0;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TemperatureSensor()
            : base()
        {
            Degrees = -239.0;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="sensor"></param>
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
