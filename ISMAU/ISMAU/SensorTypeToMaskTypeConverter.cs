using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ISMAU
{
	/// <summary>
	/// Sets the input constrains of the mask
	/// </summary>
	public class SensorTypeToMaskTypeConverter : IValueConverter
	{
		/// <summary>
		/// Converts the target value based on the sensor type
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return "";
			string sensorType = (string)value;
			if (sensorType.Equals("ElPowerSensor"))
				return "d6";
			if (sensorType.Equals("NoiseSensor"))
				return "d3";
			if (sensorType.Equals("HumiditySensor"))
				return "#3.2";
			if (sensorType.Equals("TemperatureSensor"))
				return "#3.1";
			return "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
