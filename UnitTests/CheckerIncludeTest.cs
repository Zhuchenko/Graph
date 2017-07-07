using Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class CheckerIncludeTest
    {
        [TestMethod]
        public void CheckVertexTest()
        {
            var incEdges = new List<string>();
            var incVertexes = new List<string> { "B" };
            var checker = new CheckerInclude<string>(incEdges, incVertexes);
            Assert.IsFalse(checker.CheckPath());
            checker.CheckVertex("B");
            Assert.IsTrue(checker.CheckPath());
        }

        [TestMethod]
        public void CheckEdgeTest()
        {
            var incEdges = new List<string> { "AC" };
            var incVertexes = new List<string> { "B" };
            var checker = new CheckerInclude<string>(incEdges, incVertexes);
            Assert.IsFalse(checker.CheckPath());
            checker.CheckVertex("B");
            Assert.IsFalse(checker.CheckPath());
            checker.CheckEdge(new Edge<string>("A", "C", "AC", 1));
            Assert.IsTrue(checker.CheckPath());
        }

        [TestMethod]
        public void CheckPathTest()
        {
            var incEdges = new List<string> { "AC" };
            var incVertexes = new List<string> { "B" };
            var checker = new CheckerInclude<string>(incEdges, incVertexes);
            Assert.IsFalse(checker.CheckPath());
            checker.CheckVertex("B");
            Assert.IsFalse(checker.CheckPath());
            checker.CheckEdge(new Edge<string>("A", "C", "AC", 1));
            Assert.IsTrue(checker.CheckPath());
        }
    }
}
