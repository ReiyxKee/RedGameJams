using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private GameObject collidedTarget;
    [SerializeField] private string targetTag = "grid";
    private bool placeable;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.transform.gameObject.tag == targetTag)
        {
            collidedTarget = _collision.transform.gameObject;
            placeable = true;

        }
        else
        {
            collidedTarget = null;
            placeable = false;
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.transform.gameObject.tag == targetTag)
        {
            collidedTarget = null;
            placeable = false;
        }
    }

    public GameObject CurrentTarget()
    {
        return collidedTarget;
    }

    public bool IsActivatedUnit()
    {
        return sprite.activeInHierarchy;
    }

    public bool IsPlaceable()
    {
        if (sprite != null && !sprite.activeInHierarchy) return true;
        
        return placeable;    
    }
}