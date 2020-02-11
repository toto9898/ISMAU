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

			//delete later
			currSensorType = 0;
		}

		private void btnHomePage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new HomePage();
		}

		private void btnRegisterPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new RegistryPage(sensorLogic);
		}

		private	async void btnListPage_Click(object sender, RoutedEventArgs e)
		{
			await sensorLogic.getValuesForAllSensorsFromAPI();
			pageWindow.Content = new ListPage(
							sensorLogic.Sensors,
							id => OpenDetails(id),
							id => OpenEdit(id));
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
			pageWindow.Content = new ListPage(
				sensorLogic.GetOutOfBoundsSensors(),
				id => OpenDetails(id),
				id => OpenEdit(id));
		}

		public void OpenDetails(ulong id)
		{
			pageWindow.Content = new RegistryPage(sensorLogic);
		}

		public void OpenEdit(ulong id)
		{
			pageWindow.Content = new RegistryPage(sensorLogic, sensorLogic.Sensors.Find(s => s.Id == id));
		}
	}
}
