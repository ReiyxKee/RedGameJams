using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class GridGenerator : MonoBehaviour
{
     private static GridGenerator instance;
     public static GridGenerator Instance
    {
        get { return instance; }
    }

      private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private bool generateGrid;
    [SerializeField] private GameObject unitGrid;
    [SerializeField] public const int gridSize_x = 9;
    [SerializeField] public const int gridSize_y = 18;
    [SerializeField] private float gridGap;
    protected int gridBorder = 0;
    [SerializeField] private GameObject[,] gridArrays = new GameObject[9,18] { { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null }, { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null } };
    [SerializeField] List<GameObject> Generated = new List<GameObject>();

    public bool GetGenerateGrid() 
    {
        return generateGrid; 
    }

    private void Update()
    {
       if (generateGrid)
        {
            gridArrays = new GameObject[gridSize_x , gridSize_y];
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

        for (int i = 0; i < gridSize_x; i++)
        {
            for (int j = 0; j < gridSize_y; j++)
            {
                Vector2 localLocation = new Vector2(i * gridGap,j * gridGap);
                GameObject _unit = Instantiate(unitGrid);
                _unit.name = "Grid_" + i.ToString() + "_" + j.ToString();
                _unit.transform.parent = this.transform;
                _unit.transform.localPosition = localLocation;

                _unit.AddComponent<Node>();

                if (isBorder(i,j))
                {
                    _unit.GetComponent<SpriteRenderer>().color = Color.clear;
                }

                gridArrays[i,j] = _unit;

                Generated.Add(_unit);
            }
        }

        AutoAssignNeightbour();
    }

    private void AutoAssignNeightbour()
    {
        for(int i = 0; i < gridSize_x; i++)
        {
            for (int j = 0; j < gridSize_y; j++)
            {
                if ( i - 1 >= 0)
                {
                    Debug.Log("Add Neightbour");
                    gridArrays[i,j].GetComponent<Node>().AddNeightbour(gridArrays[i-1,j].GetComponent<Node>());
                }

                if (i + 1 < gridSize_x)
                {
                    Debug.Log("Add Neightbour");
                    gridArrays[i,j].GetComponent<Node>().AddNeightbour(gridArrays[i + 1,j].GetComponent<Node>());
                }

                if (j - 1 >= 0)
                {
                    Debug.Log("Add Neightbour");
                    gridArrays[i,j].GetComponent<Node>().AddNeightbour(gridArrays[i,j - 1].GetComponent<Node>());
                }

                if (j + 1 < gridSize_y)
                {
                    Debug.Log("Add Neightbour");
                    gridArrays[i,j].GetComponent<Node>().AddNeightbour(gridArrays[i,j + 1].GetComponent<Node>());
                }
            }
        }
    }


    public Node[,] GridArray
    {
        get {
            Node[,] nodeArray = new Node[gridSize_x, gridSize_y];


            for (int i = 0; i < gridSize_x; i++)
            {
                for (int j = 0; j < gridSize_y; j++)
                {
                    nodeArray[i, j] = gridArrays[i, j].GetComponent<Node>();
                }
            }
           
            return nodeArray;
        }
    }

    private bool isBorder(int i, int j)
    {
        if (i < gridBorder && j < gridBorder) return true;

        if (i >= gridBorder + gridSize_x && j < gridBorder) return true;

        if (i < gridBorder && j >= gridBorder + gridSize_y) return true;

        if (i >= gridBorder + gridSize_x && j >= gridBorder + gridSize_y) return true;

        if (i < gridBorder) return true;

        if (j < gridBorder) return true;

        if (i >= gridBorder + gridSize_x) return true;

        if (j >= gridBorder + gridSize_y) return true;

        return false;
    }

}
