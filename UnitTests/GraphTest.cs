using System.Collections.Generic;
using Graph;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void GetPathesWithOptionsTestOnePath()
        {
            var G = new Graph<string>(new List<Edge<string>>
            {
                new Edge<string>(new Vertex<string>("A"), new Vertex<string>("B"), "AB0"),
                new Edge<string>(new Vertex<string>("A"), new Vertex<string>("B"), "AB1"),
                new Edge<string>(new Vertex<string>("A"), new Vertex<string>("C"), "AC"),
                new Edge<string>(new Vertex<string>("A"), new Vertex<string>("D"), "AD"),
                new Edge<string>(new Vertex<string>("B"), new Vertex<string>("A"), "BA"),
                new Edge<string>(new Vertex<string>("B"), new Vertex<string>("C"), "BC"),
                new Edge<string>(new Vertex<string>("D"), new Vertex<string>("C"), "DC"),
                new Edge<string>(new Vertex<string>("D"), new Vertex<string>("D"), "DD")
            });
            var optForEdges = new Options<string>(new List<string>(), new List<string> { "AB0", "DD" });
            var optForVertexes = new Options<string>(new List<string> { "B", "D" }, new List<string>());
            var answer = G.GetPathesWithOptions(new Vertex<string>("A"), new Vertex<string>("C"), optForEdges, optForVertexes);
            var expected = new string[] { "AB1", "BA", "AD", "DC" };
            foreach (var path in answer)
            {
                int i = 0;
                foreach (var edge in path)
                {
                    Assert.AreEqual(expected[i], edge.Name);
                    i++;
                }
            }
        }

        [TestMethod]
        public void GetPathesWithOptionsTestNoPath()
        {
            var G = new Graph<string>(new List<Edge<string>>
            {
                new Edge<string>(new Vertex<string>("A"), new Vertex<string>("B"), "AB"),
                new Edge<string>(new Vertex<string>("A"), new Vertex<string>("C"), "AC"),
                new Edge<string>(new Vertex<string>("B"), new Vertex<string>("C"), "BC")
            });
            var optForEdges = new Options<string>(new List<string>(), new List<string>());
            var optForVertexes = new Options<string>(new List<string>(), new List<string> { "A" });
            var answer = G.GetPathesWithOptions(new Vertex<string>("A"), new Vertex<string>("C"), optForEdges, optForVertexes);
            Assert.AreEqual(0, answer.Count());
        }

        [TestMethod]
        public void GetPathesWithOptionsTestAllPath()
        {
            var G = new Graph<string>(new List<Edge<string>>
            {
                new Edge<string>(new Vertex<string>("A"), new Vertex<string>("B"), "AB"),
                new Edge<string>(new Vertex<string>("A"), new Vertex<string>("C"), "AC"),
                new Edge<string>(new Vertex<string>("A"), new Vertex<string>("D"), "AD"),
                new Edge<string>(new Vertex<string>("B"), new Vertex<string>("A"), "BA"),
                new Edge<string>(new Vertex<string>("B"), new Vertex<string>("C"), "BC"),
                new Edge<string>(new Vertex<string>("D"), new Vertex<string>("C"), "DC")
            });
            var optForEdges = new Options<string>(new List<string>(), new List<string>());
            var optForVertexes = new Options<string>(new List<string>(), new List<string>());
            var answer = G.GetPathesWithOptions(new Vertex<string>("A"), new Vertex<string>("C"), optForEdges, optForVertexes);
            var expected = new string[][] 
            {
                new string[] {"AB", "BA", "AC" },
                new string[] {"AB", "BA", "AD", "DC" },
                new string[] {"AB", "BC" },
                new string[] {"AC" },
                new string[] {"AD", "DC" }
            };
            int j = 0;
            foreach (var path in answer)
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
    }
}
