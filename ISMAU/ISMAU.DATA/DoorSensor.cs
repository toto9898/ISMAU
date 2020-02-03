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

		private bool isClosed;

		public bool IsClosed
		{
			get { return isClosed; }
			set { isClosed = value; }
		}

        public DoorSensor(
            string name, 
            string description, 
            Location location,
            float tickOff,
            int pollingInterval = 1000) 
            : base(name, description, location, tickOff, pollingInterval)
        {
            // true will be the default state of the door/window
            IsClosed = true;
        }

        public DoorSensor() : base()
        {
            IsClosed = true;
        }

        public DoorSensor(DoorSensor sensor) : base(sensor)
        {
            IsClosed = sensor.IsClosed;
        }

        public override ToolTip GetData()
        {
            ToolTip tip = new ToolTip();
            string content = "Type: DoorSensor";
            content += string.Format("\nName: {0}\nLatitude: {1},  Longitude: {2}", Name, Location.Latitude, Location.Longitude);
            tip.Content = content;
            return tip;
        }
    }
}
