using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISMAU.DATA;
using System.IO;

namespace ISMAU.FUNCTIONALITY
{
	public class SensorLogic
	{

		#region Data Members
		private List<Sensor> sensors;
		private const string DATABASE_NAME = @"\data.xml";
		private string databasePath;

		#endregion

		#region Constructor
		public SensorLogic()
		{
			string current = Environment.CurrentDirectory;
			current = Directory.GetParent(current).Parent.Parent.FullName;
			databasePath = current + DATABASE_NAME;

			try
			{
				sensors = sensors.Deserialize(databasePath);
			}
			catch (Exception)
			{
				sensors = new List<Sensor>();
			}
		} 
		#endregion

		#region Properties
		public string DatabaseName
		{
			get { return DATABASE_NAME; }
			private set { }
		}

		public string DatabasePath
		{
			get { return databasePath; }
			private set { }
		}
		#endregion

		#region Methods
		private List<Sensor> getSensors(StreamReader streamReader)
		{
			streamReader.ReadLine(); //skips first line
			streamReader.ReadLine(); //skips second line
			List<Sensor> readSensors = new List<Sensor>();
			SensorReader reader = new SensorReader();

			string currentRow;
			Sensor currentSensor = null;
			while (true)
			{
				currentRow = streamReader.ReadLine();
				if (!currentRow.Equals("<Sensor>"))
					break;

				currentSensor = reader.readSensor(streamReader);
				readSensors.Add(currentSensor);
			}

			return readSensors;
		}

		public void SaveState()
		{
			sensors.Serialize(databasePath);
		}

		#endregion
	}
}
