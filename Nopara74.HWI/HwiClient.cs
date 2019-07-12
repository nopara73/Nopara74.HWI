using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NBitcoin;
using Newtonsoft.Json.Linq;
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

		public async Task<string> GetHelpAsync(CancellationToken cancel)
		{
			SendCommandResponse response = await SendCommandAsync("--help", null, null, cancel).ConfigureAwait(false);
			return response.ResponseString;
		}

		public async Task<Version> GetVersionAsync(CancellationToken cancel)
		{
			SendCommandResponse response = await SendCommandAsync("--version", null, null, cancel).ConfigureAwait(false);

			string responseString = response.ResponseString;

			// Example output: hwi 1.0.0
			var vTry1 = responseString.Substring(responseString.IndexOf("hwi") + 3).Trim();
			if (Version.TryParse(vTry1, out Version v1))
			{
				return v1;
			}

			// Example output: hwi.exe 1.0.0
			var vTry2 = responseString.Substring(responseString.IndexOf("hwi.exe") + 7).Trim();
			if (Version.TryParse(vTry2, out Version v2))
			{
				return v2;
			}

			throw new FormatException($"Cannot parse version from HWI's response. Response: {response}");
		}

		public async Task<IEnumerable<string>> EnumerateAsync(CancellationToken cancel)
		{
			SendCommandResponse response = await SendCommandAsync(null, "enumerate", null, cancel).ConfigureAwait(false);

			JArray jarr = response.ParseToJToken() as JArray;

			var hwis = new List<string>();
			foreach (JObject json in jarr)
			{
				string jsonString = json.ToString();
				hwis.Add(jsonString);
			}

			return hwis;
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

		private async Task<SendCommandResponse> SendCommandAsync(string hwiOptions, string command, string commandOptions, CancellationToken cancel)
		{
			var argumentBuilder = new StringBuilder();

			if (Network != Network.Main)
			{
				argumentBuilder.Append("--testnet");
				argumentBuilder.Append(" ");
			}

			if (!string.IsNullOrWhiteSpace(hwiOptions))
			{
				argumentBuilder.Append(hwiOptions);
				argumentBuilder.Append(" ");
			}

			if (!string.IsNullOrWhiteSpace(command))
			{
				argumentBuilder.Append(command);
				argumentBuilder.Append(" ");
			}

			if (!string.IsNullOrWhiteSpace(commandOptions))
			{
				argumentBuilder.Append(commandOptions);
			}

			string arguments = argumentBuilder.ToString().Trim();
			using (var process = Process.Start(
				new ProcessStartInfo
				{
					FileName = HwiPath,
					Arguments = arguments,
					RedirectStandardOutput = true,
					UseShellExecute = false,
					CreateNoWindow = true,
					WindowStyle = ProcessWindowStyle.Hidden
				}
			))
			{
				await process.WaitForExitAsync(cancel).ConfigureAwait(false); // TODO: https://github.com/zkSNACKs/WalletWasabi/issues/1452;

				if (process.ExitCode != 0)
				{
					throw new IOException($"Command: {arguments} exited with incorrect exit code.", process.ExitCode);
				}

				string response = await process.StandardOutput.ReadToEndAsync().ConfigureAwait(false);

				return new SendCommandResponse(response);
			}
		}

		private ExtPubKey SendCommand(string devicePath, DeviceType deviceType, string password)
		{
			devicePath = Guard.NotNullOrEmptyOrWhitespace(nameof(devicePath), devicePath);

			throw new HwiException(ErrorCode.NoDevicePath, "foo");
		}
	}
}
