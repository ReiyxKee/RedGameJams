using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapRange : MonoBehaviour
{
    private static MapRange _instance = null;

    private MapRange() { }

    public static MapRange Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MapRange>();
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject("MapRange");
                    _instance = gameObject.AddComponent<MapRange>();
                }
            }

            return _instance;
        }
    }

    [SerializeField] private bool UpdateInfo = true;
    [SerializeField] private List<LocationInfo> locations = new List<LocationInfo>();
    [SerializeField] private List<LocationInfo> locationInRange = new List<LocationInfo>();
    [SerializeField] private Vector2 r_From;
    [SerializeField] private Vector2 r_To;

    [SerializeField] private GameObject r_TopLeft;
    [SerializeField] private GameObject r_TopRight;
    [SerializeField] private GameObject r_BtmLeft;
    [SerializeField] private GameObject r_BtmRight;
    [SerializeField] private RangeInfo _rangeInfo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateInfo)
        {
            r_From = r_TopLeft.transform.position;
            r_To = r_BtmRight.transform.position;

            LocationInRange();

            _rangeInfo = new RangeInfo(r_TopRight, r_TopLeft, r_BtmRight, r_BtmLeft);
            UpdateInfo = false;
        }
    }

    public List<LocationInfo> LocationsInRange
    {
        get
        {
            return locationInRange;
        }
    }

    public RangeInfo rangeInfo
    {
        get
        {
            return rangeInfo;
        }
    }

    void LocationInRange()
    {
        locationInRange = new List<LocationInfo>();

        foreach (LocationInfo loc in locations)
        {
            if (checkCoordinate(loc.transform.position))
            {
                locationInRange.Add(loc);
            }
        }
    }

    public List<LocationInfo> GetLocationInRange(Vector2 _r_From, Vector2 _r_To)
    {
        List<LocationInfo> _locationInRange = new List<LocationInfo>();

        foreach (LocationInfo loc in locations)
        {
            if (checkCustomCoordinate(loc.transform.position, _r_From, _r_To))
            {
                _locationInRange.Add(loc);
            }
        }

        return _locationInRange;
    }

    bool checkCustomCoordinate(Vector2 target, Vector2 _r_From, Vector2 _r_To)
    {
        if (target.x < _r_From.x) return false;

        if (target.x > _r_To.x) return false;

        if (target.y > _r_From.y) return false;

        if (target.y < _r_To.y) return false;

        return true;
    }

    bool checkCoordinate(Vector2 target)
    {
        if (target.x < r_From.x) return false;

        if (target.x > r_To.x) return false;

        if (target.y > r_From.y) return false;

        if (target.y < r_To.y) return false;

        return true;
    }
}

[System.Serializable]
public class RangeInfo
{
    private Vector2 point_TR;
    private Vector2 point_TL;
    private Vector2 point_BR;
    private Vector2 point_BL;

    private Vector2 range;

    public RangeInfo(GameObject _TR, GameObject _TL, GameObject _BR, GameObject _BL)
    {
        point_TR = _TR.transform.position;
        point_TL = _TL.transform.position;
        point_BR = _BR.transform.position;
        point_BL = _BL.transform.position;

        range = new Vector2(Mathf.Abs(point_TL.x - point_BR.x), Mathf.Abs(point_TL.y - point_BR.y));
    }

    public float Width
    {
        get
        {
            return range.x;
        }
    }

    public float Height
    {
        get
        {
            return range.y;
        }
    }

    public Vector2 CenterPoint
    {
        get
        {
            return new Vector2(point_TL.x + range.x / 2, point_TL.y + range.y / 2);
        }
    }


}