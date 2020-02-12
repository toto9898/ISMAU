using System;
using System.Collections.Generic;
using ISMAU.DATA;
using System.IO;
using Microsoft.Maps.MapControl.WPF;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ISMAU.FUNCTIONALITY
{
    public struct PushpinMetadata
    {
        public string Type;
        public string Name;
        public Location Location;
        public ulong Id;
    }



    public class SensorLogic : ViewModelBase
    {

        #region Data Members
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
                Sensors = Sensors.Deserialize(databasePath);
            }
            catch (Exception)
            {
                Sensors = new List<Sensor>();
            }
        }
        #endregion

        #region Properties
        public string DatabaseName
        {
            get { return DATABASE_NAME; }
            private set { }
        }

        public string DatabasePath
        {
            get { return databasePath; }
            private set { }
        }

        public List<Sensor> Sensors { get; set; }
        #endregion

        #region Methods
        public void SaveState()
        {
            Sensors.Serialize(databasePath);
        }

        public bool AddSensor(SensorData data, RangeBoundaries<string> rangeBoundaries)
        {
            bool success = true;
            Sensor sensor = null;

            try
            {
                switch (data.Type)
                {
                    case "DoorSensor":
                        sensor = new DoorSensor(data);
                        break;
                    case "ElPowerSensor":
                            sensor = new ElPowerSensor(data, new RangeBoundaries<int>(int.Parse(rangeBoundaries.Min), int.Parse(rangeBoundaries.Max)));
                        break;
                    case "NoiseSensor":
                            sensor = new NoiseSensor(data, new RangeBoundaries<int>(int.Parse(rangeBoundaries.Min), int.Parse(rangeBoundaries.Max)));
                        break;
                    case "HumiditySensor":
                        sensor = new HumiditySensor(data, new RangeBoundaries<float>(float.Parse(rangeBoundaries.Min), float.Parse(rangeBoundaries.Max)));
                        break;
                    case "TemperatureSensor":
                        sensor = new TemperatureSensor(data, new RangeBoundaries<double>(double.Parse(rangeBoundaries.Min), double.Parse(rangeBoundaries.Max)));
                        break;
                    default:
                        break;
                }
                Sensors.Add(sensor);
                SaveState();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to add the sensor!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                success = false;
            }

            return success;
        }

        public List<Pushpin> initializePins()
        {
            List<Pushpin> output = new List<Pushpin>();
            foreach (var elem in Sensors)
            {
                Pushpin pin = new Pushpin();
                pin.Tag = new PushpinMetadata { Type = elem.GetType().Name, Name = elem.Name, Location = elem.Location, Id = elem.Id };
                pin.ToolTip = elem.GetToolTip();
                pin.Location = new Location(elem.Location.Latitude, elem.Location.Longitude);
                output.Add(pin);
            }
            return output;
        }

		public async Task getValuesForAllSensorsFromAPI()
		{
			foreach(var sensor in Sensors)
                await UpdadeSensor(sensor);
		}

        public async Task UpdadeSensor(Sensor sensor)
        {
            ApiOutput apiOutput = await ApiConnector.getCurrentValue(sensorTypeForAPICall(sensor));
            sensor.DataAsString = apiOutput.Value.ToString();
            sensor.ConvertValueString();
        }

		private string sensorTypeForAPICall(Sensor sensor)
		{
			if (sensor is DoorSensor)
				return "window";
			if (sensor is ElPowerSensor)
				return "electric power";
			if (sensor is HumiditySensor)
				return "humidity";
			if (sensor is NoiseSensor)
				return "noise";
			if (sensor is TemperatureSensor)
				return "temperature";
			return "invalid";
		}
        
        public List<Sensor> GetOutOfBoundsSensors()
        {
            List<Sensor> OBSensors = new List<Sensor>();
            foreach (var s in Sensors)
                if (s.OutOfBounds())
                    OBSensors.Add(s);

            return OBSensors;
        }
        
        #endregion
    }
}
