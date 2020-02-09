using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ISMAU.DATA
{
    public class TemperatureSensor : Sensor
    {
        private const double INVALID_VALUE = -239.0;
        private double degrees;
        private RangeBoundaries<double> Boundaries;

        public double Degrees
        {
            get => degrees;
            set
            {
				if (value >= Boundaries.Min && value <= Boundaries.Max)
				{
					degrees = value;
				}
				else
					degrees = INVALID_VALUE;
            }
        }

		public RangeBoundaries<double> SetBoundaries
		{
			get { return Boundaries; }
			set
			{
				Boundaries = new RangeBoundaries<double>();
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


		public TemperatureSensor(
            string name,
            string description,
            Location location,
            RangeBoundaries<double> acceptableRange,
            float tickOff,
            int pollingInterval = 1000)
            : base(name, description, location, tickOff, "TemperatureSensor", pollingInterval)

		{
            Boundaries = acceptableRange;
            Degrees = INVALID_VALUE;
        }

        public TemperatureSensor() : base()
        {
            Boundaries = new RangeBoundaries<double>();
            Degrees = INVALID_VALUE;
        }

        public TemperatureSensor(TemperatureSensor sensor)
        {
            Boundaries.Min = sensor.Boundaries.Min;
            Boundaries.Max = sensor.Boundaries.Max;
            Degrees = sensor.Degrees;
        }

        public override ToolTip GetData()
        {
            ToolTip tip = new ToolTip();
            string content = "Type: TemperatureSensor";
            content += string.Format("\nName: {0}\nLatitude: {1},  Longitude: {2}", Name, Location.Latitude, Location.Longitude);
            tip.Content = content;
            return tip;
        }

		public override void ConvertValueString()
		{
			double.TryParse(DataAsString, out degrees);
		}
	}
}
