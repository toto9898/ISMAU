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
    /// Interaction logic for ViewPage.xaml
    /// </summary>
    /// <remarks>
    /// This page shows live visual representation of a sensor
    /// </remarks>
    public partial class ViewPage : Page
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
        /// This constructor
        /// - Sets the sensor and the function for the updating of a sensor
        /// - Sets the XAML values with the sensor's data
        /// </summary>
        /// <param name="sensor"></param>
        /// <param name="update"></param>
        public ViewPage(Sensor sensor, Func<Sensor, Task> update)
        {
            InitializeComponent();
            updadeSensor = update;

            InitializeLabels(sensor);
            SetBoundaries(sensor);
            StartTimer(sensor);
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
        /// Sets the bounds of the gauge with the sensor Boundaries
        /// </summary>
        private void SetBoundaries(Sensor sensor)
        {
            if (sensor is ElPowerSensor e)
            {
                scale.Min = e.Boundaries.Min;
                scale.Max = e.Boundaries.Max;
            }
            if (sensor is HumiditySensor h)
            {
                scale.Min = h.Boundaries.Min;
                scale.Max = h.Boundaries.Max;
            }
            if (sensor is NoiseSensor n)
            {
                scale.Min = n.Boundaries.Min;
                scale.Max = n.Boundaries.Max;
            }
            if (sensor is TemperatureSensor t)
            {
                scale.Min = t.Boundaries.Min;
                scale.Max = t.Boundaries.Max;
            }
        }

        /// <summary>
        /// Sets the meter needle value to the sensor data
        /// </summary>
        /// <param name="sensor"></param>
        private void SetNeedleData(Sensor sensor)
        {
            if (sensor is ElPowerSensor e)
                needle.Value = e.Data;
            else if (sensor is HumiditySensor h)
                needle.Value = h.Data;
            else if (sensor is NoiseSensor n)
                needle.Value = n.Data;
            else if (sensor is TemperatureSensor t)
                needle.Value = t.Data;
        }

        /// <summary>
        /// Initializes the labels with the data from the sensor
        /// </summary>
        /// <param name="sensor"></param>
        private void InitializeLabels(Sensor sensor)
        {
            SensorData data = sensor.GetSensorData();

            TypeLbl.Content = data.Type;
            NameLbl.Content = data.Name;
            DescriptionLbl.Content = data.Description;
            PollingIntervalLbl.Content = data.PollingInterval;
            LongLbl.Content = data.Location.Longitude;
            LatLbl.Content = data.Location.Latitude;
        }
    }
}
