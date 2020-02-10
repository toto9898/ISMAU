using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ISMAU.DATA
{
    public class ElPowerSensor : BoundedSensor<int>
    {

		public int Wats { get; set; }

        public ElPowerSensor(SensorData data, RangeBoundaries<int> rangeBoundaries)
            : base(data, rangeBoundaries)
        {
            Wats = -1;
        }

        public ElPowerSensor()
            : base()
        {
            Wats = -1;
        }

        public ElPowerSensor(ElPowerSensor sensor)
            : base(sensor)
        {
            Wats = sensor.Wats;
        }

        public override void GetData()
        {

        }
        public override void ConvertValueString()
		{
            int temp = 0;
			Int32.TryParse(DataAsString, out temp);
            Wats = temp;
		}
	}
}
