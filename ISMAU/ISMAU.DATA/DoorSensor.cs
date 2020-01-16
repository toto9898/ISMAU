using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public DoorSensor() : this("", "", null, 0)
		{ }

        public override void GetData()
        {
            throw new NotImplementedException();
        }
    }
}
