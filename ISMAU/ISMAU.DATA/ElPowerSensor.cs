using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMAU.DATA
{
    class ElPowerSensor : Sensor<int>
    {
        private static int INVALID_VALUE = -1;
        private int wats;

        public int Wats
        {
            get => wats;
            set 
            {
                if (value >= AcceptableRange.Min && value <= AcceptableRange.Max)
                    wats = value;
                else
                    wats = INVALID_VALUE;
            }
        }

        public ElPowerSensor(
            string name,
            string description,
            Location location,
            RangeBoundaries<int> acceptableRange,
            float tickOff,
            int pollingInterval = 1000)
            : base(name, description, location, acceptableRange, tickOff, pollingInterval)
        {
            Wats = INVALID_VALUE;
        }


        public override int GetData()
        {
            throw new NotImplementedException();
        }
    }
}
