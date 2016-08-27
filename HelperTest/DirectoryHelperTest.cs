using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelperProject.Helpers;

namespace HelperTest
{
    [TestClass]
    public class DirectoryHelperTest
    {
        [TestMethod]
        public void CreateDirectorySuccessTest()
        {
            string path = "D:/Test/New";
            var result = DirectoryHelper.CreatDirectoryIfNotExists(path);
            Assert.AreEqual(true, result.IsSuccess);
        }

        [TestMethod]
        public void CreateDirectoryFailTest()
        {
            //string path = "K:/Test/New";
            string path = null;
            //string path = "";
            var result = DirectoryHelper.CreatDirectoryIfNotExists(path);
            Assert.AreEqual(false, result.IsSuccess);
        }

    }

    public class TestMain
    {
        private static void Main()
        {
            DirectoryHelperTest test = new DirectoryHelperTest();
            test.CreateDirectorySuccessTest();
            test.CreateDirectoryFailTest();
        }
    }
}
