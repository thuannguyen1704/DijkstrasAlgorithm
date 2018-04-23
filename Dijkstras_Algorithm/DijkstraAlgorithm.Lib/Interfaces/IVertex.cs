using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DijkstraAlgorithm.Lib.Implementations;

namespace DijkstraAlgorithm.Lib.Interfaces
{
    /// <summary>
    /// The abstraction of vertex, inherit from this to create your own vertex type.
    /// </summary>
    /// <typeparam name="TData">Extra data for a vertex</typeparam>
    public interface IVertex<TData>
    {
        /// <summary>
        /// Name defined as the name of vertex.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Extra data for a vertex
        /// </summary>
        TData Data { get; set; }

        /// <summary>
        /// Representing the previous vertex from this vertex.
        /// </summary>
        IVertex<TData> Previous { get; set; }

        /// <summary>
        /// Representing for the distance from the start point to this vertex. 
        /// </summary>
        double DistanceFromStart { get; set; }

        /// <summary>
        /// Representing for all the vertex that linked to this vertex.
        /// </summary>
        IList<IVertexNeighbor<IVertex<TData>, TData>> Neighbors { get; set; }


        /// <summary>
        /// The function that add a neighbor for this vertex.
        /// </summary>
        /// <param name="neighbor">Neighbor object.</param>
        void AddNeighbor(IVertexNeighbor<IVertex<TData>, TData> neighbor);

    }
}
