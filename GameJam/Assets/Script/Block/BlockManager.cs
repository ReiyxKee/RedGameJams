using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] BlockUnitPlaceholder[] blockUnits = new BlockUnitPlaceholder[] { };

    private void Update()
    {
    }

    bool IsPlacable()
    {
        foreach(BlockUnitPlaceholder _blockUnit in blockUnits)
        {
            if (!_blockUnit.GetDetector().IsPlaceable()) return false;
        }

        return true;
    }

}
