using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUnitPlaceholder : MonoBehaviour
{
    [SerializeField] private Detector detector;

    public bool IsActiveUnit() { return detector.IsActivatedUnit();  }

    public Detector GetDetector() { return detector; }
}
