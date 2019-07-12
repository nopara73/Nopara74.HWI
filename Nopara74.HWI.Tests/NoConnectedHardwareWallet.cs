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
			#region Arrange

			HwiClient hwiClientNullNetworkFunc() => new HwiClient(null);

			#endregion Arrange

			#region Assert

			var ex = Assert.Throws<ArgumentNullException>(hwiClientNullNetworkFunc);
			Assert.Equal("network", ex.ParamName);

			#endregion Assert
		}

		[Fact]
		public void ThrowsDevicePathArgumentExceptions()
		{
			#region Arrange

			var client = new HwiClient(Network.Main);
			var expectedParamName = "devicePath";

			#endregion Arrange

			#region ArrangeAct

			var nullFuncs = GetWalletSpecificFunctions(client, deviceType: DeviceType.Coldcard, devicePath: null, password: null);

			var emptyFuncs = GetWalletSpecificFunctions(client, deviceType: DeviceType.Coldcard, devicePath: "", password: null);
			var whitespaceFuncs = GetWalletSpecificFunctions(client, deviceType: DeviceType.Coldcard, devicePath: " ", password: null);
			var emptyAndWhitespaceFuncs = emptyFuncs.Concat(whitespaceFuncs);

			#endregion ArrangeAct

			#region Assert

			IterateAssertArgumentExceptionParamName<ArgumentNullException>(nullFuncs, expectedParamName);

			IterateAssertArgumentExceptionParamName<ArgumentException>(emptyAndWhitespaceFuncs, expectedParamName);

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
		public void ThrowsNoDevicePathHwiException(HwiClient client, DeviceType deviceType)
		{
			#region Arrange

			var passwords = new List<string>
			{
				null,
				"",
				" ",
				"password",
				" password wit spaces trim ",
				"~!@#$%^&*()_+"
			};

			var devicePaths = new List<string>
			{
				"wrongdevicepath",
				" wrong device path with spaces ",
				"wrong device path with ~!@#$%^&*()_+"
			};

			#endregion Arrange

			#region Act

			var funcs = new List<Func<object>>();

			foreach (var password in passwords)
			{
				funcs.AddRange(GetWalletSpecificFunctions(client, deviceType, devicePath: devicePaths.First(), password));
			}

			foreach (var devicePath in devicePaths)
			{
				funcs.AddRange(GetWalletSpecificFunctions(client, deviceType, devicePath, password: passwords.First()));
			}

			#endregion Act

			#region Assert

			foreach (Func<object> func in funcs)
			{
				var ex = Assert.Throws<HwiException>(func);
				Assert.Equal(ErrorCode.NoDevicePath, ex.ErrorCode);
				Assert.NotEmpty(ex.Message);
			}

			#endregion Assert
		}

		private static void IterateAssertArgumentExceptionParamName<T>(IEnumerable<Func<object>> funcs, string expectedParamName) where T : ArgumentException
		{
			foreach (Func<object> func in funcs)
			{
				var ex = Assert.Throws<T>(func);
				Assert.Equal(expectedParamName, ex.ParamName);
			}
		}

		private static IEnumerable<Func<object>> GetWalletSpecificFunctions(HwiClient client, DeviceType deviceType, string devicePath, string password)
		{
			var funcs = new List<Func<object>>
			{
				() => client.GetMasterXpub(devicePath, deviceType, password),
				() => client.GetXpub(devicePath, deviceType, password),
				() => client.SignTx(devicePath, deviceType, password),
				() => client.SignMessage(devicePath, deviceType, password),
				() => client.GetKeypool(devicePath, deviceType, password),
				() => client.DisplayAddress(devicePath, deviceType, password),
				() => client.Setup(devicePath, deviceType, password),
				() => client.Wipe(devicePath, deviceType, password),
				() => client.Restore(devicePath, deviceType, password),
				() => client.Backup(devicePath, deviceType, password),
				() => client.PromptPin(devicePath, deviceType, password),
				() => client.SendPin(devicePath, deviceType, password)
			};

			return funcs;
		}

		public static IEnumerable<object[]> GetOptionCombinationValues()
		{
			Array deviceTypes = Enum.GetValues(typeof(DeviceType));

			foreach (object[] clientValues in GetHwiClientValues())
			{
				object clientValue = clientValues.First();
				foreach (DeviceType deviceType in deviceTypes)
				{
					yield return new object[] { clientValue, deviceType };
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
