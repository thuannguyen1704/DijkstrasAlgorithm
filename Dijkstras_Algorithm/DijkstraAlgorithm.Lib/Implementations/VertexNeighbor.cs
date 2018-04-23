using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DijkstraAlgorithm.Lib.Interfaces;

namespace DijkstraAlgorithm.Lib.Implementations
{
    /// <summary>
    /// Representing for links that belong to one vertex.
    /// </summary>
    public class VertexNeighbor<TVertex, TVertexType> :
        IVertexNeighbor<TVertex, TVertexType> where TVertex :
        IVertex<TVertexType>
    {

        /// <inheritdoc />
        public TVertex Target { get; set; }

        /// <inheritdoc />
        public double Distance { get; set; }

        public VertexNeighbor(TVertex target, double distance)
        {
            Target = target;
            Distance = distance;
        }

        public VertexNeighbor()
        {
        }
    }
}
