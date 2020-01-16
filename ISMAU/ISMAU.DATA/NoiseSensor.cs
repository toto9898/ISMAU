using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMAU.DATA
{
    public class NoiseSensor : Sensor
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

		public RangeBoundaries<int> SetBoundaries
		{
			get { return Boundaries; }
			set
			{
				Boundaries = new RangeBoundaries<int>();
				if (value != null)
				{
					Boundaries.Max = value.Max;
					Boundaries.Min = value.Min;
				}
				else
				{
					Boundaries.Max = Boundaries.Min = 0;
				}
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

		public NoiseSensor() : this("", "", null, null, 0) 
		{ }

        public override void GetData()
        {
            throw new NotImplementedException();
        }
    }
}
