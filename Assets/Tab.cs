using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tab : MonoBehaviour
{
    public GameObject scrollView;

    public List<GameObject> blocksToLoad = new List<GameObject>();

    private void Start()
    {
        foreach (GameObject block in blocksToLoad)
        {
            Instantiate(block, scrollView.GetComponent<ScrollRect>().content);
        }

    }
}
