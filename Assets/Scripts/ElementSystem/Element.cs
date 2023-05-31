using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Element
{
    public readonly ElementType type;
    public readonly ElementCategory category;
    public float amount;
    public readonly float time;
    public readonly float speed;
    public event Action<float, ElementType> onAmountChanged;
    public event Action<Element> onDisappear;

    public enum ElementType { 
        WEAK = 0,
        STRONG = 1,
        EX_STRONG = 2,
    }

    public enum ElementCategory
    {
        FIRE = 0,
        WATER = 1,
        WIND = 2,
        ROCK = 3,
        ICE = 4,
        THUNDER = 5,
        GRASS = 6,
    }

    public Element(ElementType type, ElementCategory category, bool enableAttachDescend)
    {
        this.type = type;
        this.category = category;
        amount = GetInitialAmount(type);
        if (enableAttachDescend)
        {
            amount *= 0.8f;
        }
        amount = GetInitialAmount(type);
        time = 7 + 2.5f * amount;
        speed = amount / time;
    }

    // 获取初始元素量
    public static int GetInitialAmount(ElementType type)
    {
        switch (type)
        {
            case ElementType.WEAK:
                return 1;
            case ElementType.STRONG:
                return 2;
            case ElementType.EX_STRONG:
                return 4;
            default:
                return -1;
        }
    }

    public Color GetElementColor()
    {
        switch (category)
        {
            case ElementCategory.FIRE:
                return new Color(239, 76, 56);
            case ElementCategory.GRASS:
                return new Color(118, 218, 42);
            case ElementCategory.ICE:
                return new Color(182, 31, 95);
            case ElementCategory.ROCK:
                return new Color(231, 181, 56);
            case ElementCategory.THUNDER:
                return new Color(187, 119, 237);
            case ElementCategory.WATER:
                return new Color(3, 165, 255);
            case ElementCategory.WIND:
                return new Color(86, 227, 193);
            default:
                return Color.white;
        }
    }

    public void ConsumeAmount(float value)
    {
        amount = Mathf.Max(amount - value, 0);
    }

    //元素衰减
    public IEnumerator NaturalDescendElement()
    {
        while(amount > 0)
        {
            amount -= Time.deltaTime * speed;
            onAmountChanged?.Invoke(amount, type);
            yield return null;
        }
        onDisappear?.Invoke(this);
    }
}
