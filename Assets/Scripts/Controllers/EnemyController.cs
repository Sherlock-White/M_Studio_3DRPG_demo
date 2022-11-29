using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates {GUARD,PATROL,CHASE,DEAD }

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private EnemyStates enemyState;
    private NavMeshAgent agent;
    private Animator anim;

    [Header("Basic Settings")]
    public float sightRadius;
    public bool isGuard;
    private float speed;
    bool isWalk;
    bool isChase;
    bool isFollow;
    private GameObject attackTatget;
    public float lookAtTime;
    private float remainLookAtTime;

    [Header("Patrol State")]
    public float patrolRange;
    private Vector3 wayPoint;
    private Vector3 guardPos;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        speed = agent.speed;
        guardPos = transform.position;
        remainLookAtTime = lookAtTime;
    }

    private void Start()
    {
        if (isGuard)
        {
            enemyState = EnemyStates.GUARD;
        }
        else
        {
            enemyState = EnemyStates.PATROL;
            GetNewWayPoint();
        }
    }

    private void Update()
    {
        SwitchStates();
        SwitchAnimation();
    }

    void SwitchAnimation()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Chase", isChase);
        anim.SetBool("Follow", isFollow);
    }

    void SwitchStates()
    {
        if (FindPlayer())
        {
            enemyState = EnemyStates.CHASE;
        }

        switch (enemyState)
        {
            case EnemyStates.GUARD:
                break;
            case EnemyStates.PATROL:
                isChase = false;
                agent.speed = 0.5f * speed;
                EnemyPatrol();
                break;
            case EnemyStates.CHASE:
                agent.speed = speed;
                isWalk = false;
                isChase = true;
                EnemyChase();
                break;
            case EnemyStates.DEAD:
                break;
        }
    }

    void EnemyPatrol()
    {
        //check whether enemy has moved to target point
        if (Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance)
        {
            isWalk = false;
            if (remainLookAtTime > 0)
                remainLookAtTime -= Time.deltaTime;
            else
            {
                remainLookAtTime = lookAtTime;
                GetNewWayPoint();
            }
        }
        else
        {
            isWalk = true;
            agent.destination = wayPoint;
        }
    }

    void EnemyChase()
    {
        if (!FindPlayer())
        {
            isFollow = false;
            if (remainLookAtTime > 0)
            {
                agent.destination = transform.position;
                remainLookAtTime -= Time.deltaTime;
            }
            else if (isGuard)
                enemyState = EnemyStates.GUARD;
            else
                enemyState = EnemyStates.PATROL;
        }
        else
        {
            isFollow = true;
            agent.destination = attackTatget.transform.position;
        }
    }

    bool FindPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        foreach(var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                attackTatget = target.gameObject;
                return true;
            }
        }
        attackTatget = null;
        return false;
    }

    void GetNewWayPoint()
    {
        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomZ = Random.Range(-patrolRange, patrolRange);
        Vector3 randomPoint = new Vector3(guardPos.x + randomX, transform.position.y, guardPos.z + randomZ);
        NavMeshHit hit;
        //1 represent Walkable
        wayPoint = NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 1) ? hit.position : transform.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}
