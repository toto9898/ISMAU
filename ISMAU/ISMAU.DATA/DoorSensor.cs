using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMAU.DATA
{
    public class DoorSensor : Sensor<bool>
    {
        public bool IsClosed { get; set; }

        public DoorSensor(
            string name, 
            string description, 
            Location location, 
            RangeBoundaries<bool> acceptableRange, 
            float tickOff, 
            int pollingInterval = 1000) 
            : base(name, description, location, acceptableRange, tickOff, pollingInterval)
        {
            // true will be the default state of the door/window
            IsClosed = true;
        }

        public override bool GetData()
        {
            throw new NotImplementedException();
        }
    }
}
