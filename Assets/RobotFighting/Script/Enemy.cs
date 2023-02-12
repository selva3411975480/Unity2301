using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

/// <summary>
/// 敌人
/// </summary>
namespace RobotFighting
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : LivingEntity
    {
        public enum State
        {
            Idle,
            Chasing,
            Attacking
        }; //当前状态等待，追踪，攻击

        private State currentState;

        private NavMeshAgent pathfinder;
        private Transform target;
        private LivingEntity targetEntity; //目标实体
        private Material skinMaterial;

        private Color originalColour;
        public GameObject[] itemsPrefab;

        private float attackDistanceThreshold = .5f; //阈值
        private float timeBetweenAttacks = 1;
        private float damage = 1;

        private float nextAttackTime;
        private float myCollisionRadlus;
        private float targetCollisionRadius;

        private bool hasTarget; //是否有目标


        protected override void Start()
        {
            pathfinder = GetComponent<NavMeshAgent>();
            skinMaterial = GetComponent<Renderer>().material;
            originalColour = skinMaterial.color;

            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                currentState = State.Chasing;
                hasTarget = true;

                target = GameObject.FindGameObjectWithTag("Player").transform;
                targetEntity = target.GetComponent<LivingEntity>();
                targetEntity.OnDeath += OnTargetDeath;

                myCollisionRadlus = GetComponent<CapsuleCollider>().radius;
                targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

                StartCoroutine(UpdatePath());
            }

            base.Start();
        }

        private void Update()
        {
            if (hasTarget)
            {
                //Debug.Log(currentState);
                if (Time.time > nextAttackTime)
                {
                    //目标坐标减去自身坐标
                    float sqrDstToTarget = (target.position - transform.position).sqrMagnitude; //得到距离平方
                    if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadlus + targetCollisionRadius,
                        2)) //如果距离平方小于pow里的值
                    {
                        nextAttackTime = Time.time + timeBetweenAttacks;
                        StartCoroutine(Attack());
                    }
                }
                //Debug.Log(health);//查看敌人生命值
                //pathfinder.SetDestination(target.position);
            }

            LookAtCamera();
            //Enemy spwnedEnemy = Instantiate(gameObject, Vector3.zero,Quaternion.identity) as Enemy;
        }

        void OnTargetDeath()
        {
            hasTarget = false;
            currentState = State.Idle;
        }

        IEnumerator Attack()
        {
            currentState = State.Attacking;
            pathfinder.enabled = false;

            Vector3 originalPosition = transform.position;
            Vector3 dirToTarget = (target.position - transform.position).normalized; //目标方向
            Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadlus + targetCollisionRadius); //位置
            Vector3 attackPosition = target.position;

            float attackSpeed = 3;
            float percent = 0;

            skinMaterial.color = Color.red;
            bool hasAppliedDamage = false;

            while (percent <= 1)
            {
                if (percent >= .5f && !hasAppliedDamage)
                {
                    hasAppliedDamage = true;
                    targetEntity.TakeDamage(damage);
                }

                percent += Time.deltaTime * attackSpeed;
                float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
                transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

                yield return null;
            }

            skinMaterial.color = originalColour;
            currentState = State.Chasing;
            pathfinder.enabled = true;
        }

        IEnumerator UpdatePath()
        {
            float refreshRate = .25f;

            while (hasTarget)
            {
                if (currentState == State.Chasing)
                {
                    Vector3 dirToTarget = (target.position - transform.position).normalized; //目标方向
                    Vector3 targetPosition = target.position - dirToTarget *
                        (myCollisionRadlus + targetCollisionRadius + attackDistanceThreshold / 2); //位置
                    if (!dead)
                    {
                        pathfinder.SetDestination(targetPosition);
                    }
                }

                yield return new WaitForSeconds(refreshRate);
            }
        }

        protected override void ItemDrop()
        {
            int a = Random.Range(0, 3);
            if (a == 0)
            {
                int index = Random.Range(0, itemsPrefab.Length);
                Instantiate(itemsPrefab[index], transform.position, Quaternion.identity);
            }
        }
    }
}
