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
	/// <remarks>
	/// This page has a grid with all the sensors
	/// where for every sensor, there is:
	/// - Name, Description and location of the sensor.
	/// - The data from the sensor, received when starting the page.
	/// - Button for modifying the sensor.
	/// - Button for going to the page with the sensor visuzlization.
	/// </remarks>
	public partial class ListPage : Page
	{
		/// <summary>
		/// Holds the collection of sensors
		/// </summary>
		public List<Sensor> Sensors { get; set; }

		/// <summary>
		/// Initializes the data
		/// </summary>
		/// <param name="sensors"> The sensors, to be used in this page </param>
		/// <param name="openDetailsDelegate">The function for openning the Details page</param>
		/// <param name="openEditDelegate">The function for openning the page for modifying the sensor</param>
		public ListPage(List<Sensor> sensors,
			Action<Sensor> openDetailsDelegate,
			Action<Sensor> openEditDelegate)
		{
			InitializeComponent();
			Sensors = sensors;

			initializeGrid(openDetailsDelegate, openEditDelegate);

		}

		/// <summary>
		/// Adds two columns with Buttons and sets their delegates.
		/// </summary>
		/// <param name="openDetails">The function for openning the Details page</param>
		/// <param name="openEdit">The function for openning the page for modifying the sensor</param>
		private void initializeGrid(
			Action<Sensor> openDetails,
			Action<Sensor> openEdit)
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

		/// <summary>
		/// Custom column for the grid
		/// </summary>
		public class DetailsButtonColumn : GridViewDataColumn
		{
			/// <summary>
			/// The action to be executed when clicking on the button
			/// </summary>
			public Action<Sensor> OnClickDelegate { get; set; }

			/// <summary>
			/// Setting the cell content to be button and assigning the delegate to its click event
			/// </summary>
			/// <param name="cell"></param>
			/// <param name="dataItem"></param>
			/// <returns></returns>
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

			/// <summary>
			/// Executes the delegate when the button is clicked
			/// </summary>
			/// <param name="sender">In this case this is the button</param>
			/// <param name="e"></param>
			public void DetailsButton_Click(object sender, RoutedEventArgs e)
			{
				var button = sender as RadButton;
				var sensor = button.CommandParameter as Sensor;
				OnClickDelegate(sensor);
			}
		}

	}
}
