# Dijkstar's Algorithm C# Project

## Introduction

See this [a link](https://en.wikipedia.org/wiki/Dijkstra's_algorithm) to get more information.

## Objective

For this project I going to share with you how we implement the **Dijkstra Algorithm** using Priority Queue in C#.

If you have no idea the **Dijkstra Algorithm** is, you should read the introducion above or know some mathematics
keywords following:

### Graph
- In mathematics, a **Graph** is a set of interconnected nodes. These nodes often represent real-world objects. For example, a **Graph** may be 
used to represent a road network, with each node corresponding to a city, town, village or other place of interest. 
The connections between the nodes would represent the roads that join those places. A **Graph**'s connections may be symmetric or asymmetric, 
also known as undirected or directed respectively. A symmetric connection can be followed in both directions, such as most roads. 
An asymmetric connection can only be followed in one direction, perhaps representing a one-way street.

### Vertex
- In mathematics, and more specifically in graph theory, a **Vertex** (plural vertices) or node is the fundamental unit of which graphs are 
formed: an undirected graph consists of a set of vertices and a set of edges (unordered pairs of vertices), while a directed graph consists
of a set of vertices and a set of arcs (ordered pairs of vertices). In a diagram of a graph, a **Vertex** is usually represented by a circle 
with a label, and an edge is represented by a line or arrow extending from one **Vertex** to another. Sometimes, the call a **Vertex** is a **Node**, this is the same meaning. For Example:

<p align="center">
  <img src="https://www.mathsisfun.com/geometry/images/vertex.gif"/>
</p>


### Edge
- An edge is a line segment that joins two vertices (on the boundary or where faces meet).
For Example:
<p align="center">
  <img src="https://www.mathsisfun.com/geometry/images/edges.gif"/>
</p>

## Dijkstra's Algorithm

- Dijkstra's algorithm includes five steps that calculate the distance of each **Vertex** in a **Graph**. The steps are:

1. Initialise the graph by setting the distance(Distance from start point) for every **Vertex** to infinity, except for the starting **Vertex**, which has a distance of zero. Mark every **Vertex** in the **Graph** as unvisited.

2. Choose the current vertex. This is the unvisited **Vertex** with the smallest, non-infinite distance. If every **Vertex** in the **Graph** has been visited, or only vertecies with an infinite distance remain unvisited, the algorithm has completed. During the first iteration, the current **Vertex** is the starting point.

3. Calculate the distance (remain again, we can call it as the Distance from start point) for all of the current Vertex's unvisited neighbours by adding the current distance to the length of the connection to the neighbour. For each neighbour, if the calculated distance is shorter than the neighbour's current distance, set the distance to the new value. For example, if the current Vertex's distance from the starting point is 20 miles and the distance to a neighbour is 5 miles, the neighbour's potential distance is 25 miles. If the neighbour's distance is already less than 25 miles, it remains unchanged. If it is more than 25 miles, it's distance is overwritten and becomes 25 miles.

4. Mark the current **Vertex** as visited.

5. Repeat the process from step 2.

## Implementation Dijkstra's Algorithm
- In the remainder of the article we'll implement Dijkstra's algorithm using C#. This includes some LINQ standard query operators, which require .NET 3.5.

### Vertex Class

Firstly, I declare a generic `IVertex` interface represent for the abstraction of Vertex, this is a public generic interface with `TData` is a generic type for carrying some extra data such as primitive type and object type because may be vertex is not just a vertex, sometimes we want to declare a vertex as a City or a Transport station, etc.. So i made it for you to help you easy to use on some specific cases.

```C#

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
```

As you can see here, the `Name` property here is really important, as you know in a **Graph** we have many many **Vertex** and each **Vertex** must a unique name and mustn't be NUll because we will use it to define exactly the shortest path in a Graph. 

Next, the `TData` property, as i said it before, we use it to carry extra data for a **Vertex**.

For The `Previous` propety, it represent as the previous **Vertex** that linked to current **Vertex**. For example: Assuming that we are have a Square **Graph**, we have to find the shortest path from A to C and the vertex that we have go through are A->D->C, if we are currently stand in the **Vertex** C, the `Previous` of C is the D **Vertex** and same as D is A. 

<p align="center">
  <img src="https://lh3.googleusercontent.com/cz95qNxBAciecFpWQADTWjqBxj_pqtKclBZYkgFU0Ef3Pn170lspz0I0CoMPY-h24V8Gscz0ISYLaMGapsKmwt0enskC9T5B0g7GGcAnUdq62LtHdEqPAOPqxOP94diYTvNQt_979xUsRtMwTGRKCdbKuWkSpPTA2aIYSEBbZI6FYtYZ15EJhipncAVhfRx09uMUnzBVZ-Zoh47uAE0SZdJUQMNml3O1RW5OUR067MrsdTY0qsvmO_XkJ-Zix4bvuO8J69Ex_yCpVj-doGT_QWAl3OcDPfni6JUX10DZNQ6HtlChrfNxN0dt0z9-wdKPeXYaFJfqKZ0VDFPDUv4j99nhXxBIQ9Y1U1h2BwkBNMSZg3V50KGIENP-4tq23xKN2lfh7nQHgjWIForY4PujFDrbNVYEpqOd-l45pWiHmhBMnupMtbmg2eiTZqUFMc1I2luVm9RGgzkohAibacqqHjEzr6qkDhlWQVp3zGW_6PvOOaFJvRET4JEZQn4s-GHBE87DsegUYBAEWa4UtHgcjGUyUK2Yo0eg5wc_Ey6u9V9_2_2nAc3sSpeMkQjaTTmwu8czsDoHqeil524LJ8lfTdGxh2I0eKer0q2bPr9Y23AWiRg=w296-h276-no"/>
</p>

For the `DistanceFromStart` property, it represent as the distance from the start point to current **Vertex**. For example, Look at the triangle picture above, as we calculated before the path will be A->D->C if we are on the Vertex D the distance from start of D is 2 after that if we move to C so the distance from start of C is 6. The distance that i mentioned it above the Dijkstra's Algorithm section is belong to this property.  

The next one is `Neighbors` property, this property represent for the neighbor of a vertex. We will use it to update the distance for a vertex's neighbors.

The last property is `AddNeighbor`, this is a method for creating a new neighbor for current **Vertex**.

The `Vertex` class represent for the `IVertex` implementation.

```C#
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
        public IList<Interfaces.IVertexNeighbor<IVertex<TData>, TData>> Neighbors { get; set; }


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
```
### VertexNeighbor Class

The public `VertexNeighbor`represent for the relationship between one **Vertex** to many other Vertecies (or you can call it as a Edge). 

The Interface
```C#
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
```

The Implementation

```C#
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
```
As you guys can see here, we have another `Distance` is it different from the distance from start that i mentioned above?  Ya, it's difference, the difference between two of them is, for this distance, it represent for a distance between two **Vertex** not from the start point.

### Graph Class

The **Graph** class represent for a **Graph** which contains many verticies and methods to help us easier create vertex and link between one vertex to many vertecies.

```C#
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
```
### Dijkstra's Algorithm Class

This is the main path of this topic, the Dijkstra class represent for the implementation of Dijkstra's Algorithm which help us to find the shortest path from a origin **Vertex** to destination **Vertex**. With the data structures in place we can now implement the algorithm. To begin implement this, we should create a class name ```Dijkstra``` with generic `TData` for constraint and return the extra data from **Vertex** and add the method declaration below shown below. The method has parameters to receive a **Graph** and a starting and end point of **Vertex**. It returns a dictionary containing the names of the **Vertex** in the graph that belong to the shortest way and extra data that belong to a **Vertex**.

```C#
  public class Dijkstra<TData>
  {
       public IDictionary<string, TData> FindShortestPath(IGraph<TData> graph, string originVer, string destinationVer)
       {
       }
  }
```

Before starting the calculations, we will create a method to check some rules to make sure the necessary data is already filled and corrected.

```C#
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
```

To complete the method we'll break the algorithm into three phases and create a further, private method for each. The first phase will validate data and second phase will initialise the graph as per step 1 in the algorithm description. The final method will traverse the **Graph** and calculate the distances from origin **Vertex** to destination **Vertex** and return the resultant dictionary of **Vertex** and extra data.

```C#
  public IDictionary<string, TData> FindShortestPath(IGraph<TData> graph, string originVer, string destinationVer)
  {
            //Validate to make sure the user input valid data.
            ValidateData(graph, originVer, destinationVer);

            //Initial the distance from start for all the vetex
            InitializeGraph(graph, originVer);

            return ProcessDijkstra(graph, originVer, destinationVer);
  }
```

The initialisation step sets all of the **Vertex** distances except the starting point to infinity. The starting **Vertex** has a distance of zero.

```C#
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
```
The `ProcessDijkstra` method contains the code that traverses the **Graph** until reach to the destination **Vertex**. Rather than flagging each node as visited or unvisited, a "queue" of vertecies to be visited is generated. Initially this contains all of the vertecies. As vertecies are visited they are removed from the collection.

The while loop controls the processing of the **Vertex** queue. In each iteration the next **Vertex** to visit is obtained by retrieving the first **Vertex** from the queue when sorted by distance. The call to FirstOrDefault includes a filtering lambda expression that excludes vertecies with an infinite distance.

```C#
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
```
The next method is `UpdateVertexDistance`, which is responsible for calculating the distances for each neighbor of the current **Vertex**. To do so it first obtains the list of neighbors from the current Vertex to all unvisited neighbouring vertecies. We don't need processed neighbours as these already hold the correct shortest route distance.

The total distance to each of the neighbouring vertecies is calculated by adding the neighbor distance to the DistanceFromStart value of the current **Vertex**. If the calculated distance is shorter than the neighbours current DistanceFromStart, the property value is set to the new calculated value and `Previous` property will update as the current **Vertex**.

```C#
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
```
If the current **Vertex** is the destination, the loop will be end by setting the ```isFinish = true``` and we start the last path of finding shortest way. Now I will explain more details why we need the `Previous` property.

**Note: I will use a pattern like this "NameOfVertex(DistanceFromStart, PreviousVertexName)" that will helo you guys easier to understand and you should look at the Dijkstra's Algotithm section during the explanation**

For example, we have a square and i want to find the shortest path from A to C.

<p align="center">
  <img src="https://lh3.googleusercontent.com/r_iCvYsqO6WJQq7CaKaLYNq7c2cXae-sZp1MnfqxC-i99i8PF10G2IwcgHdnrArtD8UC9slN7G49qRaWSZe0jBQDwzx4pcYxP2-r9ozrHYRhtkn1g5bnT3FMXRLqlB2fI8P04sELSMy6VrqIAuAnKt4pzCpbBAU0nCidrQiJxuBQ0eLbfoLKUnDSplG745_IzYCE_gVPXpkNAwh8rocBEFttyJn59OqeqPmCeMfI1A0dX0S5jRKIHTH7Jif-nK2xiBgyiYNIONkERasVuIq8qiSSQHgDvgaeC9BJ6_V7TkItIHyl6sgfDZuCfeok_FU6rOISqGU3vqXGNXXNttm5hwJ1OPfGch7MXDD_TEPGDi9xYJyPXuADoG7ECrnWIHnwko3fq2E5Yq7rgMbzO_j49R1UaBl5ErJHAE41BO4uN10CiV0K-WEGxwsR90Ch7ZH1r37iD6BSTAU1LvKKQQ1ZUEZx1KVXcv2huuj7Mp6_iQrDjkE9mYLeI8RrVNdflfTFbwFFrkazmhmtRhc3DWZnQ_bT3_cmHBn-wftAaiuEE33P9BFIxzfcO37AXke7xXdDR895aILmKZIvBxqc4U6l5St0EjngyZ6M6y_sLd7e5cSJ0S4=w296-h276-no"/>
</p>
 
Now, we are currently on the `A(0, null)`, the others is `B(Infinity, null)`, `D(Infinity, nul)`, `C(Infinity, null)`. Following the Dijkstra's Algorithm section, we will calculate the neighbor of current **Vertex** is `A(0, null)` base on step 2 & 3 on Dijkstra's Algotithm section, after the calculation we have `B(5, A)`, `D(2, A)` and `C(Infinity, null)`. Then we choose `D(2, A)` because is smaller than the other, repeat the step we have `B(3, D)`, `C(5, D)`. Surely we will choose `B(3, D)` then repeat the step but unfortunately, after the calculation of `B(3, D)` we have new distance from start for `C` is 6 so we cant go on `C` from `B`, we ignore it, not update anything and keep `C(5, D)`. Finally, we only have unvisited `C(5, D)`, it dont have any unvisited nieghbor so and it is the destination one, so we will use the `Previous` property of `C` to trace back the correct shortest way, the `Previous`
 of `C(5, D)` is `D(2, A)` and `D(2, A)` is `A(0, null)` is mean we have the path C->D->A, we should do some magic to make it eaiser to read, I already add is in my code so the real result is A->D->C.


## Demostration

We can test the algorithm by creating a **Graph** and traversing it. The code below creates the **Graph** pictured below and I will try to find the shortest path from `A` to `C`.

<p align="center">
  <img src="https://lh3.googleusercontent.com/7UvRIwhScUQRyqSyNw9Tf7nTdFAPLsoYIO-kdOS8W-xXIr0gDIGLXqM5DoN48EtJXQRWWAq8KvMY24O2f4dzxX4PurkuCwrI92C8AWogGA30rxLLQhPvl52xpuBKbFX3VPy4rIxeHBtFkaGZtG9_mky5Z62jz7-tSUMM8ee5ccvcjW6Egp9qFBGK8s7TVL8xJwu_0OwF9Ap9UtJw-5UzMhbMUGyT5dy5WeeXFIhyowOT8w-umHwMYMz69sXpOKp7uWcCyXxOs_RXfbrFGhuxYLxDdZrrp0qvPimZ8QhzUdUno80OdxlBG07HQzjARBA26QrbbVOqugoA7a1-Y9YLDfXei-Goh5Hgyqzfy8B1JAPJHXhd2V6vtEQ0W5f0icoJyO4O1bkN12bf26ydZJxKFQ0pd4E5g0CZO_Telm__vwq87kbwIrzR4WSnI8_STsEPnKS0otXzFFTqsydH3yeecuRTw54ZEZSsDa2cBW4ztK7kmOyN860HoB3dlr7sxeK22k1a1HhqogsX5duUbshJzT5VTIyT4nXPBYjJBHG6mE8oC-VSIzpMXQCXMMcD-3ujm4If7sOtcGBckq8A0K3YwXP2cT8BViDFGNOkooOsDvdXsSI=w486-h319-no"/>
</p>

```C#
            IGraph<string> graph = new Graph<string, VertexNeighbor<IVertex<string>, string>>();

            //Add vertex

            graph.AddVertex(new Vertex<string>("A"));
            graph.AddVertex(new Vertex<string>("B"));
            graph.AddVertex(new Vertex<string>("C"));
            graph.AddVertex(new Vertex<string>("D"));
            graph.AddVertex(new Vertex<string>("E"));

            //link vertex
            graph.LinkVertex("A", "B", 6);
            graph.LinkVertex("A", "D", 1);
            graph.LinkVertex("B", "D", 2);
            graph.LinkVertex("B", "E", 2);
            graph.LinkVertex("B", "C", 5);
            graph.LinkVertex("D", "E", 1);
            graph.LinkVertex("E", "C", 5);

            Dijkstra<string> d = new Dijkstra<string>();

            var result = d.FindShortestPath(graph, "A", "C").Keys.ToList();

            Console.WriteLine(string.Join("->", result));
            
            /* OUTPUT A->D->E->C */
```
Thanks for reading article.
