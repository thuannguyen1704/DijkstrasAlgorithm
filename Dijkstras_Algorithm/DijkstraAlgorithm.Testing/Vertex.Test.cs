using System;
using DijkstraAlgorithm.Lib.Implementations;
using DijkstraAlgorithm.Lib.Interfaces;
using NUnit.Framework;

namespace DijkstraAlgorithm.Testing
{
    [TestFixture]
    public partial class Vertex
    {
        [TestCaseSource(nameof(TestAddNeighborDataType))]
        public void ShouldCreateSuccessNeighbor_When_GivenDifferenceTypeOfDataSource<TData>(IVertexNeighbor<IVertex<TData>, TData> vertexNeighbor)
        {
            //Arrange
            IVertex<TData> vertexTest = new Vertex<TData>("A");
            //Action
            vertexTest.AddNeighbor(vertexNeighbor);
            //Assert
            Assert.Greater(vertexTest.Neighbors.Count, 0);
        }

        [Test]
        public void Should_ThrowNullException_When_GivenNullNeighbor()
        {
            //Arrange
            IVertex<string> vertexTest = new Vertex<string>("A");
            //Action & Assert
            Assert.Throws<ArgumentNullException>(() => vertexTest.AddNeighbor(null));
        }
    }
}
