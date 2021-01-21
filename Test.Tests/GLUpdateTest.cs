using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Tests.Modules;

namespace Test.Tests
{
    [TestClass]
    public class GLUpdateTest
    {
        [TestMethod]
        public void IsUpdateTrue()
        {
            Assert.IsTrue(GLUpdater.Model.IsUpdate());
        }
        public void IsUpdateFalse()
        {
            Assert.IsFalse(GLUpdater.Model.IsUpdate());
        }

        [TestMethod]
        public void GetResultIsNotNull()
        {
            Assert.IsNotNull(GLUpdater.Model.GetResult());
        }
    }

}
