using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    private static StageGenerator _instance = null;

    private StageGenerator() { }

    public static StageGenerator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StageGenerator>();
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("StageGenerator");
                    _instance = gameObject.AddComponent<StageGenerator>();
                }
            }

            return _instance;
        }
    }

    [SerializeField] private Transform MapParent;
    [SerializeField] private Transform MapOffsetPos;
    [SerializeField] private Transform TargetLocation;
    [SerializeField] private List<LocationInfo> LocationsInRange = new List<LocationInfo>();
    public void AdjustMapPos()
    {
        Vector2 offSetDistance = (Vector2)(MapParent.position - MapOffsetPos.position);
        MapParent.position = (Vector2)(GridGenerator.Instance.CenterPoint) + offSetDistance;
    }

    bool test = true;
    private void Update()
    {

        if (test)
        {
            RefreshGrid();
            AdjustMapPos();
            SelectDestinationFromLocationInRange();
            test = false;
        }
    }

    void RefreshGrid()
    {
        Node[,] gridArray = GridGenerator.Instance.GridArray;

        Color color = ColorUtility.TryParseHtmlString("#FFFFFF4B", out Color parsedColor) ? parsedColor : Color.white;

        foreach (Node n in gridArray)
        {
            n.transform.GetComponent<SpriteRenderer>().color = color;
        }
    }

    void SelectDestinationFromLocationInRange()
    {
        Vector2 offSetDistance = (Vector2)(MapParent.position - MapOffsetPos.position);
        Vector2 _from = (Vector2)((GridGenerator.Instance.CenterPoint) + (GridGenerator.Instance.Range / 2));
        Vector2 _to = (Vector2)((GridGenerator.Instance.CenterPoint) - (GridGenerator.Instance.Range / 2));
        LocationsInRange = MapRange.Instance.GetLocationInRange(_from, _to);

        if (LocationsInRange.Count > 0)
        {
            int _randIndex = Random.Range(0, LocationsInRange.Count);

            TargetLocation = LocationsInRange[_randIndex].collidedGrid;

            TargetLocation.GetComponent<SpriteRenderer>().color = Color.blue;

            Debug.Log(_randIndex + " " + TargetLocation);
        }
        else
        {
            TargetLocation = null;
        }
    }

    public Node Destination
    {
        get
        {
            return TargetLocation.GetComponent<Node>();
        }
    }
}
