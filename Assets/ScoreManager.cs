using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    int initialHP;

    private int beautyScore = 0;
    private int resistanceScore = 0;
    private int survivalScore = 0;

    private void Awake()
    {
        instance = this;
    }

    public void CalculateScore()
    {
        beautyScore = 0;
        survivalScore = 0;
        List<BaseBlock> blocks = Grid.Instance.GetAllBlocks();
        beautyScore = GetBeautyScore(blocks);
        survivalScore = GetSurvivalScore(blocks);
        Debug.Log(beautyScore);
        Debug.Log(survivalScore);
    }

    public void CalculateInitialResistanceScore(List<BaseBlock> blocks)
    {
        int result = 0;

        foreach (BaseBlock block in blocks)
        {
            result += block.runtimeData.hp;
        }

        initialHP = result;
    }

    private BaseBlock GetBestBeautyObject()
    {
        BaseBlock bestBlock = null;
        float bestRate = 0.0f;
        foreach(BaseBlock block in GameManager.instance.allAvailableBlocks)
        {
            float rate = block.data.beauty / block.data.price;
            if (bestBlock == null)
            {
                bestBlock = block;
                bestRate = rate;
                continue;
            }
            if (rate > bestRate)
            {
                bestBlock = block;
                bestRate = rate;
            }
        }
        return bestBlock;
    }

    private int GetBestBeautyScore()
    {
        BaseBlock bestBlock = GetBestBeautyObject();
        int nbInstance = Player.instance.rules.initialMoney / bestBlock.data.price;
        return bestBlock.data.beauty * nbInstance;
    }

    public int GetBeautyScore(List<BaseBlock> blocks)
    {
        int bestResult = GetBestBeautyScore();
        int result = 0;

        foreach (BaseBlock block in blocks)
        {
            result += block.runtimeData.beauty;
        }

        return (int)(Mathf.Lerp(0, 100, Mathf.InverseLerp(0, bestResult, result)) * Player.instance.rules.beautyMultiplier);
    }

    public int GetResistanceScore(List<BaseBlock> blocks)
    {
        int result = 0;

        foreach (BaseBlock block in blocks)
        {
            result += block.runtimeData.hp;
        }        

        return (int)Mathf.Lerp(0, 100, Mathf.InverseLerp(0, initialHP, result));
    }


    int waterScore;
    int foodScore;
    int energyScore;
    int weaponryScore;

    private BaseBlock GetBestSurvivalObject()
    {
        BaseBlock bestBlock = null;
        float bestRate = 0.0f;
        foreach (BaseBlock block in GameManager.instance.allAvailableBlocks)
        {
            int fullSurvival = 0;
            foreach(Resource res in block.data.resources)
            {
                fullSurvival += res.value;
            }
            float rate = (float)fullSurvival / (float)block.data.price;
            if (bestBlock == null)
            {
                bestBlock = block;
                bestRate = rate;
                continue;
            }
            if (rate > bestRate)
            {
                bestBlock = block;
                bestRate = rate;
            }
        }
        return bestBlock;
    }

    private int GetBestSurvivalScore()
    {
        BaseBlock bestBlock = GetBestSurvivalObject();
        int nbInstance = Player.instance.rules.initialMoney / bestBlock.data.price;
        int food = 0;
        int water = 0;
        int equipment = 0;
        int energy = 0;
        foreach (Resource res in bestBlock.data.resources)
        {
            switch (res.type)
            {
                case ResourceType.Water:
                    water += res.value;
                    break;
                case ResourceType.Food:
                    food += res.value;
                    break;
                case ResourceType.Energy:
                    energy += res.value;
                    break;
                case ResourceType.Equipment:
                    equipment += res.value;
                    break;
            }
        }
        GameRules rules = Player.instance.rules;
        return (int)((food * rules.foodMultiplier + water * rules.waterMultiplier + energy * rules.energyMultiplier + equipment * rules.equipmentMultiplier) * nbInstance);
    }

    public int GetSurvivalScore(List<BaseBlock> blocks)
    {
        int bestScore = GetBestSurvivalScore();
        foreach (BaseBlock block in blocks)
        {
            foreach (Resource r in block.data.resources)
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
        /*Debug.Log("survival max score = " + bestScore);
        Debug.Log("survival actual score = " + (weaponryScore * rules.equipmentMultiplier) + (energyScore * rules.energyMultiplier) + (foodScore * rules.foodMultiplier) + (waterScore * rules.waterMultiplier));*/
        return (int)Mathf.Lerp(0, 100, Mathf.InverseLerp(0, bestScore,  (weaponryScore * rules.equipmentMultiplier) + (energyScore * rules.energyMultiplier) + (foodScore * rules.foodMultiplier) + (waterScore * rules.waterMultiplier)));
    }
}
