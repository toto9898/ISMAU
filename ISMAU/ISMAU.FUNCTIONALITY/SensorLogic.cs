using System;
using System.Collections.Generic;
using ISMAU.DATA;
using System.IO;
using Microsoft.Maps.MapControl.WPF;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using System.Collections.ObjectModel;

namespace ISMAU.FUNCTIONALITY
{
	public class SensorLogic : ViewModelBase
	{

		#region Data Members
		private ObservableCollection<Sensor> sensors;
		private const string DATABASE_NAME = @"\data.xml";
		private string databasePath;

		#endregion

		#region Constructor
		public SensorLogic()
		{
			string current = Environment.CurrentDirectory;
			current = Directory.GetParent(current).Parent.Parent.FullName;
			databasePath = current + DATABASE_NAME;

			try
			{
				sensors = sensors.Deserialize(databasePath);
			}
			catch (Exception)
			{
				sensors = new ObservableCollection<Sensor>();
			}
		} 
		#endregion

		#region Properties
		public string DatabaseName
		{
			get 
			{ 
			return DATABASE_NAME; 
			}
			private set { }
		}

		public string DatabasePath
		{
			get 
			{ 
				return databasePath; 
			}
			private set { }
		}

		public ObservableCollection<Sensor> Sensors
		{
			get
			{
				return sensors;
			}
			private set { }
		}
		#endregion

		#region Methods
		public void SaveState()
		{
			sensors.Serialize(databasePath);
		}

		public bool AddSensor
		(
			string name,
			string description,
			Location location,
			float tickOff,
			string type,
			string boundariesMin,
			string boundariesMax,
			int pollingInterval = 1000)
		{
			bool success = true;

			if (type == "DoorSensor")
				sensors.Add(new DoorSensor(name, description, location, tickOff, pollingInterval));
			else if (type == "ElPowerSensor" || type == "NoiseSensor")
			{
				success = int.TryParse(boundariesMin, out int min);
				if (!success) return false;
				success = int.TryParse(boundariesMax, out int max);
				if (!success) return false;

				RangeBoundaries<int> boundaries = new RangeBoundaries<int>();
				boundaries.Min = min;
				boundaries.Max = max;

				if (type == "ElPowerSensor")
					sensors.Add(new ElPowerSensor(name, description, location, boundaries, tickOff, pollingInterval));
				else
					sensors.Add(new NoiseSensor(name, description, location, boundaries, tickOff, pollingInterval));
			}
			else if (type == "HumiditySensor")
			{
				success = float.TryParse(boundariesMin, out float min);
				if (!success) return false;
				success = float.TryParse(boundariesMax, out float max);
				if (!success) return false;

				RangeBoundaries<float> boundaries = new RangeBoundaries<float>();
				boundaries.Min = min;
				boundaries.Max = max;

				sensors.Add(new HumiditySensor(name, description, location, boundaries, tickOff, pollingInterval));
			}
			else if (type == "TemperatureSensor")
			{
				success = double.TryParse(boundariesMin, out double min);
				if (!success) return false;
				success = double.TryParse(boundariesMax, out double max);
				if (!success) return false;

				RangeBoundaries<double> boundaries = new RangeBoundaries<double>();
				boundaries.Min = min;
				boundaries.Max = max;

				sensors.Add(new TemperatureSensor(name, description, location, boundaries, tickOff, pollingInterval));
			}
			return success;
		}

		public List<Pushpin> initializePins()
		{
			List<Pushpin> output = new List<Pushpin>();
			foreach(var elem in sensors)
			{
				Pushpin pin = new Pushpin();
				pin.ToolTip = elem.GetData();
				pin.Location = new Location(elem.Location.Latitude, elem.Location.Longitude);
				output.Add(pin);
			}
			return output;
		}
		#endregion
	}
}
