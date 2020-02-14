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
    /// Interaction logic for DoorDataVisualization.xaml
    /// </summary>
    public partial class DoorDataVisualization : UserControl
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
        public DoorDataVisualization()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Initializes the members of the class and the meter's data
        /// </summary>
        /// <param name="sensor"></param>
        /// <param name="update"></param>
        public void InitializeData(DoorSensor sensor, Func<Sensor, Task> update)
        {
            updadeSensor = update;
            StartTimer(sensor);
        }

        /// <summary>
        /// Starts a Timer for the updating of the sensor data 
        /// </summary>
        /// <param name="sensor"></param>
        private void StartTimer(DoorSensor sensor)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(sensor.PollingInterval);

            Timer = new System.Threading.Timer(
                async (e) =>
                {
                    await updadeSensor(sensor);
                    Application.Current.Dispatcher.Invoke(() => SetLightIndicator(sensor));
                },
                null,
                startTimeSpan, periodTimeSpan);
        }

        /// <summary>
        /// Sets the right light according to the state of the sensor
        /// </summary>
        /// <param name="sensor"></param>
        private void SetLightIndicator(DoorSensor sensor)
        {
            if (sensor.IsClosed)
            {
                RedRect.Visibility = Visibility.Visible;
                GreenRect.Visibility = Visibility.Hidden;
            }
            else
            {
                RedRect.Visibility = Visibility.Hidden;
                GreenRect.Visibility = Visibility.Visible;
            }
        }
    }
}
