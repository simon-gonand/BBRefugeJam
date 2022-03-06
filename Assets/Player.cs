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

    public float warningMessageDuration;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        money = rules.initialMoney;
        BlockSelectorMenu.instance.seedNameText.text = rules.seedName.ToString();
    }

    public bool EnoughMoney(int price)
    {
        return money >= price ? true : false;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void RemoveMoney(int amount)
    {
        money -= amount;
    }

        public List<GameObject> all = new List<GameObject>();
        public List<GameObject> underabove = new List<GameObject>();
    public bool PlacementAllowed()
    {
        all.Clear();
        underabove.Clear();
        List<GameObject> adjacents = Grid.Instance.GetAdjacentCells(currentlyHoveredCell.posInGrid.x, currentlyHoveredCell.posInGrid.y, currentlyHoveredCell.posInGrid.z);
        List<GameObject> above = Grid.Instance.GetAboveCells(currentlyHoveredCell.posInGrid.x, currentlyHoveredCell.posInGrid.y, currentlyHoveredCell.posInGrid.z);
        List<GameObject> under = Grid.Instance.GetUnderCells(currentlyHoveredCell.posInGrid.x, currentlyHoveredCell.posInGrid.y, currentlyHoveredCell.posInGrid.z);

        all.AddRange(adjacents);
        all.AddRange(above);
        all.AddRange(under);

        underabove.AddRange(under);
        underabove.AddRange(above);

        foreach(GameObject go in all)
        {
            BaseBlock neighbour = go.GetComponent<BaseBlock>();
            switch (neighbour.globalRestrictions)
            {
                case Direction.All:
                    WarningMessage.instance.Warning("The block " + neighbour.data.blockName + " can't have anything around him", warningMessageDuration);
                    return false;
                case Direction.UnderAndAbove:
                    foreach (GameObject obj in underabove)
                    {
                        if (obj.GetComponent<BaseBlock>().data.blockName == neighbour.data.blockName)
                        {
                            WarningMessage.instance.Warning("The block " + neighbour.data.blockName + " can't have another block above or under him", warningMessageDuration);
                            return false;
                        }
                    }
                    break;
                case Direction.Adjacents:
                    foreach (GameObject obj in adjacents)
                    {
                        if (obj.GetComponent<BaseBlock>().data.blockName == neighbour.data.blockName)
                        {
                            WarningMessage.instance.Warning("The block " + neighbour.data.blockName + " can't have another block next to him", warningMessageDuration);
                            return false;
                        }
                    }
                    break;
                case Direction.Above:
                    foreach (GameObject obj in under)
                    {
                        if (obj.GetComponent<BaseBlock>().data.blockName == neighbour.data.blockName)
                        {
                            WarningMessage.instance.Warning("The block " + neighbour.data.blockName + " can't have another block above him", warningMessageDuration);
                            return false;
                        }
                    }
                    break;
                case Direction.Under:
                    foreach (GameObject obj in above)
                    {
                        if (obj.GetComponent<BaseBlock>().data.blockName == neighbour.data.blockName)
                        {
                            WarningMessage.instance.Warning("The block " + neighbour.data.blockName + " can't have another block under him", warningMessageDuration);
                            return false;
                        }
                    }
                    break;
            }
        }

        if (currentBlock.modifiers.Count > 0 && currentlyHoveredCell != null)
        {

            switch(currentBlock.globalRestrictions)
            {
                case Direction.All:
                    if (adjacents.Count > 0 || under.Count > 0 || above.Count > 0)
                    {
                        WarningMessage.instance.Warning("This block " + currentBlock.data.blockName + " can't have another block around him", warningMessageDuration);
                        return false;
                    }
                    else break;
                case Direction.UnderAndAbove:
                    if (under.Count > 0 || above.Count > 0)
                    {
                        WarningMessage.instance.Warning("This block " + currentBlock.data.blockName + " can't have another block above or under him", warningMessageDuration);
                        return false;
                    }
                    else break;
                case Direction.Adjacents:
                    if (adjacents.Count > 0)
                    {
                        WarningMessage.instance.Warning("This block " + currentBlock.data.blockName + " can't have another block next to him", warningMessageDuration);
                        return false;
                    }
                    else break;
                case Direction.Above:
                    if (above.Count > 0)
                    {
                        WarningMessage.instance.Warning("This block " + currentBlock.data.blockName + " can't have another block above him", warningMessageDuration);
                        return false;
                    }
                    else break;
                case Direction.Under:
                    if (under.Count > 0)
                    {
                        WarningMessage.instance.Warning("This block " + currentBlock.data.blockName + " can't have another block under him", warningMessageDuration);
                        return false;
                    }
                    else break;
            }

            foreach (ProximityModifier pm in currentBlock.modifiers)
            {
                string pmBlockName = pm.block.GetComponent<BaseBlock>().data.blockName;
                if (pm.restrictPlacement)
                    switch (pm.restrictions)
                    {
                        case Direction.All:
                            foreach (GameObject obj in all)
                            {
                                
                                if (obj.GetComponent<BaseBlock>().data.blockName == pmBlockName)
                                {
                                    WarningMessage.instance.Warning("This block " + currentBlock.data.blockName + " can't have " + pmBlockName + " around him", warningMessageDuration);
                                    return false;
                                }
                            }
                            break;
                        case Direction.UnderAndAbove:
                            foreach (GameObject obj in underabove)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pmBlockName)
                                {
                                    WarningMessage.instance.Warning("This block " + currentBlock.data.blockName + " can't have " + pmBlockName + " under or above him", warningMessageDuration);
                                    return false;
                                }
                            }
                            break;
                        case Direction.Adjacents:
                            foreach (GameObject obj in adjacents)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pmBlockName)
                                {
                                    WarningMessage.instance.Warning("This block " + currentBlock.data.blockName + " can't have " + pmBlockName + " next to him", warningMessageDuration);
                                    return false;
                                }
                            }
                            break;
                        case Direction.Above:
                            foreach (GameObject obj in above)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pmBlockName)
                                {
                                    WarningMessage.instance.Warning("This block " + currentBlock.data.blockName + " can't have " + pmBlockName + " above him", warningMessageDuration);
                                    return false;
                                }
                            }
                            break;
                        case Direction.Under:
                            foreach (GameObject obj in under)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pmBlockName)
                                {
                                    WarningMessage.instance.Warning("This block " + currentBlock.data.blockName + " can't have " + pmBlockName + " under him", warningMessageDuration);
                                    return false;
                                }
                            }
                            break;
                    }
            }


        }

        return true;
    }

}
