using System.Collections;
using Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class CheckerTest
    {
        [TestMethod]
        public void CheckStartVertexTest()
        {
            var optForEdges = new Options<string>(new List<string>(), new List<string>());
            var optForVertexes = new Options<string>(new List<string> { "A" }, new List<string> { "C" });
            var checker = new Checker<string>(optForEdges, optForVertexes);
            Assert.IsTrue(checker.CheckStartVertex("A"));
            Assert.IsFalse(checker.CheckStartVertex("C"));
            Assert.IsTrue(checker.CheckPath());
        }

        [TestMethod]
        public void CheckForMinusTest()
        {
            var optForEdges = new Options<string>(new List<string>(), new List<string> { "AB" });
            var optForVertexes = new Options<string>(new List<string>(), new List<string> { "C" });
            var checker = new Checker<string>(optForEdges, optForVertexes);
            Assert.IsTrue(checker.CheckForExclude("AB", "C"));
            Assert.IsFalse(checker.CheckForExclude("EF", "D"));
            Assert.IsTrue(checker.CheckForExclude("EF", "C"));
        }

        [TestMethod]
        public void CheckForIncludeTestTrue()
        {
            var optForEdges = new Options<string>(new List<string> { "AB", "DE" }, new List<string>());
            var optForVertexes = new Options<string>(new List<string> { "B", "E" }, new List<string>());
            var checker = new Checker<string>(optForEdges, optForVertexes);
            checker.CheckForInclude("AB", "B");
            checker.CheckForInclude("DE", "E");
            Assert.IsTrue(checker.CheckPath());
        }

        [TestMethod]
        public void CheckForIncludeTestFalse()
        {
            var optForEdges = new Options<string>(new List<string> { "AB", "DE" }, new List<string>());
            var optForVertexes = new Options<string>(new List<string> { "B", "E" }, new List<string>());
            var checker = new Checker<string>(optForEdges, optForVertexes);
            checker.CheckForInclude("AB", "F");
            checker.CheckForInclude("DE", "E");
            Assert.IsFalse(checker.CheckPath());
        }

        [TestMethod]
        public void UncheckForIncludeTest()
        {
            var optForEdges = new Options<string>(new List<string> { "AB", "DE" }, new List<string>());
            var optForVertexes = new Options<string>(new List<string> { "B", "E" }, new List<string>());
            var checker = new Checker<string>(optForEdges, optForVertexes);
            checker.CheckForInclude("AB", "B");
            checker.CheckForInclude("DE", "E");
            Assert.IsTrue(checker.CheckPath());
            checker.UncheckForInclude("DE", "E");
            Assert.IsFalse(checker.CheckPath());
        }

        [TestMethod]
        public void CheckPathTest()
        {
            var optForEdges = new Options<string>(new List<string> { "AB", "DE" }, new List<string>());
            var optForVertexes = new Options<string>(new List<string> { "B", "E" }, new List<string>());
            var checker = new Checker<string>(optForEdges, optForVertexes);
            Assert.IsFalse(checker.CheckPath());
            checker.CheckForInclude("AB", "B");
            checker.CheckForInclude("DE", "E");
            Assert.IsTrue(checker.CheckPath());
        }
    }
}
