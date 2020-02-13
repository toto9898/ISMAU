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
    /// This class represents a noise sensor
    /// </summary>
    public class NoiseSensor : BoundedSensor<int>
    {
        /// <summary>
        /// Holds the decibels measured from the sensor
        /// </summary>
		public int Decibels { get => Data; set => Data = value; }

        /// <summary>
        /// Constructs the object with the given data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rangeBoundaries"></param>
        public NoiseSensor(SensorData data, RangeBoundaries<int> rangeBoundaries)
            : base(data, rangeBoundaries)
        {
            Decibels = 0;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public NoiseSensor()
            : base()
        {
            Decibels = 0;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="sensor"></param>
        public NoiseSensor(NoiseSensor sensor)
            : base(sensor)
        {
            Decibels = sensor.Decibels;
        }

        public override void GetData()
        {

        }

        public override void ConvertValueString()
		{
            int temp = 0;
			if (Int32.TryParse(DataAsString, out temp))
                Decibels = temp;
		}
	}
}
