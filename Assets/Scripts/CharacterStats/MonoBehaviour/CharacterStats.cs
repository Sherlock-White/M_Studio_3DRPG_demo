using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public event Action<int, int> UpdateHealthBarOnAttack;
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
        //防止出现负数伤害
        int damage = Mathf.Max(attacker.CurrentDamage() - defender.CurrentDefence,0);
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        if (attacker.isCritical)
        {
            defender.GetComponent<Animator>().SetTrigger("Hit");
        }
        //Update UI
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth,MaxHealth);
        //经验update
        if(CurrentHealth <= 0)
            attacker.characterData.UpdateExp(characterData.killPoint);
    }

    public void TakeDamage(int damage,CharacterStats defender)
    {
        int currentDamage = Mathf.Max(damage - defender.CurrentDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - currentDamage, 0);
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0)
            GameManager.Instance.playerStats.characterData.UpdateExp(characterData.killPoint);
    }

    private int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attackData.minDanger, attackData.maxDanger);
        if (isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;
            Debug.Log("暴击！" + coreDamage);
        }
        return (int)coreDamage;
    }
    #endregion
}
