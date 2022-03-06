using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    private int initialHP;

    private int _beautyScore = 0;
    public int beautyScore { get { return _beautyScore; } }
    private int _resistanceScore = 0;
    public int resistanceScore { get { return _resistanceScore; } }
    private int _survivalScore = 0;
    public int survivalScore { get { return _survivalScore; } }

    private int _waterScore;
    public int waterScore { get { return _waterScore; } }
    private int _foodScore;
    public int foodScore { get { return _foodScore; } }
    private int _energyScore;
    public int energyScore { get { return _energyScore; } }
    private int _weaponScore;
    public int weaponScore { get { return _weaponScore; } }

    private void Awake()
    {
        instance = this;
    }

    public void CalculateScoreBeforeApo()
    {
        _beautyScore = 0;
        _resistanceScore = 0;
        _survivalScore = 0;
        List<BaseBlock> blocks = Grid.Instance.GetAllBlocks();
        _beautyScore = GetBeautyScore(blocks);
        CalculateInitialResistanceScore(blocks);
    }

    public void CalculateScoreAfterApo()
    {
        List<BaseBlock> blocks = Grid.Instance.GetAllBlocks();
        _resistanceScore = GetResistanceScore(blocks);
        _survivalScore = GetSurvivalScore(blocks);
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
            if (block.data.beauty <= 0) continue;
            float rate = (float)block.data.beauty / (float)block.data.price;
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
        Debug.Log(bestBlock.name);
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

        Debug.Log(result);
        Debug.Log(bestResult);
        return (int)(Mathf.Lerp(0, 100, Mathf.InverseLerp(0, bestResult, result)));
    }

    private BaseBlock GetBestResistanceObject()
    {
        BaseBlock bestBlock = null;
        float bestRate = 0.0f;
        foreach (BaseBlock block in GameManager.instance.allAvailableBlocks)
        {
            if (block.data.hp <= 0) continue;
            float rate = (float)block.data.hp / (float)block.data.price;
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

    private int GetBestResistanceScore()
    {
        BaseBlock bestBlock = GetBestResistanceObject();
        int nbInstance = Player.instance.rules.initialMoney / bestBlock.data.price;
        return bestBlock.data.hp * nbInstance;
    }

    public int GetResistanceScore(List<BaseBlock> blocks)
    {
        int result = 0;

        foreach (BaseBlock block in blocks)
        {
            if (block.runtimeData.hp < block.data.hp)
                Debug.Log(block.data.hp - block.runtimeData.hp);
            result += block.runtimeData.hp;
        }

        return (int)(Mathf.Lerp(0, 100, Mathf.InverseLerp(0, initialHP, result)) * (float)initialHP / (float)GetBestResistanceScore());
    }

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
            if (fullSurvival <= 0) continue;
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
                        _waterScore += r.value;
                        break;
                    case ResourceType.Food:
                        _foodScore += r.value;
                        break;
                    case ResourceType.Energy:
                        _energyScore += r.value;
                        break;
                    case ResourceType.Equipment:
                        _weaponScore += r.value;
                        break;
                }
            }
        }
        GameRules rules = Player.instance.rules;
        return (int)Mathf.Lerp(0, 100, Mathf.InverseLerp(0, bestScore,  (_weaponScore * rules.equipmentMultiplier) + (_energyScore * rules.energyMultiplier) + (_foodScore * rules.foodMultiplier) + (_waterScore * rules.waterMultiplier)));
    }
}
