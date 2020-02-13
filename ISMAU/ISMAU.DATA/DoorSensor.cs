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
    /// This class represents a door sensor
    /// </summary>
    public class DoorSensor : Sensor
    {
        /// <summary>
        /// It's true iff the door is closed
        /// </summary>
        public bool IsClosed { get; set; }

        /// <summary>
        /// Constructs the object with the given data
        /// </summary>
        /// <param name="data"></param>
        public DoorSensor(SensorData data)
            : base(data)
        {
            // true will be the default state of the door/window
            IsClosed = true;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DoorSensor()
            : base()
        {
            IsClosed = true;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="sensor"></param>
        public DoorSensor(DoorSensor sensor)
            : base(sensor)
        {
            IsClosed = sensor.IsClosed;
        }

        public override void GetData()
        {

        }

        public override void ConvertValueString()
		{
			if (DataAsString.Equals("1"))
				IsClosed = true;
			else
				IsClosed = false;
		}

        public override bool OutOfBounds()
        {
            return false;
        }
    }
}
