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

            #endregion

            #region Act

            string help = hwiClient.GetHelp();

            #endregion

            #region Assert

            Assert.NotEmpty(help);

            #endregion
        }

        [Fact]
        public void CanGetVersion()
        {
            #region Arrange

            var hwiClient = new HwiClient();

            #endregion

            #region Act

            Version version = hwiClient.GetVersion();

            #endregion

            #region Assert

            Assert.Equal(new Version("1.0.1"), version);

            #endregion
        }

        [Fact]
        public void CanEnumerate()
        {
            #region Arrange

            var hwiClient = new HwiClient();

            #endregion

            #region Act

            string enumerate = hwiClient.Enumerate();

            #endregion

            #region Assert

            Assert.Equal("[]", enumerate);

            #endregion
        }
    }
}
