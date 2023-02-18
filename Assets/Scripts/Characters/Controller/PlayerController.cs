using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _anim;
    private GameObject _attackTarget; //攻击目标
    private float _lastAttackTime; //普攻计时器
    private CharacterStats _characterStats;

    private bool isCritical;
    private bool isDead;
    private bool isAutoAtk;
    //private float stopDistance;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _characterStats = GetComponent<CharacterStats>();
    }

    private void Start()
    {
        _characterStats.InitializedCharacterData();
        _characterStats.UpdateNavMeshAgentData(_agent, _characterStats); //更新
        //TODO:需放在OnEnable
        GameManager.Instance.RegisterPlayerStats(_characterStats); //注册data数据
        MouseManager.Instance.OnMouseClicked += EventMoveToTarget;
        MouseManager.Instance.OnEnemyClicked += EventAttack;
    }

    private void Update()
    {
        UpdateSwitchPlayerAnimation();
        AutoAtkTarget();
    }

    /// <summary>
    /// 持续检测，满足条件播放动画
    /// </summary>
    private void UpdateSwitchPlayerAnimation()
    {
        _anim.SetFloat(AnimStrings.SpeedFloat, _agent.velocity.sqrMagnitude); //更新Locomotion 混合树的动画
        _anim.SetBool(AnimStrings.isDead,isDead);
        
    }

    /// <summary>
    /// MouseManger OnMouseClicked事件绑定的委托
    /// </summary>
    private void EventMoveToTarget(Vector3 target)
    {
        if (isDead)
        {
            return;
        }
        StopAllCoroutines(); //停止所有协程 攻击停止
        isAutoAtk = false; //停止攻击

        _agent.speed = _characterStats.characterData.curSpeed;
        _agent.isStopped = false;
        _agent.destination = target;
    }

    /// <summary>
    /// MouseManger OnEnemyClicked事件绑定的委托
    /// 通过它启动攻击委托
    /// </summary>
    private void EventAttack(GameObject target)
    {
        if (isDead)
        {
            return;
        }
        if (target != null)
        {
            _attackTarget = target;
            StartCoroutine(MoveToAttackTarget());
        }
    }

    /// <summary>
    /// 协程，满足条件下一帧继续执行
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveToAttackTarget()
    {
 
        _agent.isStopped = false;
  
        while (Vector3.Distance(transform.position, _attackTarget.transform.position) >
               _characterStats.characterData.curAtkRange) //移向敌人
        {
            _agent.destination = _attackTarget.transform.position;
            yield return null; //满足条件下一帧继续执行
        }
        Vector3 lookAttackTarget = new Vector3(_attackTarget.transform.position.x, transform.position.y,
            _attackTarget.transform.position.z); //将看向位置调整
        transform.LookAt(lookAttackTarget); //面向敌人
        _agent.isStopped = true;
        //TODO:自动攻击
        isAutoAtk = true;
        AutoAtkTarget();
    }

    /// <summary>
    /// 播放攻击动画
    /// </summary>
    private void AutoAtkTarget()
    {
        if (isAutoAtk && _attackTarget != null && _lastAttackTime < 0)
        {
            transform.LookAt(_attackTarget.transform);
            isCritical = _characterStats.IsCritical();
            _anim.SetTrigger(AnimStrings.CommonAttackTrigger);
            _anim.SetBool(AnimStrings.isCritical, isCritical);
            _lastAttackTime = _characterStats.characterData.curCommonAtkTime; //重置时间
        }

        _lastAttackTime -= Time.deltaTime; //更新计时器
    }

    /// <summary>
    /// 通过动画事件，结算伤害
    /// </summary>
    private void AnimationEventHit()
    {
        if (_attackTarget!=null)
        {
            _characterStats.CalculateDamage(_attackTarget.GetComponent<CharacterStats>(), isCritical); //计算伤害
            _characterStats.TargetTakeDamage(_attackTarget.GetComponent<CharacterStats>()); //结算伤害
            if (isCritical)
            {
                _attackTarget.GetComponent<Animator>().SetTrigger(AnimStrings.GetHitTrigger);
            }

            _attackTarget.GetComponent<EnemyController>().IsCharacterDead();
            
            _attackTarget.GetComponent<HealthBarUI>().UpdateHealthBar(_attackTarget.GetComponent<CharacterStats>());
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
            GameManager.Instance.NotifyObservers();
        }
    }
    
}