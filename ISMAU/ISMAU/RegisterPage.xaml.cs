using ISMAU.DATA;
using ISMAU.FUNCTIONALITY;
using Microsoft.Maps.MapControl.WPF;
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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    /// <remarks>
    /// This page has fields for the sensor members.
    /// Depending on the used consrtuctor, 
    /// the page will have the option to add or modify the sensor
    /// with the data in the fields
    /// </remarks>
    public partial class RegisterPage : Page
    {
        /// <summary>
        /// Refference to the sensorLogic from the MainWndow.
        /// </summary>
        private SensorLogic logic;
        /// <summary>
        /// The sensor
        /// </summary>
        private Sensor sensor;

        /// <summary>
        /// Constructor which sets the page for adding a sensor
        /// </summary>
        /// <param name="sensorLogic"></param>
        public RegisterPage(SensorLogic sensorLogic)
        {
            InitializeComponent();
            logic = sensorLogic;

            ModifyBtn.Visibility = Visibility.Hidden;
            AddBtn.IsEnabled = true;
        }

        /// <summary>
        /// Constructor which sets the page for adding a sensor,
        /// but with some already known data, which is filled in the fields
        /// </summary>
        /// <param name="sensorLogic"></param>
        /// <param name="knownData"></param>
        public RegisterPage(SensorLogic sensorLogic, SensorData knownData) : this(sensorLogic)
        {
            txtName.Text = knownData.Name;
            txtDesc.Text = knownData.Description;
            numLat.Value = knownData.Location.Latitude;
            numLong.Value = knownData.Location.Longitude;
            sensorTypeChooser.Text = knownData.Type;
            numPoll.Value = knownData.PollingInterval;
        }

        /// <summary>
        /// Constructor which sets the page for modifying a sensor
        /// </summary>
        /// <param name="sensorLogic"></param>
        /// <param name="currentSensor"></param>
        public RegisterPage(SensorLogic sensorLogic, Sensor currentSensor)
        {
            InitializeComponent();
            logic = sensorLogic;
            sensor = currentSensor;

            InitializeTextFields(sensor);

            sensorTypeChooser.IsEnabled = false;
            AddBtn.Visibility = Visibility.Hidden;
            txtName.IsEnabled = false;
        }

        /// <summary>
        /// This function adds a sensor with the data from the fields to the collection of sensors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            SensorData data = new SensorData
            {
                Name = txtName.Text,
                Description = txtDesc.Text,
                Location = new Location((double)numLat.Value, (double)numLong.Value),
                Type = sensorTypeChooser.Text,
                PollingInterval = (int)numPoll.Value
            };

            RangeBoundaries<string> boundaries = new RangeBoundaries<string>();
            boundaries.Min = numMinVal.Text;
            boundaries.Max = numMaxVal.Text;

            if (logic.AddSensor(data, boundaries))
                logic.SaveState();
        }

        /// <summary>
        /// Initializes the text fields with the sensor data
        /// </summary>
        /// <param name="sensor"></param>
        private void InitializeTextFields(Sensor sensor)
        {
            if (sensor != null)
            {
                txtName.Text = sensor.Name;
                txtDesc.Text = sensor.Description;
                numLat.Value = sensor.Location.Latitude;
                numLong.Value = sensor.Location.Longitude;
                sensorTypeChooser.Text = sensor.GetType().Name;
                numPoll.Value = sensor.PollingInterval;
                if (sensor is BoundedSensor<int> s)
                {
                    numMinVal.Value = s.Boundaries.Min;
                    numMaxVal.Value = s.Boundaries.Max;
                }
                else if (sensor is BoundedSensor<float> s2)
                {
                    numMinVal.Value = s2.Boundaries.Min;
                    numMaxVal.Value = s2.Boundaries.Max;
                }
                else if (sensor is BoundedSensor<double> s3)
                {
                    numMinVal.Value = s3.Boundaries.Min;
                    numMaxVal.Value = s3.Boundaries.Max;
                }
            }
        }

        /// <summary>
        /// This function modifies the sensor with the new data from the fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            SensorData data = new SensorData
            {
                Description = txtDesc.Text,
                Location = new Location(numLat.Value ?? default(double), numLong.Value ?? default(double)),
                PollingInterval = Int32.Parse(numPoll.Text)
            };

            logic.ModifySensor(sensor, data, numMinVal.Text, numMaxVal.Text);
            logic.SaveState();
        }


        //      private string trimSpace(string input)
        //{
        //	string output = "";
        //	for (int i = 0; i < input.Length; ++i)
        //		if (input[i] != ' ')
        //			output += input[i];
        //	return output;
        //}
    }
}

