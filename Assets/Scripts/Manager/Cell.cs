using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cell : MonoBehaviour
{
    public Vector3 posInGrid;
    public GameObject bloc;


    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log(posInGrid);
        }

    }
}
