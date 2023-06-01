using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementApplication : MonoBehaviour
{
    private List<Element> elementList = new List<Element>();

    public Element CreateAttackElementApplication(string key, Const.ElementType type, Const.ElementCategory category)
    {
        bool enableAttachDescend = true;
        Element element = new Element(key, type, category, enableAttachDescend);
        if (element == null)
        {
            return null;
        }
        // ¿ªÊ¼Ë¥¼õ
        StartCoroutine(element.NaturalDescendElement());
        elementList.Add(element);
        element.onDisappear += RemoveElementApplication;
        return element;
    }

    public void RemoveElementApplication(Element element)
    {
        StopCoroutine(element.NaturalDescendElement());
        elementList.Remove(element);
        ElementManager.Instance.RemoveKey(element.key);
        element.onDisappear -= RemoveElementApplication;
    }

    public List<Element> GetElementList()
    {
        return elementList;
    }
}
