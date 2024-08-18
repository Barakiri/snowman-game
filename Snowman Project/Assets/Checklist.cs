using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Checklist : MonoBehaviour
{
    public GameObject ChecklistItem;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameManager.Instance.RequiredPickups.Count; i++)
        {
            GameObject newChecklistItem = Instantiate(ChecklistItem, transform);
            newChecklistItem.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.Instance.RequiredPickups[i].name;
            newChecklistItem.GetComponent<Toggle>().onValueChanged.AddListener(delegate { Strikeout(GetComponent<Toggle>()); });
            newChecklistItem.name = GameManager.Instance.RequiredPickups[i].name;
        }
    }

    public void Check(GameObject gameObject)
    {
        print(gameObject.name);
        foreach (Transform checklistitem in transform)
        {
            if(checklistitem.GetComponentInChildren<TextMeshProUGUI>().text == gameObject.name)
            {
                checklistitem.GetComponent<Toggle>().isOn = true;
            }
        }
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
