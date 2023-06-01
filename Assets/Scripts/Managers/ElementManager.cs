using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : Singleton<ElementManager>
{
    private Dictionary<string, bool> keyMap = new Dictionary<string, bool>();
    private ElementCavasUI elementCanvasUI;


    public bool AddKey(string key)
    {
        if (keyMap.ContainsKey(key))
        {
            return false;
        }
        else
        {
            keyMap.Add(key, true);
            return true;
        }
    }

    public bool RemoveKey(string key)
    {
        if (keyMap.ContainsKey(key))
        {
            keyMap.Remove(key);
            return true;
        }
        else
        {
            Debug.LogError(string.Format("dictionary dose not contains %s", key));
            return false;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        GameObject elementCanvas = GameObject.Find("ElementCanvas");
        elementCanvasUI = elementCanvas.GetComponent<ElementCavasUI>();
        if(elementCanvas == null)
        {
            elementCanvas.AddComponent<ElementCavasUI>();
        }
    }

    public void CreateElementApplication(Const.ElementType type, Const.ElementCategory category, GameObject target)
    {
        ElementApplication elementComponent = target.GetComponent<ElementApplication>();
        if(elementComponent == null)
        {
            target.AddComponent<ElementApplication>();
        }
        bool res = false;
        string key = "";
        while (!res)
        {
            key = string.Format("%s_%d_%d", target.name, (int)type, (int)category, Random.Range(0, 100));
            res = AddKey(key);
        }
        Element element = elementComponent.CreateAttackElementApplication(key, type, category);
        elementCanvasUI.GetFromPoolAndInit(element, target);
    }

    public List<Element> GetElementApplicationList(GameObject target)
    {
        if (target == null) return null;
        ElementApplication elementApplication = target.GetComponent<ElementApplication>();
        if (elementApplication == null) return null;
        return elementApplication.GetElementList();
    }
}
