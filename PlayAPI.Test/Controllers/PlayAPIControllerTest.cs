using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayAPI.Controllers.Test
{
    [TestClass]
    public class PlayAPIControllerTest
    {
        private readonly PlayAPIController _playAPIController;
        public PlayAPIControllerTest()
        {
            _playAPIController = new PlayAPIController(new ConfigurationManager());
        }
        [TestMethod]
        public void GetNumTest()
        {
            int result = _playAPIController.GetNum(1);
            Assert.AreEqual(1, result);
            result = _playAPIController.GetNum(2);
            Assert.AreEqual(0, result);
            result = _playAPIController.GetNum(-1);
            Assert.AreEqual(-1, result);
        }
    }
}