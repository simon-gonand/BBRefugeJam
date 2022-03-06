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
    private bool _hasBeenLaunched = false;
    public bool hasBeenLaunched { get { return _hasBeenLaunched; } }

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
        if (!_hasBeenLaunched)
        {
            StartCoroutine(LaunchMeteoritesCoroutine());
            _hasBeenLaunched = true;
        }
    }

    public IEnumerator LaunchMeteoritesCoroutine()
    {
        for (int i = 0; i < meteorites.Count; ++i)
        {
            meteorites[i].Launch(self.localPosition);
            yield return new WaitForSeconds(Random.Range(0.0f, 0.3f));
        }
        _hasBeenLaunched = false;
    }

    public bool CheckAllMeteoritesDown()
    {
        for (int i = 0; i < meteorites.Count; ++i)
        {
            if (!meteorites[i].hasExplode) return false;
        }
        return true;
    }
}
