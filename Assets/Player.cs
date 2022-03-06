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
        Debug.Log("Hello");

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

        if (currentBlock.modifiers.Count > 0 && currentlyHoveredCell != null)
        {
            foreach(GameObject go in all)
            {
                BaseBlock neighbour = go.GetComponent<BaseBlock>();
                switch (neighbour.globalRestrictions)
                {
                    case Direction.All:
                        return false;
                    case Direction.UnderAndAbove:
                        foreach (GameObject obj in underabove)
                        {
                            if (obj.GetComponent<BaseBlock>().data.blockName == neighbour.data.blockName) return false;
                        }
                        break;
                    case Direction.Adjacents:
                        foreach (GameObject obj in adjacents)
                        {
                            if (obj.GetComponent<BaseBlock>().data.blockName == neighbour.data.blockName) return false;
                        }
                        break;
                    case Direction.Above:
                        Debug.Log("yes");
                        foreach (GameObject obj in under)
                        {
                            if (obj.GetComponent<BaseBlock>().data.blockName == neighbour.data.blockName) return false;
                        }
                        break;
                    case Direction.Under:
                        foreach (GameObject obj in above)
                        {
                            if (obj.GetComponent<BaseBlock>().data.blockName == neighbour.data.blockName) return false;
                        }
                        break;
                }
            }

            switch(currentBlock.globalRestrictions)
            {
                case Direction.All:
                    if (adjacents.Count > 0 || under.Count > 0 || above.Count > 0) return false;
                    else break;
                case Direction.UnderAndAbove:
                    if (under.Count > 0 || above.Count > 0) return false;
                    else break;
                case Direction.Adjacents:
                    if (adjacents.Count > 0) return false;
                    else break;
                case Direction.Above:
                    if (above.Count > 0) return false;
                    else break;
                case Direction.Under:
                    if (under.Count > 0) return false;
                    else break;
            }

            foreach (ProximityModifier pm in currentBlock.modifiers)
            {
                if(pm.restrictPlacement)
                    switch (pm.restrictions)
                    {
                        case Direction.All:
                            foreach (GameObject obj in all)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pm.block.GetComponent<BaseBlock>().data.blockName) return false;
                            }
                            break;
                        case Direction.UnderAndAbove:
                            foreach (GameObject obj in underabove)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pm.block.GetComponent<BaseBlock>().data.blockName) return false;
                            }
                            break;
                        case Direction.Adjacents:
                            foreach (GameObject obj in adjacents)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pm.block.GetComponent<BaseBlock>().data.blockName) return false;
                            }
                            break;
                        case Direction.Above:
                            foreach (GameObject obj in above)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pm.block.GetComponent<BaseBlock>().data.blockName) return false;
                            }
                            break;
                        case Direction.Under:
                            foreach (GameObject obj in under)
                            {
                                if (obj.GetComponent<BaseBlock>().data.blockName == pm.block.GetComponent<BaseBlock>().data.blockName) return false;
                            }
                            break;
                    }
            }


        }

        return true;
    }

}
