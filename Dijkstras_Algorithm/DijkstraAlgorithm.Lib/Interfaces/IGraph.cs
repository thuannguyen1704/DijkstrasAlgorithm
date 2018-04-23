using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraAlgorithm.Lib.Interfaces
{
    /// <summary>
    /// The contact for Graph
    /// </summary>
    /// <typeparam name="TData">For the datasource type</typeparam>
    public interface IGraph<TData>
    {
        /// <summary>
        /// Defined as a set of vertecies for the Graph.
        /// </summary>
        IDictionary<string, IVertex<TData>> Vertecies { get; }

        /// <summary>
        /// Add vertex to the Graph.
        /// </summary>
        /// <param name="vertex"></param>
        void AddVertex(IVertex<TData> vertex);

        /// <summary>
        /// Add vertecies to the Graph.
        /// </summary>
        /// <param name="vertecies"></param>
        void AddRangeVertex(IEnumerable<IVertex<TData>> vertecies);

        /// <summary>
        /// Create a link between two vertex or we can call is creation of an Edge.
        /// </summary>
        /// <param name="fromVertex">Origin vertex</param>
        /// <param name="toVertex">Target vertex</param>
        /// <param name="distance">Distance between two</param>
        bool LinkVertex(string fromVertex, string toVertex, int distance);
    }
}
