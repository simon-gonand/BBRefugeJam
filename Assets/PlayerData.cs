using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public int money { get; private set; }

    public GameRules rules;

    public static PlayerData instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        money = rules.initialMoney;
        BlockSelectorMenu.instance.seedNameText.text = rules.seedName.ToString();
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void RemoveMoney(int amount)
    {
        money -= amount;
    }
}
