using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ISMAU
{
	/// <summary>
	/// Used to set constrains to the visibility of an element
	/// </summary>
	public class SensorTypeToRangesVisibilityConverter : IValueConverter
	{
		/// <summary>
		/// Returns the target value based on the sensor type, later used for visibility constrains
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return Visibility.Visible;
			string sensorType = (string)value;
			if (sensorType.Equals("DoorSensor"))
				return Visibility.Hidden;
			return Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
