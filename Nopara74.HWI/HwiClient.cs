using System;
using System.IO;
using System.Threading.Tasks;
using NBitcoin;
using Nopara74.HWI.Exceptions;
using Nopara74.HWI.Guarding;
using Nopara74.HWI.Models;

namespace Nopara74.HWI
{
	public class HwiClient
	{
		public Network Network { get; }

		public string HwiPath { get; }

		public HwiClient(Network network)
		{
			Network = Guard.NotNull(nameof(network), network);

			var fullBaseDirectory = Path.GetFullPath(AppContext.BaseDirectory);
			HwiPath = Path.Combine(fullBaseDirectory, "Binaries", "hwi-win64", "hwi.exe");

			if (!File.Exists(HwiPath))
			{
				throw new FileNotFoundException($"{nameof(HwiPath)} not found.", HwiPath);
			}
		}

		public string GetHelp()
		{
			return "foo";
		}

		public Version GetVersion()
		{
			return new Version("1.0.1");
		}

		public string Enumerate()
		{
			return "[]";
		}

		public ExtPubKey GetMasterXpub(string devicePath, DeviceType deviceType, string password = null)
		{
			return SendCommand(devicePath, deviceType, password);
		}

		public ExtPubKey GetXpub(string devicePath, DeviceType deviceType, string password = null)
		{
			return SendCommand(devicePath, deviceType, password);
		}

		public ExtPubKey SignTx(string devicePath, DeviceType deviceType, string password = null)
		{
			return SendCommand(devicePath, deviceType, password);
		}

		public ExtPubKey SignMessage(string devicePath, DeviceType deviceType, string password = null)
		{
			return SendCommand(devicePath, deviceType, password);
		}

		public ExtPubKey GetKeypool(string devicePath, DeviceType deviceType, string password = null)
		{
			return SendCommand(devicePath, deviceType, password);
		}

		public ExtPubKey DisplayAddress(string devicePath, DeviceType deviceType, string password = null)
		{
			return SendCommand(devicePath, deviceType, password);
		}

		public ExtPubKey Setup(string devicePath, DeviceType deviceType, string password = null)
		{
			return SendCommand(devicePath, deviceType, password);
		}

		public ExtPubKey Wipe(string devicePath, DeviceType deviceType, string password = null)
		{
			return SendCommand(devicePath, deviceType, password);
		}

		public ExtPubKey Restore(string devicePath, DeviceType deviceType, string password = null)
		{
			return SendCommand(devicePath, deviceType, password);
		}

		public ExtPubKey Backup(string devicePath, DeviceType deviceType, string password = null)
		{
			return SendCommand(devicePath, deviceType, password);
		}

		public ExtPubKey PromptPin(string devicePath, DeviceType deviceType, string password = null)
		{
			return SendCommand(devicePath, deviceType, password);
		}

		public ExtPubKey SendPin(string devicePath, DeviceType deviceType, string password = null)
		{
			return SendCommand(devicePath, deviceType, password);
		}

		private ExtPubKey SendCommand(string devicePath, DeviceType deviceType, string password)
		{
			devicePath = Guard.NotNullOrEmptyOrWhitespace(nameof(devicePath), devicePath);

			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}
	}
}
