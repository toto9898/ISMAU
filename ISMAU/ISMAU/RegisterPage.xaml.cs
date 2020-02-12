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
    public partial class RegisterPage : Page
    {
        private SensorLogic logic;
        private Sensor sensor;

        public RegisterPage(SensorLogic sensorLogic, Sensor currentSensor = null)
        {
            InitializeComponent();
            logic = sensorLogic;
            sensor = currentSensor;

            if (sensor != null)
            {
                InitializeTextFields(sensor);
                sensorTypeChooser.IsEnabled = false;
                AddBtn.Visibility = Visibility.Hidden;
                txtName.IsEnabled = false;
            }
            else
            {
                ModifyBtn.Visibility = Visibility.Hidden;
                AddBtn.IsEnabled = true;
            }
        }

        public RegisterPage(SensorLogic sensorLogic, SensorData knownData) : this(sensorLogic)
        {
            txtName.Value = knownData.Name;
            txtDesc.Value = knownData.Description;
            numLat.Value = knownData.Location.Latitude;
            numLong.Value = knownData.Location.Longitude;
            sensorTypeChooser.Text = knownData.Type;
            numPoll.Value = knownData.PollingInterval;
        }

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

        private void InitializeTextFields(Sensor sensor)
        {
            if (sensor != null)
            {
                txtName.Value = sensor.Name;
                txtDesc.Value = sensor.Description;
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

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            SensorData data = new SensorData
            {
                Name = txtName.Text,
                Description = txtDesc.Text,
                Location = new Location(numLat.Value ?? default(double), numLong.Value ?? default(double)),
                Type = sensorTypeChooser.Text,
                PollingInterval = Int32.Parse(numPoll.Text)
            };

            sensor = logic.Sensors.Find(sen => sen.Id == sensor.Id);
            sensor.Description = data.Description;
            sensor.Location = data.Location;
            sensor.PollingInterval = data.PollingInterval;
            if (sensor is BoundedSensor<int> s)
            {
                s.Boundaries.Min = Convert.ToInt32(numMinVal.Value);
                s.Boundaries.Max = Convert.ToInt32(numMaxVal.Value);
            }
            else if (sensor is BoundedSensor<float> s2)
            {
                s2.Boundaries.Min = (float)numMinVal.Value;
                s2.Boundaries.Max = (float)numMaxVal.Value;
            }
            else if (sensor is BoundedSensor<double> s3)
            {
                s3.Boundaries.Min = numMinVal.Value ?? default(double);
                s3.Boundaries.Max = numMaxVal.Value ?? default(double);
            }
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

