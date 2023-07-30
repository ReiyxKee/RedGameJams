using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkablePathDetector : MonoBehaviour
{
    public Transform target;

    [SerializeField] private float raycastDistance = 0.01f;

    private void Update()
    {
        // Perform the raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, raycastDistance);

        // Check if the raycast hit something and it has the "walkable" tag
        if (hit.collider != null && hit.collider.CompareTag("walkable"))
        {
            target = hit.transform;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector2.zero * raycastDistance);
    }
}
