using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DijkstraAlgorithm.Lib.Interfaces;

namespace DijkstraAlgorithm.Testing
{
    public partial class Graph
    {
        private static readonly object[] TestCaseForLinkVertex =
        {
            new object[] {"", ""},
            new object[] {"", "A"},
            new object[] {"A", ""},
            new object[] {"", null},
            new object[] {null, ""},
            new object[] {null, null},
            new object[] {"A", null},
            new object[] {null, "A"}
        };

        private static readonly object[] TestCaseForLinkVertexInvalidName =
        {
            new object[] {"D", "D"},
            new object[] {"D", "A"},
            new object[] {"A", "D"},
            new object[] {"F", "G"},
            new object[] {"B", "G"},
            new object[] {"W", "Z"},
            new object[] {"A", "%"},
            new object[] {")", "("}
        };

        private static readonly object[] TestCaseForLinkVertexValidName =
        {
            new object[] {"A", "B"},
            new object[] {"B", "C"},
            new object[] {"C", "A"},
        };


        private static object[] TestAddNeighborDistanceNegative()
        {
            var result = new object[100];
            Random randomDistance = new Random();
            for (int i = 0; i < 100; i++)
            {
                result[i] = new object[] {"A", "C",  randomDistance.Next(0, int.MaxValue) * -1 };
            }
            return result;
        }
    }
}
