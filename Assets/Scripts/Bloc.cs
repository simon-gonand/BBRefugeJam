using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{
    public Cell bottomCell, topCell;

    private Camera cam;
    public LayerMask layer;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        //Ray r = cam.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(r.origin, r.direction * 100, Color.red, 0.1f);
    }

    void OnMouseOver()
    {
        //TODO : highlight cell

        //add bloc on side
        /*
        if (Input.GetMouseButtonDown(0))
        {
            //ray cast
            RaycastHit hit;

            // Draw ray from mouse position to check if we hit anything with certain layer 
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layer))
            {

                Debug.Log(GetHitFace(hit));
                switch (GetHitFace(hit))
                {
                    case MCFace.West:
                        GameObject pointedCell1 = Grid.Instance.GetCell((uint)bottomCell.posInGrid.x-1, (uint)bottomCell.posInGrid.z, (uint)bottomCell.posInGrid.y);
                        if (pointedCell1 != null && pointedCell1.GetComponent<Cell>().bloc == null)
                        {
                            pointedCell1.GetComponent<Cell>().AddBlocOnCell();
                        }
                        else
                        {
                            GameObject newCell1 = Grid.Instance.NewCell(bottomCell.gameObject, Grid.MCFace.West);
                            if(newCell1 != null) newCell1.GetComponent<Cell>().AddBlocOnCell();
                        }
                        break;
                    case MCFace.East:
                        GameObject pointedCell2 = Grid.Instance.GetCell((uint)bottomCell.posInGrid.x + 1, (uint)bottomCell.posInGrid.z, (uint)bottomCell.posInGrid.y);
                        if (pointedCell2 != null && pointedCell2.GetComponent<Cell>().bloc == null)
                        {
                            pointedCell2.GetComponent<Cell>().AddBlocOnCell();
                        }
                        else
                        {
                            GameObject newCell2 = Grid.Instance.NewCell(bottomCell.gameObject, Grid.MCFace.East);
                            if (newCell2 != null) newCell2.GetComponent<Cell>().AddBlocOnCell();
                        }
                        
                        break;
                    case MCFace.South:
                        GameObject pointedCell3 = Grid.Instance.GetCell((uint)bottomCell.posInGrid.x, (uint)bottomCell.posInGrid.z+1, (uint)bottomCell.posInGrid.y);
                        if (pointedCell3 != null && pointedCell3.GetComponent<Cell>().bloc == null)
                        {
                            pointedCell3.GetComponent<Cell>().AddBlocOnCell();
                        }
                        else
                        {
                            GameObject newCell3 = Grid.Instance.NewCell(bottomCell.gameObject, Grid.MCFace.South);
                            if (newCell3 != null) newCell3.GetComponent<Cell>().AddBlocOnCell();
                        }
                        break;
                    case MCFace.North:
                        GameObject pointedCell4 = Grid.Instance.GetCell((uint)bottomCell.posInGrid.x, (uint)bottomCell.posInGrid.z - 1, (uint)bottomCell.posInGrid.y);
                        if (pointedCell4 != null && pointedCell4.GetComponent<Cell>().bloc == null)
                        {
                            pointedCell4.GetComponent<Cell>().AddBlocOnCell();
                        }
                        else
                        {
                            GameObject newCell4 = Grid.Instance.NewCell(bottomCell.gameObject, Grid.MCFace.North);
                            if (newCell4 != null) newCell4.GetComponent<Cell>().AddBlocOnCell();
                        }
                        break;
                    default:
                        //nothing happens
                        break;
                }

            }
            else
            {
                Debug.Log("not on block");
            }

        }
        */
        if (Input.GetMouseButtonDown(0))
        {
            //ray cast
            RaycastHit hit;

            // Draw ray from mouse position to check if we hit anything with certain layer 
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layer))
            {
                if (bottomCell == null) Debug.Log("ERROR : bottomCell is null");

                Debug.Log(GetHitFace(hit));
                switch (GetHitFace(hit))
                {
                    case MCFace.West:
                        if (Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y, bottomCell.posInGrid.z - 1) == null) break;
                        Cell cellScript = Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y, bottomCell.posInGrid.z - 1).GetComponent<Cell>();
                        if (cellScript.bloc == null)
                        {
                            cellScript.AddBlocOnCell();
                        }
                        break;
                    case MCFace.East:
                        if (Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y, bottomCell.posInGrid.z + 1) == null) break;
                        Cell cellScript2 = Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y, bottomCell.posInGrid.z + 1).GetComponent<Cell>();
                        if (cellScript2.bloc == null)
                        {
                            cellScript2.AddBlocOnCell();
                        }

                        break;
                    case MCFace.South:
                        if (Grid.Instance.GetCell(bottomCell.posInGrid.x + 1, bottomCell.posInGrid.y, bottomCell.posInGrid.z) == null) break;
                        Cell cellScript3 = Grid.Instance.GetCell(bottomCell.posInGrid.x + 1, bottomCell.posInGrid.y, bottomCell.posInGrid.z).GetComponent<Cell>();
                        if (cellScript3.bloc == null)
                        {
                            cellScript3.AddBlocOnCell();
                        }
                        break;
                    case MCFace.North:
                        if (Grid.Instance.GetCell(bottomCell.posInGrid.x - 1, bottomCell.posInGrid.y, bottomCell.posInGrid.z) == null) break;
                        Cell cellScript4 = Grid.Instance.GetCell(bottomCell.posInGrid.x - 1, bottomCell.posInGrid.y, bottomCell.posInGrid.z).GetComponent<Cell>();
                        if (cellScript4.bloc == null)
                        {
                            cellScript4.AddBlocOnCell();
                        }
                        break;
                    default:
                        //nothing happens
                        break;
                }

            }
            else
            {
                Debug.Log("not on block");
            }

        }


        //delete bloc
        if (Input.GetMouseButtonDown(1))
        {
            topCell.gameObject.SetActive(false);
            if(bottomCell.posInGrid.y == 0)
            {
                bottomCell.mc.enabled = true;
                bottomCell.bc.enabled = true;
            }
            //Grid.Instance.DeleteCell(topCell);
            //if (bottomCell.bloc != null) bottomCell.mc.enabled = true;
            Destroy(gameObject);
        }

    }

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

    public MCFace GetHitFace(RaycastHit hit)
    {
        Vector3 incomingVec = hit.normal - Vector3.up;

        if (incomingVec == new Vector3(0, -1, -1))
            return MCFace.South;

        if (incomingVec == new Vector3(0, -1, 1))
            return MCFace.North;

        if (incomingVec == new Vector3(0, 0, 0))
            return MCFace.Up;

        if (incomingVec == new Vector3(1, 1, 1))
            return MCFace.Down;

        if (incomingVec == new Vector3(-1, -1, 0))
            return MCFace.West;

        if (incomingVec == new Vector3(1, -1, 0))
            return MCFace.East;

        return MCFace.None;
    }
}
