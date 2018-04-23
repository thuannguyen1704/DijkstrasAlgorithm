using System;
using System.Collections.Generic;
using System.Linq;
using DijkstraAlgorithm.Lib;
using DijkstraAlgorithm.Lib.Implementations;
using DijkstraAlgorithm.Lib.Interfaces;

namespace DijkstraAlgorithm.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();

            //Add vertex

            graph.AddVertex(new Vertex<string>("A"));
            graph.AddVertex(new Vertex<string>("B"));
            graph.AddVertex(new Vertex<string>("C"));
            graph.AddVertex(new Vertex<string>("D"));
            graph.AddVertex(new Vertex<string>("E"));
            graph.AddVertex(new Vertex<string>("F"));
            graph.AddVertex(new Vertex<string>("H"));
            graph.AddVertex(new Vertex<string>("G"));

            //link vertex
            graph.LinkVertex("A", "B", 6);
            graph.LinkVertex("A", "D", 1);
            graph.LinkVertex("B", "D", 2);
            graph.LinkVertex("B", "E", 2);
            graph.LinkVertex("B", "C", 5);
            graph.LinkVertex("D", "E", 1);
            graph.LinkVertex("E", "C", 5);

            ////graph.LinkVertex("A", "B", 8, true);
            ////graph.LinkVertex("A", "D", 5, true);
            ////graph.LinkVertex("A", "C", 2, true);
            ////graph.LinkVertex("B", "D", 2, true);
            ////graph.LinkVertex("B", "F", 13, true);
            ////graph.LinkVertex("D", "C", 2, true);
            ////graph.LinkVertex("D", "E", 1, true);
            ////graph.LinkVertex("D", "F", 6, true);
            ////graph.LinkVertex("D", "G", 3, true);
            ////graph.LinkVertex("C", "E", 5, true);
            ////graph.LinkVertex("E", "G", 1, true);
            ////graph.LinkVertex("G", "F", 2, true);
            ////graph.LinkVertex("G", "H", 6, true);
            ////graph.LinkVertex("F", "H", 3, true);

            Dijkstra<string> d = new Dijkstra<string>();

            var result = d.FindShortestPath(graph, "A", "C").Keys.ToList();

            Console.WriteLine(string.Join("->", result));
        }
    }
}
