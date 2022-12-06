using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO templateData;
    public CharacterData_SO characterData;
    public AttackData_SO attackData;
    [HideInInspector]
    public bool isCritical;

    private void Awake()
    {
        if(templateData != null)
            characterData = Instantiate(templateData);
    }

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

    #region Charater Combat
    public void TakeDamage(CharacterStats attacker,CharacterStats defender)
    {
        //��ֹ���ָ����˺�
        int damage = Mathf.Max(attacker.CurrentDamage() - defender.CurrentDefence,0);
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        if (attacker.isCritical)
        {
            defender.GetComponent<Animator>().SetTrigger("Hit");
        }
        //TODO: Update UI
        //TODO: ����update
    }

    public void TakeDamage(int damage,CharacterStats defender)
    {
        int currentDamage = Mathf.Max(damage - defender.CurrentDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - currentDamage, 0);
    }

    private int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attackData.minDanger, attackData.maxDanger);
        if (isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;
            Debug.Log("������" + coreDamage);
        }
        return (int)coreDamage;
    }
    #endregion
}
