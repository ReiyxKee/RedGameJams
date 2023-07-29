using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] SelectionPlaceholder[] generators = new SelectionPlaceholder[] { };
    [SerializeField] int attempt = 0;
    [SerializeField] int attemptCap = 8;

    public void AddAttempt()
    {
        attempt++;
    }

    public bool IsMaxAttempt()
    {
        return attempt < attemptCap;
    }

    public void RefreshAll()
    {
        foreach (SelectionPlaceholder s in generators)
        {
            s.blockPlaced();
        }
    }
}
