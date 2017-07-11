using Graph.Option;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class MaxLengthTest
    {
        [TestMethod]
        public void CheckPathTestTrue()
        {
            var path = Builder.BuildPath(new List<Tuple<string, string, int>>{
                Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 1), Tuple.Create("A", "D", 1),
                Tuple.Create("B", "A", 1), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) });
            var maxLen = new MaxLength<string>(6);
            Assert.IsTrue(maxLen.CheckPath(path));
        }

        [TestMethod]
        public void CheckPathTestFalse()
        {
            var path = Builder.BuildPath(new List<Tuple<string, string, int>>{
                Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 1), Tuple.Create("A", "D", 1),
                Tuple.Create("B", "A", 1), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) });
            var maxLen = new MaxLength<string>(3);
            Assert.IsFalse(maxLen.CheckPath(path));
        }
    }
}
