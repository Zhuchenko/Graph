using Graph;
using Graph.Option;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class AllPathesFinderTest
    {
        [TestMethod]
        public void GirthOfGraphTestRoundTrip()
        {
            var graph = Builder.BuildGraph(new List<Tuple<string, string, int>>{
                Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 1), Tuple.Create("A", "D", 1),
                Tuple.Create("B", "A", 1), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) });
            
            var option =  new OptionComposite<string>(new IOption<string>[0]);
            
            var expected = new string[][] { 
                new string[] { "AB", "BA", "AC" }, new string[] { "AB", "BA", "AD", "DC" },
                new string[] { "AB", "BC" }, new string[] { "AC" }, new string[] { "AD", "DC" }};

            var actaul = new List<string[]>();
            var finder = new AllPathesFinder<string>();
            foreach (var path in finder.GirthOfGraph(graph, "A", "C", option))
                actaul.Add((from edge in path select edge.Name).ToArray());

            Assert.AreEqual(expected.Length, actaul.Count);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].Length, actaul[i].Length);
                for (int j = 0; j < expected[i].Length; j++)
                    Assert.AreEqual(expected[i][j], actaul[i][j]);
            }
        }

        [TestMethod]
        public void GirthOfGraphTestNotRoundTrip()
        {
            var graph = Builder.BuildGraph(new List<Tuple<string, string, int>>{
                Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 1), Tuple.Create("A", "D", 1),
                Tuple.Create("B", "A", 1), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) });


            var incEdges = new List<string> { "AC" };
            var incVertexes = new List<string> { "B" };
            var excEdges = new List<string> { "BC" };
            var excVertexes = new List<string>();
            var incexc = new IncludeExclude<string>(incEdges, incVertexes, excEdges, excVertexes);
            var option = new OptionComposite<string>(new IOption<string>[] { incexc, new MaxLength<string>(3) });

            var finder = new AllPathesFinder<string>();

            var expected = new string[] { "AB", "BA", "AC" };
            ;
            foreach (var path in finder.GirthOfGraph(graph, "A", "C", option))
            {
                var actual = (from edge in path select edge.Name).ToArray();
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void GirthOfGraphTestNoOneTrip()
        {
            var graph = Builder.BuildGraph(new List<Tuple<string, string, int>>{
                Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 1), Tuple.Create("A", "D", 1),
                Tuple.Create("B", "A", 1), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) });


            var incEdges = new List<string>();
            var incVertexes = new List<string> { "B" };
            var excEdges = new List<string> { "AC" };
            var excVertexes = new List<string> { "D" };
            var incexc = new IncludeExclude<string>(incEdges, incVertexes, excEdges, excVertexes);
            var option = new OptionComposite<string>(new IOption<string>[] { incexc, new MaxLength<string>(1) });

            var finder = new AllPathesFinder<string>();
            var actual = finder.GirthOfGraph(graph, "A", "C", option);

            Assert.AreEqual(0, actual.Count());
             
        }
    }
}
