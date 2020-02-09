using ISMAU.DATA;
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
	/// Interaction logic for ModifyPage.xaml
	/// </summary>
	public partial class ModifyPage : Page
	{
		private Sensor sensorToModify;
		private SensorLogic sensorLogic;

		public ModifyPage(Sensor sensor, SensorLogic logic)
		{
			InitializeComponent();
			sensorToModify = sensor;
			sensorLogic = logic;
			initialize();
		}

		private void initialize()
		{
			sensorTypeChooser.Text = sensorToModify.Type;
			txtName.Value = sensorToModify.Name;
			txtDesc.Value = sensorToModify.Description;
			numPoll.Value = sensorToModify.PollingInterval;
			numLat.Value = sensorToModify.Location.Latitude;
			numLong.Value = sensorToModify.Location.Longitude;
			if(sensorToModify is ElPowerSensor)
			{
				numMaxVal.Value = ((ElPowerSensor)sensorToModify).SetBoundaries.Max;
				numMinVal.Value = ((ElPowerSensor)sensorToModify).SetBoundaries.Min;
			}
			if(sensorToModify is HumiditySensor)
			{
				numMaxVal.Value = ((HumiditySensor)sensorToModify).SetBoundaries.Max;
				numMinVal.Value = ((HumiditySensor)sensorToModify).SetBoundaries.Min;
			}
			if (sensorToModify is NoiseSensor)
			{
				numMaxVal.Value = ((NoiseSensor)sensorToModify).SetBoundaries.Max;
				numMinVal.Value = ((NoiseSensor)sensorToModify).SetBoundaries.Min;
			}
			if (sensorToModify is TemperatureSensor)
			{
				numMaxVal.Value = ((TemperatureSensor)sensorToModify).SetBoundaries.Max;
				numMinVal.Value = ((TemperatureSensor)sensorToModify).SetBoundaries.Min;
			}
		}

		private void Modify_Click(object sender, RoutedEventArgs e)
		{
			sensorToModify.Name = txtName.Text;
			sensorToModify.Description = txtDesc.Text;
			sensorToModify.PollingInterval = Int32.Parse(numPoll.Text);
			sensorToModify.Location.Latitude = double.Parse(numLat.Text);
			sensorToModify.Location.Longitude = double.Parse(numLong.Text);
			if (sensorToModify is ElPowerSensor)
			{
				((ElPowerSensor)sensorToModify).SetBoundaries.Max = Int32.Parse(numMaxVal.Text);
				((ElPowerSensor)sensorToModify).SetBoundaries.Min = Int32.Parse(numMinVal.Text);
			}
			if (sensorToModify is HumiditySensor)
			{
				((HumiditySensor)sensorToModify).SetBoundaries.Max = float.Parse(numMaxVal.Text);
				((HumiditySensor)sensorToModify).SetBoundaries.Min = float.Parse(numMinVal.Text);
			}
			if (sensorToModify is NoiseSensor)
			{
				((NoiseSensor)sensorToModify).SetBoundaries.Max = Int32.Parse(numMaxVal.Text);
				((NoiseSensor)sensorToModify).SetBoundaries.Min = Int32.Parse(numMinVal.Text);
			}
			if (sensorToModify is TemperatureSensor)
			{
				((TemperatureSensor)sensorToModify).SetBoundaries.Max = double.Parse(numMaxVal.Text);
				((TemperatureSensor)sensorToModify).SetBoundaries.Min = double.Parse(numMinVal.Text);
			}
			sensorLogic.SaveState();
			sensorLogic.showList(sensorLogic);
		}
	}
}
