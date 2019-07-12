using NBitcoin;
using Nopara74.HWI;
using Nopara74.HWI.Exceptions;
using Nopara74.HWI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Nopara74.HWI.Tests
{
	public class NoConnectedHardwareWalletTests
	{
		[Fact]
		public void ThrowsHwiClientConstructorArgumentNullException()
		{
			#region ArrangeAct

			HwiClient hwiClientNullNetworkFunc() => new HwiClient(null);

			#endregion ArrangeAct

			#region Assert

			var ex = Assert.Throws<ArgumentNullException>(hwiClientNullNetworkFunc);
			Assert.Equal("network", ex.ParamName);

			#endregion Assert
		}

		[Fact]
		public void ThrowsDevicePathArgumentNullException()
		{
			#region ArrangeAct

			var client = new HwiClient(Network.Main);

			List<Func<object>> funcs = GetWalletSpecificFunctions(client, deviceType: DeviceType.Coldcard, devicePath: null);

			#endregion ArrangeAct

			#region Assert

			foreach (Func<object> func in funcs)
			{
				var ex = Assert.Throws<ArgumentNullException>(func);
				Assert.Equal("devicePath", ex.ParamName);
			}

			#endregion Assert
		}

		[Theory]
		[MemberData(nameof(GetHwiClientValues))]
		public void CanGetHelp(HwiClient client)
		{
			#region Act

			string help = client.GetHelp();

			#endregion Act

			#region Assert

			Assert.NotEmpty(help);

			#endregion Assert
		}

		[Theory]
		[MemberData(nameof(GetHwiClientValues))]
		public void CanGetVersion(HwiClient client)
		{
			#region Act

			Version version = client.GetVersion();

			#endregion Act

			#region Assert

			Assert.Equal(new Version("1.0.1"), version);

			#endregion Assert
		}

		[Theory]
		[MemberData(nameof(GetHwiClientValues))]
		public void CanEnumerate(HwiClient client)
		{
			#region Act

			string enumerate = client.Enumerate();

			#endregion Act

			#region Assert

			Assert.Equal("[]", enumerate);

			#endregion Assert
		}

		[Theory]
		[MemberData(nameof(GetOptionCombinationValues))]
		public void ThrowsNoDevicePathHwiException(HwiClient client, DeviceType deviceType, string devicePath)
		{
			#region ArrangeAct

			List<Func<object>> funcs = GetWalletSpecificFunctions(client, deviceType, devicePath);

			#endregion ArrangeAct

			#region Assert

			foreach (Func<object> func in funcs)
			{
				var ex = Assert.Throws<HwiException>(func);
				Assert.Equal(ErrorCode.NoDevicePath, ex.ErrorCode);
				Assert.NotEmpty(ex.Message);
			}

			#endregion Assert
		}

		private static List<Func<object>> GetWalletSpecificFunctions(HwiClient client, DeviceType deviceType, string devicePath)
		{
			var funcs = new List<Func<object>>
			{
				() => client.GetMasterXpub(devicePath, deviceType),
				() => client.GetXpub(devicePath, deviceType),
				() => client.SignTx(devicePath, deviceType),
				() => client.SignMessage(devicePath, deviceType),
				() => client.GetKeypool(devicePath, deviceType),
				() => client.DisplayAddress(devicePath, deviceType),
				() => client.Setup(devicePath, deviceType),
				() => client.Wipe(devicePath, deviceType),
				() => client.Restore(devicePath, deviceType),
				() => client.Backup(devicePath, deviceType),
				() => client.PromptPin(devicePath, deviceType),
				() => client.SendPin(devicePath, deviceType)
			};

			return funcs;
		}

		public static IEnumerable<object[]> GetOptionCombinationValues()
		{
			Array deviceTypes = Enum.GetValues(typeof(DeviceType));

			var devicePaths = new List<string>
			{
				"",
				"wrongdevicepath",
				"wrong device path with spaces",
				"wrong device path with ~!@#$%^&*()_+"
			};

			foreach (object[] clientValues in GetHwiClientValues())
			{
				object clientValue = clientValues.First();
				foreach (DeviceType deviceType in deviceTypes)
				{
					foreach (string devicePath in devicePaths)
					{
						yield return new object[] { clientValue, deviceType, devicePath };
					}
				}
			}
		}

		public static IEnumerable<object[]> GetHwiClientValues()
		{
			var networks = new List<Network>
			{
				Network.Main,
				Network.TestNet,
				Network.RegTest
			};

			foreach (Network network in networks)
			{
				var client = new HwiClient(network);
				yield return new object[] { client };
			}
		}
	}
}
