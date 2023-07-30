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

    public List<Node> FindShortestPath(Node startNode)
    {
        List<Node> finalPath = new List<Node>();

        // Iterate through each checkpoint
        for (int i = 0; i < checkpoints.Count; i++)
        {
            // Find the shortest path between the current checkpoint and the next one
            List<Node> currentPath = FindShortestPath(startNode, checkpoints[i]);

            // If there is a valid path between the checkpoints, add it to the final path
            if (currentPath != null)
            {
                finalPath.AddRange(currentPath);
                startNode = checkpoints[i]; // Set the next starting node to the current checkpoint
            }
            else
            {
                // If there is no valid path between checkpoints, return null
                return null;
            }
        }

        return finalPath;
    }

    public static List<Node> FindShortestPath(Node startNode, Node destinationNode)
    {
        Debug.Log(startNode.name);
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

            foreach (Node neighbor in currentNode.GetNeighbors())
            {
                if (visitedNodes.Contains(neighbor))
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
        List<Node> completePath = new List<Node>();
        Node current = destinationNode;
        while (current != null)
        {
            completePath.Insert(0, current);
            current = previousNodes[current];
        }

        return completePath;
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

    // Method to count the number of steps required in the path
    public static int CountStepsInPath(List<Node> path)
    {
        return path.Count - 1; // The number of steps is equal to the number of nodes minus one
    }
}
