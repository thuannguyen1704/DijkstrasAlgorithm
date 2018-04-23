using System;
using DijkstraAlgorithm.Lib.Implementations;
using DijkstraAlgorithm.Lib.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DijkstraAlgorithm.Testing
{
    public partial class Vertex
    {
        static Random randomDistance = new Random();

        private static object[] TestAddNeighborDataType =
        {
            new object[] {new VertexNeighbor<IVertex<string>, string>() {Distance = randomDistance.Next(0, int.MaxValue), Target = new Vertex<string>("A") }},
          new object[] {new VertexNeighbor<IVertex<int>, int>() {Distance = randomDistance.Next(0, int.MaxValue), Target = new Vertex<int>("A") }},
          new object[] {new VertexNeighbor<IVertex<char>, char>() {Distance = randomDistance.Next(0, int.MaxValue), Target = new Vertex<char>("A") }},
          new object[] {new VertexNeighbor<IVertex<bool>, bool>() {Distance = randomDistance.Next(0, int.MaxValue), Target = new Vertex<bool>("A") }},
          new object[] {new VertexNeighbor<IVertex<object>, object>() {Distance = randomDistance.Next(0, int.MaxValue), Target = new Vertex<object>("A") }},
        };
    }
}
