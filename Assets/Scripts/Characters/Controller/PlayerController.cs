using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _anim;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //TODO:需放在OnEnable
        MouseManager.Instance.OnMouseClicked += MoveToTarget;
    }

    private void Update()
    {
        UpdateSwitchPlayerAnimation();
    }
    
    /// <summary>
    /// 持续检测，满足条件播放动画
    /// </summary>
    private void UpdateSwitchPlayerAnimation()
    {
        _anim.SetFloat(AnimStrings.SpeedFloat,_agent.velocity.sqrMagnitude);//更新Locomotion 混合树的动画
    }
    
    /// <summary>
    /// MouseManger OnMouseClicked事件绑定的委托
    /// </summary>
    public void MoveToTarget(Vector3 target)
    {
        _agent.isStopped = false;
        _agent.destination = target;
    }
    
    
}
