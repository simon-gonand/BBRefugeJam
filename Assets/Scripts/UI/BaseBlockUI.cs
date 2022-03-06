using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseBlockUI : MonoBehaviour
{

    public GameObject block;
    BaseBlock baseBlock;
    Image image;
    public TextMeshProUGUI blockNameText;

    private void Start()
    {
        image = GetComponent<Image>();
        baseBlock = block.GetComponent<BaseBlock>();
        image.sprite = baseBlock.data.thumbnail;
        blockNameText.text = baseBlock.data.blockName;
    }

    public void LoadInfos()
    {
        BlockSelectorMenu.instance.infoPanel.Load(baseBlock.data);
        BlocSelector.Instance.currentBloc = block;
        Player.instance.currentBlock = baseBlock;
    }
}
