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

		public MapPage(SensorLogic sensorLogic, Action<ulong> modifySensorDelegate)
		{
			InitializeComponent();
			ModifySensorDelegate = modifySensorDelegate;

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

	}
}
