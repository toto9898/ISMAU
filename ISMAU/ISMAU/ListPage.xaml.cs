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
		public List<Sensor> Sensors { get; set; }

		public ListPage(List<Sensor> sensors,
			Action<ulong> openDetailsDelegate,
			Action<ulong> openEditDelegate)
		{
			InitializeComponent();
			Sensors = sensors;

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

			sensorsGridView.ItemsSource = Sensors;
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
