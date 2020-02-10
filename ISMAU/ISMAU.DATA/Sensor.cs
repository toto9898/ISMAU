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
    public class SensorData
    {
        public string Name = string.Empty;
        public string Description = string.Empty;
        public Location Location = new Location();
        public float TickOff = 0f;
        public string Type = string.Empty;
        public int PollingInterval = 1000;
    }


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
        private static UInt64 sensorCounter = 0;

        private readonly UInt64 id;

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

        public float TickOff { get; set; }

        public int PollingInterval { get; set; } = 1000;

        public ulong Id => id;
        public string DataAsString { get; set; }

        public Sensor(SensorData data)
        {
            id = sensorCounter++;
            Name = data.Name;
            Description = data.Description;
            Location = data.Location;
            TickOff = data.TickOff;
            PollingInterval = data.PollingInterval;
        }
        public Sensor()
            : this(new SensorData())
        {

        }
        public Sensor(Sensor sensor)
        {
            Name = sensor.Name;
            Description = sensor.Description;
            Location = sensor.Location;
            TickOff = sensor.TickOff;
            PollingInterval = sensor.PollingInterval;
        }

        public abstract void GetData();

        public ToolTip GetToolTip()
        {
            ToolTip tip = new ToolTip();
            string content = "Type: " + GetType().Name;
            content += string.Format("\nName: {0}\nLatitude: {1},  Longitude: {2}", Name, Location.Latitude, Location.Longitude);
            tip.Content = content;
            return tip;
        }

        public abstract void ConvertValueString();
    }
}
