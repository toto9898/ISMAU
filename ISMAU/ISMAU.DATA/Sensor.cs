using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;
using Microsoft.Maps.MapControl.WPF;
using Telerik.Windows.Controls;

namespace ISMAU.DATA
{
    /// <summary>
    /// This class holds the data which a sensor can have
    /// </summary>
    public class SensorData
    {
        public string Name = string.Empty;
        public string Description = string.Empty;
        public Location Location = new Location();
        public bool TickOff = false;
        public string Type = string.Empty;
        public int PollingInterval = 1000;
    }

    /// <summary>
    /// This class represents a sensor and it's data
    /// </summary>
    /// <remarks>
    /// Each sensor has:
    /// - Id
    /// - Name
    /// - Description
    /// - Location
    /// - Polling interval (the time between each refresh of the data)
    /// </remarks>
    [Serializable]
    [XmlInclude(typeof(DoorSensor))]
    [XmlInclude(typeof(ElPowerSensor))]
    [XmlInclude(typeof(HumiditySensor))]
    [XmlInclude(typeof(NoiseSensor))]
    [XmlInclude(typeof(TemperatureSensor))]
    public abstract class Sensor : ViewModelBase
    {
        private string name;
        private string description;
        private static ulong sensorCounter = 0;

        public string Name
        {
            get => name;
            set => name = value ?? "NoName";
        }
        public string Description
        {
            get => description;
            set => description = value ?? "NoDescription";
        }
        public Location Location { get; set; }

        public bool TickOff { get; set; }

        /// <summary>
        /// The time between each refresh of the sensor's data
        /// </summary>
        public int PollingInterval { get; set; } = 1;

        public ulong Id { get; }
        public string DataAsString { get; set; }

        /// <summary>
        /// This constructor initializes the sensor's data
        /// </summary>
        /// <param name="data"></param>
        public Sensor(SensorData data)
        {
            Id = sensorCounter++;
            Name = data.Name;
            Description = data.Description;
            Location = data.Location;
            TickOff = data.TickOff;
            PollingInterval = data.PollingInterval;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Sensor()
            : this(new SensorData())
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="sensor"></param>
        public Sensor(Sensor sensor)
        {
            Name = sensor.Name;
            Description = sensor.Description;
            Location = sensor.Location;
            TickOff = sensor.TickOff;
            PollingInterval = sensor.PollingInterval;
        }

        public abstract void GetData();

        /// <summary>
        /// Gets the sensors data as "SensorData" object
        /// </summary>
        /// <returns>The sensors data as "SensorData" object</returns>
        public SensorData GetSensorData()
        {
            SensorData data = new SensorData();

            data.Name = Name;
            data.Description = Description;
            data.Location = Location;
            data.TickOff = TickOff;
            data.Type = GetType().Name;
            data.PollingInterval = PollingInterval;

            return data;
        }

        /// <summary>
        /// Sets the sensors data to "data"
        /// </summary>
        /// <param name="data">The new data</param>
        public void SetSensorData(SensorData data)
        {
            Name = data.Name;
            Description = data.Description;
            Location = data.Location;
            TickOff = data.TickOff;
            PollingInterval = data.PollingInterval;
        }

        /// <summary>
        /// Creates a ToolTip with data from the sensor
        /// </summary>
        /// <returns>A ToolTip with data from the sensor</returns>
        public ToolTip GetToolTip()
        {
            ToolTip tip = new ToolTip();
            string content = "Type: " + GetType().Name;
            content += string.Format("\nName: {0}\nLatitude: {1},  Longitude: {2}", Name, Location.Latitude, Location.Longitude);
            tip.Content = content;
            return tip;
        }

        /// <summary>
        /// Sets the value of the sensor, using the DataAsAsString property
        /// </summary>
        public abstract void ConvertValueString();

        /// <summary>
        /// Checks if the sensor is out of bounds
        /// </summary>
        /// <returns>
        ///  - true if the sensor data is out of the boundaries
        ///  - false if the sensor data is not out of the boundaries
        /// </returns>
        public abstract bool OutOfBounds();
    }
}
