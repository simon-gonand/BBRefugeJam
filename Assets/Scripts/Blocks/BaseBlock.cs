using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseBlock : MonoBehaviour, IDamageable
{

    public BlockData data;

    public RuntimeBlockData runtimeData;
    public UnityEvent onTakeDamages;
    public UnityEvent onDeath;

    public Direction globalRestrictions;

    public List<ProximityModifier> modifiers = new List<ProximityModifier>();

    private void Start()
    {
        LoadData();
    }

    public void TakeDamages(int damages)
    {
        runtimeData.hp -= (damages - runtimeData.resistance);
       
        HPCheck();
    }


    public void HPCheck()
    {
       if(runtimeData.hp <= 0)
       {
            onDeath.Invoke();
       }
       else
       {
           onTakeDamages.Invoke();
       }
    }

    public void LoadData()
    {
        runtimeData.price = data.price;
        runtimeData.hp = data.hp;
        runtimeData.resistance = data.resistance;
        runtimeData.beauty = data.beauty;
        runtimeData.resources = data.resources;
    }
}


public interface IDamageable
{
    public void TakeDamages(int damages);
}

[System.Serializable]
public class RuntimeBlockData
{
    public int price;

    [Header("Resistance Score")]
    public int hp;
    public int resistance;

    [Header("Beauty Score")]
    public int beauty;

    [Header("Survival Score")]
    public List<Resource> resources = new List<Resource>();
}

[System.Serializable]
public class ProximityModifier
{
    public BaseBlock block;
    public bool restrictPlacement;
    public Direction directions;
    public ModifierData modifier;
}

[System.Serializable]
public class ModifierData
{
    [Header("Resistance Score")]
    [Range(-1000, 1000)]
    public int hp;
    [Range(-500, 500)]
    public int resistance;

    [Header("Beauty Score")]
    [Range(-100, 100)]
    public int beauty;

    [Header("Survival Score")]
    public List<Resource> resources = new List<Resource>();
}

public enum Direction {Nothing, All, Adjacents, UnderAndAbove, Under, Above,}
