﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMAU.DATA
{
    public class ElPowerSensor : Sensor
    {
        private const int INVALID_VALUE = -1;
        private int wats;
		private RangeBoundaries<int> Boundaries;

		public int Wats
        {
            get => wats;
            set 
            {
                if (value >= Boundaries.Min && value <= Boundaries.Max)
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
            : base(name, description, location, tickOff, pollingInterval)
        {
            Wats = INVALID_VALUE;
			Boundaries = acceptableRange;
        }


        public override void GetData()
        {
            throw new NotImplementedException();
        }
    }
}