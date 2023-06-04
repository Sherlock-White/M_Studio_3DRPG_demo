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
            Transform temp = target.transform.Find("elementBarPoint/Canvas/Scroll View/Viewport/Content");
            elementUI.SetParent(temp);
        }
        else
        {
            GameObject cloneElementUI = Instantiate(templatePrefab);
            cloneElementUI.GetComponent<ElementBarUI>().SetupElementUI(element, target);
            Transform temp = target.transform.Find("elementBarPoint/Canvas/Scroll View/Viewport/Content");
            cloneElementUI.transform.SetParent(temp);
        }
    }
}
