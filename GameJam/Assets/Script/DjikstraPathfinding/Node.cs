using UnityEngine;
using System.Collections.Generic; // Add this line to import List

public class Node : MonoBehaviour
{
    private bool isWalkable = false;
    private bool isAssignment = false;
    [SerializeField] private List<Node> neighbors = new List<Node>();

    private void Start()
    {
        Reset();
    }

    public Node()
    {
        neighbors = new List<Node>(); // Initialize the list in the constructor
    }

    // Getter method to get the walkability of the node
    public bool IsWalkable()
    {
        return isWalkable;
    }

    public void UpdateWalkability(bool isUnitActive)
    {
        isWalkable = !isUnitActive;
    }

    // Calculate the distance between this node and another node using Euclidean distance
    public int GetDistanceToNode(Node otherNode)
    {
        int dstX = Mathf.RoundToInt(Mathf.Abs(transform.position.x - otherNode.transform.position.x));
        int dstY = Mathf.RoundToInt(Mathf.Abs(transform.position.y - otherNode.transform.position.y));

        return dstX + dstY;
    }
    public List<Node> GetNeighbors() // Change the return type to List<Node>
    {
        return neighbors;
    }

    public void AddNeightbour(Node _neighbour)
    {
        neighbors.Add(_neighbour);
    }

    //private void OnMouseEnter()
    //{
    //    string NeighbourNames = "";

    //    foreach (Node n in neighbors)
    //    {
    //        NeighbourNames += n.gameObject.name + " ";
    //    }
    //   Debug.Log("I am " + this.gameObject.name + ". My Neightbours are: " + NeighbourNames);
    //}

    //private void OnMouseOver()
    //{
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        Debug.Log("Toggle Test");
    //        PathfindingControl.Instance.ToggleCheckpoint(this);
    //    }
    //}
    public void Reset()
    {
        isWalkable = false;
        isAssignment = false;
    }

    public void SetAssignmentState(bool set)
    {
        isAssignment = set;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isAssignment) return;

        if (!isWalkable) return;

        if (collision.tag != "location") return;

        PathfindingControl.Instance.ToggleCheckpoint(this);
    }
}
