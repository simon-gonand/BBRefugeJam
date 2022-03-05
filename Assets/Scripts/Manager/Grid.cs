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
                newCell.GetComponent<Cell>().posInGrid = new Vector3(i, 0, j);
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

    public GameObject GetHighestCell(uint x, uint y)
    {
        for (uint i = 0; i < heightMax; i++)
        {
            if(grid[x * width + y, i] == null)
            {
                return grid[x * width + y, i - 1];
            }
        }
        //impossible
        return null;
    }

    public GameObject NewCell(GameObject underCell)
    {
        Cell underCellScript = underCell.GetComponent<Cell>();
        GameObject newCell = Instantiate<GameObject>(cell, underCell.transform.position + new Vector3(0, BlocSelector.Instance.currentBloc.transform.localScale.y, 0), cell.transform.rotation, transform);
        newCell.GetComponent<Cell>().posInGrid = underCellScript.posInGrid + new Vector3(0, 1, 0);
        //newCell.GetComponent<Cell>().x = un
        grid[(int)underCellScript.posInGrid.x * width + (int)underCellScript.posInGrid.z, (int)underCellScript.posInGrid.y + 1] = newCell;
        return newCell;
    }

    public void DeleteCell(Cell cell)
    {
        grid[(int)cell.posInGrid.x * width + (int)cell.posInGrid.z, (int)cell.posInGrid.y] = null;
        cell.bloc = null;
        Destroy(cell.gameObject);
    }

    

    //public GameObject


}
