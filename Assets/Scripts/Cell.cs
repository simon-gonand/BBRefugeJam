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

    // Start is called before the first frame update
    void Start()
    {
        mc = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetBloc()
    {
        return bloc;
    }

    void OnMouseOver()
    {
        //TODO : highlight cell

        //Put current selected bloc
        if (Input.GetMouseButtonDown(0))
        {
            AddBlocOnCell();

            //Debug.Log(posInGrid);
        }

    }

    public void AddBlocOnCell()
    {
        Vector3 pos = transform.position;
        bloc = Instantiate<GameObject>(BlocSelector.Instance.currentBloc, new Vector3(pos.x, pos.y + BlocSelector.Instance.currentBloc.transform.localScale.y/2, pos.z), BlocSelector.Instance.currentBloc.transform.rotation);
        bloc.GetComponent<Bloc>().bottomCell = this;
        bloc.GetComponent<Bloc>().topCell = Grid.Instance.NewCell(gameObject).GetComponent<Cell>();

        mc.enabled = false;
    }
}
