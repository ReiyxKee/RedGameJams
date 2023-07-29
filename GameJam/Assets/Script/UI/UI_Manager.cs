using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI attemptText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        attemptText.text = "Attempts: " + SelectionManager.Instance.attempt.ToString() + " / " + SelectionManager.Instance.attemptCap.ToString();
    }
}
