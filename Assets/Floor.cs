using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(Grid.Instance.width, 1, Grid.Instance.lenght);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
