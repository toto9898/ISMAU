﻿using ISMAU.FUNCTIONALITY;
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

			pageWindow.Content = new HomePage();

			sensorLogic = new SensorLogic();
		}

		private void btnHomePage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new HomePage();
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
			pageWindow.Content = new MapPage(sensorLogic, OpenEditById);
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

		public void OpenEdit(Sensor sensor)
		{
			pageWindow.Content = new RegisterPage(sensorLogic, sensor);
		}

		public void OpenEditById(ulong id)
		{
			pageWindow.Content = new RegisterPage(sensorLogic, sensorLogic.Sensors.Find(s => s.Id == id));
		}

	}
}
