using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Meteorite : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Transform self;
    [SerializeField]
    private UnityEvent feedback;

    [Header("Stats")]
    [SerializeField]
    private int damage;
    [SerializeField]
    private float speed;

    private bool isLaunched = false;
    private bool _hasExplode = false;
    public bool hasExplode { get { return _hasExplode; } }
    private Vector3 start;
    private Vector3 destination;
    private float t = 0.0f;

    private void OnCollisionEnter(Collision collision)
    {
        feedback.Invoke();
        _hasExplode = true;
        BaseBlock block = collision.collider.GetComponent<BaseBlock>();
        if (block == null) return;
        block.TakeDamages(damage);
    }

    private void Start()
    {
        start = self.position;
    }

    public void Launch(Vector3 poolLocalPos)
    {
        isLaunched = true;

        int x = Random.Range(0, (int)Grid.Instance.lenght);
        int y = Random.Range(0, (int)Grid.Instance.width);

        destination = Grid.Instance.GetCell((uint)x, 0, (uint)y).transform.localPosition;
        destination -= poolLocalPos;
    }

    private void Update()
    {
        if (isLaunched)
        {
            t += Time.deltaTime * speed;
            self.localPosition = Vector3.Lerp(start, destination, t);
        }
    }
}
