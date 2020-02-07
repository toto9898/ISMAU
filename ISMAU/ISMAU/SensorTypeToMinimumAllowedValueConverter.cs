using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ISMAU
{
	public class SensorTypeToMinimumAllowedValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return "";
			string sensorType = (string)value;
			if (sensorType.Equals("ElPowerSensor"))
				return "0";
			if (sensorType.Equals("NoiseSensor"))
				return "-100";
			if (sensorType.Equals("HumiditySensor"))
				return "0";
			if (sensorType.Equals("TemperatureSensor"))
				return "-40";
			return "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
