using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            var str = string.Join("", k.FomatNumberList(1234000005678910));

            Console.WriteLine(str);

            Assert.IsTrue(str == "1234兆567万8910");
        }

        [TestMethod()]
        public void TakeFomatNumberTest()
        {
            var k = new KanjiNumber();

            var str = k.TakeFormatNumber(1234000005678910, 2);

            Console.WriteLine(str);

            Assert.IsTrue(str == "1234兆");
        }

        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestCase(1234000005678910UL, 2)]
        [TestCase(1234000005678910UL, 0)]
        public void TakeFomatNumberTestAll()
        {
            TestContext.Run((ulong n, int take) =>
            {
                var k = new KanjiNumber();

                var str = k.TakeFormatNumber(n, take);
                Console.WriteLine(str);
            });

        }

        [TestMethod()]
        public void DecimalFormatNumberTest()
        {
            var k = new KanjiNumber();

            var str = k.DecimalFormatNumber(1234000005678910, 2);

            Console.WriteLine(str);

            Assert.IsTrue(str == "1234.00兆");
        }

        [TestMethod]
        [TestCase(1234000005678910UL, -10)]
        [TestCase(1234000005678910UL, 5)]
        public void DecimalFormatNumberExTest()
        {
            TestContext.Run((ulong n, int len) =>
            {
                var k = new KanjiNumber();

                var ex = AssertEx.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var str = k.DecimalFormatNumber(n, len);

                    Console.WriteLine(str);
                });

                Console.WriteLine(ex.Message);
            });
        }

        [TestMethod()]
        public void CreateUnitTest()
        {
            var k = new KanjiNumber();

            var list = (string[])k.AsDynamic().CreateUnit(3);

            Console.WriteLine(list[0]);

            Assert.IsTrue(list.Last() == "cz");
        }

        [TestMethod]
        [TestCase(-10)]
        [TestCase(1234)]
        public void CreateUnitExTest()
        {            
            TestContext.Run((int count) =>
            {
                var k = new KanjiNumber();
                var ex = AssertEx.Throws<TargetInvocationException>(() =>
                {
                    var list = (string[])k.AsDynamic().CreateUnit(count);
                });

                Console.WriteLine(ex.InnerException.Message);
            });
        }
    }
}