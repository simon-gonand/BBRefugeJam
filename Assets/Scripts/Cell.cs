using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cell : MonoBehaviour
{
    public Vector3 posInGrid;
    public GameObject bloc;

    //component
    public MeshRenderer mc;
    public BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        //mc = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetBloc()
    {
        return bloc;
    }

    public List<GameObject> adjacent = new List<GameObject>();

    void OnMouseOver()
    {
        //TODO : highlight cell

        Player.instance.currentlyHoveredCell = this;

        //Put current selected bloc
        if (Input.GetMouseButtonDown(0) && bc.enabled)
        {

            //adjacent = Grid.Instance.GetAdjacentCells(posInGrid.x, posInGrid.y, posInGrid.z);

            if(Player.instance.PlacementAllowed())
            {
                AddBlocOnCell();
            }

            //Debug.Log(posInGrid);
        }
        else if (bc.enabled)
        {
            if (BlocSelector.Instance.previewTmp != null) Destroy(BlocSelector.Instance.previewTmp);

            Vector3 pos = transform.position;
            BlocSelector.Instance.previewTmp = Instantiate<GameObject>(BlocSelector.Instance.currentBloc, new Vector3(pos.x, pos.y + BlocSelector.Instance.currentBloc.transform.localScale.y / 2, pos.z), Grid.Instance.gr.transform.rotation * Quaternion.Euler(0, 90 * BlocSelector.Instance.nbOfRotation, 0), Grid.Instance.transform);
            BlocSelector.Instance.previewTmp.GetComponent<Bloc>().isPreview = true;
            BlocSelector.Instance.previewTmp.GetComponent<Bloc>().bottomCell = this;
            BlocSelector.Instance.previewTmp.GetComponent<Bloc>().bc.enabled = false;
            BlocSelector.Instance.previewTmp.GetComponentInChildren<MeshRenderer>().material = BlocSelector.Instance.previewMaterial;
            BlocSelector.Instance.previewTmp.transform.localPosition = this.transform.localPosition + (Vector3.up * (BlocSelector.Instance.currentBloc.transform.localScale.y / 2));
        }

    }

    public void AddBlocOnCell()
    {
        Vector3 pos = transform.position;
        bloc = Instantiate<GameObject>(BlocSelector.Instance.currentBloc, new Vector3(pos.x, pos.y + BlocSelector.Instance.currentBloc.transform.localScale.y/2, pos.z), Grid.Instance.gr.transform.rotation * Quaternion.Euler(0, 90 * BlocSelector.Instance.nbOfRotation, 0), Grid.Instance.transform);
        bloc.transform.localPosition = this.transform.localPosition + (Vector3.up * (BlocSelector.Instance.currentBloc.transform.localScale.y / 2));
        bloc.GetComponent<Bloc>().bottomCell = this;
        if (Grid.Instance.GetCell(posInGrid.x, posInGrid.y + 1, posInGrid.z).GetComponent<Cell>() != null)
        {
            bloc.GetComponent<Bloc>().topCell = Grid.Instance.GetCell(posInGrid.x, posInGrid.y + 1, posInGrid.z).GetComponent<Cell>();
        }

        Cell newBlocBottomCell = Grid.Instance.GetCell(posInGrid.x, posInGrid.y + 1, posInGrid.z).GetComponent<Cell>();
        if(newBlocBottomCell.bloc == null)
        {
            Grid.Instance.GetCell(posInGrid.x, posInGrid.y + 1, posInGrid.z).GetComponent<Cell>().bc.enabled = true;
            Grid.Instance.GetCell(posInGrid.x, posInGrid.y + 1, posInGrid.z).GetComponent<Cell>().mc.enabled = true;
        }
        bc.enabled = false;
        mc.enabled = false;
    }
}
