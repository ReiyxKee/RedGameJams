using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class GridGenerator : MonoBehaviour
{
    [SerializeField] private bool generateGrid;
    [SerializeField] private GameObject unitGrid;
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private float gridGap;
    [SerializeField] private int gridBorder;

    List<GameObject> Generated = new List<GameObject>();

    public bool GetGenerateGrid() 
    {
        return generateGrid; 
    }

    private void Update()
    {
       if (generateGrid)
        {
            RemoveGeneratedGrid();
            GenerateGrid();
            generateGrid = false;
        }
    }

    private void RemoveGeneratedGrid()
    {
        foreach(GameObject g in Generated)
        {
            DestroyImmediate(g);
        }

        Generated = new List<GameObject>();
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < gridSize.x + (2 * gridBorder) ; i++)
        {
            for (int j = 0; j < gridSize.y + (2 * gridBorder); j++)
            {
                Vector2 localLocation = new Vector2(i * gridGap,j * gridGap);
                GameObject _unit = Instantiate(unitGrid);
                _unit.transform.parent = this.transform;
                _unit.transform.localPosition = localLocation;

                if (isBorder(i,j))
                {
                    _unit.GetComponent<SpriteRenderer>().color = Color.clear;
                }

                Generated.Add(_unit);
            }
        }
    }

    private bool isBorder(int i, int j)
    {
        if (i < gridBorder && j < gridBorder) return true;

        if (i >= gridBorder + gridSize.x && j < gridBorder) return true;

        if (i < gridBorder && j >= gridBorder + gridSize.y) return true;

        if (i >= gridBorder + gridSize.x && j >= gridBorder + gridSize.y) return true;

        if (i < gridBorder) return true;

        if (j < gridBorder) return true;

        if (i >= gridBorder + gridSize.x) return true;

        if (j >= gridBorder + gridSize.y) return true;

        return false;
    }

}
