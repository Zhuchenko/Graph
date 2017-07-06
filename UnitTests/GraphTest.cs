using System;
using System.Collections.Generic;
using System.Linq;
using Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void GirthOfGraphTestRoundTrip()
        {
            var G = Builder.Build(new List<Tuple<string, string, int>>
            { Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 1), Tuple.Create("A", "D", 1),
              Tuple.Create("B", "A", 1), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) });

            var exclude = new Options<string>(new List<string>(), new List<string>());
            var starting = new Vertex<string>("A");
            var final = new Vertex<string>("C");

            var expected = new string[][]
            { new string[]{ "AB", "BA", "AC" }, new string[]{ "AD", "DC" },
              new string[]{ "BC" }, new string[]{ "AC" }, new string[]{ "AD", "DC" } };

            int j = 0;
            foreach (var section in G.GirthOfGraph(starting, final, exclude))
            {
                int i = 0;
                foreach(var edge in section)
                {
                    Assert.AreEqual(expected[j][i], edge.Name);
                    i++;
                }
                j++;
            }
        }

        [TestMethod]
        public void GirthOfGraphTestNotRoundTrip()
        {
            var G = Builder.Build(new List<Tuple<string, string, int>>
            { Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 1), Tuple.Create("A", "D", 1),
              Tuple.Create("B", "A", 1), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) });

            var exclude = new Options<string>(new List<string> { "AC" }, new List<string> { "D" });
            var starting = new Vertex<string>("A");
            var final = new Vertex<string>("C");

            var expected = new string[]{ "AB", "BC" };

            int i = 0;
            foreach (var section in G.GirthOfGraph(starting, final, exclude))
            {
                foreach (var edge in section)
                {
                    Assert.AreEqual(expected[i], edge.Name);
                    i++;
                }
            }
        }

        [TestMethod]
        public void FindAllPathesWithOptionsTestAll()
        {
            var G = Builder.Build(new List<Tuple<string, string, int>>
            { Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 1), Tuple.Create("A", "D", 1),
              Tuple.Create("B", "A", 1), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) });

            var include = new Options<string>(new List<string>(), new List<string>());
            var exclude = new Options<string>(new List<string>(), new List<string>());
            var starting = new Vertex<string>("A");
            var final = new Vertex<string>("C");

            var expected = new string[][]
            { new string[]{ "AB", "BA", "AC" }, new string[]{ "AB", "BA", "AD", "DC" },
              new string[]{ "AB", "BC" }, new string[]{ "AC" }, new string[]{ "AD", "DC" } };

            var actual = G.FindAllPathesWithOptions(starting, final, include, exclude);

            int j = 0;
            foreach (var path in actual)
            {
                int i = 0;
                foreach (var edge in path)
                {
                    Assert.AreEqual(expected[j][i], edge.Name);
                    i++;
                }
                j++;
            }
        }

        [TestMethod]
        public void FindAllPathesWithOptionsTestNotAll()
        {
            var G = Builder.Build(new List<Tuple<string, string, int>>
            { Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 1), Tuple.Create("A", "D", 1),
              Tuple.Create("B", "A", 1), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) });

            var include = new Options<string>(new List<string>(), new List<string> { "B" });
            var exclude = new Options<string>(new List<string> { "AC" }, new List<string>());
            var starting = new Vertex<string>("A");
            var final = new Vertex<string>("C");

            var expected = new string[][]
            { new string[]{ "AB", "BA", "AD", "DC" }, new string[]{ "AB", "BC" } };

            var actual = G.FindAllPathesWithOptions(starting, final, include, exclude);

            int j = 0;
            foreach (var path in actual)
            {
                int i = 0;
                foreach (var edge in path)
                {
                    Assert.AreEqual(expected[j][i], edge.Name);
                    i++;
                }
                j++;
            }
        }

        [TestMethod]
        public void FindAllPathesWithOptionsTestNoOne()
        {
            var G = Builder.Build(new List<Tuple<string, string, int>>
            { Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 1), Tuple.Create("A", "D", 1),
              Tuple.Create("B", "A", 1), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) });

            var include = new Options<string>(new List<string> { "BC" }, new List<string>());
            var exclude = new Options<string>(new List<string>(), new List<string> { "B" });
            var starting = new Vertex<string>("A");
            var final = new Vertex<string>("C");
            

            var actual = G.FindAllPathesWithOptions(starting, final, include, exclude);
            
            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void FindBestPathWithOptionsTestBest()
        {
            var G = Builder.Build(new List<Tuple<string, string, int>>
            { Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 7), Tuple.Create("A", "D", 1),
              Tuple.Create("B", "A", 2), Tuple.Create("B", "C", 4), Tuple.Create("D", "C", 1) });

            var include = new Options<string>(new List<string>(), new List<string>());
            var exclude = new Options<string>(new List<string>(), new List<string>());
            var starting = new Vertex<string>("A");
            var final = new Vertex<string>("C");

            var expected = new string[]{ "AD", "DC" };

            var actual = G.FindBestPathWithOptions(starting, final, include, exclude);

            int i = 0;
            foreach (var edge in actual)
            {
                Assert.AreEqual(expected[i], edge.Name);
                i++;
            }
        }

        [TestMethod]
        public void FindBestPathWithOptionsTestBestShortest()
        {
            var G = Builder.Build(new List<Tuple<string, string, int>>
            { Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 2), Tuple.Create("A", "D", 1),
              Tuple.Create("B", "A", 2), Tuple.Create("B", "C", 4), Tuple.Create("D", "C", 1) });

            var include = new Options<string>(new List<string>(), new List<string>());
            var exclude = new Options<string>(new List<string>(), new List<string>());
            var starting = new Vertex<string>("A");
            var final = new Vertex<string>("C");

            var expected = new string[] { "AC" };

            var actual = G.FindBestPathWithOptions(starting, final, include, exclude);

            int i = 0;
            foreach (var edge in actual)
            {
                Assert.AreEqual(expected[i], edge.Name);
                i++;
            }
        }

        [TestMethod]
        public void FindBestPathWithOptionsTestBestShortestFirst()
        {
            var G = Builder.Build(new List<Tuple<string, string, int>>
            { Tuple.Create("A", "B", 1), Tuple.Create("A", "C", 3), Tuple.Create("A", "D", 1),
              Tuple.Create("B", "A", 3), Tuple.Create("B", "C", 1), Tuple.Create("D", "C", 1) });

            var include = new Options<string>(new List<string>(), new List<string>());
            var exclude = new Options<string>(new List<string>(), new List<string>());
            var starting = new Vertex<string>("A");
            var final = new Vertex<string>("C");

            var expected = new string[] { "AB", "BC" };

            var actual = G.FindBestPathWithOptions(starting, final, include, exclude);

            int i = 0;
            foreach (var edge in actual)
            {
                Assert.AreEqual(expected[i], edge.Name);
                i++;
            }
        }
    }
}
