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
        /// This constructor
        /// - Sets the sensor and the function for the updating of a sensor
        /// - Sets the XAML values with the sensor's data
        /// </summary>
        /// <param name="sensor"></param>
        /// <param name="update"></param>
        public ViewPage(Sensor sensor, Func<Sensor, Task> update)
        {
            InitializeComponent();

            basicMeter.InitializeMeter(sensor, update);

            InitializeLabels(sensor);
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
