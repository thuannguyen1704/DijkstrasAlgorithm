using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DijkstraAlgorithm.Lib.Implementations;
using DijkstraAlgorithm.Lib.Interfaces;

namespace DijkstraAlgorithm.Testing
{
    public partial class Dijkstra
    {
        public void AddVertexAndLinkForGraph<TDatasource>(IGraph<TDatasource> graph)
        {
            //Add vertex
            graph.AddVertex(new Vertex<TDatasource>("A"));
            graph.AddVertex(new Vertex<TDatasource>("B"));
            graph.AddVertex(new Vertex<TDatasource>("C"));
            graph.AddVertex(new Vertex<TDatasource>("D"));
            graph.AddVertex(new Vertex<TDatasource>("E"));

            //link vertex
            graph.LinkVertex("A", "B", 6);
            graph.LinkVertex("A", "D", 1);
            graph.LinkVertex("B", "D", 2);
            graph.LinkVertex("B", "E", 2);
            graph.LinkVertex("B", "C", 5);
            graph.LinkVertex("D", "E", 1);
            graph.LinkVertex("E", "C", 5);
        }

        private static readonly object[] TestCaseForVertexInvalidName =
        {
            new object[] {"AA", "D"},
            new object[] {"F", "G"},
            new object[] {"B", "G"},
            new object[] {"W", "Z"},
            new object[] {"A", "%"},
            new object[] {")", "("}
        };

        private static readonly object[] TestCaseForShortestPathResult =
        {
            new object[] {"A", "C", new List<string>() {"A", "D", "E", "C"}},
            new object[] {"C", "A", new List<string>() {"C", "E", "D", "A"}},
            new object[] {"A", "D", new List<string>() {"A", "D"}},
            new object[] {"D", "C", new List<string>() {"D", "E", "C"}},
            new object[] {"C", "B", new List<string>() {"C", "B"}},
            new object[] {"B", "D", new List<string>() {"B", "D"}}
        };
    }
}
