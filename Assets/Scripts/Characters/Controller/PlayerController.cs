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
    private CharacterStats _playerStats;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _playerStats = GetComponent<CharacterStats>();
       
    }

    private void Start()
    {
        //TODO:需放在OnEnable
        DataManager.Instance.RegisterPlayerStats(_playerStats); //注册data数据
        DataManager.Instance.InitializedPlayerData();
        MouseManager.Instance.OnMouseClicked += EventMoveToTarget;
        MouseManager.Instance.OnEnemyClicked += EventAttack;

        _agent.speed = _playerStats.characterData.curSpeed;

    }

    private void Update()
    {
        UpdateSwitchPlayerAnimation();
        _lastAttackTime -= Time.deltaTime; //更新计时器
    }

    /// <summary>
    /// 持续检测，满足条件播放动画
    /// </summary>
    private void UpdateSwitchPlayerAnimation()
    {
        _anim.SetFloat(AnimStrings.SpeedFloat, _agent.velocity.sqrMagnitude); //更新Locomotion 混合树的动画
    }

    /// <summary>
    /// MouseManger OnMouseClicked事件绑定的委托
    /// </summary>
    private void EventMoveToTarget(Vector3 target)
    {
        StopAllCoroutines(); //停止所有协程 攻击停止
        _agent.speed = _playerStats.characterData.curSpeed;
        _agent.isStopped = false;
        _agent.destination = target;
    }

    /// <summary>
    /// MouseManger OnEnemyClicked事件绑定的委托
    /// 通过它启动攻击委托
    /// </summary>
    private void EventAttack(GameObject target)
    {
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
        Vector3 lookAttackTarget = new Vector3(_attackTarget.transform.position.x, transform.position.y,
            _attackTarget.transform.position.z); //将看向位置调整

        transform.LookAt(lookAttackTarget); //面向敌人
        while (Vector3.Distance(transform.position, _attackTarget.transform.position) >
               _playerStats.characterData.curAtkRange) //移向敌人
        {
            _agent.destination = _attackTarget.transform.position;
            yield return null; //满足条件下一帧继续执行
        }

        _agent.isStopped = true;

        if (_lastAttackTime < 0)
        {
            _anim.SetTrigger(AnimStrings.CommonAttackTrigger);
            _lastAttackTime = _playerStats.characterData.curAtkTime; //重置时间
        }
    }
}