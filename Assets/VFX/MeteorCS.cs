using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCS : MonoBehaviour
{
    [SerializeField] GameObject hitPS;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject ps = Instantiate(hitPS, transform);
        ps.transform.parent = null;
        Destroy(this.gameObject);
    }
}
