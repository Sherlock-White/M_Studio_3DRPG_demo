using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const
{
    public enum ElementType
    {
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

    public static List<Color> ElementColor = new List<Color>() {
        new Color(239, 76, 56),
        new Color(3, 165, 255),
        new Color(86, 227, 193),
        new Color(231, 181, 56),
        new Color(182, 31, 95),
        new Color(187, 119, 237),
        new Color(118, 218, 42),
    };
}
