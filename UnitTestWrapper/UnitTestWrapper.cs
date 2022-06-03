using DXC_GROrderPadWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestWrapper
{
    [TestClass]
    public class UnitTestWrapper
    {
        [TestMethod]
        public void TestCreateOrder()
        {
            var wrapper = new DXC_GROrderPadWrapper.DXC_GROrderPadWrapper();
   
            var result = wrapper.CreateNewOrder("testId", "Test Company", "dbName", "partition", "dataArea");
        }
        [TestMethod]
        public void TestEditOrder()
        {
            var wrapper = new DXC_GROrderPadWrapper.DXC_GROrderPadWrapper();

            var result = wrapper.EditViewOrder("testId", "Test Company", "dbName", "partition", "dataArea");
        }
    }
}
