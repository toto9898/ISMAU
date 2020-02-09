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
	/// Interaction logic for RegistryPage.xaml
	/// </summary>
	public partial class RegistryPage : Page
	{
		private SensorLogic logic;

		public RegistryPage(SensorLogic sensorLogic)
		{
			InitializeComponent();
			logic = sensorLogic;
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			string name = txtName.Text;
			string description = txtDesc.Text;
			Location location = new Location();
			location.Latitude = double.Parse(trimSpace(numLat.Text));
			location.Longitude = double.Parse(trimSpace(numLong.Text));
			float tickOff = 0;
			string type = sensorTypeChooser.Text;
			string boundariesMin = trimSpace(trimSpace(numMinVal.Text));
			string boundariesMax = trimSpace(numMaxVal.Text);
			int pollingInterval = Int32.Parse(trimSpace(numPoll.Text));
			if (logic.AddSensor(name, description, location, tickOff, type, boundariesMin, boundariesMax, pollingInterval))
				logic.SaveState();
		}

		private string trimSpace(string input)
		{
			string output = "";
			for (int i = 0; i < input.Length; ++i)
				if (input[i] != ' ')
					output += input[i];
			return output;
		}
	}
}

