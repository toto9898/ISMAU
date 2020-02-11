using ISMAU.DATA;
using ISMAU.FUNCTIONALITY;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ISMAU
{
	/// <summary>
	/// Interaction logic for MapPage.xaml
	/// </summary>
	public partial class MapPage : Page
	{

		public List<Pushpin> pushpins;
		Action<ulong> ModifySensorDelegate;
		Action<SensorData> AddSensorDelegate;

		public MapPage(SensorLogic sensorLogic, Action<SensorData> addSensorDelegate, Action<ulong> modifySensorDelegate)
		{
			InitializeComponent();

			locationMap.MouseDoubleClick += AddSensorOnClick;

			ModifySensorDelegate = modifySensorDelegate;
			AddSensorDelegate = addSensorDelegate;

			pushpins = sensorLogic.initializePins();

			Binding binding = new Binding();
			binding.Source = locationMap;
			binding.Path = new PropertyPath("Heading");
			binding.Mode = BindingMode.TwoWay;

			foreach (var pin in pushpins)
			{
				pin.SetBinding(Pushpin.HeadingProperty, binding);
				pin.MouseDown += ModifyPinClicked;
				locationMap.Children.Add(pin);
			}
		}

		public void ModifyPinClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Pushpin p = sender as Pushpin;
			PushpinMetadata metadata = (PushpinMetadata)p.Tag;

			ModifySensorDelegate(metadata.Id);
		}

		public void AddSensorOnClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Map map = sender as Map;

			Point mousePosition = e.GetPosition(map);
			Location mouseGeocode = map.ViewportPointToLocation(mousePosition);

			AddSensorDelegate(new SensorData { Location = mouseGeocode });
		}

	}
}
