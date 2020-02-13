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
		/// <summary>
		/// Collection of the pushpins for the map
		/// </summary>
		public List<Pushpin> pushpins;
		/// <summary>
		/// Delegate for navigating to the register page in "modify sensor" mode
		/// </summary>
		Action<ulong> ModifySensorDelegate;
		/// <summary>
		/// Delegate for navigating to the register page in "add sensor" mode
		/// </summary>
		Action<SensorData> AddSensorDelegate;

		/// <summary>
		/// - Initializeng the delegates, 
		/// - Adding a function to the map double click event, for adding a new sensor,
		/// - Binding the pushpins with the map
		/// </summary>
		/// <param name="sensorLogic"></param>
		/// <param name="addSensorDelegate"></param>
		/// <param name="modifySensorDelegate"></param>
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

		/// <summary>
		/// Switches to the register page in "modifying sensor" mode when a pushpin is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ModifyPinClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Pushpin p = sender as Pushpin;
			PushpinMetadata metadata = (PushpinMetadata)p.Tag;

			ModifySensorDelegate(metadata.Id);
		}

		/// <summary>
		/// Switches to the register page in "adding sensor" mode when a pushpin is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void AddSensorOnClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Map map = sender as Map;

			Point mousePosition = e.GetPosition(map);
			Location mouseGeocode = map.ViewportPointToLocation(mousePosition);

			AddSensorDelegate(new SensorData { Location = mouseGeocode });
		}

	}
}
