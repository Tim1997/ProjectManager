using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace template__
{
    [TestClass]
    [TestCategory("Base")]
    public class UnitTest1 : BaseSession
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Init(Id, context);
        }

        [TestMethod]
        public void TestMethod1()
        {
            //Thread.Sleep(10000);
            Log.Passed("Method test do some thing description here!");
        }

        [TestMethod]
        public void TestMethod2()
        {
            Thread.Sleep(500);
            Log.Passed("Method test do some thing description here!");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Close();
        }
        
    }
}
