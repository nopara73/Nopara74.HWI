﻿using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

public static class ProcessExtensions
{
	public static async Task WaitForExitAsync(this Process process, CancellationToken cancellationToken)
	{
		while (!process.HasExited)
		{
			await Task.Delay(100, cancellationToken).ConfigureAwait(false);
		}
	}
}
