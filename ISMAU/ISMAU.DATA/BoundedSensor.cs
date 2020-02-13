using System;

namespace ISMAU.DATA
{
    /// <summary>
    /// This class is used to hold the acceptable range for a sensor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RangeBoundaries<T>
    {
        public RangeBoundaries()
        {
            Min = default;
            Max = default;
        }
        public RangeBoundaries(T min, T max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// The lower bound
        /// </summary>
        public T Min { get; set; }
        /// <summary>
        /// The upper bound
        /// </summary>
        public T Max { get; set; }
    }

    /// <summary>
    /// This class represents the sensors with:
    /// - data of type T
    /// - have bounds and can have value outside it's bounds
    /// </summary>
    /// <remarks>
    /// The type T must implement IComparable interface
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public abstract class BoundedSensor<T> : Sensor
        where T: IComparable
    {
        private RangeBoundaries<T> boundaries;
        /// <summary>
        /// Holds the data of the sensor
        /// </summary>
        public T Data;

        /// <summary>
        /// This constructor sets the boundaries
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rangeBoundaries"></param>
        public BoundedSensor(SensorData data, RangeBoundaries<T> rangeBoundaries)
            : base(data)
        {
            Boundaries = rangeBoundaries;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BoundedSensor() : base()
        {
            Boundaries = new RangeBoundaries<T>();
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="sensor"></param>
        public BoundedSensor(BoundedSensor<T> sensor) : base(sensor)
        {
            Boundaries.Min = sensor.Boundaries.Min;
            Boundaries.Max = sensor.Boundaries.Max;
        }

        /// <summary>
        /// Boundaries property
        /// </summary>
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
