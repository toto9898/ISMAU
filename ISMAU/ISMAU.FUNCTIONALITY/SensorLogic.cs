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
    /// <summary>
    /// Holds the data to initialize a pushpin
    /// </summary>
    public struct PushpinMetadata
    {
        public string Type;
        public string Name;
        public Location Location;
        public ulong Id;
    }


    /// <summary>
    /// This class is handles the logic with the sensors
    /// </summary>
    public class SensorLogic : ViewModelBase
    {

        #region Data Members
        private const string DATABASE_NAME = @"\data.xml";
        private string databasePath;
        #endregion

        #region Constructors
        /// <summary>
        /// This constructor deserializes the sensors from the save(data.xml) file
        /// </summary>
        public SensorLogic()
        {
            string current = Environment.CurrentDirectory;
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
        /// <summary>
        /// Holds the name of the save file
        /// </summary>
        public string DatabaseName
        {
            get { return DATABASE_NAME; }
            private set { }
        }

        /// <summary>
        /// Holds the path to the save file
        /// </summary>
        public string DatabasePath
        {
            get { return databasePath; }
            private set { }
        }

        /// <summary>
        /// Holds all the sensors
        /// </summary>
        public List<Sensor> Sensors { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Serializes the sensors
        /// </summary>
        public void SaveState()
        {
            Sensors.Serialize(databasePath);
        }

        /// <summary>
        /// Adds sensor with the given "data" to the Sensors List
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rangeBoundaries"></param>
        /// <returns>True if the sensor is added successfully, False otherwise</returns>
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

        /// <summary>
        /// Creates pushpins with data from the sensors
        /// </summary>
        /// <returns>Collections of pushpins of the sensors</returns>
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

        /// <summary>
        /// Updates the data of the sensors with current data, received from the API
        /// </summary>
        /// <returns></returns>
		public async Task getValuesForAllSensorsFromAPI()
        {
            foreach (var sensor in Sensors)
                await UpdadeSensor(sensor);
        }

        /// <summary>
        /// Updates the data of the sensor with current data, received from the API
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns></returns>
        public async Task UpdadeSensor(Sensor sensor)
        {
            try
            {
                ApiOutput apiOutput = await ApiConnector.getCurrentValue(sensorTypeForAPICall(sensor));
                sensor.DataAsString = apiOutput.Value.ToString();
                sensor.ConvertValueString();
            }
            catch (Exception)
            {
                MessageBox.Show("Exception occured!");
            }
        }

        /// <summary>
        /// Returns string representing the sensor type
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns>String representing the sensor type</returns>
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

        /// <summary>
        /// Returns List with only the sensors which are out of bounds
        /// </summary>
        /// <returns>List with only the sensors which are out of bounds</returns>
        public List<Sensor> GetOutOfBoundsSensors()
        {
            List<Sensor> OBSensors = new List<Sensor>();
            foreach (var s in Sensors)
                if (s.TickOff && s.OutOfBounds())
                    OBSensors.Add(s);

            return OBSensors;
        }

        /// <summary>
        /// Modifies a sensor's data with the given data
        /// </summary>
        /// <param name="sensor">The sensor to be modified</param>
        /// <param name="data">The new data</param>
        /// <param name="minVal">The new minimal bounary value</param>
        /// <param name="maxVal">The new maximal bounary value</param>
        public void ModifySensor(Sensor sensor, SensorData data, RangeBoundaries<string> rangeBoundaries)
        {
            sensor.Description = data.Description;
            sensor.Location = data.Location;
            sensor.PollingInterval = data.PollingInterval;

            rangeBoundaries.Min = rangeBoundaries.Min.Trim();
            rangeBoundaries.Max = rangeBoundaries.Max.Trim();
            if (rangeBoundaries.Min[0] == '-' && rangeBoundaries.Min[1] == ' ')
                rangeBoundaries.Min = rangeBoundaries.Min.Remove(1, 1);
            if (rangeBoundaries.Max[0] == '-' && rangeBoundaries.Max[1] == ' ')
                rangeBoundaries.Max = rangeBoundaries.Max.Remove(1, 1);

            if (sensor is BoundedSensor<int> s)
            {
                s.Boundaries = new RangeBoundaries<int>
                (
                    int.Parse(rangeBoundaries.Min),
                    int.Parse(rangeBoundaries.Max)
                );
            }
            else if (sensor is BoundedSensor<float> s2)
            {
                s2.Boundaries = new RangeBoundaries<float>
                (
                    float.Parse(rangeBoundaries.Min),
                    float.Parse(rangeBoundaries.Max)
                );
            }
            else if (sensor is BoundedSensor<double> s3)
            {
                s3.Boundaries = new RangeBoundaries<double>
                (
                    double.Parse(rangeBoundaries.Min),
                    double.Parse(rangeBoundaries.Max)
                );
            }
        }

        #endregion
    }
}
