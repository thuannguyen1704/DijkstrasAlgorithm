using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DijkstraAlgorithm.Lib.Interfaces;

namespace DijkstraAlgorithm.Lib.Implementations
{

    /// <summary>
    /// Representing for a Graph that contain many vertex.
    /// </summary>
    public class Graph<TData, TNieghbor> : IGraph<TData> where TNieghbor : IVertexNeighbor<IVertex<TData>, TData>, new()
    {
        /// <inheritdoc />
        public IDictionary<string, IVertex<TData>> Vertecies { get; }

        public Graph()
        {
            Vertecies = new Dictionary<string, IVertex<TData>>();
        }

        /// <inheritdoc />
        public void AddVertex(IVertex<TData> vertex)
        {
            if (vertex == null)
            {
                throw new ArgumentNullException("vertex");
            }

            if (string.IsNullOrEmpty(vertex.Name))
            {
                throw new ArgumentException("Name of vertex can't be null or empty.");
            }

            if (Vertecies.ContainsKey(vertex.Name))
            {
                throw new ArgumentException("Key already existed.");
            }

            Vertecies.Add(vertex.Name, vertex);
        }

        /// <inheritdoc />
        public void AddRangeVertex(IEnumerable<IVertex<TData>> vertecies)
        {
            if (vertecies == null)
            {
                throw new ArgumentNullException("vertecies");
            }

            foreach (var vertex in vertecies)
            {
                Vertecies.Add(vertex.Name, vertex);
            }
        }

        /// <inheritdoc />
        public bool LinkVertex(string fromVertex, string toVertex, int distance)
        {
            LinkNeighborValidation(fromVertex, toVertex, distance);

            //Two way link
            Vertecies[fromVertex].AddNeighbor(new TNieghbor()
            {
                Target = Vertecies[toVertex],
                Distance = distance
            });

            Vertecies[toVertex].AddNeighbor(new TNieghbor()
            {
                Target = Vertecies[fromVertex],
                Distance = distance
            });
            return true;
        }

        private void LinkNeighborValidation(string fromVertex, string toVertex, int distance)
        {
            if (string.IsNullOrEmpty(fromVertex) || string.IsNullOrEmpty(toVertex))
            {
                throw new ArgumentException(string.IsNullOrEmpty(fromVertex) ? "fromVertex" : "toVertex");
            }

            if (!Vertecies.ContainsKey(fromVertex) || !Vertecies.ContainsKey(toVertex))
            {
                throw new KeyNotFoundException(
                    $"Can't find the {(!Vertecies.ContainsKey(fromVertex) ? "{" + fromVertex + "}" : "{" + toVertex + "}")}");
            }

            if (Vertecies[fromVertex].Neighbors.Any(a => a.Target.Name == toVertex))
            {
                throw new ArgumentException("This link already made.");
            }

            if (fromVertex == toVertex)
            {
                throw new ArgumentException("Can't neighbor itself.");
            }

            if (distance <= 0)
            {
                throw new ArgumentException("Distance must be positive.");
            }
        }
    }
}
