using Graph;
using Graph.Option;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class BestPathFinderTest
    {
        [TestMethod]
        public void FindTestBest()
        {
            var graph = new Graph<string>(Builder.BuildListOfEdge(new List<Tuple<string, string, int>>{
                Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 7), Tuple.Create("A", "D", 1),
                Tuple.Create("B", "A", 2), Tuple.Create("B", "C", 4), Tuple.Create("D", "C", 1) }));


            var option = new Option<string>(new IOption<string>[0]);
            var finder = new BestPathFinder<string>();

            var expected = new string[] { "AD", "DC" };

            var path = finder.Find(graph, "A", "C", option);
            var actual = (from edge in path select edge.Name).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindTestBestShortest()
        {
            var graph = new Graph<string>(Builder.BuildListOfEdge(new List<Tuple<string, string, int>>{
                Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 2), Tuple.Create("A", "D", 1),
                Tuple.Create("B", "A", 2), Tuple.Create("B", "C", 4), Tuple.Create("D", "C", 1) }));


            var option = new Option<string>(new IOption<string>[0]);

            var finder = new BestPathFinder<string>();

            var expected = new string[] { "AC" };

            var path = finder.Find(graph, "A", "C", option);
            var actual = (from edge in path select edge.Name).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindTestBestShortestFirst()
        {
            var graph = new Graph<string>(Builder.BuildListOfEdge(new List<Tuple<string, string, int>>{
                Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 3), Tuple.Create("A", "D", 1),
                Tuple.Create("B", "A", 3), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) }));
            
            var option = new Option<string>(new IOption<string>[0]);

            var finder = new BestPathFinder<string>();

            var expected = new string[] { "AB", "BC" };

            var path = finder.Find(graph, "A", "C", option);
            var actual = (from edge in path select edge.Name).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindTestWithOptions()
        {
            var graph = new Graph<string>(Builder.BuildListOfEdge(new List<Tuple<string, string, int>>{
                Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 3), Tuple.Create("A", "D", 1),
                Tuple.Create("B", "A", 3), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) }));

            var incEdges = new List<string>();
            var incVertexes = new List<string>();
            var excEdges = new List<string> { "AC" };
            var excVertexes = new List<string>();
            var incexc = new IncludeExclude<string>(incEdges, incVertexes, excEdges, excVertexes);
            var option = new Option<string>(new IOption<string>[] { incexc, new MaxLength<string>(2) });

            var finder = new BestPathFinder<string>();

            var expected = new string[] { "AB", "BC" };

            var path = finder.Find(graph, "A", "C", option);
            var actual = (from edge in path select edge.Name).ToArray();

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
