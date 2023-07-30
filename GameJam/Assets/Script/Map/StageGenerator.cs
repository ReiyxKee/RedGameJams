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

    [SerializeField] private LocationInfo StartLocation;
    [SerializeField] private LocationInfo TargetLocation;

    [SerializeField] private List<LocationInfo> LocationsInRange = new List<LocationInfo>();

    public bool Init = false;

    private void Update()
    {
        if (!Init)
        {
            GetNewStage();
            Init = true;
        }

        if (StartLocation && TargetLocation)
        {
            if (StartLocation.collidedGrid && TargetLocation.collidedGrid)
            {
                if(StartLocation.collidedGrid.GetComponent<SpriteRenderer>().color != Color.yellow || TargetLocation.collidedGrid.GetComponent<SpriteRenderer>().color != Color.red)
                {
                    RefreshGrid();
                    StartLocation.collidedGrid.GetComponent<SpriteRenderer>().color = Color.yellow;
                    TargetLocation.collidedGrid.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
    }

    void GetNewStage()
    {
        RefreshGrid();
        SetOffset();
        SelectDestinationFromLocationInRange();
    }

    public void SetOffset() 
    {
        RangeInfo rangeInfo = MapRange.Instance.rangeInfo;
        Vector2 viewportSize = GridGenerator.Instance.Range;

        Vector2 min = MapRange.Instance.Min;
        Vector2 max = MapRange.Instance.Max;

        float minX = min.x + (viewportSize.x/2);
        float minY = min.y - (viewportSize.y/2);

        float maxX = max.x + (viewportSize.x/2);
        float maxY = max.y + (viewportSize.y/2);

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector2 randomPosition = new Vector2(randomX, randomY);

        Debug.Log(min + " " + max + " " + randomPosition);

        MapOffsetPos.position = randomPosition;

        AdjustMapPos();
    }
    
    public void AdjustMapPos()
    {
        Vector2 offSetDistance = (Vector2)(MapParent.position - MapOffsetPos.position);
        MapParent.position = (Vector2)(GridGenerator.Instance.CenterPoint) + offSetDistance;
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
        StartLocation = null;

        TargetLocation = null;

        LocationsInRange.Clear();

        Vector2 offSetDistance = (Vector2)(MapParent.position - MapOffsetPos.position);
        Vector2 _from = (Vector2)((GridGenerator.Instance.CenterPoint) + (GridGenerator.Instance.Range / 2));
        Vector2 _to = (Vector2)((GridGenerator.Instance.CenterPoint) - (GridGenerator.Instance.Range / 2));

        try
        {
            LocationsInRange.AddRange(MapRange.Instance.GetLocationInRange(_from, _to));
        }
        catch
        {
            Debug.Log("Error");
        }
        finally
        {

            if (LocationsInRange.Count == 2)
            {
                StartLocation = LocationsInRange[0];

                TargetLocation = LocationsInRange[1];
            }
            else if (LocationsInRange.Count > 2)
            {
                int _randIndex_1 = Random.Range(0, LocationsInRange.Count);

                int _randIndex_2 = Random.Range(0, LocationsInRange.Count); ;

                do
                {
                    _randIndex_2 = Random.Range(0, LocationsInRange.Count);
                }
                while (_randIndex_2 == _randIndex_1);

                Debug.Log("Rand 1 " + _randIndex_1 + " 2 " + _randIndex_2);

                StartLocation = LocationsInRange[_randIndex_1];

                TargetLocation = LocationsInRange[_randIndex_2];

                StartTravel.Instance.Chara.transform.position = StartLocation.transform.position;
            }
            else
            {
                GetNewStage();
            }
        }
    }

    public Node Destination
    {
        get
        {
            return TargetLocation.collidedGrid.GetComponent<Node>();
        }
    }
    public Node Start
    {
        get
        {
            return StartLocation.collidedGrid.GetComponent<Node>();
        }
    }
}
