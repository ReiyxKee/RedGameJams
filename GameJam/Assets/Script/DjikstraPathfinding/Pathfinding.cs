using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private static Pathfinding instance;
    public static Pathfinding Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Pathfinding();
            }
            return instance;
        }
    }

    private Pathfinding() { }

    private List<Node> checkpoints = new List<Node>();

    // Method to add nodes as checkpoints
    public void AddCheckpoint(Node checkpointNode)
    {
        if (!checkpoints.Contains(checkpointNode))
        {
            checkpoints.Add(checkpointNode);
        }
    }
    public void RemoveCheckpoint(Node checkpointNode)
    {
        if (checkpoints.Contains(checkpointNode))
        {
            checkpoints.Remove(checkpointNode);
        }
    }

    public static List<Node> FindShortestPath(Node startNode, Node destinationNode)
    {
        // Dictionary to store the shortest distance from the start node to each node
        Dictionary<Node, int> distances = new Dictionary<Node, int>();

        // Dictionary to store the previous node in the shortest path
        Dictionary<Node, Node> previousNodes = new Dictionary<Node, Node>();

        // List to keep track of nodes that have been visited
        List<Node> visitedNodes = new List<Node>();

        // Get all nodes from the GridGenerator's 2D array
        Node[,] allNodes = GridGenerator.Instance.GridArray;

        // Initialize distances to infinity and previous nodes to null
        foreach (var node in allNodes)
        {
            distances[node] = int.MaxValue;
            previousNodes[node] = null;
        }

        // Set distance to start node as 0
        distances[startNode] = 0;

        while (visitedNodes.Count < allNodes.Length)
        {
            Node currentNode = GetClosestNode(distances, visitedNodes);

            if (currentNode == destinationNode)
                break;

            visitedNodes.Add(currentNode);

            foreach (Node neighbor in GetNeighbors(currentNode, allNodes))
            {
                if (!neighbor.IsWalkable() || visitedNodes.Contains(neighbor))
                    continue;

                int tentativeDistance = distances[currentNode] + currentNode.GetDistanceToNode(neighbor);
                if (tentativeDistance < distances[neighbor])
                {
                    distances[neighbor] = tentativeDistance;
                    previousNodes[neighbor] = currentNode;
                }
            }
        }

        // Reconstruct the path from the destination node to the start node
        List<Node> shortestPath = new List<Node>();
        Node current = destinationNode;
        while (current != null)
        {
            shortestPath.Insert(0, current);
            current = previousNodes[current];
        }

        return shortestPath;
    }

    // Helper method to get the closest node that has not been visited yet
    private static Node GetClosestNode(Dictionary<Node, int> distances, List<Node> visitedNodes)
    {
        int minDistance = int.MaxValue;
        Node closestNode = null;
        foreach (var pair in distances)
        {
            if (pair.Value < minDistance && !visitedNodes.Contains(pair.Key))
            {
                minDistance = pair.Value;
                closestNode = pair.Key;
            }
        }
        return closestNode;
    }

    // Method to find the neighbors of a node based on the 2D array of nodes
    private static List<Node> GetNeighbors(Node node, Node[,] allNodes)
    {
        List<Node> neighbors = new List<Node>();
        int x = Mathf.RoundToInt(node.transform.position.x);
        int y = Mathf.RoundToInt(node.transform.position.y);

        // Check all four directions (up, down, left, right)
        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };

        for (int i = 0; i < dx.Length; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];

            if (nx >= 0 && nx < allNodes.GetLength(0) &&
                ny >= 0 && ny < allNodes.GetLength(1))
            {
                Node neighborNode = allNodes[nx, ny];
                neighbors.Add(neighborNode);
            }
        }

        return neighbors;
    }

    // Method to count the number of steps required in the path
    public static int CountStepsInPath(List<Node> path)
    {
        return path.Count - 1; // The number of steps is equal to the number of nodes minus one
    }
}
