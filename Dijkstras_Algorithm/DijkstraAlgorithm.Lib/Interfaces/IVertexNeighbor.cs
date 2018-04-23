using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm.Lib.Interfaces
{
    /// <summary>
    /// The abstraction of vertex's neighbor (the Edge), inherit this to create your own vertex's neighbor.
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TVertexType"></typeparam>
    public interface IVertexNeighbor<TVertex, TVertexType> where TVertex : IVertex<TVertexType>
    {
        /// <summary>
        /// Representing for the distance between origin vertex and target vertex.
        /// </summary>
        double Distance { get; set; }

        /// <summary>
        /// The end point of the origin vertex.
        /// </summary>
        TVertex Target { get; set; }
    }
}
