using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DijkstraAlgorithm.Lib;
using DijkstraAlgorithm.Lib.Implementations;
using DijkstraAlgorithm.Lib.Interfaces;
using NUnit.Framework;

namespace DijkstraAlgorithm.Testing
{
    [TestFixture]
    public partial class Dijkstra
    {
        [TestCaseSource(nameof(TestCaseForShortestPathResult))]
        public void Should_FoundtheShortestPath_When_InputValidGraph(string originVertex, string destinationVertex, IList<string> expectedKeys)
        {
            //Arrange
            Dijkstra<string> dijkstra = new Dijkstra<string>();
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            AddVertexAndLinkForGraph(graph);
            //Action
            var result = dijkstra.FindShortestPath(graph, originVertex, destinationVertex).Keys.ToList();
            //Assert
            CollectionAssert.AreEqual(expectedKeys, result);
        }

        [Test]
        public void Should_ThrowArgumentNullException_When_InputNullGraph()
        {
            //Arrange
            Dijkstra<string> dijkstra = new Dijkstra<string>();
            //Action && Assert
            Assert.Throws<ArgumentNullException>(() => dijkstra.FindShortestPath(null, "A", "C"));
        }

        [Test]
        public void Should_ThrowArgumentException_When_OriginPointAndDestinationAreSame()
        {
            //Arrange
            Dijkstra<string> dijkstra = new Dijkstra<string>();
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            AddVertexAndLinkForGraph(graph);
            //Action && Assert
            Assert.Throws<ArgumentException>(() => dijkstra.FindShortestPath(graph, "A", "A"));
        }

        [TestCaseSource(nameof(TestCaseForVertexInvalidName))]
        public void Should_ThrowKeyNotFoundException_When_GivenNonExistedVertexName(string originVertex, string destinationVertex)
        {
            //Arrange
            Dijkstra<string> dijkstra = new Dijkstra<string>();
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            AddVertexAndLinkForGraph(graph);
            //Action & Assert
            Assert.Throws<KeyNotFoundException>(() => dijkstra.FindShortestPath(graph, originVertex, destinationVertex));
        }
    }
}
