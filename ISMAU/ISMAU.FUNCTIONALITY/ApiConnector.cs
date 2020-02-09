using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;

namespace ISMAU.FUNCTIONALITY
{
	public class ApiOutput
	{
		private string timeStamp;
		private string apiValue;

		public string TimeStamp
		{
			get { return timeStamp; }
			set
			{
				if (value != null)
					timeStamp = value;
				else
					timeStamp = "";
			}
		}

		public string ApiValue
		{
			get { return apiValue; }
			set
			{
				if (value != null)
					apiValue = value;
				else
					apiValue = "";
			}
		}
	}

	public class ApiConnector
	{
		#region Data Members
		private const string API_BASE_PATH
			= "http://fmi-sensors.icb.bg/testservices/api/sensor?sensorId=b549c8fb-4538-4cf7-9d8f-39ac27b27f25&sensorType=";
		#endregion

		#region Methods
		public static async Task<ApiOutput> getCurrentValue(string type)
		{
			string url = API_BASE_PATH + type;

			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Add("auth-token", "8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0");
				HttpResponseMessage response = await client.GetAsync(url);
				if (response.IsSuccessStatusCode)
				{
					ApiOutput output = await response.Content.ReadAsAsync<ApiOutput>();
					return output;
				}
			}

			return null;
		} 
		#endregion
	}
}
