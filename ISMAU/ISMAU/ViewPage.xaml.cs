using ISMAU.DATA;
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
    /// Interaction logic for ViewPage.xaml
    /// </summary>
    public partial class ViewPage : Page
    {
        private Func<Sensor, Task> updadeSensor;

        public Sensor Sensor { get; set; }

        public ViewPage(Sensor sensor, Func<Sensor, Task> update)
        {
            InitializeComponent();
            Sensor = sensor;
            updadeSensor = update;

            SetBoundaries();


        }

        private void SetBoundaries()
        {
            if (Sensor is ElPowerSensor e)
            {
                scale.Min = e.Boundaries.Min;
                scale.Max = e.Boundaries.Max;
            }
            if (Sensor is HumiditySensor h)
            {
                scale.Min = h.Boundaries.Min;
                scale.Max = h.Boundaries.Max;
            }
            if (Sensor is NoiseSensor n)
            {
                scale.Min = n.Boundaries.Min;
                scale.Max = n.Boundaries.Max;
            }
            if (Sensor is TemperatureSensor t)
            {
                scale.Min = t.Boundaries.Min;
                scale.Max = t.Boundaries.Max;
            }
        }


    }
}
