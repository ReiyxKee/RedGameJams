using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private BlockUnitPlaceholder blockUnitPlaceholder; // Reference to the BlockUnitPlaceholder script
    [SerializeField] private GameObject block;
    [SerializeField] private float gridSizeX = 1f; // Width of the node, adjust this in the Inspector
    [SerializeField] private float gridSizeY = 1f; // Height of the node, adjust this in the Inspector

    private Node dynamicNode;

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        GameObject nodeObj = Instantiate(nodePrefab, Vector3.zero, Quaternion.identity);
        dynamicNode = nodeObj.GetComponent<Node>();

        bool isUnitActive = blockUnitPlaceholder.IsActiveUnit(); // Get the initial walkability state from BlockUnitPlaceholder
        dynamicNode.UpdateWalkability(isUnitActive);

        // Assuming you have a reference to the other GameObject whose hitbox size you want to use
        if (block != null)
        {
            // Get the hitbox size of the block
            Vector3 hitboxSize = block.GetComponent<BoxCollider2D>().size;

            // Set gridSizeX and gridSizeY to match the hitbox size
            gridSizeX = hitboxSize.x;
            gridSizeY = hitboxSize.y;
        }

        ConnectNeighbors();
    }

    private void ConnectNeighbors()
    {
        if (dynamicNode == null)
        {
            Debug.LogError("Dynamic node is null! Make sure the nodePrefab is assigned and contains the Node script.");
            return;
        }

        // Clear the neighbors list before adding new neighbors for the dynamicNode
        dynamicNode.GetNeighbors().Clear();

        // Add all adjacent walkable neighbors for the dynamicNode
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue; // Skip the center node (itself)

                Vector3 neighborPos = dynamicNode.transform.position + new Vector3(i * gridSizeX, j * gridSizeY, 0f);

                // Perform a raycast to check if the neighbor position is walkable
                bool isNeighborWalkable = !Physics2D.Raycast(dynamicNode.transform.position, neighborPos - dynamicNode.transform.position, Mathf.Sqrt(gridSizeX * gridSizeX + gridSizeY * gridSizeY), LayerMask.GetMask("Obstacles"));

                if (isNeighborWalkable)
                {
                    // Find the neighbor node from the grid and add it to the neighbors list of the dynamicNode
                    Node neighborNode = GetNodeFromPosition(neighborPos);
                    if (neighborNode != null)
                    {
                        dynamicNode.GetNeighbors().Add(neighborNode);
                    }
                }
            }
        }
    }

    private Node GetNodeFromPosition(Vector3 position)
    {
        // Get the collider attached to the nodePrefab
        Collider2D nodeCollider = nodePrefab.GetComponent<Collider2D>();

        // Check if the nodePrefab has a collider component
        if (nodeCollider == null)
        {
            Debug.LogError("The nodePrefab doesn't have a Collider2D component attached.");
            return null;
        }

        // Perform an OverlapPoint to find the node at the given position
        Collider2D[] colliders = Physics2D.OverlapPointAll(position, LayerMask.GetMask("Nodes"));

        // Iterate through the colliders found at the position
        foreach (Collider2D collider in colliders)
        {
            if (collider == nodeCollider)
            {
                // Found the correct node at the position
                return collider.GetComponent<Node>();
            }
        }

        // Return null if no node is found at the position
        return null;
    }

    public void VisualizePath(List<Node> path)
    {
        foreach (Node node in path)
        {
            Debug.Log("Node is now PURPLE!");
            node.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 1f);
        }
    }
}
