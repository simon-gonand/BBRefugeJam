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

                if (!Player.instance.EnoughMoney(Player.instance.currentBlock.data.price))
                {
                    WarningMessage.instance.Warning("Not enough money !", 2f);
                    return;
                }


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

            if (Player.instance.EnoughMoney(Player.instance.currentBlock.data.price))
            {
                if (Player.instance.PlacementAllowed()) bottomCell.AddBlocOnCell();
                Destroy(this);
            }
        }
        else
        {
            cam = Camera.main;
            RaycastHit hit;

            // Draw ray from mouse position to check if we hit anything with certain layer 
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            {
                switch (GetHitFace(hit))
                {
                    case MCFace.West:
                        if (BlocSelector.Instance.previewTmp != null) Destroy(BlocSelector.Instance.previewTmp);

                        Cell adjacentCell = Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y, bottomCell.posInGrid.z - 1).GetComponent<Cell>();
                        Vector3 pos = adjacentCell.transform.position;
                        BlocSelector.Instance.previewTmp = Instantiate<GameObject>(BlocSelector.Instance.currentBloc, new Vector3(pos.x, pos.y + BlocSelector.Instance.currentBloc.transform.localScale.y / 2, pos.z), Grid.Instance.gr.transform.rotation * Quaternion.Euler(0, 90 * BlocSelector.Instance.nbOfRotation, 0), Grid.Instance.transform);
                        BlocSelector.Instance.previewTmp.transform.localPosition = adjacentCell.transform.localPosition + (Vector3.up * (BlocSelector.Instance.currentBloc.transform.localScale.y / 2));

                        Grid.Instance.selectionFrame.transform.position = new Vector3(pos.x, pos.y + BlocSelector.Instance.currentBloc.transform.localScale.y / 2, pos.z);
                        Grid.Instance.selectionFrame.transform.rotation = Grid.Instance.gr.transform.rotation * Quaternion.Euler(0, 90 * BlocSelector.Instance.nbOfRotation, 0);

                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().isPreview = true;
                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().bottomCell = adjacentCell;
                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().bc.enabled = false;
                        BlocSelector.Instance.previewTmp.GetComponentInChildren<MeshRenderer>().material = BlocSelector.Instance.previewMaterial;
                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().topCell = Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y + 1, bottomCell.posInGrid.z - 1).GetComponent<Cell>();
                        //BlocSelector.Instance.previewTmp.GetComponent<MeshRenderer>().material = BlocSelector.Instance.previewMaterial;
                        //BlocSelector.Instance.previewTmp.transform.localPosition = this.transform.localPosition + (Vector3.up * (BlocSelector.Instance.currentBloc.transform.localScale.y / 2));
                        break;
                    case MCFace.East:
                        if (BlocSelector.Instance.previewTmp != null) Destroy(BlocSelector.Instance.previewTmp);

                        Cell adjacentCell2 = Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y, bottomCell.posInGrid.z + 1).GetComponent<Cell>();
                        Vector3 pos2 = adjacentCell2.transform.position;
                        BlocSelector.Instance.previewTmp = Instantiate<GameObject>(BlocSelector.Instance.currentBloc, new Vector3(pos2.x, pos2.y + BlocSelector.Instance.currentBloc.transform.localScale.y / 2, pos2.z), Grid.Instance.gr.transform.rotation * Quaternion.Euler(0, 90 * BlocSelector.Instance.nbOfRotation, 0), Grid.Instance.transform);
                        BlocSelector.Instance.previewTmp.transform.localPosition = adjacentCell2.transform.localPosition + (Vector3.up * (BlocSelector.Instance.currentBloc.transform.localScale.y / 2));

                        Grid.Instance.selectionFrame.transform.position = new Vector3(pos2.x, pos2.y + BlocSelector.Instance.currentBloc.transform.localScale.y / 2, pos2.z);
                        Grid.Instance.selectionFrame.transform.rotation = Grid.Instance.gr.transform.rotation * Quaternion.Euler(0, 90 * BlocSelector.Instance.nbOfRotation, 0);

                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().isPreview = true;
                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().bottomCell = adjacentCell2;
                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().bc.enabled = false;
                        BlocSelector.Instance.previewTmp.GetComponentInChildren<MeshRenderer>().material = BlocSelector.Instance.previewMaterial;
                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().topCell = Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y + 1, bottomCell.posInGrid.z + 1).GetComponent<Cell>();
                        break;
                    case MCFace.North:
                        if (BlocSelector.Instance.previewTmp != null) Destroy(BlocSelector.Instance.previewTmp);

                        Cell adjacentCell3 = Grid.Instance.GetCell(bottomCell.posInGrid.x - 1, bottomCell.posInGrid.y, bottomCell.posInGrid.z).GetComponent<Cell>();
                        Vector3 pos3 = adjacentCell3.transform.position;
                        BlocSelector.Instance.previewTmp = Instantiate<GameObject>(BlocSelector.Instance.currentBloc, new Vector3(pos3.x, pos3.y + BlocSelector.Instance.currentBloc.transform.localScale.y / 2, pos3.z), Grid.Instance.gr.transform.rotation * Quaternion.Euler(0, 90 * BlocSelector.Instance.nbOfRotation, 0), Grid.Instance.transform);
                        BlocSelector.Instance.previewTmp.transform.localPosition = adjacentCell3.transform.localPosition + (Vector3.up * (BlocSelector.Instance.currentBloc.transform.localScale.y / 2));

                        Grid.Instance.selectionFrame.transform.position = new Vector3(pos3.x, pos3.y + BlocSelector.Instance.currentBloc.transform.localScale.y / 2, pos3.z);
                        Grid.Instance.selectionFrame.transform.rotation = Grid.Instance.gr.transform.rotation * Quaternion.Euler(0, 90 * BlocSelector.Instance.nbOfRotation, 0);

                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().isPreview = true;
                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().bottomCell = adjacentCell3;
                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().bc.enabled = false;
                        BlocSelector.Instance.previewTmp.GetComponentInChildren<MeshRenderer>().material = BlocSelector.Instance.previewMaterial;
                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().topCell = Grid.Instance.GetCell(bottomCell.posInGrid.x - 1, bottomCell.posInGrid.y + 1, bottomCell.posInGrid.z).GetComponent<Cell>();
                        break;
                    case MCFace.South:
                        if (BlocSelector.Instance.previewTmp != null) Destroy(BlocSelector.Instance.previewTmp);

                        Cell adjacentCell4 = Grid.Instance.GetCell(bottomCell.posInGrid.x + 1, bottomCell.posInGrid.y, bottomCell.posInGrid.z).GetComponent<Cell>();
                        //Player.instance.currentlyHoveredCell = adjacentCell4;
                        Vector3 pos4 = adjacentCell4.transform.position;
                        BlocSelector.Instance.previewTmp = Instantiate<GameObject>(BlocSelector.Instance.currentBloc, new Vector3(pos4.x, pos4.y + BlocSelector.Instance.currentBloc.transform.localScale.y / 2, pos4.z), Grid.Instance.gr.transform.rotation * Quaternion.Euler(0, 90 * BlocSelector.Instance.nbOfRotation, 0), Grid.Instance.transform);
                        BlocSelector.Instance.previewTmp.transform.localPosition = adjacentCell4.transform.localPosition + (Vector3.up * (BlocSelector.Instance.currentBloc.transform.localScale.y / 2));

                        Grid.Instance.selectionFrame.transform.position = new Vector3(pos4.x, pos4.y + BlocSelector.Instance.currentBloc.transform.localScale.y / 2, pos4.z);
                        Grid.Instance.selectionFrame.transform.rotation = Grid.Instance.gr.transform.rotation * Quaternion.Euler(0, 90 * BlocSelector.Instance.nbOfRotation, 0);

                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().isPreview = true;
                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().bottomCell = adjacentCell4;
                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().bc.enabled = false;
                        BlocSelector.Instance.previewTmp.GetComponentInChildren<MeshRenderer>().material = BlocSelector.Instance.previewMaterial;
                        BlocSelector.Instance.previewTmp.GetComponent<Bloc>().topCell = Grid.Instance.GetCell(bottomCell.posInGrid.x + 1, bottomCell.posInGrid.y + 1, bottomCell.posInGrid.z).GetComponent<Cell>();
                        break;
                }
            }
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

        Player.instance.AddMoney(this.GetComponent<BaseBlock>().data.price);

        topCell.gameObject.GetComponent<BoxCollider>().enabled = false;
        if (bottomCell.posInGrid.y == 0)
        {
            bottomCell.bc.enabled = true;
        }
        if(bottomCell.posInGrid.y - 1 >=0 && Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y - 1, bottomCell.posInGrid.z).GetComponent<Cell>().GetBloc() != null)
        {
            Grid.Instance.GetCell(bottomCell.posInGrid.x, bottomCell.posInGrid.y - 1, bottomCell.posInGrid.z).GetComponent<Cell>().GetBloc().GetComponent<Bloc>().topCell.bc.enabled = true;
        }
        //Grid.Instance.DeleteCell(topCell);
        //if (bottomCell.bloc != null) bottomCell.mc.enabled = true;
        Instantiate(GameManager.instance.removeBlock, transform.position, Quaternion.identity);
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
