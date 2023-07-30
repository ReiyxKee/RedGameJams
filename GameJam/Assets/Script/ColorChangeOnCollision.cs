using UnityEngine;

public class ColorChangeOnCollision : MonoBehaviour
{
    [SerializeField] private Color hoverColor; // The color to change to when colliding with a block.
    [SerializeField] public Color originalColor; // The original color of the GameObject.
    private bool isCollidingWithBlock; // Flag to track if colliding with a "block" GameObject.

    [SerializeField] private string targetTag = "detector";

    private void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color; 
    }


    public void ChangeToHoverColor()
    {
        GetComponent<SpriteRenderer>().color = isCollidingWithBlock ? hoverColor : originalColor;
    }


    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.transform.gameObject.tag == targetTag)
        {
            if (_collision.transform.gameObject.GetComponent<Detector>().IsActivatedUnit())
            {
                isCollidingWithBlock = true;
                ChangeToHoverColor();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        if (_collision.transform.gameObject.tag == targetTag)
        {
            isCollidingWithBlock = false;
            ChangeToHoverColor();
        }
    }

    private void Update()
    {
        if(GetComponent<SpriteRenderer>().color != hoverColor)
        {
            originalColor = GetComponent<SpriteRenderer>().color;
        }
    }
}