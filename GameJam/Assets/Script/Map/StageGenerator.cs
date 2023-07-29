using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] private Transform MapParent;
    [SerializeField] private Transform MapOffsetPos;

    public void AdjustMapPos()
    {
        Vector2 offSetDistance = (Vector2)(MapParent.position - MapOffsetPos.position);
        MapParent.position = (Vector2)(GridGenerator.Instance.CenterPoint) + offSetDistance;
    }

    private void Update()
    {
        AdjustMapPos();
    }
}
