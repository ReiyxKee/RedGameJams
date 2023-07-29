using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    private bool isDragging;
    private bool isOverDraggableObj;
    [SerializeField] private GameObject currentDraggingTarget;
    [SerializeField] private string targetTag = "Block";

    private void Start()
    {
        startPosition = transform.position;
        isDragging = false;
    }

    private void OnMouseDown()
    {
        if (!isOverDraggableObj) return;

        StartDragging();
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            // Reset the GameObject to its original position
            currentDraggingTarget.transform.position = startPosition;
            currentDraggingTarget = null;
            isDragging = false;
        }
    }

    private void Update()
    {
        isOverDraggableObj = IsMouseOverDraggableObject();
        if (Input.GetKeyDown(KeyCode.Mouse0)) { OnMouseDown(); };
        if (Input.GetKeyUp(KeyCode.Mouse0)) { OnMouseUp(); };
        DragFunction();
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
        if (hit.collider != null && hit.transform.CompareTag(targetTag))
        {
            currentDraggingTarget = hit.transform.gameObject;
            startPosition = hit.transform.gameObject.transform.position;
            return true; // The mouse is over a GameObject with the target tag
        }

        currentDraggingTarget = null;
        return false; // The mouse is not over any GameObject with the target tag
    }
}
