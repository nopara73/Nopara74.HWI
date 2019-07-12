using Newtonsoft.Json.Linq;
using Nopara74.HWI.Guarding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nopara74.HWI.Models
{
	public class SendCommandResponse
	{
		public string ResponseString { get; }

		public SendCommandResponse(string response)
		{
			ResponseString = Guard.Correct(response);
		}

		public bool TryParseToJToken(out JToken responseToken)
		{
			responseToken = null;
			try
			{
				responseToken = ParseToJToken();
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}

		public JToken ParseToJToken()
		{
			return JToken.Parse(ResponseString);
		}
	}
}
