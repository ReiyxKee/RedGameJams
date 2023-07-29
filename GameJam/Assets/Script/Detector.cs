using UnityEngine;

public class Detector : MonoBehaviour
{
    private bool placeable;
    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("GridPlaceholder"))
        {
            Debug.Log("Detector has come in contact with grid placeholder!");
            placeable = true;
           
        }
        else 
        {
            placeable = false;
        }
    }

    public bool IsPlaceable()
    {
        return placeable;    
    }
}