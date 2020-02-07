using ISMAU.FUNCTIONALITY;
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
using Microsoft.Maps.MapControl.WPF;

namespace ISMAU
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		private SensorLogic sensorLogic;


		//test data will be deleted
		string[] sensorTypes = { "temperature", "humidity", "electric power", "window", "noise" };
		const int numberSensorTypes = 5;
		int currSensorType;

        public MainWindow()
        {
			InitializeComponent();

			pageWindow.Content = new HomePage();

			sensorLogic = new SensorLogic();

			sensorLogic.AddSensor("s1", "this is sensor 1", new Location(101d, 18d), 5f, "DoorSensor", "1", "5");
			sensorLogic.AddSensor("s2", "this is sensor 2", new Location(12d, 100d), 5f, "ElPowerSensor", "2", "4");
			sensorLogic.AddSensor("s3", "this is sensor 3", new Location(13d, 18d), 5f, "HumiditySensor", "3", "3");
			sensorLogic.AddSensor("s4", "this is sensor 4", new Location(14d, 18d), 5f, "NoiseSensor", "4", "2");
			sensorLogic.AddSensor("s5", "this is sensor 5", new Location(15d, 18d), 5f, "TemperatureSensor", "5", "1");

			sensorLogic.SaveState();

			sensorLogic = null;

			sensorLogic = new SensorLogic();

			//delete later
			currSensorType = 0;
		}

		private void btnHomePage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new HomePage();
		}

		private void btnRegisterPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new RegistryPage();
		}

		private void btnModifyPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new ModifyPage();
		}

		private void btnListPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new ListPage(sensorLogic);
		}

		private void btnViewPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new ViewPage();
		}

		private void btnMapPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new MapPage(sensorLogic);
		}

		private void btnReportPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new ReportPage();
		}

		//delete in final version
		private async void btnTest_Click(object sender, RoutedEventArgs e)
		{
			ApiOutput output = await ApiConnector.getCurrentValue(sensorTypes[currSensorType]);
			if (output == null)
				return;
			//txtTest.Text = output.TimeStamp + " " + output.Value;
			if (currSensorType < numberSensorTypes - 1)
				++currSensorType;
			else
				currSensorType = 0;
		}
	}
}
