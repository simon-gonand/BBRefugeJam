using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grid : MonoBehaviour
{
    //Singleton
    public static Grid Instance;

    public uint width;
    public uint lenght;
    public uint heightMax;

    public GameObject cell;
    public float cellWidth;

    public GameObject[, ] grid;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    void Awake()
    {
        #region Make Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GenerateGrid()
    {
        ///WARNING : width & lenght must be pair
        if (width % 2 != 0) width++;
        if (lenght % 2 != 0) lenght++;

        grid = new GameObject[width * lenght, heightMax];

        float startX = -(width / 2) * cellWidth;
        float startY = (lenght / 2) * cellWidth;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                GameObject newCell = Instantiate<GameObject>(cell, new Vector3(startX, 0, startY), cell.transform.rotation);
                newCell.GetComponent<Cell>().posInGrid = new Vector3(i, j, 0);
                grid[i * width + j, 0] = newCell;
                startX += cellWidth;

            }
            startX = -(width / 2) * cellWidth;
            startY -= cellWidth;
        }
    }

    public GameObject GetCell(uint x, uint y, uint height = 0)
    {
        return grid[x * width + y, height];
    }

    

    //public GameObject


}
