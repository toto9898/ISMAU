using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMAU.DATA
{
    class NoiseSensor : Sensor<int>
    {
        private static int INVALID_VALUE = -1;
        private int decibels;

        public int Decibels
        {
            get => decibels;
            set
            {
                if (value >= AcceptableRange.Min && value <= AcceptableRange.Max)
                    decibels = value;
                else
                    decibels = INVALID_VALUE;
            }
        }


        public NoiseSensor(
            string name, 
            string description, 
            Location location, 
            RangeBoundaries<int> acceptableRange, 
            float tickOff, 
            int pollingInterval = 1000) 
            : base(name, description, location, acceptableRange, tickOff, pollingInterval)
        {
            Decibels = INVALID_VALUE;
        }

        public override int GetData()
        {
            throw new NotImplementedException();
        }
    }
}
