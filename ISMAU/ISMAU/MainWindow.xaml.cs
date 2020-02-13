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
	/// <remarks>
	/// This class is responsible for the navigation through he paegs
	/// </remarks>
    public partial class MainWindow : Window
    {
		/// <summary>
		/// Holds the functionality logic for the sensors
		/// </summary>
		private SensorLogic sensorLogic;

		/// <summary>
		/// Initializes the sensorLogic and creates the home page
		/// </summary>
        public MainWindow()
        {
			InitializeComponent();

			sensorLogic = new SensorLogic();
			
			pageWindow.Content = new HomePage( 
				new MapPage(
				sensorLogic,
				OpenDetails,
				id => OpenEdit(sensorLogic.Sensors.Find(sensor => sensor.Id == id)))
				);

		}

		/// <summary>
		/// Switches to the home page when its button os clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnHomePage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new HomePage(
				new MapPage(
				sensorLogic,
				OpenDetails,
				id => OpenEdit(sensorLogic.Sensors.Find(sensor => sensor.Id == id)))
				);
		}

		/// <summary>
		/// Switches to the regster page when its button os clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRegisterPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new RegisterPage(sensorLogic);
		}

		/// <summary>
		/// Switches to the list page when its button os clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void btnListPage_Click(object sender, RoutedEventArgs e)
		{
			await sensorLogic.getValuesForAllSensorsFromAPI();
			pageWindow.Content = new ListPage(
				sensorLogic.Sensors,
				OpenDetails,
				OpenEdit);
		}

		/// <summary>
		/// Switches to the map page when its button os clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnMapPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new MapPage(
				sensorLogic, 
				OpenDetails, 
				id => OpenEdit(sensorLogic.Sensors.Find(sensor => sensor.Id == id)));
		}

		/// <summary>
		/// Switches to the report page when its button os clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReportPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new ListPage(
				sensorLogic.GetOutOfBoundsSensors(),
				OpenDetails,
				OpenEdit);
		}

		/// <summary>
		/// Switches to the view page of the sensor
		/// </summary>
		/// <param name="sensor"></param>
		public void OpenDetails(Sensor sensor)
		{
			pageWindow.Content = new ViewPage(sensor, sensorLogic.UpdadeSensor);
		}

		/// <summary>
		/// Switches to the register page in "adding sensor" mode
		/// </summary>
		/// <param name="data"></param>
		public void OpenDetails(SensorData data)
		{
			pageWindow.Content = new RegisterPage(sensorLogic, data);
		}

		/// <summary>
		/// Switches to the register page in "modifying sensor" mode
		/// </summary>
		/// <param name="sensor"></param>
		public void OpenEdit(Sensor sensor)
		{
			pageWindow.Content = new RegisterPage(sensorLogic, sensor);
		}
	}
}
