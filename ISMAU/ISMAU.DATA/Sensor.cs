using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISMAU.DATA
{
    public class Location
    {
        public Location(double latitude, double longtitude)
        {
            Latitude = latitude;
            Longtitude = longtitude;
        }

        public Location() : this(0d, 0d)
        {
        }

        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }

    public class RangeBoundaries<T>
        where T : struct
    {
        public T Min { get; set; }
        public T Max { get; set; }
    }

    public abstract class Sensor<T>
        where T : struct
    {
        public string name;
        public string description;

        public string Name
        {
            get => default;
            set => name = value != null ? value : "NoName";
        }
        public string Description
        {
            get => default;
            set => description = value != null ? value : "NoDescription";
        }
        public int PollingInterval { get; set; }
        public Location Location { get; set; }
        public RangeBoundaries<T> AcceptableRange { get; set; }
        public float TickOff { get; set; }


        public abstract T GetData();
    }
}
