using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class LocationInfo : MonoBehaviour
{
    
    public string locationName = "";
    public bool hasLandmarkPrep = false;
    public Transform collidedGrid;

    private void Update()
    {
        locationName = this.name;

        hasLandmarkPrep = this.transform.Find("landmark");
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
}
