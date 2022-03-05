using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField]
    private Transform self;

    private bool isLaunched = false;
    private Vector3 start;
    private Vector3 destination;
    private float t = 0.0f;

    private void Start()
    {
        start = self.position;
    }

    public void Launch()
    {
        isLaunched = true;

        int x = Random.Range(0, (int)Grid.Instance.lenght - 1);
        int y = Random.Range(0, (int)Grid.Instance.width - 1);
        int z = Random.Range(0, (int)Grid.Instance.heightMax - 1);

        destination = Grid.Instance.GetCell((uint)x, (uint)y).transform.position;
    }

    private void Update()
    {
        if (isLaunched)
        {
            t += Time.deltaTime;
            self.position = Vector3.Lerp(start, destination, t);
        }
    }
}
