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
    /// This class represents a el power sensor
    /// </summary>
    public class ElPowerSensor : BoundedSensor<int>
    {
        /// <summary>
        /// Holds the wats measured from the sensor
        /// </summary>
        public int Watts { get => Data; set => Data = value; }

        /// <summary>
        /// Constructs the object with the given data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rangeBoundaries"></param>
        public ElPowerSensor(SensorData data, RangeBoundaries<int> rangeBoundaries)
            : base(data, rangeBoundaries)
        {
            Watts = -1;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ElPowerSensor()
            : base()
        {
            Watts = -1;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="sensor"></param>
        public ElPowerSensor(ElPowerSensor sensor)
            : base(sensor)
        {
            Watts = sensor.Watts;
        }

        public override void GetData()
        {

        }
        public override void ConvertValueString()
		{
            int temp = 0;
			Int32.TryParse(DataAsString, out temp);
            Watts = temp;
		}
	}
}
