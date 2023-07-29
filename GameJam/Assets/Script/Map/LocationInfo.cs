using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class LocationInfo : MonoBehaviour
{
    
    public string locationName = "";
    public bool hasLandmarkPrep = false;

    private void Update()
    {
        locationName = this.name;

        hasLandmarkPrep = this.transform.Find("landmark");
    }
}
