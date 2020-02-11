using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ISMAU.DATA
{
    public class NoiseSensor : BoundedSensor<int>
    {
		public int Decibels { get => Data; set => Data = value; }

        public NoiseSensor(SensorData data, RangeBoundaries<int> rangeBoundaries)
            : base(data, rangeBoundaries)
        {
            Decibels = 0;
        }

        public NoiseSensor()
            : base()
        {
            Decibels = 0;
        }

        public NoiseSensor(NoiseSensor sensor)
            : base(sensor)
        {
            Decibels = sensor.Decibels;
        }

        public override void GetData()
        {

        }

        public override void ConvertValueString()
		{
            int temp = 0;
			if (Int32.TryParse(DataAsString, out temp))
                Decibels = temp;
		}
	}
}
