using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ISMAU.DATA
{
    public class DoorSensor : Sensor
    {
        public bool IsClosed { get; set; }

        public DoorSensor(SensorData data)
            : base(data)
        {
            // true will be the default state of the door/window
            IsClosed = true;
        }

        public DoorSensor()
            : base()
        {
            IsClosed = true;
        }

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
	}
}
