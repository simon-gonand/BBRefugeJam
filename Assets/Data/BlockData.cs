using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Block Data", menuName = "Block Data")]

public class BlockData : ScriptableObject
{
    [Header("Resistance Score")]
    [ColorRange(1, 1, 1, 0.38f, 0.83f, 0.38f), Clamped(0, 100)]
    public int hp;
    [ColorRange(1, 1, 1, 0.38f, 0.74f, 0.83f), Clamped(0, 100)]
    public int resistance;

    [Header("Beauty Score")]
    [ColorRange(1, 1, 1, 0.83f, 0.38f, 0.83f), Clamped(0, 100)]
    public int beauty;

    [Header("Survival Score")]
    public List<Resource> resources = new List<Resource>();
}

[System.Serializable]
public class Resource
{
    public ResourceType type;
    [Clamped(0,100)]
    public int value;
}


public enum ResourceType
{
    Water,
    Food,
    Energy,
    Weaponry
}
