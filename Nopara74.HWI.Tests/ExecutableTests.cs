using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Nopara74.HWI.Tests
{
	public class ExecutableTests
	{
		[Fact]
		public void HwiExecutableExists()
		{
			var client = new HwiClient(Network.Main);
			Assert.True(File.Exists(client.HwiPath));
		}
	}
}
