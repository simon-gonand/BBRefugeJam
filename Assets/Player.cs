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
                    WarningMessage.instance.PopMessage("The block " + neighbour.data.blockName + " can't have anything around him", 1.0f);
                    return false;
                case Direction.UnderAndAbove:
                    foreach (GameObject obj in underabove)
                    {
                        if (obj.GetComponent<BaseBlock>().data.blockName == neighbour.data.blockName)
                        {
                            WarningMessage.instance.PopMessage("The block " + neighbour.data.blockName + " can't have another block above or under him", 1.0f);
                            return false;
                        }
                    }
                    break;
                case Direction.Adjacents:
                    foreach (GameObject obj in adjacents)
                    {
                        if (obj.GetComponent<BaseBlock>().data.blockName == neighbour.data.blockName)
                        {
                            WarningMessage.instance.PopMessage("The block " + neighbour.data.blockName + " can't have another block next to him", 1.0f);
                            return false;
                        }
                    }
                    break;
                case Direction.Above:
                    foreach (GameObject obj in under)
                    {
                        if (obj.GetComponent<BaseBlock>().data.blockName == neighbour.data.blockName)
                        {
                            WarningMessage.instance.PopMessage("The block " + neighbour.data.blockName + " can't have another block above him", 1.0f);
                            return false;
                        }
                    }
                    break;
                case Direction.Under:
                    foreach (GameObject obj in above)
                    {
                        if (obj.GetComponent<BaseBlock>().data.blockName == neighbour.data.blockName)
                        {
                            WarningMessage.instance.PopMessage("The block " + neighbour.data.blockName + " can't have another block under him", 1.0f);
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
                        WarningMessage.instance.PopMessage("This block " + currentBlock.data.blockName + " can't have another block around him", 1.0f);
                        return false;
                    }
                    else break;
                case Direction.UnderAndAbove:
                    if (under.Count > 0 || above.Count > 0)
                    {
                        WarningMessage.instance.PopMessage("This block " + currentBlock.data.blockName + " can't have another block above or under him", 1.0f);
                        return false;
                    }
                    else break;
                case Direction.Adjacents:
                    if (adjacents.Count > 0)
                    {
                        WarningMessage.instance.PopMessage("This block " + currentBlock.data.blockName + " can't have another block next to him", 1.0f);
                        return false;
                    }
                    else break;
                case Direction.Above:
                    if (above.Count > 0)
                    {
                        WarningMessage.instance.PopMessage("This block " + currentBlock.data.blockName + " can't have another block above him", 1.0f);
                        return false;
                    }
                    else break;
                case Direction.Under:
                    if (under.Count > 0)
                    {
                        WarningMessage.instance.PopMessage("This block " + currentBlock.data.blockName + " can't have another block under him", 1.0f);
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
                                    WarningMessage.instance.PopMessage("This block " + currentBlock.data.blockName + " can't have " + pmBlockName + " around him", 1.0f);
                                    return false;
                                }
                            }
                            break;
                        case Direction.UnderAndAbove:
                            foreach (GameObject obj in underabove)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pmBlockName) return false;
                            }
                            break;
                        case Direction.Adjacents:
                            foreach (GameObject obj in adjacents)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pmBlockName) return false;
                            }
                            break;
                        case Direction.Above:
                            foreach (GameObject obj in above)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pmBlockName) return false;
                            }
                            break;
                        case Direction.Under:
                            foreach (GameObject obj in under)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pmBlockName) return false;
                            }
                            break;
                    }
            }


        }

        return true;
    }

}
