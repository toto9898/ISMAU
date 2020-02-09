using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ISMAU.DATA
{
    public class HumiditySensor : Sensor
    {
        private const double INVALID_VALUE = -1.0;
        private double humidity;
		private RangeBoundaries<float> Boundaries;

		public double Humidity
        {
            get => humidity;
            set
            {
				if (value >= Boundaries.Min && value <= Boundaries.Max)
				{
					humidity = value;
				}
				else
					humidity = INVALID_VALUE;
            }
        }

		public RangeBoundaries<float> SetBoundaries
		{
			get { return Boundaries; }
			set
			{
				Boundaries = new RangeBoundaries<float>();
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


        public HumiditySensor(
            string name,
            string description,
            Location location,
            RangeBoundaries<float> acceptableRange,
            float tickOff,
            int pollingInterval = 1000)
            : base(name, description, location, tickOff, "HumiditySensor", pollingInterval)

		{
            Boundaries = acceptableRange;
            Humidity = INVALID_VALUE;
        }

        public HumiditySensor() : base()
        {
            Boundaries = new RangeBoundaries<float>();
            Humidity = INVALID_VALUE;
        }
        public HumiditySensor(HumiditySensor sensor)
        {
            Boundaries.Min = sensor.Boundaries.Min;
            Boundaries.Max = sensor.Boundaries.Max;
            Humidity = sensor.Humidity;
        }

        public override ToolTip GetData()
        {
            ToolTip tip = new ToolTip();
            string content = "Type: HumiditySensor";
            content += string.Format("\nName: {0}\nLatitude: {1},  Longitude: {2}", Name, Location.Latitude, Location.Longitude);
            tip.Content = content;
            return tip;
        }

		public override void ConvertValueString()
		{
			double.TryParse(DataAsString, out humidity);
		}
	}
}
