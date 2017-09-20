using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberText.Tests
{
    [TestClass()]
    public class KanjiNumberTests
    {
        [TestMethod()]
        public void FomatNumberTest()
        {
            var k = new KanjiNumber();
            
            var str = string.Join("",k.FomatNumberList(1234000005678910));

            Console.WriteLine(str);

            Assert.IsTrue(str == "1234兆567万8910");
        }

        [TestMethod()]
        public void TakeFomatNumberTest()
        {
            var k = new KanjiNumber();

            var str = k.TakeFormatNumber(1234000005678910,2);

            Console.WriteLine(str);

            Assert.IsTrue(str == "1234兆");
        }

        [TestMethod()]
        public void DecimalFormatNumberTest()
        {
            var k = new KanjiNumber();

            var str = k.DecimalFormatNumber(1234000005678910,2);

            Console.WriteLine(str);

            Assert.IsTrue(str == "1234.00兆");
        }
    }
}