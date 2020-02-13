using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;

namespace ISMAU.FUNCTIONALITY
{
	/// <summary>
	/// Holds the output from the API
	/// </summary>
	public class ApiOutput
	{
		public string TimeStamp	{ get; set; }

		public string Value { get; set; }
	}

	/// <summary>
	/// This class does the API work
	/// </summary>
	public class ApiConnector
	{
		#region Data Members
		private const string API_BASE_PATH
			= "http://fmi-sensors.icb.bg/testservices/api/sensor?sensorId=b549c8fb-4538-4cf7-9d8f-39ac27b27f25&sensorType=";
		#endregion

		#region Methods
		/// <summary>
		/// Gets the current value of a sensor with type "type", using the API
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
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
