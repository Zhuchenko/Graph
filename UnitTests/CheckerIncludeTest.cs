using System.Collections;
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
            var include = new Options<string>(new List<string>(), new List<string> { "B" });
            var checker = new CheckerInclude<string>(include);
            Assert.IsFalse(checker.CheckPath());
            checker.CheckVertex("B");
            Assert.IsTrue(checker.CheckPath());
        }

        [TestMethod]
        public void CheckEdgeTest()
        {
            var include = new Options<string>(new List<string> { "AC" }, new List<string> { "B" });
            var checker = new CheckerInclude<string>(include);
            Assert.IsFalse(checker.CheckPath());
            checker.CheckVertex("B");
            Assert.IsFalse(checker.CheckPath());
            checker.CheckEdge("AC", "C");
            Assert.IsTrue(checker.CheckPath());
        }

        [TestMethod]
        public void UnCheckVertexTest()
        {
            var include = new Options<string>(new List<string>(), new List<string> { "B" });
            var checker = new CheckerInclude<string>(include);
            checker.CheckVertex("B");
            Assert.IsTrue(checker.CheckPath());
            checker.UnCheckVertex("B");
            Assert.IsFalse(checker.CheckPath());
        }

        [TestMethod]
        public void UnCheckEdgeTest()
        {
            var include = new Options<string>(new List<string> { "AC" }, new List<string> { "B" });
            var checker = new CheckerInclude<string>(include);
            Assert.IsFalse(checker.CheckPath());
            checker.CheckVertex("B");
            checker.CheckEdge("AC", "C");
            Assert.IsTrue(checker.CheckPath());
            checker.UnCheckEdge("AC", "C");
            Assert.IsFalse(checker.CheckPath());
        }
        
        [TestMethod]
        public void CheckPathTest()
        {
            var include = new Options<string>(new List<string> { "AC" }, new List<string> { "B" });
            var checker = new CheckerInclude<string>(include);
            Assert.IsFalse(checker.CheckPath());
            checker.CheckVertex("B");
            Assert.IsFalse(checker.CheckPath());
            checker.CheckEdge("AC", "C");
            Assert.IsTrue(checker.CheckPath());
        }
    }
}
