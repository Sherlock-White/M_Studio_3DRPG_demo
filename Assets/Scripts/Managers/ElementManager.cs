using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    static bool CreateElementApplication(Element.ElementType type, Element.ElementCategory category, GameObject target)
    {
        ElementApplication elementComponent = target.GetComponent<ElementApplication>();
        if(elementComponent == null)
        {
            target.AddComponent<ElementApplication>();
        }
        bool result = elementComponent.CreateAttackElementApplication(type, category);
        return result;
    }
}
