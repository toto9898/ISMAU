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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace ISMAU
{
	/// <summary>
	/// Interaction logic for ListPage.xaml
	/// </summary>
	public partial class ListPage : Page
	{
		public SensorLogic sensorLogic;

		public ListPage(SensorLogic sensors,
			Action<ulong> openDetailsDelegate,
			Action<ulong> openEditDelegate)
		{
			InitializeComponent();
			sensorLogic = sensors;

			initializeGrid(openDetailsDelegate, openEditDelegate);

		}

		private void initializeGrid(
			Action<ulong> openDetails,
			Action<ulong> openEdit)
		{
			sensorsGridView.Columns.Add(new DetailsButtonColumn
			{
				Header = "Details",
				OnClickDelegate = openDetails
			});

			sensorsGridView.Columns.Add(new DetailsButtonColumn
			{
				Header = "Edit",
				OnClickDelegate = openEdit
			});

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


		public class DetailsButtonColumn : GridViewDataColumn
		{
			public Action<ulong> OnClickDelegate { get; set; }

			public override FrameworkElement CreateCellElement(GridViewCell cell, object dataItem)
			{
				RadButton button = cell.Content as RadButton;
				if (button == null)
				{
					button = new RadButton();
					button.Content = Header;
					button.Click += DetailsButton_Click;
				}

				button.CommandParameter = dataItem;

				return button;
			}

			public void DetailsButton_Click(object sender, RoutedEventArgs e)
			{
				var button = sender as RadButton;
				var sensor = button.CommandParameter as Sensor;
				OnClickDelegate(sensor.Id);
			}
		}

	}
}
