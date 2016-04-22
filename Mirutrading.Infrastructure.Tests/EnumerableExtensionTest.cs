using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Infrastructure.Tests
{
    class TestForEach
    {
        public int a { get; set; }
    }

    [TestClass]
    public class EnumerableExtensionTest
    {
        [TestMethod]
        public void test_for_each()
        {
            TestForEach[] a = new TestForEach[3];
            a[0] = new TestForEach(); a[1] = new TestForEach(); a[2] = new TestForEach();
            var c = a.ForEach(m => m.a = 1);

            foreach(var b in c)
            {
                Assert.AreEqual(b.a, 1);
            }
        }

    }
}
