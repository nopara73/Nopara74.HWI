using NBitcoin;
using System;
using Xunit;

namespace Nopara73.HWI.Tests
{
    public class NoConnectedHardwareWalletTests
    {
        [Fact]
        public void CanGetHelp()
        {
            #region Arrange

            var hwiClient = new HwiClient();

            #endregion Arrange

            #region Act

            string help = hwiClient.GetHelp();

            #endregion Act

            #region Assert

            Assert.NotEmpty(help);

            #endregion Assert
        }

        [Fact]
        public void CanGetVersion()
        {
            #region Arrange

            var hwiClient = new HwiClient();

            #endregion Arrange

            #region Act

            Version version = hwiClient.GetVersion();

            #endregion Act

            #region Assert

            Assert.Equal(new Version("1.0.1"), version);

            #endregion Assert
        }

        [Fact]
        public void CanEnumerate()
        {
            #region Arrange

            var hwiClient = new HwiClient();

            #endregion Arrange

            #region Act

            string enumerate = hwiClient.Enumerate();

            #endregion Act

            #region Assert

            Assert.Equal("[]", enumerate);

            #endregion Assert
        }

        [Fact]
        public void CantGetMasterXpub()
        {
            #region Arrange

            var hwiClient = new HwiClient();

            #endregion Arrange

            #region ActAndAssert

            Assert.Throws<HwiException>(hwiClient.GetMasterXpub);

            #endregion ActAndAssert
        }
    }
}
