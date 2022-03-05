using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    int initialHP;

    private void Awake()
    {
        instance = this;
    }


    public void CalculateInitialResistanceScore(List<GameObject> blocks)
    {
        int result = 0;

        foreach (GameObject go in blocks)
        {
            BaseBlock block = go.GetComponent<BaseBlock>();

            result += block.runtimeData.hp;
        }

        initialHP = result;
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

    public int GetResistanceScore(List<GameObject> blocks)
    {
        int result = 0;

        foreach (GameObject go in blocks)
        {
            BaseBlock block = go.GetComponent<BaseBlock>();

            result += block.runtimeData.hp;
        }        

        return (int)Mathf.Lerp(0, 100, Mathf.InverseLerp(0, initialHP, result));
    }


    int waterScore;
    int foodScore;
    int energyScore;
    int weaponryScore;

    public int GetSurvivalScore(List<GameObject> blocks)
    {
        foreach (GameObject go in blocks)
        {
            BaseBlock block = go.GetComponent<BaseBlock>();


            foreach (Resource r in block.runtimeData.resources)
            {
                switch (r.type)
                {
                    case ResourceType.Water:
                        waterScore += r.value;
                        break;
                    case ResourceType.Food:
                        foodScore += r.value;
                        break;
                    case ResourceType.Energy:
                        energyScore += r.value;
                        break;
                    case ResourceType.Equipment:
                        weaponryScore += r.value;
                        break;
                }
            }
        }

        GameRules rules = Player.instance.rules;

        return (int)Mathf.Lerp(0, 100, Mathf.InverseLerp(0, initialHP,  (weaponryScore * rules.equipmentMultiplier) + (energyScore * rules.energyMultiplier) + (foodScore * rules.foodMultiplier) + (waterScore * rules.waterMultiplier)));
    }
}
