using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkablePathManager : MonoBehaviour
{
    public List<Node> walkableCheck = new List<Node>();
    private void Update()
    {
        if (DragAndDrop.Instance.IsWalkCheck)
        {
            Debug.Log("Place update");
            AssignWalkableNode();
            DragAndDrop.Instance.CheckedWalk();
        }
    }


    void AssignWalkableNode()
    {
        foreach (Node n in GridGenerator.Instance.GridArray)
        {
            if (n.transform.GetComponentInChildren<WalkablePathDetector>().target != null)
            {
                Debug.Log("Found Walkable");
                walkableCheck.Add(n);
                n.UpdateWalkability(true);
            }
            else
            {
                walkableCheck.Remove(n);
                n.UpdateWalkability(false);
            }
        }
    }
}
