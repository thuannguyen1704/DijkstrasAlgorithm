using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DijkstraAlgorithm.Lib.Implementations;
using DijkstraAlgorithm.Lib.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace DijkstraAlgorithm.Testing
{
    [TestFixture]
    public partial class Graph
    {
        [Test]
        public void Should_ThrowArgumentNullException_When_GivenNullVertex_AddVertex()
        {
            //Arrange
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            //Action & Assert
            Assert.Throws<ArgumentNullException>(() => graph.AddVertex(null));
        }

        [Test]
        public void Should_ThrowArgumentException_When_DuplicateVertexName_AddVertex()
        {
            //Arrange
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            //Action 
            graph.AddVertex(new Vertex<string>("A"));
            Assert.Throws<ArgumentException>(() => graph.AddVertex(new Vertex<string>("A")));
        }

        [Test]
        public void Should_ThrowArgumentException_When_GivenNameOfVertexWasNullOrEmpty_AddVertex()
        {
            //Arrange
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            IVertex<string> vertex = new Vertex<string>("");
            //Action & Assert
            Assert.Throws<ArgumentException>(() => graph.AddVertex(vertex));
        }

        [Test]
        public void Should_CreateVertexSuccess_When_GivenValidVertex_AddVertex()
        {
            //Arrange
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            IVertex<string> vertex = new Vertex<string>("A");
            //Action
            graph.AddVertex(vertex);
            //Assert
            Assert.Greater(graph.Vertecies.Count, 0);
        }

        [Test]
        public void Should_ThrowArgumentNullException_When_GivenNullVertecies_AddVertexRange()
        {
            //Arrange
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            //Action && Assert
            Assert.Throws<ArgumentNullException>(() => graph.AddRangeVertex(null));
        }

        [Test]
        public void Should_AddVertexRangeSuccess_When_GivenValidVertecies()
        {
            //Arrange
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            IList<IVertex<string>> vertecies = new List<IVertex<string>>()
            {
                new Vertex<string>("A"),
                new Vertex<string>("B")
            };
            //Action
            graph.AddRangeVertex(vertecies);
            //Assert
            Assert.AreEqual(graph.Vertecies.Count, 2);
        }

        [TestCaseSource(nameof(TestCaseForLinkVertex))]
        public void Should_LinkFail_When_FromVertexOrToVertexArgumentGetNullOrEmpty(string fromVertex, string toVertex)
        {
            //Arrange
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            //Action & Assert
            Assert.Throws<ArgumentException>(() => graph.LinkVertex(fromVertex, toVertex, 10));

        }

        [TestCaseSource(nameof(TestCaseForLinkVertexInvalidName))]
        public void Should_ThrowKeyNotFoundException_When_GivenInvalidVertexName(string fromVertex, string toVertex)
        {
            //Arrange
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            graph.AddVertex(new Vertex<string>("A"));
            graph.AddVertex(new Vertex<string>("B"));
            graph.AddVertex(new Vertex<string>("C"));
            //Action & Assert
            Assert.Throws<KeyNotFoundException>(() => graph.LinkVertex(fromVertex, toVertex, 10));
        }

        [Test]
        public void Should_LinkFail_When_GivenDuplicateLinkData()
        {
            //Arrange
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            graph.AddVertex(new Vertex<string>("A"));
            graph.AddVertex(new Vertex<string>("B"));
            graph.AddVertex(new Vertex<string>("C"));
            //Action & Assert
            graph.LinkVertex("A", "C", 10);
            Assert.Throws<ArgumentException>(() => graph.LinkVertex("A", "C", 10));
        }

        [Test]
        public void Should_LinkFail_When_GivenNeighborItSelf()
        {
            //Arrange
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            graph.AddVertex(new Vertex<string>("A"));
            graph.AddVertex(new Vertex<string>("B"));
            graph.AddVertex(new Vertex<string>("C"));
            //Action & Assert
            Assert.Throws<ArgumentException>(() => graph.LinkVertex("A", "A", 10));
        }

        [TestCaseSource(nameof(TestAddNeighborDistanceNegative))]
        public void Should_LinkFail_When_GivenNegativeDistance(string fromVertex, string toVertex, int distance)
        {
            //Arrange
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            graph.AddVertex(new Vertex<string>("A"));
            graph.AddVertex(new Vertex<string>("B"));
            graph.AddVertex(new Vertex<string>("C"));
            //Action & Assert
            Assert.Throws<ArgumentException>(() => graph.LinkVertex(fromVertex, toVertex, distance));
        }

        [TestCaseSource(nameof(TestCaseForLinkVertexValidName))]
        public void Should_LinkSuccess_When_GivenValidData(string fromVertex, string toVertex)
        {
            //Arrange
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();
            graph.AddVertex(new Vertex<string>("A"));
            graph.AddVertex(new Vertex<string>("B"));
            graph.AddVertex(new Vertex<string>("C"));
            //Action
            var result = graph.LinkVertex(fromVertex, toVertex, 10);
            //Assert
            Assert.AreEqual(result, true);
        }
    }
}
