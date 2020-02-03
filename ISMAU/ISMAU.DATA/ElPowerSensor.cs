using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        public ElPowerSensor(
            string name,
            string description,
            Location location,
            RangeBoundaries<int> acceptableRange,
            float tickOff,
            int pollingInterval = 1000)
            : base(name, description, location, tickOff, pollingInterval)
        {
			Boundaries = acceptableRange;
            Wats = INVALID_VALUE;
        }

        public ElPowerSensor() : base()
        {
            Boundaries = new RangeBoundaries<int>();
            Wats = INVALID_VALUE;
        }
        public ElPowerSensor(ElPowerSensor sensor) :base(sensor)
        {
            Boundaries.Min = sensor.Boundaries.Min;
            Boundaries.Max = sensor.Boundaries.Max;
            Wats = sensor.Wats;
        }


        public override ToolTip GetData()
        {
            ToolTip tip = new ToolTip();
            string content = "Type: ElPowerSensor";
            content += string.Format("\nName: {0}\nLatitude: {1},  Longitude: {2}", Name, Location.Latitude, Location.Longitude);
            tip.Content = content;
            return tip;
        }
    }
}
