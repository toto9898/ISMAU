using ISMAU.DATA;
using ISMAU.FUNCTIONALITY;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Forms;

namespace ISMAU
{
	/// <summary>
	/// Interaction logic for ListPage.xaml
	/// </summary>
	public partial class ListPage : Page
	{
		public SensorLogic sensorLogic;

		public ListPage(SensorLogic sensors)
		{
			InitializeComponent();
			sensorLogic = sensors;
			sensorLogic.getValuesForAllSensorsFromAPI();
			initializeGrid();
		}


		private void initializeGrid()
		{
			sensorsGridView.ItemsSource = sensorLogic.Sensors;
		}

		private void Modify_Click(object sender, RoutedEventArgs e)
		{
			var cellInfos = sensorsGridView.SelectedCells;
			Sensor sensor;
			if (cellInfos.Count > 0)
			{
				sensor = cellInfos[0].Item as Sensor;
				sensorLogic.modifier(sensor, sensorLogic);
			}
		}
	}
}
