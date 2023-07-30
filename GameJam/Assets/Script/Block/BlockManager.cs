using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] BlockUnitPlaceholder[] blockUnits = new BlockUnitPlaceholder[] { };

    private void Update()
    {
    }

    public Vector3 GetTargetCenterPostiton()
    {
        return blockUnits[4].GetDetector().CurrentTarget().transform.position;
    }

    public bool IsPlacable()
    {
        foreach(BlockUnitPlaceholder _blockUnit in blockUnits)
        {
            if (!_blockUnit.GetDetector().IsPlaceable()) return false;
        }

        return true;
    }

    public void UpdateNode()
    {
        foreach (BlockUnitPlaceholder _blockUnit in blockUnits)
        {
            if (_blockUnit.GetDetector().CurrentTarget() != null)
            {
                Node _node = _blockUnit.GetDetector().CurrentTarget().GetComponent<Node>();

                if (_node)
                {
                    _node.UpdateWalkability(true);
                }
            }
        }
    }

}
