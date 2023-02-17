using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum EnemyBehaviourStates
{
    GUARD,
    PATROL,
    CHASE,
    DEAD
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private EnemyBehaviourStates _enemyBehaviourStates;
    private NavMeshAgent _agent;
    private Animator _anim;
    private CharacterStats _enemyStats;
    
    [Header("Basic Seetting")] //基础设置
    public float sightRadius; //可视范围

    private GameObject _attackTarget; //攻击目标
    public bool isGuard;
    private float speed;

    [Header("Patrol Seetting")] //巡逻设置
    public float patrolRadius; //巡逻范围

    //public float lookAtTime; //计时
    private float remainLookAtTime;
    private Vector3 wayPoint; //生成的巡逻点
    private Vector3 guardPos;

    //动画 bool
    private bool isWalk;
    private bool isChase;
    private bool isFllow;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        speed = _agent.speed;
        _anim = GetComponent<Animator>();
        guardPos = transform.position;
        _enemyStats = GetComponent<CharacterStats>();
        remainLookAtTime = _enemyStats.characterData.lookAtTime;
    }

    private void Start()
    {
        if (isGuard)
        {
            _enemyBehaviourStates = EnemyBehaviourStates.GUARD;
        }
        else
        {
            _enemyBehaviourStates = EnemyBehaviourStates.PATROL;
            GetNewWayPoint();
        }
    }

    private void Update()
    {
        SwitchStates();
        SwitchAnimation();
    }

    /// <summary>
    /// 检测是否需要切换动画
    /// </summary>
    private void SwitchAnimation()
    {
        _anim.SetBool(AnimStrings.isWalk, isWalk);
        _anim.SetBool(AnimStrings.isChase, isChase);
        _anim.SetBool(AnimStrings.isFollow, isFllow);
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    private void SwitchStates()
    {
        if (FoundPlayer())
        {
            remainLookAtTime = _enemyStats.characterData.lookAtTime; //重置等待时间
            _enemyBehaviourStates = EnemyBehaviourStates.CHASE; //改变敌人状态
        }

        switch (_enemyBehaviourStates)
        {
            case EnemyBehaviourStates.GUARD:
                break;
            case EnemyBehaviourStates.PATROL:
                EnemyPatrolBehaviour();
                break;
            case EnemyBehaviourStates.CHASE:
                EnemyChaseBehaviour();
                break;
            case EnemyBehaviourStates.DEAD:
                break;
        }
    }


    /// <summary>
    /// 获取新的巡逻点
    /// </summary>
    private void GetNewWayPoint()
    {
        remainLookAtTime = _enemyStats.characterData.lookAtTime;
        float wayPointX = Random.Range(-patrolRadius, patrolRadius);
        float wayPointZ = Random.Range(-patrolRadius, patrolRadius);
        wayPoint = new Vector3(guardPos.x + wayPointX, transform.position.y, guardPos.z + wayPointZ);
        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(wayPoint, out hit, _agent.height, 1) ? hit.position : transform.position;
    }

    /// <summary>
    /// 敌人巡逻行为
    /// </summary>
    private void EnemyPatrolBehaviour()
    {
        isChase = false;
        _agent.speed = speed * 0.5f;
        if (Vector3.Distance(transform.position, wayPoint) <= _agent.stoppingDistance)
        {
            isWalk = false;
            if (remainLookAtTime > 0)
            {
                remainLookAtTime -= Time.deltaTime;
            }
            else
            {
                GetNewWayPoint();
            }
        }
        else
        {
            /*巡逻*/
            isWalk = true;
            _agent.destination = wayPoint;
        }
    }

    /// <summary>
    /// 判断范围内是否存在Player
    /// </summary>
    private bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                _attackTarget = target.gameObject; //找到攻击目标

                return true;
            }
        }

        _attackTarget = null;
        return false;
    }

    /// <summary>
    /// enemy追击时行为
    /// </summary>
    private void EnemyChaseBehaviour()
    {
        isWalk = false;
        isChase = true;
        _agent.speed = speed;
        if (!FoundPlayer()) //脱战行为
        {
            isFllow = false;
            if (remainLookAtTime > 0)
            {
                _agent.destination = transform.position;
                remainLookAtTime -= Time.deltaTime;
            }
            else if (isGuard)
            {
                _enemyBehaviourStates = EnemyBehaviourStates.GUARD;
            }
            else
            {
                _enemyBehaviourStates = EnemyBehaviourStates.PATROL;
            }
        }
        else
        {
            isFllow = true;
            _agent.isStopped = false;
            _agent.destination = _attackTarget.transform.position;
        }
    }


    /*在场景中画出物体的视野范围*/
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}