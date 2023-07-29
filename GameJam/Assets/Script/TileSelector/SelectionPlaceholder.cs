using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPlaceholder : MonoBehaviour
{
    [SerializeField] private GameObject currentBlock;
    [SerializeField] private bool blockGenerated;
    [SerializeField] private List<GameObject> blockSeries;

    public void blockPlaced()
    {
        blockGenerated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!blockGenerated)
        {
            GenerateRandomBlock();

            blockGenerated = true;
        }

        Debug.Log(DragAndDrop.Instance.IsPlaced());

        if (DragAndDrop.Instance.IsPlaced())
        {
            if(currentBlock.transform.position != this.transform.position)
            {
                blockGenerated = false;
                SelectionManager.Instance.AddAttempt();
                DragAndDrop.Instance.SetPlaced(false);
            }
        }
    }

    void GenerateRandomBlock()
    {
        int rand = Random.Range(0, blockSeries.Count);

        GameObject newBlock = Instantiate(blockSeries[rand]);
        newBlock.transform.position = this.transform.position;
        currentBlock = newBlock;
    }
}
