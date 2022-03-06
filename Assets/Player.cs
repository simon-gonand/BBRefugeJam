using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int money { get; private set; }

    public GameRules rules;

    public Cell currentlyHoveredCell;
    public BaseBlock currentBlock;

    public static Player instance;

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

    public bool PlacementAllowed()
    {
        if(currentBlock.modifiers.Count > 0 && currentlyHoveredCell != null)
        {
            //switch();
        }


            foreach(GameObject go in Grid.Instance.GetAdjacentCells(currentlyHoveredCell.posInGrid.x, currentlyHoveredCell.posInGrid.y, currentlyHoveredCell.posInGrid.z))
            ;
        return true;
    }

}
