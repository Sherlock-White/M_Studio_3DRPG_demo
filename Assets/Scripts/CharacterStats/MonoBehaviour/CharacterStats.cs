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
    private AttackData_SO baseAttackData;

    [Header("Weapon")]
    public Transform weaponSlot;

    [HideInInspector]
    public bool isCritical;

    private void Awake()
    {
        if(templateData != null)
            characterData = Instantiate(templateData);

        baseAttackData = Instantiate(attackData);
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

    #region Character Combat
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
        float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);
        if (isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;
            Debug.Log("暴击！" + coreDamage);
        }
        return (int)coreDamage;
    }
    #endregion

    #region Equip Weapon

    public void ChangeWeapon(ItemData_SO weapon)
    {
        UnEquipWeapon();
        EquipWeapon(weapon);
    }

    public void EquipWeapon(ItemData_SO weapon)
    {
        if(weapon.weaponPrefab != null)
        {
            Instantiate(weapon.weaponPrefab,weaponSlot);
        }
        //TODO：更新属性
        //TODO：切换动画
        attackData.ApplyWeaponData(weapon.weaponData);
    }

    public void UnEquipWeapon()
    {
        if(weaponSlot.transform.childCount != 0)
        {
            for(int i = 0;i < weaponSlot.transform.childCount; i++)
            {
                Destroy(weaponSlot.transform.GetChild(i).gameObject);
            }
        }
        attackData.ApplyWeaponData(baseAttackData);
        //TODO：切换动画
    }

    #endregion

    #region Apply Data Change
    public void ApplyHealth(int amount)
    {
        CurrentHealth = Math.Min(MaxHealth, CurrentHealth + amount);
    }
    #endregion
}
