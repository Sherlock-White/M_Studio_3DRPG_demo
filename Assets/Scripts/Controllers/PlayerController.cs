using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private CharacterStats characterStats;
    private GameObject attackTarget;
    private float lastAttackTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
    }

    private void Start()
    {
        MouseManager.Instance.OnMouseClicked += MoveToTarget;
        MouseManager.Instance.OnEnemyClicked += EventAttack;
    }

    private void Update()
    {
        SwitchAnimation();
        lastAttackTime -= Time.deltaTime;
    }
    public void SwitchAnimation()
    {
        anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }
    public void MoveToTarget(Vector3 target)
    {
        StopAllCoroutines();
        agent.isStopped = false;
        agent.destination = target;
    }
    public void EventAttack(GameObject target)
    {
        if(target != null)
        {
            attackTarget = target;
            StartCoroutine(MoveToAttackTarget());
        }
    }
    IEnumerator MoveToAttackTarget()
    {
        agent.isStopped = false;
        transform.LookAt(attackTarget.transform);
        while (Vector3.Distance(attackTarget.transform.position, transform.position) > characterStats.attackData.attackRange)
        {
            agent.destination = attackTarget.transform.position;
            yield return null;
        }
        agent.isStopped = true;

        if(lastAttackTime < 0)
        {
            anim.SetTrigger("Attack");
            //reset attack CD
            lastAttackTime = 0.5f;
        }
    }
}
