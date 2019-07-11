using NBitcoin;
using Nopara74.HWI;
using Nopara74.HWI.Exceptions;
using System;
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
        public void CantGetMasterXpub()
        {
            #region Act

            Func<object> getMasterXpub = Client.GetMasterXpub;

            #endregion Act

            #region Assert

            var ex = Assert.Throws<HwiException>(getMasterXpub);
            Assert.Equal(ErrorCode.NoDevicePath, ex.ErrorCode);
            Assert.NotEmpty(ex.Message);

            #endregion Assert
        }
    }
}
