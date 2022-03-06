using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{
    public Cell bottomCell, topCell;

    private Camera cam;
    public LayerMask layer;

    public bool isPreview = false;

    [HideInInspector]
    public BoxCollider bc;

    private void Awake()
    {
        bc = GetComponent<BoxCollider>();
        
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        //Ray r = cam.ScreenPointToRay(Input.mousePosition);
        //Ray r = cam.ViewportPointToRay(Input.mousePosition);
        //Debug.DrawRay(r.origin, r.direction * 100, Color.red, 0.1f);

        RaycastHit hit;

        // Draw ray from mouse position to check if we hit anything with certain layer 
        if (isPreview && !Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1000))
        {
            Destroy(gameObject);
        }
    }

    void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0) && !isPreview)
        {
            //ray cast
            cam = Camera.main;
            RaycastHit hit;

            // Draw ray from mouse position to check if we hit anything with certain layer 
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1000))
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
                            Player.instance.currentlyHoveredCell = cellScript;
                            if(Player.instance.PlacementAllowed()) cellScript.AddBlocOnCell();
                        }
                        break;
                    case MCFace.East:
                        if (Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y, bottomCell.posInGrid.z + 1) == null) break;
                        Cell cellScript2 = Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y, bottomCell.posInGrid.z + 1).GetComponent<Cell>();
                        if (cellScript2.bloc == null)
                        {
                            Player.instance.currentlyHoveredCell = cellScript2;
                            if (Player.instance.PlacementAllowed()) cellScript2.AddBlocOnCell();
                        }

                        break;
                    case MCFace.South:
                        if (Grid.Instance.GetCell(bottomCell.posInGrid.x + 1, bottomCell.posInGrid.y, bottomCell.posInGrid.z) == null) break;
                        Cell cellScript3 = Grid.Instance.GetCell(bottomCell.posInGrid.x + 1, bottomCell.posInGrid.y, bottomCell.posInGrid.z).GetComponent<Cell>();
                        if (cellScript3.bloc == null)
                        {
                            Player.instance.currentlyHoveredCell = cellScript3;
                            if (Player.instance.PlacementAllowed()) cellScript3.AddBlocOnCell();
                        }
                        break;
                    case MCFace.North:
                        if (Grid.Instance.GetCell(bottomCell.posInGrid.x - 1, bottomCell.posInGrid.y, bottomCell.posInGrid.z) == null) break;
                        Cell cellScript4 = Grid.Instance.GetCell(bottomCell.posInGrid.x - 1, bottomCell.posInGrid.y, bottomCell.posInGrid.z).GetComponent<Cell>();
                        if (cellScript4.bloc == null)
                        {
                            Player.instance.currentlyHoveredCell = cellScript4;
                            if (Player.instance.PlacementAllowed()) cellScript4.AddBlocOnCell();
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
        else if (Input.GetMouseButtonDown(0) && isPreview)
        {
            bottomCell.AddBlocOnCell();
            Destroy(this);
        }


        //delete bloc
        if (Input.GetMouseButtonDown(1))
        {
            DestroyBloc();
        }

    }

    public void DestroyBloc()
    {
        //topCell.gameObject.SetActive(false);
        topCell.gameObject.GetComponent<BoxCollider>().enabled = false;
        topCell.gameObject.GetComponent<MeshRenderer>().enabled = false;
        if (bottomCell.posInGrid.y == 0)
        {
            bottomCell.mc.enabled = true;
            bottomCell.bc.enabled = true;
        }
        if(bottomCell.posInGrid.y - 1 >=0 && Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y - 1, bottomCell.posInGrid.z).GetComponent<Cell>().GetBloc() != null)
        {
            Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y - 1, bottomCell.posInGrid.z).GetComponent<Cell>().GetBloc().GetComponent<Bloc>().topCell.mc.enabled = true;
            Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y - 1, bottomCell.posInGrid.z).GetComponent<Cell>().GetBloc().GetComponent<Bloc>().topCell.bc.enabled = true;
        }
        //Grid.Instance.DeleteCell(topCell);
        //if (bottomCell.bloc != null) bottomCell.mc.enabled = true;
        DestroyImmediate(this.gameObject);
    }

    private void OnMouseExit()
    {
        if (isPreview)
        {
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
        Vector3 incomingVec = hit.normal;

        //Debug.Log(incomingVec);
        Vector3 relative;
        relative = transform.InverseTransformDirection(incomingVec);
        Debug.Log(relative);

        relative = new Vector3(Mathf.Round(relative.x), Mathf.Round(relative.y), Mathf.Round(relative.z));

        if (relative == new Vector3(0, 0, -1))
            return MCFace.South;

        if (relative == new Vector3(0, 0, 1))
            return MCFace.North;

        if (relative == new Vector3(0, 1, 0))
            return MCFace.Up;

        if (relative == new Vector3(1, -1, 1))
            return MCFace.Down;

        if (relative == new Vector3(-1, 0, 0))
            return MCFace.West;

        if (relative == new Vector3(1, -0, 0))
            return MCFace.East;
        //Debug.Log(relative);

        return MCFace.None;
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

}
