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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
			InitializeComponent();

			pageWindow.Content = new HomePage();

		}

		private void btnHomePage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new HomePage();
		}

		private void btnRegisterPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new RegistryPage();
		}

		private void btnModifyPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new ModifyPage();
		}

		private void btnListPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new ListPage();
		}

		private void btnViewPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new ViewPage();
		}

		private void btnMapPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new MapPage();
		}

		private void btnReportPage_Click(object sender, RoutedEventArgs e)
		{
			pageWindow.Content = new ReportPage();
		}
	}
}
