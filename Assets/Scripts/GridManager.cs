using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Node[,] graph;

    public static GridManager instance;

    public Grid<GridEntity> characterGrid;
    public Grid<GridEntity> objectGrid;

    [SerializeField] private GridCharacter selectedCharacter;
    [Space(10)]

    [SerializeField] private Vector2Int gridSize;

    private void Awake()
    {
        // If there is no instance of the grid manager, assign the singelton as this instance
        if (instance == null) { instance = this; }
    }

    private void Start()
    {
        // Create the character grid
        characterGrid = CreateGrid(gridSize.x, gridSize.y, 1.0f, transform.position);
        // Create the object grid
        objectGrid = CreateGrid(gridSize.x, gridSize.y, 1.0f, transform.position);
    }

    private Grid<GridEntity> CreateGrid(int sizeX, int sizeY, float cellSize, Vector3 origin)
    {
        return new Grid<GridEntity>(sizeX, sizeY, cellSize, origin);
    }

    public class Node
    {
        public int x;
        public int z;

        public List<Node> neighbours;

        public Node()
        {
            neighbours = new List<Node>();
        }

        public float DistanceTo(Node n)
        {
            return Vector2.Distance(
                    new Vector2(x, z),
                    new Vector2(n.x, n.z)
                );
        }
    }

    private void GeneratePathfindingGraph()
    {
        // initialize the array
        graph = new Node[gridSize.x, gridSize.y];

        // Initialize a Node for each spot in the array
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.y; z++)
            {
                graph[x, z] = new Node();
                // Allow nodes to be self-aware of their position relative to the grid
                graph[x, z].x = x;
                graph[x, z].z = z;
            }
        }

        // Now that nodes are initialized, calculate their neighbours
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.y; z++)
            {
                if (objectGrid.IsValidGridPosition(x, z))
                {
                    // Nodes are connected at their 4 edges
                    // Add the neighbours of this node to this nodes neighbour list
                    graph[x, z].neighbours.Add(graph[x - 1, z]);                
                    graph[x, z].neighbours.Add(graph[x + 1, z]);                
                    graph[x, z].neighbours.Add(graph[x, z - 1]);                
                    graph[x, z].neighbours.Add(graph[x, z + 1]);                
                }
            }
        }
    }

    /// <summary>
    /// Dijkstra pathfinding algorithm 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void GeneratePathTo(int x, int z)
    {
        // Clear out the unit's old path
        selectedCharacter.currentPath = null;

        Dictionary<Node, float> distance = new Dictionary<Node, float>();
        Dictionary<Node, Node> previousNode = new Dictionary<Node, Node>();

        // Setup the list of nodes that haven't been checked yet
        List<Node> unvisited = new List<Node>();

        Node source = graph[selectedCharacter.x, selectedCharacter.z];
        Node targetNode = graph[x, z];

        distance[source] = 0.0f;
        previousNode[source] = null;

        // Initialize everything to have INFINITY distance, since we don't know any
        // better right now. Also, it's possible that some nodes CANNOT be reached from 
        // the source hence INFINITY becomes a reasonable value to assign
        foreach (Node vertex in graph)
        {
            if (vertex != source)
            {
                distance[vertex] = Mathf.Infinity;
                // Don't know which node comes before in the chain
                previousNode[vertex] = null;
            }

            unvisited.Add(vertex);
        }

        while (unvisited.Count > 0.0f)
        {
            // "u" is the going to be the univisted node with the smallest distance
            Node u = null;
         
            foreach(Node possibleU in unvisited)
            {
                if (u == null || distance[possibleU] < distance[u])
                {
                    u = possibleU;
                }
            }

            if (u == targetNode)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node vertex in u.neighbours)
            {
                float alt = distance[u] + u.DistanceTo(vertex);
                if (alt < distance[vertex])
                {
                    distance[vertex] = alt;
                    previousNode[vertex] = u;
                }
            }
        }

        // If this point is reached, then either the shortest route has
        // been found, or there is no route AT ALL to the target

        if (previousNode[targetNode] == null)
        {
            // No route between out target node and the source node
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node currNode = targetNode;

        // Step through the "previousNode" sequence and add it to our path
        while (currNode != null)
        {
            currentPath.Add(currNode);
            currNode = previousNode[currNode];
        }

        // "currentPath" describes route from target to our source, so it needs to be inverted
        currentPath.Reverse();

        selectedCharacter.currentPath = currentPath;
    }
}
