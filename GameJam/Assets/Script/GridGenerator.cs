using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class GridGenerator : MonoBehaviour
{
    [SerializeField] private bool generateGrid;
    [SerializeField] private GameObject unitGrid;
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private float gridGap;

    public bool GetGenerateGrid() 
    {
        return generateGrid; 
    }

    private void Update()
    {
       if (generateGrid)
        {
            GenerateGrid();
            generateGrid = false;
        }
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector2 localLocation = new Vector2(i * gridGap,j * gridGap);
                GameObject _unit = Instantiate(unitGrid);
                _unit.transform.parent = this.transform;
                _unit.transform.localPosition = localLocation;
            }
        }
    }

}
