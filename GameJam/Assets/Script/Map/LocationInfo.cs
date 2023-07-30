using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class LocationInfo : MonoBehaviour
{
    
    public string locationName = "";
    public bool hasLandmarkPrep = false;
    public Transform collidedGrid;
    public GameObject NameTagPrefab;

    private void Start()
    {
        if (this.transform.Find("NameTag"))
        {
            this.GetComponentInChildren<TextMeshProUGUI>().text = locationName.ToUpper();
        }
        else
        {
            GameObject obj = Instantiate(NameTagPrefab, this.transform);
            obj.name = "NameTag";
            obj.GetComponentInChildren<TextMeshProUGUI>().text = locationName.ToUpper();
        }
    }

    private void Update()
    {
        locationName = this.name.ToUpper();

        hasLandmarkPrep = this.transform.Find("landmark");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "grid")
        {
            collidedGrid = collision.transform;
        }
        else
        {
            collidedGrid = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "grid")
        {
            collidedGrid = collision.transform;
        }
        else
        {
            collidedGrid = null;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        collidedGrid = null;
    }
}
