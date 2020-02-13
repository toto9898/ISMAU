using ISMAU.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// The sensor for visualization
        /// </summary>
        public Sensor Sensor { get; set; }

        private System.Threading.Timer timer;

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
            Sensor = sensor;
            updadeSensor = update;


            ValueLbl.Content = 100;

            SetBoundaries();

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(sensor.PollingInterval);

            timer = new System.Threading.Timer(
                async (e) =>
                {
                    await updadeSensor(sensor);
                    Application.Current.Dispatcher.Invoke(() => ValueLbl.Content = sensor.DataAsString);
                    Application.Current.Dispatcher.Invoke(SetNeedleData);
                },
                null,
                startTimeSpan, periodTimeSpan);

        }

        /// <summary>
        /// Sets the bounds of the gauge with the sensor Boundaries
        /// </summary>
        private void SetBoundaries()
        {
            if (Sensor is ElPowerSensor e)
            {
                scale.Min = e.Boundaries.Min;
                scale.Max = e.Boundaries.Max;
            }
            if (Sensor is HumiditySensor h)
            {
                scale.Min = h.Boundaries.Min;
                scale.Max = h.Boundaries.Max;
            }
            if (Sensor is NoiseSensor n)
            {
                scale.Min = n.Boundaries.Min;
                scale.Max = n.Boundaries.Max;
            }
            if (Sensor is TemperatureSensor t)
            {
                scale.Min = t.Boundaries.Min;
                scale.Max = t.Boundaries.Max;
            }
        }

        private void SetNeedleData()
        {
            if (Sensor is ElPowerSensor e)
                needle.Value = e.Data;
            else if (Sensor is HumiditySensor h)
                needle.Value = h.Data;
            else if (Sensor is NoiseSensor n)
                needle.Value = n.Data;
            else if (Sensor is TemperatureSensor t)
                needle.Value = t.Data;
        }

    }
}
