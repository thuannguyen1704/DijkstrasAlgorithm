using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using DijkstraAlgorithm.Lib.Implementations;
using DijkstraAlgorithm.Lib.Interfaces;

namespace DijkstraAlgorithm.Lib
{
    /// <summary>
    /// This is a implementation of Dijkstra Algorithm.
    /// </summary>
    public class Dijkstra<TData>
    {
        /// <summary>
        /// Finding the shortest path in a Graph.
        /// </summary>
        /// <param name="graph">The target Graph</param>
        /// <param name="originVer">The origin vertex</param>
        /// <param name="destinationVer">The destination vertex</param>
        /// <returns>Returning a dictionary that contain the vertex name as a key and value as TData type.</returns>
        public IDictionary<string, TData> FindShortestPath(IGraph<TData> graph, string originVer, string destinationVer)
        {
            //Validate to make sure the user input valid data.
            ValidateData(graph, originVer, destinationVer);

            //Initial the distance from start for all the vetex
            InitializeGraph(graph, originVer);

            return ProcessDijkstra(graph, originVer, destinationVer);
        }

        /// <summary>
        /// Calculating the shortest path for the target Graph.
        /// </summary>
        /// <param name="graph">The target Graph</param>
        /// <param name="originVer">The origin vertex</param>
        /// <param name="destinationVer">The destination vertex</param>
        /// <returns>Returning the set of vertex's name that represent for the shortest path</returns>
        private IDictionary<string, TData> ProcessDijkstra(IGraph<TData> graph, string originVer, string destinationVer)
        {

            //condition for the while loop and make sure we visit all the vertex in the Graph.
            bool isFinish = false;
            IDictionary<string, TData> shortestPathResult = new Dictionary<string, TData>();
            var vertecies = graph.Vertecies.Values.ToList();

            //Add origin Vertex as the first result in the shortest path.
            shortestPathResult.Add(originVer, graph.Vertecies[originVer].Data);

            while (!isFinish)
            {
                //I get the vertex that have smallest distance from start in the unvisited list, i assume this line of code as the Priority Queue.
                var currentVertex = vertecies.OrderBy(a => a.DistanceFromStart)
                    .FirstOrDefault(a => !double.IsInfinity(a.DistanceFromStart));

                //Calculate distance from start for each vertex in unvisited list.
                UpdateVertexDistance(currentVertex, vertecies);

                //when we reach to the destination vertex and we nearly find the shortest path this condition will be satisfied .
                if (currentVertex != null && destinationVer == currentVertex.Name)
                {
                    //end the while loop
                    isFinish = true;
                    //calculate the shortest path from the Graph
                    CalculateShortestPath(shortestPathResult, currentVertex);
                }

                //I remove current vertex because it will be visited.
                vertecies.Remove(currentVertex);
            }
            return shortestPathResult;
        }

        /// <summary>
        /// Validate data for make sure the user input valid data.
        /// </summary>
        /// <param name="graph">Graph data</param>
        /// <param name="originVer">The start point for the shortest path.</param>
        /// <param name="destinationVer">The end point for the shortest path</param>
        private static void ValidateData(IGraph<TData> graph, string originVer, string destinationVer)
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }

            if (string.IsNullOrEmpty(originVer) || string.IsNullOrEmpty(destinationVer))
            {
                throw new ArgumentException("The origin and destination vertex can't be null or empty.");
            }

            if (!graph.Vertecies.ContainsKey(originVer) || !graph.Vertecies.ContainsKey(destinationVer))
            {
                throw new KeyNotFoundException(
                    $"Key name {(!graph.Vertecies.ContainsKey(originVer) ? "{" + originVer + "}" : "{" + destinationVer + "}")} can't be found.");
            }
            if (originVer == destinationVer)
            {
                throw new ArgumentException("Can't find shortest path for itself.");
            }
        }

        /// <summary>
        /// Defining the shortest path in the Graph by destination vertex.
        /// </summary>
        /// <param name="shortestPathResult">Representing as the shortest path result</param>
        /// <param name="destinationVertex">The destination vertex</param>
        /// <returns>A set of vertex name that represent for the shortest path</returns>
        private void CalculateShortestPath(IDictionary<string, TData> shortestPathResult, IVertex<TData> destinationVertex)
        {
            IDictionary<string, TData> tempResult = new Dictionary<string, TData>();
            //Tracing the previous vertex to get the shortest path's details
            while (destinationVertex.Previous != null)
            {
                tempResult.Add(destinationVertex.Name, destinationVertex.Data);
                //move to next previous vertex
                destinationVertex = destinationVertex.Previous;
            }

            //reverse the data to have a good looking path
            var keys = tempResult.Keys.ToList();
            for (int i = keys.Count - 1; i >= 0 ; i--)
            {
                shortestPathResult.Add(keys[i], tempResult[keys[i]]);
            }
        }

        /// <summary>
        /// Calculating a new distance from start for each vertex in the unvisited list.
        /// </summary>
        /// <param name="currentVertex">Current Vertex that i base on.</param>
        /// <param name="vertecies">The unvisited vertex in the Graph</param>
        private void UpdateVertexDistance(IVertex<TData> currentVertex, List<IVertex<TData>> vertecies)
        {
            //Get all the unvisited link (neighbors) from the current Vertex.
            var neighbors = currentVertex.Neighbors.Where(a => vertecies.Contains(a.Target));

            //Going through all the link and calculate the new distance from start for each of them.
            foreach (var nei in neighbors)
            {
                //If the new distance from start is bigger than the current one, we ignore it and opposite.
                double distance = currentVertex.DistanceFromStart + nei.Distance;
                if (distance < nei.Target.DistanceFromStart)
                {
                    nei.Target.DistanceFromStart = distance;

                    //We save the previous vertex for target vertex because we need it for tracing back to the start point
                    //and choose the right path after we reach to the destination vertex.
                    nei.Target.Previous = currentVertex;
                }
            }
        }

        /// <summary>
        /// Initial distance from start for all the vertex that existed in the Graph.
        /// </summary>
        /// <param name="graph">The target Graph</param>
        /// <param name="startingVer">The starting vertex</param>
        private void InitializeGraph(IGraph<TData> graph, string startingVer)
        {
            //Set all the vertex's distanceFromStart property to Infinity (must be Positive infinity)
            foreach (var vertex in graph.Vertecies.Values)
            {
                var ver =  vertex;
                ver.DistanceFromStart = double.PositiveInfinity;
            }

            //Set the origin's distanceFromStart property to Zero because is a start point for the shortest path that we a going to find.
            graph.Vertecies[startingVer].DistanceFromStart = 0;
        }
    }
}
