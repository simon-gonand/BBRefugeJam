using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Rules", menuName = "Game Rules")]
public class GameRules : ScriptableObject
{

    public string seedName;

    public int initialMoney = 5000;

    [Range(0, 10)]
    public float beautyMultiplier = 1f;

    [Range(0, 10)]
    public float waterMultiplier = 1f;
    [Range(0, 10)]
    public float foodMultiplier = 1f;
    [Range(0, 10)]
    public float energyMultiplier = 1f;
    [Range(0, 10)]
    public float equipmentMultiplier = 1f;
}
