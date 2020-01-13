using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMAU.DATA
{
    class NoiseSensor : Sensor
    {
        private const int INVALID_VALUE = -1;
        private int decibels;
		private RangeBoundaries<int> Boundaries;

		public int Decibels
        {
            get => decibels;
            set
            {
                if (value >= Boundaries.Min && value <= Boundaries.Max)
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
            : base(name, description, location, tickOff, pollingInterval)
        {
            Decibels = INVALID_VALUE;
            Boundaries = acceptableRange;
        }

        public override void GetData()
        {
            throw new NotImplementedException();
        }
    }
}
