using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum EnemyBehaviourStates
{
    GUARD,
    PATROL,
    CHASE,
    DEAD
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterStats))]
public class EnemyController : MonoBehaviour,IEndGameObserver
{
    private EnemyBehaviourStates _enemyBehaviourStates;
    private NavMeshAgent _agent;
    private Animator _anim;
    private CharacterStats _characterStats;
    private Collider _collider;

    [Header("Basic Seetting")] //基础设置
    public float sightRadius; //可视范围

    private GameObject _attackTarget; //攻击目标

    public bool isGuard;
    //private float speed;

    [Header("Patrol Seetting")] //巡逻设置
    public float patrolRadius; //巡逻范围

    //public float lookAtTime; //计时
    private float remainLookAtTime;
    private Vector3 wayPoint; //生成的巡逻点
    private Vector3 guardPos;
    private Quaternion guardRotation; //出生旋转

    private float _lastCommonAtkTime; //上次攻击

    private bool playerDead;

    //动画 bool
    private bool isWalk;
    private bool isChase;
    private bool isFllow;
    private bool isCritical;
    private bool isDead;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        guardPos = transform.position;
        guardRotation = transform.rotation;
        _characterStats = GetComponent<CharacterStats>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _characterStats.InitializedCharacterData(); //初始化数据
        remainLookAtTime = _characterStats.characterData.lookAtTime;
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
        if (!playerDead)
        {
            SwitchStates();
            SwitchAnimation();
            _lastCommonAtkTime -= Time.deltaTime;
        }
     
    }

    private void OnEnable()
    {
        GameManager.Instance.AddObserver(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.RemoveObserver(this);
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
        if (FoundPlayer() && !isDead)
        {
            _enemyBehaviourStates = EnemyBehaviourStates.CHASE; //改变敌人状态
        }

        switch (_enemyBehaviourStates)
        {
            case EnemyBehaviourStates.GUARD:
                EnemyGuardBehaviour();
                break;
            case EnemyBehaviourStates.PATROL:
                EnemyPatrolBehaviour();
                break;
            case EnemyBehaviourStates.CHASE:
                EnemyChaseBehaviour();
                break;
            case EnemyBehaviourStates.DEAD:
                EnemyDeadBehaviour();
                break;
        }
    }

    /// <summary>
    /// 敌人守卫行为
    /// </summary>
    private void EnemyGuardBehaviour()
    {
        isChase = false;
        remainLookAtTime -= Time.deltaTime; //脱战等待时间
        if (remainLookAtTime < 0 && transform.position != guardPos)
        {
            isWalk = true;
            _agent.speed = _characterStats.characterData.curSpeed * 0.5f;
            _agent.destination = guardPos;
            if (Vector3.Distance(transform.position, guardPos) <= _agent.stoppingDistance) //停止
            {
                isWalk = false;
                transform.rotation = Quaternion.Lerp(transform.rotation, guardRotation, 0.01f); //复位旋转
            }
        }
    }

    /// <summary>
    /// 敌人巡逻行为
    /// </summary>
    private void EnemyPatrolBehaviour()
    {
        isChase = false;
        _agent.speed = _characterStats.characterData.curSpeed * 0.5f;
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
    /// 敌人追击时行为
    /// </summary>
    private void EnemyChaseBehaviour()
    {
        isWalk = false;
        isChase = true;
        _agent.speed = _characterStats.characterData.curSpeed;
        //追击 脱战
        if (!FoundPlayer())
        {
            isFllow = false;
            if (remainLookAtTime > 0)
            {
                _agent.destination = transform.position;
                remainLookAtTime -= Time.deltaTime;
            }
            else if (isGuard)
            {
                remainLookAtTime = _characterStats.characterData.lookAtTime; //重置等待时间
                _enemyBehaviourStates = EnemyBehaviourStates.GUARD;
            }
            else
            {
                remainLookAtTime = _characterStats.characterData.lookAtTime; //重置等待时间
                _enemyBehaviourStates = EnemyBehaviourStates.PATROL;
            }
        }
        else
        {
            isFllow = true;
            _agent.isStopped = false;
            _agent.destination = _attackTarget.transform.position;
        }

        //进入攻击范围
        if (IsInCommonAtkRange())
        {
            isFllow = false;
            _agent.isStopped = true;
            if (_lastCommonAtkTime < 0&& _attackTarget.GetComponent<CharacterStats>().characterData.curHp>0)
            {
                _lastCommonAtkTime = _characterStats.characterData.curCommonAtkTime;
                EnemyCommonAtk();
            }
        }
    }

    /// <summary>
    /// 敌人死亡时行为
    /// </summary>
    private void EnemyDeadBehaviour()
    {
        _collider.enabled = false;
        _agent.enabled = false;
        _anim.SetBool(AnimStrings.isDead, true);
        Destroy(gameObject, 2f);
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
    /// 获取新的巡逻点
    /// </summary>
    private void GetNewWayPoint()
    {
        remainLookAtTime = _characterStats.characterData.lookAtTime;
        float wayPointX = Random.Range(-patrolRadius, patrolRadius);
        float wayPointZ = Random.Range(-patrolRadius, patrolRadius);
        wayPoint = new Vector3(guardPos.x + wayPointX, transform.position.y, guardPos.z + wayPointZ);
        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(wayPoint, out hit, _agent.height, 1) ? hit.position : transform.position;
    }

    /// <summary>
    /// 是否在普通攻击范围
    /// </summary>
    private bool IsInCommonAtkRange()
    {
        if (_attackTarget != null)
        {
            return Vector3.Distance(transform.position, _attackTarget.transform.position) <=
                   _characterStats.characterData.curAtkRange;
        }

        return false;
    }

    /// <summary>
    /// 播放攻击动画
    /// </summary>
    private void EnemyCommonAtk()
    {
        transform.LookAt(_attackTarget.transform);
        isCritical = _characterStats.IsCritical();
        _anim.SetTrigger(AnimStrings.CommonAttackTrigger);
        _anim.SetBool(AnimStrings.isCritical, isCritical);
    }

    /// <summary>
    /// 通过动画事件，结算伤害
    /// </summary>
    private void AnimationEventHit()
    {
        _characterStats.CalculateDamage(_attackTarget.GetComponent<CharacterStats>(), isCritical); //计算伤害
        _characterStats.TargetTakeDamage(_attackTarget.GetComponent<CharacterStats>()); //结算伤害
        if (isCritical)
        {
            _attackTarget.GetComponent<Animator>().SetTrigger(AnimStrings.GetHitTrigger);
        }

        _attackTarget.GetComponent<PlayerController>().IsCharacterDead();
        //TODO:伤害数字
        if (!playerDead)
        {
            Debug.Log(transform.name + "攻击了" + _attackTarget.name + "造成了" + _characterStats.characterData.curDamage);
        }
      
    }

    /// <summary>
    /// 是否hp为o
    /// </summary>
    public void IsCharacterDead()
    {
        if (_characterStats.characterData.curHp <= 0)
        {
            isDead = true;
            _enemyBehaviourStates = EnemyBehaviourStates.DEAD;
        }
    }

    /// <summary>
    /// 在场景中画出物体的视野范围
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    public void EndNotify()
    {
        _anim.SetBool(AnimStrings.isWin,true);
        playerDead = true;
        isChase = false;
        isFllow = false;
        _attackTarget = null;
    }
}