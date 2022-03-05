using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BlockSelectorMenu : MonoBehaviour
{

    public TextMeshProUGUI seedNameText;

    public static BlockSelectorMenu instance;
    public TextMeshProUGUI moneyText;
    public InfoPanel infoPanel;

    Animator animator;
    Image panel;

    public Tab currentTab;


    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        animator = GetComponent<Animator>();
        panel = GetComponent<Image>();
    }


    public void Select()
    {
        
    }

    bool show;
    public void ShowHideSelectionPanel()
    {
        if(show)
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

    public void SetPanelToTabColor(Image tab)
    {
        panel.color = tab.color;
    }

    public void DisableCurrentTab()
    {
        
    }

    public void SetCurrentTab(Tab tab)
    {
        if(currentTab != null) currentTab.scrollView.SetActive(false);

        currentTab = tab;
        currentTab.scrollView.SetActive(true);
    }


    private void Update()
    {
        moneyText.text = "CASH: " + PlayerData.instance.money.ToString() + "$";
    }
}
