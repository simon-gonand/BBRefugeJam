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

    //public GameObject[, ] grid;
    public GameObject[, , ] grid;

    public GroundRotation gr;

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

    /*
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
                GameObject newCell = Instantiate<GameObject>(cell, new Vector3(startX, 0, startY), cell.transform.rotation, transform);
                newCell.GetComponent<Cell>().posInGrid = new Vector3(i, 0, j);
                grid[i * width + j, 0] = newCell;
                startX += cellWidth;

            }
            startX = -(width / 2) * cellWidth;
            startY -= cellWidth;
        }
    }
    */

    public void GenerateGrid()
    {
        ///WARNING : width & lenght must be pair
        if (width % 2 != 0) width++;
        if (lenght % 2 != 0) lenght++;

        grid = new GameObject[width , heightMax, lenght];

        float startX = -(width / 2) * cellWidth;
        float startY = (lenght / 2) * cellWidth;
        float startZ = 0;

        for (int i = 0; i < width; i++) //x
        {
            for (int j = 0; j < lenght; j++) //y
            {
                for (int z = 0; z < heightMax; z++) //z
                {
                    GameObject newCell = Instantiate<GameObject>(cell, new Vector3(startX, startZ, startY), cell.transform.rotation, transform);
                    newCell.GetComponent<Cell>().posInGrid = new Vector3(i, z, j);
                    if (z > 0)
                    {
                        newCell.GetComponent<Cell>().bc.enabled = false;
                        newCell.GetComponent<Cell>().mc.enabled = false;
                        //newCell.SetActive(false);
                    }
                    grid[i , z, j] = newCell;
                    startZ += cellWidth;
                }
                startZ = 0;
                startX += cellWidth;
            }
            startZ = 0;
            startX = -(width / 2) * cellWidth;
            startY -= cellWidth;
        }
    }

    public GameObject GetCell(float x, float y, float z)
    {
        //get inexistant cell
        if (x < 0 || x >= width || z < 0 || z >= lenght || y >= heightMax) return null;

        Debug.Log("X: " + x + ", Y: " + y + ", Z: " + z);
        grid[(int)x, (int)y, (int)z].SetActive(true);

        return grid[(int)x, (int)y, (int)z];
    }

    //Ne prend que des cellules pleines /!\
    public List<GameObject> GetAdjacentCells(float x, float y, float z, int depth = 1)
    {

        List<GameObject> result = new List<GameObject>();

        for (int i = 1; i <= depth; i++)
        {
            result.Add(GetCell(x - i, y, z));
            result.Add(GetCell(x + i, y, z));
            result.Add(GetCell(x, y, z - i));
            result.Add(GetCell(x, y, z + i));
        }

        for (int i = 0; i < result.Count; i++)
        {
            if (result[i] == null || result[i].GetComponent<Cell>().bloc != null) result.RemoveAt(i);
        }

        //get inexistant cell
        if (x < 0 || x >= width || z < 0 || z >= lenght || y >= heightMax) return null;

        return result;
    }

    //Ne prend que des cellules pleines /!\
    public List<GameObject> GetAboveCells(float x, float y, float z, int depth = 1)
    {

        List<GameObject> result = new List<GameObject>();

        for (int i = 1; i <= depth; i++)
        {
            result.Add(GetCell(x, y + i, z));
        }

        for (int i = 0; i < result.Count; i++)
        {
            if (result[i] == null) result.RemoveAt(i);
        }

        //get inexistant cell
        if (x < 0 || x >= width || z < 0 || z >= lenght || y >= heightMax) return null;

        return result;
    }

    //Ne prend que des cellules pleines /!\
    public List<GameObject> GetUnderCells(float x, float y, float z, int depth = 1)
    {

        List<GameObject> result = new List<GameObject>();

        for (int i = 1; i <= depth; i++)
        {
            result.Add(GetCell(x, y - i, z));
        }

        for (int i = 0; i < result.Count; i++)
        {
            if (result[i] == null) result.RemoveAt(i);
        }

        //get inexistant cell
        if (x < 0 || x >= width || z < 0 || z >= lenght || y >= heightMax) return null;

        return result;
    }

    public GameObject GetHighestCell(uint x, uint y)
    {
        for (uint i = 0; i < heightMax; i++)
        {
            if(grid[x, i, y] == null)
            {
                return grid[x, i, y];
            }
        }
        //impossible
        return null;
    }

    /*
    public GameObject NewCell(GameObject underCell)
    {
        Cell underCellScript = underCell.GetComponent<Cell>();
        GameObject newCell = Instantiate<GameObject>(cell, underCell.transform.position + new Vector3(0, BlocSelector.Instance.currentBloc.transform.localScale.y, 0), cell.transform.rotation, transform);
        newCell.GetComponent<Cell>().posInGrid = underCellScript.posInGrid + new Vector3(0, 1, 0);
        //newCell.GetComponent<Cell>().x = un
        Debug.Log("X: " + (int)underCellScript.posInGrid.x + ", Y: " + (int)underCellScript.posInGrid.y + ", Z: " + (int)underCellScript.posInGrid.z);
        grid[(int)underCellScript.posInGrid.x, (int)underCellScript.posInGrid.z + 1, (int)underCellScript.posInGrid.y] = newCell;
        return newCell;
    }
    */

    /*public GameObject NewCell(uint x, uint y, uint height)
    {
        float startX = -(width / 2) * cellWidth;
        float startY = (lenght / 2) * cellWidth;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < lenght; j++)
            {
                if(i == x && j == y)
                {
                    GameObject newCell = Instantiate<GameObject>(cell, new Vector3(startX, height * cellWidth, startY), cell.transform.rotation, transform);
                    newCell.GetComponent<Cell>().posInGrid = new Vector3(x, height, y);
                    grid[i * width + j, height] = newCell;
                    startX += cellWidth;

                    return newCell;
                }

            }
            startX = -(width / 2) * cellWidth;
            startY -= cellWidth;
        }

        return null;
    }*/

    /*
    public GameObject NewCell(GameObject originCell, MCFace dir)
    {
        Cell underCellScript = originCell.GetComponent<Cell>();

        switch (dir)
        {
            case MCFace.West:
                GameObject newCell = Instantiate<GameObject>(cell, originCell.transform.position + new Vector3(-10, 0, 0), cell.transform.rotation, transform);
                newCell.GetComponent<Cell>().posInGrid = underCellScript.posInGrid + new Vector3(-1, 0, 0);
                //newCell.GetComponent<Cell>().x = un
                grid[(int)(underCellScript.posInGrid.x - 1) * width + (int)underCellScript.posInGrid.z, (int)underCellScript.posInGrid.y] = newCell;
                return newCell;
            case MCFace.East:
                GameObject newCell2 = Instantiate<GameObject>(cell, originCell.transform.position + new Vector3(10, 0, 0), cell.transform.rotation, transform);
                newCell2.GetComponent<Cell>().posInGrid = underCellScript.posInGrid + new Vector3(1, 0, 0);
                //newCell.GetComponent<Cell>().x = un
                grid[(int)(underCellScript.posInGrid.x + 1) * width + (int)underCellScript.posInGrid.z, (int)underCellScript.posInGrid.y] = newCell2;
                return newCell2;
            case MCFace.South:
                GameObject newCell3 = Instantiate<GameObject>(cell, originCell.transform.position + new Vector3(0, 0, -10), cell.transform.rotation, transform);
                newCell3.GetComponent<Cell>().posInGrid = underCellScript.posInGrid + new Vector3(0, 0, 1);
                //newCell.GetComponent<Cell>().x = un
                grid[(int)underCellScript.posInGrid.x * width + (int)underCellScript.posInGrid.z+1, (int)underCellScript.posInGrid.y] = newCell3;
                return newCell3;
            case MCFace.North:
                GameObject newCell4 = Instantiate<GameObject>(cell, originCell.transform.position + new Vector3(0, 0, 10), cell.transform.rotation, transform);
                newCell4.GetComponent<Cell>().posInGrid = underCellScript.posInGrid + new Vector3(0, 0, -1);
                //newCell.GetComponent<Cell>().x = un
                grid[(int)underCellScript.posInGrid.x * width + (int)underCellScript.posInGrid.z-1, (int)underCellScript.posInGrid.y] = newCell4;
                return newCell4;
            default:
                return null;
        }
    }
    */

    /*public void DeleteCell(Cell cell)
    {
        grid[(int)cell.posInGrid.x * width + (int)cell.posInGrid.z, (int)cell.posInGrid.y] = null;
        cell.bloc = null;
        Destroy(cell.gameObject);
    }*/

    public enum MCFace
    {
        None,
        Up,
        Down,
        East,
        West,
        North,
        South
    }

}
