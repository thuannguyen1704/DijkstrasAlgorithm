using System;
using System.Collections.Generic;
using DijkstraAlgorithm.Lib.Interfaces;

namespace DijkstraAlgorithm.Lib.Implementations
{

    /// <summary>
    /// Vertex class defined as a point where the lines meet in a graph.
    /// </summary>
    public class Vertex<TData> : IVertex<TData> 
    {
        /// <inheritdoc />
        public IVertex<TData> Previous { get; set; }

        /// <inheritdoc />
        public TData Data { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public double DistanceFromStart { get; set; }

        /// <inheritdoc />
        public IList<IVertexNeighbor<IVertex<TData>, TData>> Neighbors { get; set; }

        public Vertex(string vertexName, TData data)
        {
            Name = vertexName;
            Data = data;
            Neighbors = new List<IVertexNeighbor<IVertex<TData>, TData>>();
        }

        public Vertex(string vertexName)
        {
            Name = vertexName;
            Neighbors = new List<IVertexNeighbor<IVertex<TData>, TData>>();
        }

        /// <inheritdoc />
        public void AddNeighbor(IVertexNeighbor<IVertex<TData>, TData> neighbor)
        {
            if (neighbor == null)
            {
                throw new ArgumentNullException("neighbor");
            }

            Neighbors.Add(neighbor);
        }
    }
}
