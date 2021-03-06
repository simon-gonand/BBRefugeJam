using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    private void Awake()
    {
        instance = this;
    }


    public int GetBeautyScore(List<GameObject> blocks)
    {
        int result = 0;

        foreach (GameObject go in blocks)
        {
            BaseBlock block = go.GetComponent<BaseBlock>();

            result += block.runtimeData.beauty;
        }

        return result;
    }
}
