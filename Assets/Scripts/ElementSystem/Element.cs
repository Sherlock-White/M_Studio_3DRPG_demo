using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Element
{
    public string key;
    public readonly Const.ElementType type;
    public readonly Const.ElementCategory category;
    public float amount;
    public float time;
    public readonly float speed;

    public event Action<Element, float, Const.ElementType> onAmountChanged;
    public event Action<Element> onDisappear;

    public Element(string key, Const.ElementType type, Const.ElementCategory category, bool enableAttachDescend)
    {
        this.key = key;
        this.type = type;
        this.category = category;
        amount = GetInitialAmount(type);
        if (enableAttachDescend)
        {
            amount *= 0.8f;
        }
        time = 7 + 2.5f * amount;
        speed = amount / time;
    }

    // 获取初始元素量
    public static int GetInitialAmount(Const.ElementType type)
    {
        switch (type)
        {
            case Const.ElementType.WEAK:
                return 1;
            case Const.ElementType.STRONG:
                return 2;
            case Const.ElementType.EX_STRONG:
                return 4;
            default:
                Debug.LogError("invalid element type");
                return -1;
        }
    }

    public Color GetElementColor()
    {
        return Const.ElementColor[(int)category];
    }

    public void ConsumeAmount(float value)
    {
        amount = Mathf.Max(amount - value, 0);
        if(amount <= 0)
        {
            onDisappear?.Invoke(this);
        }
    }

    //元素衰减
    public IEnumerator NaturalDescendElement()
    {
        while(amount > 0)
        {
            amount -= Time.deltaTime * speed;
            onAmountChanged?.Invoke(this, amount, type);
            yield return null;
        }
        onDisappear?.Invoke(this);
    }
}
