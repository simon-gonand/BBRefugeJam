using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SeedPanel : MonoBehaviour
{

    Animator animator;

    public TextMeshProUGUI seedNameText;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI beautyText;

    public TextMeshProUGUI waterText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI equipmentText;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GetRandomRules();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetRandomRules()
    {
        Player.instance.rules = rulesList[Random.Range(0, rulesList.Count)];
        Load();
    }


    bool show;
    public void ShowHideSeedPanel()
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

    public void Load()
    {
        GameRules rules = Player.instance.rules;

        seedNameText.text = rules.seedName;

        moneyText.text = rules.initialMoney + "$";
        beautyText.text = "x" + rules.beautyMultiplier.ToString("F2");

        waterText.text = "x" + rules.waterMultiplier.ToString("F2");
        foodText.text = "x" + rules.foodMultiplier.ToString("F2");
        energyText.text = "x" + rules.energyMultiplier.ToString("F2");
        equipmentText.text = "x" + rules.equipmentMultiplier.ToString("F2");
    }

    public List<GameRules> rulesList = new List<GameRules>();
}
