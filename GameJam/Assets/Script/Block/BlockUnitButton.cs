using UnityEngine;
using UnityEngine.UI;


public class BlockUnitButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private bool isToggled = false;
    [SerializeField] private GameObject sprite;

    private void Start()
    {
        // Assign the onClick event to the ToggleButton function
        button.onClick.AddListener(ToggleButton);
    }

    private void ToggleButton()
    {
        // Toggle 
        isToggled = !isToggled;

        sprite.SetActive(isToggled);
        
    }
}