using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCavasUI : MonoBehaviour
{
    public int preloadCount = 20;
    public GameObject templatePrefab;
    private Transform elementPool;

    private void Awake()
    {
        elementPool = transform.Find("ElementPool");
        PreloadElementUI();
    }

    private void PreloadElementUI()
    {
        for(int i = 0;i < preloadCount; i++)
        {
            Instantiate(templatePrefab, elementPool);
        }
    }

    public void GetFromPoolAndInit(Element element, GameObject target)
    {
        Transform elementUI = elementPool.transform.GetChild(0);
        if (elementUI != null)
        {
            elementUI.GetComponent<ElementBarUI>().SetupElementUI(element, target);
            elementUI.SetParent(transform);
        }
        else
        {
            GameObject cloneElementUI = Instantiate(templatePrefab);
            cloneElementUI.GetComponent<ElementBarUI>().SetupElementUI(element, target);
            cloneElementUI.transform.SetParent(transform);
        }
    }
}
