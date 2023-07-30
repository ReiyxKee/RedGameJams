using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject WorldPanel;
    public void ShowPause()
    {
        PausePanel.SetActive(true);
    }


    public void HidePause()
    {
        PausePanel.SetActive(false);
    }

    public void ShowWorld()
    {
        WorldPanel.SetActive(true);
    }
    public void HideWorld()
    {
        WorldPanel.SetActive(false);
    }
}
