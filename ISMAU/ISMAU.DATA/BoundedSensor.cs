using System;

namespace ISMAU.DATA
{
    public class RangeBoundaries<T>
    {
        public T Min { get; set; }
        public T Max { get; set; }
    }


    public abstract class BoundedSensor<T> : Sensor
        where T: IComparable
    {
        private RangeBoundaries<T> boundaries;
        public T Data;

        public BoundedSensor(SensorData data, RangeBoundaries<T> rangeBoundaries)
            : base(data)
        {
            Boundaries = rangeBoundaries;
        }

        public BoundedSensor() : base()
        {
            Boundaries = new RangeBoundaries<T>();
        }

        public BoundedSensor(BoundedSensor<T> sensor) : base(sensor)
        {
            Boundaries.Min = sensor.Boundaries.Min;
            Boundaries.Max = sensor.Boundaries.Max;
        }

        public RangeBoundaries<T> Boundaries
        {
            get { return boundaries; }
            set
            {
                boundaries = new RangeBoundaries<T>();
                if (value != null)
                {
                    boundaries.Max = value.Max;
                    boundaries.Min = value.Min;
                }
                else
                {
                    boundaries.Max = boundaries.Min = default;
                }
            }
        }

        public override bool OutOfBounds()
        {
            return Data.CompareTo(Boundaries.Min) < 0 || Data.CompareTo(Boundaries.Max) > 0;
        }
    }
}
