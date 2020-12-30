using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    class Alarm
    {
		public DateTime TimeStamp { get; set; }
		public string Message { get; set; }
		public string Location { get; set; }
		public int Risk { get; set; }

		public Alarm(DateTime timeStamp, string message, string location, int risk)
		{
			TimeStamp = timeStamp;
			Location = location;
			Message = message;
			Risk = risk;
		}

		public string Serialize()
		{
			return TimeStamp.ToString() + ';' + Message + ';' + Risk;
		}

		public Alarm Deserialize(string param)
		{
			string[] tokens = param.Split(';');

			Alarm alarm = new Alarm(DateTime.Parse(tokens[0]), tokens[1], tokens[2], int.Parse(tokens[3]));

			return alarm;
		}
	}
}
