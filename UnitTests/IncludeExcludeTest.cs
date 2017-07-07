using Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace UnitTests
{
    [TestClass]
    public class IncludeExcludeTest
    {
        [TestMethod]
        public void CheckEdgeTestTrue()
        {
            var incexc = Create();
            var edge = new Edge<string>("A", "B", "AB", 1);
            Assert.IsTrue(incexc.CheckEdge(edge));
        }

        [TestMethod]
        public void CheckEdgeTestFalse()
        {
            var incexc = Create();
            var edge1 = new Edge<string>("A", "E", "AE", 1);
            var edge2 = new Edge<string>("D", "B", "DB", 1);
            Assert.IsFalse(incexc.CheckEdge(edge1));
            Assert.IsFalse(incexc.CheckEdge(edge2));
        }

        [TestMethod]
        public void CheckPathTestTrue()
        {
            var incexc = Create();
            var path = Builder.BuildListOfEdge(new List<Tuple<string, string, int>> {
                Tuple.Create("A", "B", 1), Tuple.Create("B", "E", 1), Tuple.Create("E", "C", 1)});
            incexc.CheckPath(path);
        }

        [TestMethod]
        public void CheckPathTestFalse1()
        {
            var incexc = Create();
            var path = Builder.BuildListOfEdge(new List<Tuple<string, string, int>> {
                Tuple.Create("A", "B", 1), Tuple.Create("B", "C", 1)});
            incexc.CheckPath(path);
        }

        [TestMethod]
        public void CheckPathTestFalse2()
        {
            var incexc = Create();
            var path = Builder.BuildListOfEdge(new List<Tuple<string, string, int>> {
                Tuple.Create("A", "D", 1), Tuple.Create("D", "B", 1), Tuple.Create("B", "E", 1), Tuple.Create("E", "C", 1)});
            incexc.CheckPath(path);
        }

        IncludeExclude<string> Create()
        {
            var incEdges = new List<string> { "EC" };
            var incVertexes = new List<string> { "B" };
            var excEdges = new List<string> { "AE" };
            var excVertexes = new List<string> { "D" };
            return new IncludeExclude<string>(incEdges, incVertexes, excEdges, excVertexes);
        }
    }
}
