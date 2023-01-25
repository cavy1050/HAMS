using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using log4net.Core;
using HAMS.Frame.Kernel.Services;

namespace HAMS.Frame.Kernel.Test
{
    [TestClass]
    public class ServerTest
    {
        [TestMethod]
        public void TestLogLevelConverter()
        {
            LogLevelConverter logLevelConverter = new LogLevelConverter();
            Level ret = (Level)logLevelConverter.ConvertFrom("Debug");

            Assert.AreEqual(ret, Level.Debug);
        }
    }
}
