using System;
using System.Threading.Tasks;
using NBitcoin;
using Nopara74.HWI.Exceptions;
using Nopara74.HWI.Models;

namespace Nopara74.HWI
{
	public class HwiClient
	{
		public Network Network { get; }

		public HwiClient(Network network)
		{
			Network = network ?? throw new ArgumentNullException(nameof(network));
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

		public ExtPubKey GetMasterXpub(string devicePath, DeviceType deviceType)
		{
			return SendCommand(devicePath, deviceType);
		}

		public ExtPubKey GetXpub(string devicePath, DeviceType deviceType)
		{
			return SendCommand(devicePath, deviceType);
		}

		public ExtPubKey SignTx(string devicePath, DeviceType deviceType)
		{
			return SendCommand(devicePath, deviceType);
		}

		public ExtPubKey SignMessage(string devicePath, DeviceType deviceType)
		{
			return SendCommand(devicePath, deviceType);
		}

		public ExtPubKey GetKeypool(string devicePath, DeviceType deviceType)
		{
			return SendCommand(devicePath, deviceType);
		}

		public ExtPubKey DisplayAddress(string devicePath, DeviceType deviceType)
		{
			return SendCommand(devicePath, deviceType);
		}

		public ExtPubKey Setup(string devicePath, DeviceType deviceType)
		{
			return SendCommand(devicePath, deviceType);
		}

		public ExtPubKey Wipe(string devicePath, DeviceType deviceType)
		{
			return SendCommand(devicePath, deviceType);
		}

		public ExtPubKey Restore(string devicePath, DeviceType deviceType)
		{
			return SendCommand(devicePath, deviceType);
		}

		public ExtPubKey Backup(string devicePath, DeviceType deviceType)
		{
			return SendCommand(devicePath, deviceType);
		}

		public ExtPubKey PromptPin(string devicePath, DeviceType deviceType)
		{
			return SendCommand(devicePath, deviceType);
		}

		public ExtPubKey SendPin(string devicePath, DeviceType deviceType)
		{
			return SendCommand(devicePath, deviceType);
		}

		private ExtPubKey SendCommand(string devicePath, DeviceType deviceType)
		{
			if (devicePath == null)
			{
				throw new ArgumentNullException(nameof(devicePath));
			}

			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}
	}
}
