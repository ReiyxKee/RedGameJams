using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private static DragAndDrop _instance = null;
    private DragAndDrop() { }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static DragAndDrop Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private bool isDragging;
    [SerializeField] private bool isOverDraggableObj;
    [SerializeField] private GameObject currentDraggingTarget;
    [SerializeField] private string targetTag = "block";
    [SerializeField] private bool isPlaced;
    [SerializeField] private bool isCheckedWalk;

    private void Start()
    {
        startPosition = transform.position;
        isDragging = false;
        isPlaced = false;
    }

    private void OnMouseDown()
    {
        if (!isOverDraggableObj) return;

        if (SelectionManager.Instance.IsMaxAttempt()) return;

        StartDragging();
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            OnEndDragging();
        }
    }

    private void OnEndDragging()
    {
        BlockManager _manager = currentDraggingTarget.GetComponent<BlockManager>();
        currentDraggingTarget.transform.position = (_manager.IsPlacable()) ? _manager.GetTargetCenterPostiton() : startPosition;
        isPlaced = _manager.IsPlacable();
        isCheckedWalk = true;
        _manager.UpdateNode();
        currentDraggingTarget = null;
        isDragging = false;
    }

    private void Update()
    {
        isOverDraggableObj = IsMouseOverDraggableObject();
        if (Input.GetKeyDown(KeyCode.Mouse0)) { OnMouseDown(); };

        DragFunction();

        if (Input.GetKeyUp(KeyCode.Mouse0)) { OnMouseUp(); };

    }

    private void DragFunction()
    {
        if (!isDragging) return;

        if (currentDraggingTarget == null) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        currentDraggingTarget.transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }

    private void StartDragging()
    {
        isPlaced = false;
        isDragging = true;
    }

    private bool IsMouseOverDraggableObject()
    {
        if (isDragging) return false;
        // Convert mouse position to world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Create a 2D ray from the mouse position
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        // Check if the ray hits any collider with the target tag
        if (hit.collider != null && hit.transform.tag == targetTag)
        {
            currentDraggingTarget = hit.transform.gameObject;
            startPosition = hit.transform.gameObject.transform.position;
            return true;
        }
        else
        {
            currentDraggingTarget = null;
            return false;
        }
    }

    public bool IsDragging
    {
        get { return isDragging; }
    }

    public bool IsPlaced()
    {
        return isPlaced;
    }

    public bool IsWalkCheck
    {
        get
        {
            return isCheckedWalk;
        }
    }

    public void CheckedWalk()
    {
        isCheckedWalk = false;
    }

    public void SetPlaced(bool _val)
    {
        isPlaced = _val;
    }
}
