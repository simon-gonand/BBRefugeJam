using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningMessage : MonoBehaviour
{

    TextMeshProUGUI message;
    public static WarningMessage instance;

    private void Awake()
    {
        instance = this;
    }

    public void Warning(string text, float duration)
    {
        StopAllCoroutines();
        message.text = "";
        message.enabled = false;
        StartCoroutine(PopMessage(text, duration));
    }

    // Start
    void Start()
    {
        message = GetComponent<TextMeshProUGUI>();
    }

    public IEnumerator PopMessage(string text, float duration)
    {
        message.enabled = true;
        message.text = text;
        yield return new WaitForSeconds(duration);
        message.text = "";
        message.enabled = false;
    }
}
