using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : EnemyController
{
    [Header("Skill")]
    public float kickForce = 25;

    public void KickOff()
    {
        if (attackTarget != null && transform.IsFacingTarget(attackTarget.transform))
        {
            var targetStats = attackTarget.GetComponent<CharacterStats>();

            //��ȡ��ָ����ҵ�����
            Vector3 direction = (attackTarget.transform.position - transform.position).normalized;

            //�����ܻ�����
            targetStats.GetComponent<NavMeshAgent>().isStopped = true;
            targetStats.GetComponent<NavMeshAgent>().velocity = direction * kickForce;
            targetStats.TakeDamage(characterStats, targetStats);
            targetStats.GetComponent<Animator>().SetTrigger("Dizzy");
        }
    }
}