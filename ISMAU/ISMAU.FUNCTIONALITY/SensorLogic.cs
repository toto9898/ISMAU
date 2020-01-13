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

		private List<Sensor> sensors;

		public SensorLogic(string databasePath)
		{
			/// XML Sensor outlook
			/*	
			 *	<ListOfSensors Size = "value">
			 *		<Sensor> 
			 *			<Name> NameString </Name>
			 *			<Description> DescriptionString </Description>
			 *			<Type> TypeString </Type>
			 *			<PollingInterval> Value </PollingInterval>
			 *			<Location> LocationString </Llocation>
			 *			<AcceptableValues> Values </AcceptableValues>
			 *			<TickOff> Option </TickOff>
			 *		</Sensor>
			 *	</ListOfSensors>
			 */
		}
		
	}
}
