using ISMAU.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ISMAU
{
    /// <summary>
    /// Interaction logic for BasicMeter.xaml
    /// </summary>
    public partial class BasicMeter : UserControl
    {
        /// <summary>
        /// The function which will call the API and update the data the sensor passed
        /// </summary>
        private Func<Sensor, Task> updadeSensor;

        /// <summary>
        /// Timer for refreshing the sensor's data
        /// </summary>
        public Timer Timer { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasicMeter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the members of the class and the meter's data
        /// </summary>
        /// <param name="sensor"></param>
        /// <param name="update"></param>
        public void InitializeMeter(Sensor sensor, Func<Sensor, Task> update)
        {
            updadeSensor = update;

            SetDataType(sensor);

            SetBoundaries(sensor);
            StartTimer(sensor);
        }

        /// <summary>
        /// Sets the bounds of the gauge with the sensor Boundaries
        /// </summary>
        private void SetBoundaries(Sensor sensor)
        {
            if (sensor is BoundedSensor<int> s1)
            {
                scale.Min = s1.Boundaries.Min;
                scale.Max = s1.Boundaries.Max;
            }
            if (sensor is BoundedSensor<float> s2)
            {
                scale.Min = s2.Boundaries.Min;
                scale.Max = s2.Boundaries.Max;
            }
            if (sensor is BoundedSensor<double> s3)
            {
                scale.Min = s3.Boundaries.Min;
                scale.Max = s3.Boundaries.Max;
            }
        }

        /// <summary>
        /// Sets the meter needle value to the sensor data
        /// </summary>
        /// <param name="sensor"></param>
        private void SetNeedleData(Sensor sensor)
        {
            if (sensor is BoundedSensor<int> s1)
            {
                if (s1.OutOfBounds())
                    needle.Value = Convert.ToDouble(s1.Boundaries.Max) + (Convert.ToDouble(s1.Boundaries.Max) - Convert.ToDouble(s1.Boundaries.Min)) / 10;
                else
                    needle.Value = Convert.ToDouble(s1.Data);
            }
            if (sensor is BoundedSensor<float> s2)
            {
                if (s2.OutOfBounds())
                    needle.Value = Convert.ToDouble(s2.Boundaries.Max) + (Convert.ToDouble(s2.Boundaries.Max) - Convert.ToDouble(s2.Boundaries.Min)) / 10;
                else
                    needle.Value = Convert.ToDouble(s2.Data);
            }
            if (sensor is BoundedSensor<double> s3)
            {
                if (s3.OutOfBounds())
                    needle.Value = Convert.ToDouble(s3.Boundaries.Max) + (Convert.ToDouble(s3.Boundaries.Max) - Convert.ToDouble(s3.Boundaries.Min)) / 10;
                else
                    needle.Value = Convert.ToDouble(s3.Data);
            }
        }


        /// <summary>
        /// Starts a Timer for the updating of the sensor data 
        /// </summary>
        /// <param name="sensor"></param>
        private void StartTimer(Sensor sensor)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(sensor.PollingInterval);

            Timer = new System.Threading.Timer(
                async (e) =>
                {
                    await updadeSensor(sensor);
                    Application.Current.Dispatcher.Invoke(() => ValueLbl.Content = sensor.DataAsString);
                    Application.Current.Dispatcher.Invoke(() => SetNeedleData(sensor));
                },
                null,
                startTimeSpan, periodTimeSpan);
        }

        /// <summary>
        /// Initializing the type of measurement
        /// </summary>
        /// <param name="sensor"></param>
        private void SetDataType(Sensor sensor)
        {
            if (sensor is ElPowerSensor sensor1)
                DataTypeLbl.Content = nameof(sensor1.Watts);
            if (sensor is HumiditySensor sensor2)
                DataTypeLbl.Content = nameof(sensor2.Humidity);
            if (sensor is NoiseSensor sensor3)
                DataTypeLbl.Content = nameof(sensor3.Decibels);
            if (sensor is TemperatureSensor sensor4)
                DataTypeLbl.Content = nameof(sensor4.Celsius);
        }

    }
}
