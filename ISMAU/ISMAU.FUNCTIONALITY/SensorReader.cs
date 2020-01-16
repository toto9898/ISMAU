using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISMAU.DATA;
using System.IO;

namespace ISMAU.FUNCTIONALITY
{
	public class SensorReader
	{
		private readonly string[] SENSOR_TYPES = { "DoorSensor", "ELPowerSensor", "HumiditySensor", "NoiseSensor", "TemperatureSensor" };
		private enum SENSOR_INDEXES
		{
			INVALID = -1,
			DOOR_SENSOR,
			EL_POWER_SENSOR,
			HUMIDITY_SENSOR,
			NOISE_SENSOR,
			TEMPERATURE_SENSOR,
			NUMBER_OF_SENSORS
		}

		public SensorReader() 
		{ }

		public Sensor readSensor(StreamReader reader)
		{
			int tempInt;
			float tempFloat;

			//finds type and creates the sensor
			string currentRow = reader.ReadLine();
			if (!isTag(currentRow))
				return null;
			Sensor output = createSensor(currentRow);

			//gets the name of the sensor
			currentRow = reader.ReadLine();
			if (!isTag(currentRow))
				return null;
			output.Name = trimTag(currentRow);

			//gets description of sensor
			currentRow = reader.ReadLine();
			if (!isTag(currentRow))
				return null;
			output.Description = trimTag(currentRow);

			//gets polling interval of sensor
			currentRow = reader.ReadLine();
			if (!isTag(currentRow))
				return null;
			if (!Int32.TryParse(trimTag(currentRow), out tempInt))
				return null;
			output.PollingInterval = tempInt;

			//gets location of sensor
			currentRow = reader.ReadLine();
			if (!isTag(currentRow))
				return null;
			Location currentLocation = getLocation(trimTag(currentRow));
			output.Location = currentLocation;

			//gets acceptable values
			currentRow = reader.ReadLine();
			if (!isTag(currentRow))
				return null;
			readAcceptableRange(output, trimTag(currentRow));

			//gets tick off
			currentRow = reader.ReadLine();
			if (!isTag(currentRow))
				return null;
			if (!float.TryParse(trimTag(currentRow), out tempFloat))
				return null;
			output.TickOff = tempFloat;

			reader.ReadLine(); //reads closing tag

			return output;
		}

		private bool isTag(string input)
		{
			bool isBetweenAngleBrackets = false;
			string temp = "";
			int numberOfAngleBracketPairs = 0;
			for (int i = 0; i < input.Length; ++i)
			{
				if (input[i] == '<')
				{
					isBetweenAngleBrackets = true;
					continue;
				}

				if (input[i] == '>')
				{
					isBetweenAngleBrackets = false;
					++numberOfAngleBracketPairs;
					if (!contentOfTagIsGood(temp))
						return false;
					temp = "";
					continue;
				}

				if (isBetweenAngleBrackets && input[i] != '/')
					temp += input[i];
			}

			if (numberOfAngleBracketPairs != 2)
				return false;

			return true;
		}

		bool contentOfTagIsGood(string input)
		{
			foreach (var elem in SENSOR_TYPES)
				if (input.Equals(elem))
					return true;

			return false;
		}

		private Sensor createSensor(string input)
		{
			string type = trimTag(input);

			Sensor output = null;
			switch (getIndex(type))
			{
				case SENSOR_INDEXES.DOOR_SENSOR: output = new DoorSensor(); break;
				case SENSOR_INDEXES.EL_POWER_SENSOR: output = new ElPowerSensor(); break;
				case SENSOR_INDEXES.HUMIDITY_SENSOR: output = new HumiditySensor(); break;
				case SENSOR_INDEXES.NOISE_SENSOR: output = new NoiseSensor(); break;
				case SENSOR_INDEXES.TEMPERATURE_SENSOR: output = new TemperatureSensor(); break;
			}

			return output;
		}

		private SENSOR_INDEXES getIndex(string input)
		{
			for (int i = 0; i < (int)SENSOR_INDEXES.NUMBER_OF_SENSORS; ++i)
			{
				if (input.Equals(SENSOR_TYPES[i]))
					return (SENSOR_INDEXES)i;
			}

			return SENSOR_INDEXES.INVALID;
		}

		private string trimTag(string input)
		{
			string output = "";
			bool write = false;

			for(int i = 0; i < input.Length; ++i)
			{
				if(input[i]==' ')
				{
					write = true;
					continue;
				}

				if (write)
				{
					if (input[i] == ' ')
						break;
					output += input[i];
				}
			}

			return output;
		}

		private Location getLocation(string input)
		{
			Location output = new Location();

			string left = null, right = null;
			splitString(input, left, right);
			double temp;
			if (double.TryParse(left, out temp))
				output.Latitude = temp;
			if (double.TryParse(right, out temp))
				output.Longtitude = temp;

			return output;
		}

		private void splitString(string input,string left,string right)
		{
			bool writeInLeft = true;
			for(int i = 0; i < input.Length; ++i)
			{
				if (input[i] == ';')
				{
					writeInLeft = false;
					continue;
				}

				if (writeInLeft)
					left += input[i];
				else
					right += input[i];
			}
		}

		private void readAcceptableRange(Sensor current, string input)
		{
			if(current is ElPowerSensor || current is NoiseSensor)
			{
				RangeBoundaries<int> rangeBoundaries = new RangeBoundaries<int>();
				rangeBoundaries.Max = rangeBoundaries.Min = 0;
				string left = null, right = null;
				splitString(input, left, right);
				int temp;

				if (Int32.TryParse(left, out temp))
					rangeBoundaries.Min = temp;

				if (Int32.TryParse(right, out temp))
					rangeBoundaries.Max = temp;

				if (current is ElPowerSensor)
					((ElPowerSensor)current).SetBoundaries = rangeBoundaries;
				else
					((NoiseSensor)current).SetBoundaries = rangeBoundaries;
				return;
			}

			if(current is HumiditySensor)
			{
				RangeBoundaries<float> rangeBoundaries = new RangeBoundaries<float>();
				rangeBoundaries.Max = rangeBoundaries.Min = 0;
				string left = null, right = null;
				splitString(input, left, right);
				float temp;

				if (float.TryParse(left, out temp))
					rangeBoundaries.Min = temp;

				if (float.TryParse(right, out temp))
					rangeBoundaries.Max = temp;

				((HumiditySensor)current).SetBoundaries = rangeBoundaries;
			}

			if(current is TemperatureSensor)
			{
				RangeBoundaries<double> rangeBoundaries = new RangeBoundaries<double>();
				rangeBoundaries.Max = rangeBoundaries.Min = 0;
				string left = null, right = null;
				splitString(input, left, right);
				double temp;

				if (double.TryParse(left, out temp))
					rangeBoundaries.Min = temp;

				if (double.TryParse(right, out temp))
					rangeBoundaries.Max = temp;
			}
		}
	}
}
