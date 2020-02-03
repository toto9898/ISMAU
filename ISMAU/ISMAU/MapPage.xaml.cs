using ISMAU.FUNCTIONALITY;
using Microsoft.Maps.MapControl.WPF;
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

		public MapPage(SensorLogic sensorLogic)
		{
			InitializeComponent();
			pushpins = sensorLogic.initializePins();

			Binding binding = new Binding();
			binding.Source = locationMap;
			binding.Path = new PropertyPath("Heading");
			binding.Mode = BindingMode.OneWay;

			foreach (var pin in pushpins)
			{
				pin.SetBinding(Pushpin.HeadingProperty, binding);

				locationMap.Children.Add(pin);
			}
		}
	}
}
