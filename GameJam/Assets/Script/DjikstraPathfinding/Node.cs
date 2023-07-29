using UnityEngine;
using System.Collections.Generic; // Add this line to import List

public class Node : MonoBehaviour
{
    private bool isWalkable = true;
    [SerializeField] private List<Node> neighbors = new List<Node>();

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

    private void Update()
    {
    }
    private void OnMouseEnter()
    {
        string NeighbourNames = "";
       
        foreach (Node n in neighbors)
        {
            NeighbourNames += n.gameObject.name + " ";
        }
        //Debug.Log("I am " + this.gameObject.name + ". My Neightbours are: " + NeighbourNames);

    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Toggle Test");
            PathfindingControl.Instance.ToggleCheckpoint(this);
        }
    }
}
