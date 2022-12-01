using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO characterData;
    public AttackData_SO attackData;
    [HideInInspector]
    public bool isCritical;

    #region Read from Data_SO
    public int MaxHealth
    {
        get { return characterData != null ? characterData.maxHealth: 0; }
        set { characterData.maxHealth = value; }
    }
    public int CurrentHealth
    {
        get { return characterData != null ? characterData.currentHealth : 0; }
        set { characterData.currentHealth = value; }
    }
    public int BaseDefence
    {
        get { return characterData != null ? characterData.baseDefence : 0; }
        set { characterData.baseDefence = value; }
    }
    public int CurrentDefence
    {
        get { return characterData != null ? characterData.currentDefence : 0; }
        set { characterData.currentDefence = value; }
    }
    #endregion
}
