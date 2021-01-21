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
             GLUpdater.Model.Checker();
             Assert.IsTrue(GLUpdater.Model.IsUpdate());

        }
    }
}
