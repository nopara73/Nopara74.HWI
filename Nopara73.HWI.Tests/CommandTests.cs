using System;
using Xunit;

namespace Nopara73.HWI.Tests
{
    public class CommandTests
    {
        [Fact]
        public void AssertHwiClientCanGetHelp()
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
    }
}
