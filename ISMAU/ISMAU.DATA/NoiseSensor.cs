using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
				{
					decibels = value;
				}
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
            : base(name, description, location, tickOff, "NoiseSensor", pollingInterval)

		{
            Boundaries = acceptableRange;
            Decibels = INVALID_VALUE;
        }

        public NoiseSensor() : base()
        {
            Boundaries = new RangeBoundaries<int>();
            Decibels = INVALID_VALUE;
        }

        //public NoiseSensor(NoiseSensor sensor) : base (sensor)
        //{
        //    Boundaries.Min = sensor.Boundaries.Min;
        //    Boundaries.Max = sensor.Boundaries.Max;
        //    Decibels = sensor.Decibels;
        //}

        public override ToolTip GetData()
        {
            ToolTip tip = new ToolTip();
            string content = "Type: NoiseSensor";
            content += string.Format("\nName: {0}\nLatitude: {1},  Longitude: {2}", Name, Location.Latitude, Location.Longitude);
            tip.Content = content;
            return tip;
        }

		public override void ConvertValueString()
		{
			Int32.TryParse(DataAsString, out decibels);
		}
	}
}
