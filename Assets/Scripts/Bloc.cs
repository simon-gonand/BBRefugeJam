using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{
    public Cell bottomCell, topCell;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        //TODO : highlight cell

        //delete bloc
        if (Input.GetMouseButtonDown(1))
        {
            bottomCell.mc.enabled = true;
            Grid.Instance.DeleteCell(topCell);
            Destroy(gameObject);
        }

    }
}
