using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData_SO itemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO: ����Ʒ��ӵ�����

            //װ������
            GameManager.Instance.playerStats.EquipWeapon(itemData);
            Destroy(gameObject);
        }
    }
}
