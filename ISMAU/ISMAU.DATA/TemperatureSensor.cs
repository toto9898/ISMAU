namespace ISMAU.DATA
{
    /// <summary>
    /// This class represents a temperature sensor
    /// </summary>
    public class TemperatureSensor : BoundedSensor<double>
    {
        /// <summary>
        /// Holds the decibels measured from the sensor
        /// </summary>
        public double Celsius { get => Data; set => Data = value; }

        /// <summary>
        /// Constructs the object with the given data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rangeBoundaries"></param>
        public TemperatureSensor(SensorData data, RangeBoundaries<double> rangeBoundaries)
            : base(data, rangeBoundaries)
        {
            Celsius = -239.0;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TemperatureSensor()
            : base()
        {
            Celsius = -239.0;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="sensor"></param>
        public TemperatureSensor(TemperatureSensor sensor)
            : base(sensor)
        {
            Celsius = sensor.Celsius;
        }

        public override void GetData()
        {

        }
        public override void ConvertValueString()
		{
            double temp = 0d;
			double.TryParse(DataAsString, out temp);
            Celsius = temp;
		}
	}
}
