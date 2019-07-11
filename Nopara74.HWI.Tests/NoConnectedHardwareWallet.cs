using NBitcoin;
using Nopara74.HWI;
using Nopara74.HWI.Exceptions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Nopara74.HWI.Tests
{
	public class NoConnectedHardwareWalletTests
	{
		public HwiClient Client { get; } = new HwiClient();

		[Fact]
		public void CanGetHelp()
		{
			#region Act

			string help = Client.GetHelp();

			#endregion Act

			#region Assert

			Assert.NotEmpty(help);

			#endregion Assert
		}

		[Fact]
		public void CanGetVersion()
		{
			#region Act

			Version version = Client.GetVersion();

			#endregion Act

			#region Assert

			Assert.Equal(new Version("1.0.1"), version);

			#endregion Assert
		}

		[Fact]
		public void CanEnumerate()
		{
			#region Act

			string enumerate = Client.Enumerate();

			#endregion Act

			#region Assert

			Assert.Equal("[]", enumerate);

			#endregion Assert
		}

		[Fact]
		public void AssertNoDevicePathErrors()
		{
			#region Arrange

			var funcs = new List<Func<ExtPubKey>>();

			#endregion Arrange

			#region Act

			funcs.Add(Client.GetMasterXpub);
			funcs.Add(Client.GetXpub);
			funcs.Add(Client.SignTx);
			funcs.Add(Client.SignMessage);
			funcs.Add(Client.GetKeypool);
			funcs.Add(Client.DisplayAddress);
			funcs.Add(Client.Setup);
			funcs.Add(Client.Wipe);
			funcs.Add(Client.Restore);
			funcs.Add(Client.Backup);
			funcs.Add(Client.PromptPin);
			funcs.Add(Client.SendPin);

			#endregion Act

			#region Assert

			foreach (Func<ExtPubKey> func in funcs)
			{
				var ex = Assert.Throws<HwiException>(func);
				Assert.Equal(ErrorCode.NoDevicePath, ex.ErrorCode);
				Assert.NotEmpty(ex.Message);
			}

			#endregion Assert
		}
	}
}
