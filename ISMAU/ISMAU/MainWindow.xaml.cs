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

		private void btnHomePage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new HomePage(
				new MapPage(
				sensorLogic,
				OpenDetails,
				id => OpenEdit(sensorLogic.Sensors.Find(sensor => sensor.Id == id)))
				);
		}

		private void btnRegisterPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new RegisterPage(sensorLogic);
		}

		private	async void btnListPage_Click(object sender, RoutedEventArgs e)
		{
			await sensorLogic.getValuesForAllSensorsFromAPI();
			pageWindow.Content = new ListPage(
				sensorLogic.Sensors,
				OpenDetails,
				OpenEdit);
		}

		private void btnMapPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new MapPage(
				sensorLogic, 
				OpenDetails, 
				id => OpenEdit(sensorLogic.Sensors.Find(sensor => sensor.Id == id)));
		}

		private void btnReportPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new ListPage(
				sensorLogic.GetOutOfBoundsSensors(),
				OpenDetails,
				OpenEdit);
		}

		public void OpenDetails(Sensor sensor)
		{
			pageWindow.Content = new ViewPage(sensor, sensorLogic.UpdadeSensor);
		}

		public void OpenDetails(SensorData data)
		{
			pageWindow.Content = new RegisterPage(sensorLogic, data);
		}

		public void OpenEdit(Sensor sensor)
		{
			pageWindow.Content = new RegisterPage(sensorLogic, sensor);
		}
	}
}
