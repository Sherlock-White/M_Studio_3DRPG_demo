using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementApplication : MonoBehaviour
{
    private List<Element> elementList = new List<Element>();

    public bool CreateAttackElementApplication(Element.ElementType type, Element.ElementCategory category)
    {
        bool enableAttachDescend = true;
        Element element = new Element(type, category, enableAttachDescend);
        if (element == null)
        {
            return false;
        }
        // ¿ªÊ¼Ë¥¼õ
        StartCoroutine(element.NaturalDescendElement());
        elementList.Add(element);
        element.onDisappear += RemoveElementApplication;
        return true;
    }

    public void RemoveElementApplication(Element element)
    {
        elementList.Remove(element);
        element.onDisappear -= RemoveElementApplication;
    }
}
