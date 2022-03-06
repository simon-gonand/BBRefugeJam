using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cell : MonoBehaviour
{
    public Vector3 posInGrid;
    public GameObject bloc;

    //component
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
            if (Player.instance.EnoughMoney(Player.instance.currentBlock.data.price))
            {                
                if (Player.instance.PlacementAllowed())
                {
                    AddBlocOnCell();
                }
            }
            else WarningMessage.instance.Warning("Not enough money !", 2f);

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
            BlocSelector.Instance.previewTmp.GetComponent<Bloc>().topCell = Grid.Instance.GetCell(posInGrid.x, posInGrid.y + 1, posInGrid.z).GetComponent<Cell>(); ;
            BlocSelector.Instance.previewTmp.GetComponentInChildren<MeshRenderer>().material = BlocSelector.Instance.previewMaterial;
            BlocSelector.Instance.previewTmp.transform.localPosition = this.transform.localPosition + (Vector3.up * (BlocSelector.Instance.currentBloc.transform.localScale.y / 2));

            Grid.Instance.selectionFrame.transform.position = pos + (Vector3.up * (Grid.Instance.cellWidth/2));
            Grid.Instance.selectionFrame.transform.rotation = Grid.Instance.gr.transform.rotation * Quaternion.Euler(0, 90 * BlocSelector.Instance.nbOfRotation, 0);

        }

        if (Input.GetMouseButtonDown(1) && posInGrid.y > 0)
        {
            Grid.Instance.GetCell(posInGrid.x, posInGrid.y - 1, posInGrid.z).GetComponent<Cell>().bloc.GetComponent<Bloc>().DestroyBloc();
        }

    }

    public void AddBlocOnCell()
    {

        Vector3 pos = transform.position;
        bloc = Instantiate<GameObject>(BlocSelector.Instance.currentBloc, new Vector3(pos.x, pos.y + BlocSelector.Instance.currentBloc.transform.localScale.y/2, pos.z), Grid.Instance.gr.transform.rotation * Quaternion.Euler(0, 90 * BlocSelector.Instance.nbOfRotation, 0), Grid.Instance.transform);
        bloc.transform.localPosition = this.transform.localPosition + (Vector3.up * (BlocSelector.Instance.currentBloc.transform.localScale.y / 2));
        bloc.GetComponent<Bloc>().bottomCell = this;
        bloc.GetComponent<Bloc>().topCell = Grid.Instance.GetCell(posInGrid.x, posInGrid.y+1, posInGrid.z).GetComponent<Cell>();

        Player.instance.RemoveMoney(bloc.GetComponent<BaseBlock>().data.price);

        /*if(Grid.Instance.GetCell(posInGrid.x, posInGrid.y + 1, posInGrid.z).GetComponent<Cell>() == null)
        {
            Debug.Log("error");
        }*/
        /*if (Grid.Instance.GetCell(posInGrid.x, posInGrid.y + 1, posInGrid.z).GetComponent<Cell>() != null)
        {
            bloc.GetComponent<Bloc>().topCell = Grid.Instance.GetCell(posInGrid.x, posInGrid.y + 1, posInGrid.z).GetComponent<Cell>();
        }*/

        Cell newBlocBottomCell = Grid.Instance.GetCell(posInGrid.x, posInGrid.y + 1, posInGrid.z).GetComponent<Cell>();
        if(newBlocBottomCell.bloc == null)
        {
            Grid.Instance.GetCell(posInGrid.x, posInGrid.y + 1, posInGrid.z).GetComponent<Cell>().bc.enabled = true;
        }
        bc.enabled = false;
        Instantiate(GameManager.instance.putBlock, pos, Quaternion.identity);
    }
}
