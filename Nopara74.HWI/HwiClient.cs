using System;
using System.Threading.Tasks;
using NBitcoin;
using Nopara74.HWI.Exceptions;

namespace Nopara74.HWI
{
	public class HwiClient
	{
		public HwiClient()
		{
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

		public ExtPubKey GetMasterXpub()
		{
			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}

		public ExtPubKey GetXpub()
		{
			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}

		public ExtPubKey SignTx()
		{
			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}

		public ExtPubKey SignMessage()
		{
			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}

		public ExtPubKey GetKeypool()
		{
			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}

		public ExtPubKey DisplayAddress()
		{
			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}

		public ExtPubKey Setup()
		{
			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}

		public ExtPubKey Wipe()
		{
			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}

		public ExtPubKey Restore()
		{
			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}

		public ExtPubKey Backup()
		{
			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}

		public ExtPubKey PromptPin()
		{
			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}

		public ExtPubKey SendPin()
		{
			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}
	}
}
