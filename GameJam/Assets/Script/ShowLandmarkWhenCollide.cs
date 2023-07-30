using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLandmarkWhenCollide : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Landmark")
        {
            collision.transform.GetComponent<Animator>();
        }
    }
}
