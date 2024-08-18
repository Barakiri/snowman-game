using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StrikeoutText : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener( delegate { Strikeout(GetComponent<Toggle>()); });
    }
    public void Strikeout(Toggle toggle)
    {
        if (toggle.isOn)
        {
            toggle.GetComponentInChildren<TextMeshProUGUI>().text = $"<s>{toggle.GetComponentInChildren<TextMeshProUGUI>().text}</s>";
        }
        else
        {
            toggle.GetComponentInChildren<TextMeshProUGUI>().text = toggle.GetComponentInChildren<TextMeshProUGUI>().text.Replace("<s>", "");
            toggle.GetComponentInChildren<TextMeshProUGUI>().text = toggle.GetComponentInChildren<TextMeshProUGUI>().text.Replace("</s>", "");
        }
    }
}