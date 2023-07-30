using UnityEngine;
using System.Collections.Generic;

public class PathfindingControl : MonoBehaviour
{
    private static PathfindingControl instance;
    public static PathfindingControl Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PathfindingControl>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(PathfindingControl).Name);
                    instance = singletonObject.AddComponent<PathfindingControl>();
                }
            }
            return instance;
        }
    }

    private PathfindingControl() { }


    private void Awake()
    {
    
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public GameObject unitBlockPrefab; // Reference to the UnitBlock prefab in the Unity Editor

    private Node startNode; // Set this reference to your starting node
    private Node destinationNode; // Set this reference to your destination node
    [SerializeField] private List<Node> checkpoints = new List<Node>();

    // Use this method to initiate the pathfinding process and get the path list
    public List<Node> FindAndPrintPath()
    {
        Node[,] gridArray = GridGenerator.Instance.GridArray;

        foreach(Node node in gridArray)
        {
            node.SetAssignmentState(true);
        }

        startNode = StageGenerator.Instance.Start;
        destinationNode = StageGenerator.Instance.Destination;

        Debug.Log(startNode);
        Debug.Log(destinationNode);

        checkpoints.Add(destinationNode);

        // Create a list to store the complete path
        List<Node> completePath = new List<Node>();

        // Start the path from the starting node
        completePath.Add(startNode);

        // Add each checkpoint in the order they were dropped
        foreach (Node checkpoint in checkpoints)
        {
            // Find the path from the last added node to the checkpoint
            List<Node> segmentPath = Pathfinding.FindShortestPath(completePath[completePath.Count - 1], checkpoint);
            completePath.AddRange(segmentPath.GetRange(1, segmentPath.Count - 1));
        }

        // Print the names of the nodes in the complete path
        string resultPath = "Complete Path: ";

        foreach (Node node in completePath)
        {
            resultPath += (node.transform.name) + " -> ";
            node.transform.GetComponent<SpriteRenderer>().color = Color.green;
        }

        Debug.Log(resultPath);

        //// Get the number of steps required in the complete path
        //int stepsRequired = Pathfinding.CountStepsInPath(completePath);
        //Debug.Log("Steps Required: " + stepsRequired);

        //// Calculate the outcome of the shortest route
        //float outcome = CalculateOutcome(completePath);
        //Debug.Log("Outcome of Shortest Route: " + outcome);

        return completePath;
    }

    private float CalculateOutcome(List<Node> path)
    {
        float outcome = 0f;

        for (int i = 0; i < path.Count - 1; i++)
        {
            Node current = path[i];
            Node next = path[i + 1];
            int distance = current.GetDistanceToNode(next); // Modify this based on how you calculate distance between nodes
            float weight = GetNodeWeight(next); // Implement this method to get the weight of the node
            outcome += distance * weight;
        }

        return outcome;
    }
    private float GetNodeWeight(Node node)
    {
        return 1.0f;
    }

    public void AddCheckpoint(Node _targetNode)
    {
        checkpoints.Add(_targetNode);
    }
    public void RemoveCheckpoint(Node _targetNode)
    {
        checkpoints.Remove(_targetNode);
    }
    public void ToggleCheckpoint(Node _targetNode)
    {
        if (checkpoints.Contains(_targetNode))
        {
            Debug.Log("Already Exist, Remove Checkpoint " + _targetNode.name);
            _targetNode.transform.GetComponent<SpriteRenderer>().color = Color.white;
            RemoveCheckpoint(_targetNode);
        }
        else
        {
            Debug.Log("Add Checkpoint " + _targetNode.name);
            _targetNode.transform.GetComponent<SpriteRenderer>().color = Color.yellow;
            AddCheckpoint(_targetNode);
        }
    }
    void Update()
    {
        // Example usage to test the FindAndPrintPath method
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FindAndPrintPath();
        }
    }


}