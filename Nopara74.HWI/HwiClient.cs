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
            throw new HwiException(ErrorCode.NoDevicePath);
        }
    }
}
