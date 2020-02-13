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
	/// Interaction logic for HomePage.xaml
	/// </summary>
	/// <remarks>
	/// This is the starting page of the app.
	/// It will contain a map with all the sensors 
	/// and a short description of the purpose of the app
	/// </remarks>
	public partial class HomePage : Page
	{
		/// <summary>
		/// Sets the map in this page to the given mapPage
		/// </summary>
		/// <param name="mapPage"></param>
		public HomePage(MapPage mapPage)
		{
			InitializeComponent();
			map.Content = mapPage;
		}
	}
}
