using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLandmarkWhenCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "player")
        {
            this.GetComponent<Animator>().SetTrigger("Show");
        }
    }
}
