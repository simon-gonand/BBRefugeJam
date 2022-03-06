using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoritesPool : MonoBehaviour
{
    [SerializeField]
    private Transform self;
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private float meteoritesPercentage;

    private List<Meteorite> meteorites = new List<Meteorite>();
    private bool hasBeenLaunched = false;

    // Start is called before the first frame update
    void Start()
    {
        self.position *= Grid.Instance.width / 10;
        for (int i = 0; i < Grid.Instance.width * Grid.Instance.lenght * meteoritesPercentage; ++i)
        {
            meteorites.Add(Instantiate(prefab, self.position, Quaternion.identity, self).GetComponent<Meteorite>());
        }
    }

    public void LaunchMeteorites()
    {
        if (!hasBeenLaunched)
        {
            StartCoroutine(LaunchMeteoritesCoroutine());
            hasBeenLaunched = true;
        }
    }

    public IEnumerator LaunchMeteoritesCoroutine()
    {
        for (int i = 0; i < meteorites.Count; ++i)
        {
            meteorites[i].Launch(self.localPosition);
            yield return new WaitForSeconds(Random.Range(0.0f, 0.3f));
        }
    }
}
