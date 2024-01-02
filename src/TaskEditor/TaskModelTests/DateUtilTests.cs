using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModel.Tests
{
    [TestClass()]
    public class DateUtilTests
    {
        [TestMethod()]
        public void StringToLocalDateTest1()
        {
            const string unixTestTime = "1704137113"; // 1/1/2024 8:25:13 PM, unix time = 1704137113

            Assert.AreEqual(DateUtil.DateToString(DateUtil.StringToLocalDate(unixTestTime)), unixTestTime);
        }

        [TestMethod()]
        public void StringToLocalDateTest2()
        {
            DateTime timeNow = DateTime.Now;
            Assert.AreEqual(DateUtil.StringToLocalDate(DateUtil.DateToString(timeNow)).ToString(), timeNow.ToString());
        }
    }
}