using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI resistanceText;
    public TextMeshProUGUI beautyText;

    public TextMeshProUGUI waterText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI weaponryText;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Load(BlockData data)
    {
        priceText.text = data.price.ToString() + "$";
        hpText.text = data.hp.ToString();
        resistanceText.text = data.resistance.ToString();
        beautyText.text = data.beauty.ToString();


        foreach (Resource r in data.resources)
        {
            switch (r.type)
            {
                case ResourceType.Water:
                    waterText.text = r.value.ToString();
                    break;
                case ResourceType.Food:
                    foodText.text = r.value.ToString();
                    break;
                case ResourceType.Energy:
                    energyText.text = r.value.ToString();
                    break;
                case ResourceType.Weaponry:
                    weaponryText.text = r.value.ToString();
                    break;
            }
        }
    }

    bool show;
    Animator animator;
    public void ShowHideInfoPanel()
    {
        if (show)
        {
            animator.SetTrigger("Show");
            show = false;
        }
        else
        {
            animator.SetTrigger("Hide");
            show = true;
        }
    }
}
