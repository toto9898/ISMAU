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
    public class RangeBoundaries<T>
        where T : struct
    {
        public T Min { get; set; }
        public T Max { get; set; }
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
        public string dataAsString;

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

		public string Type { get; set; }

		public string DataAsString { get => dataAsString; set => dataAsString = (value == null || value == "" ? "UnDef" : value); }

        public Sensor(
            string name,
            string description, 
            Location location, 
            float tickOff,
			string type,
			int pollingInterval = 1000)
        {
            Name = name;
            Description = description;
            Location = location;
            TickOff = tickOff;
            PollingInterval = pollingInterval;
			Type = type;
            DataAsString = "UnDef";
        }
        public Sensor() : this(string.Empty, string.Empty, new Location(), 0f, "Invalid")
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

        public abstract ToolTip GetData();

		public abstract void ConvertValueString();
    }
}
