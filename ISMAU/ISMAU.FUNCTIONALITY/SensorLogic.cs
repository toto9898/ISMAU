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
            switch (data.Type)
            {
                case "DoorSensor":
                    sensor = new DoorSensor(data);
                    break;
                case "ElPowerSensor":
                case "NoiseSensor":
                    success = int.TryParse(rangeBoundaries.Min, out int min);
                    if (!success) return false;
                    success = int.TryParse(rangeBoundaries.Max, out int max);
                    if (!success) return false;

                    RangeBoundaries<int> boundaries = new RangeBoundaries<int>();
                    boundaries.Min = min;
                    boundaries.Max = max;

                    if (data.Type == "ElPowerSensor")
                        sensor = new ElPowerSensor(data, boundaries);
                    else
                        sensor = new NoiseSensor(data, boundaries);
                    break;
                case "HumiditySensor":
                    success = float.TryParse(rangeBoundaries.Min, out float min2);
                    if (!success) return false;
                    success = float.TryParse(rangeBoundaries.Min, out float max2);
                    if (!success) return false;

                    RangeBoundaries<float> boundaries2 = new RangeBoundaries<float>();
                    boundaries2.Min = min2;
                    boundaries2.Max = max2;

                    sensor = new HumiditySensor(data, boundaries2);
                    break;
                case "TemperatureSensor":
                    success = double.TryParse(rangeBoundaries.Min, out double min3);
                    if (!success) return false;
                    success = double.TryParse(rangeBoundaries.Min, out double max3);
                    if (!success) return false;

                    RangeBoundaries<double> boundaries3 = new RangeBoundaries<double>();
                    boundaries3.Min = min3;
                    boundaries3.Max = max3;

                    sensor = new TemperatureSensor(data, boundaries3);
                    break;
                default:
                    break;
            }
            Sensors.Add(sensor);

            return success;
        }

        public List<Pushpin> initializePins()
        {
            List<Pushpin> output = new List<Pushpin>();
            foreach (var elem in Sensors)
            {
                Pushpin pin = new Pushpin();
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
