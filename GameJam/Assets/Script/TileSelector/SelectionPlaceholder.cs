using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPlaceholder : MonoBehaviour
{
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

            blockGenerated = false;
        }
    }

    void GenerateRandomBlock()
    {
        int rand = Random.Range(0, blockSeries.Count);

        GameObject newBlock = Instantiate(blockSeries[rand]);
        newBlock.transform.position = this.transform.position;

    }
}
