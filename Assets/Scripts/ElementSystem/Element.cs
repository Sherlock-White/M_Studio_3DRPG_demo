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
        switch (category)
        {
            case Const.ElementCategory.FIRE:
                return new Color((float)239 / 255, (float)76 / 255, (float)56 / 255 , 1);
            case Const.ElementCategory.WATER:
                return new Color((float)3 / 255, (float)165 / 255, (float)255 / 255, 1);
            case Const.ElementCategory.WIND:
                return new Color((float)86 / 255, (float)227 / 255, (float)193 / 255, 1);
            case Const.ElementCategory.ROCK:
                return new Color((float)231 / 255, (float)181 / 255, (float)56 / 255, 1);
            case Const.ElementCategory.ICE:
                return new Color((float)151 / 255, (float)223 / 255, (float)231 / 255, 1);
            case Const.ElementCategory.THUNDER:
                return new Color((float)187 / 255, (float)119 / 255, (float)237 / 255, 1);
            case Const.ElementCategory.GRASS:
                return new Color((float)118 / 255, (float)218 / 255, (float)42 / 255, 1);
            default:
                return Color.gray;
        }
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
