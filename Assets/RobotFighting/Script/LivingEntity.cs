using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敌方生命
/// </summary>
namespace RobotFighting
{
    public class LivingEntity : MonoBehaviour, IDamageable
    {
        public float startingHealth;
        protected float _health;
        protected bool dead;

        protected Slider _hpSlider;
        protected Camera targetCamera;

        protected float Health
        {
            set
            {
                _health = value;
                _health = Mathf.Clamp(_health, 0, startingHealth);
                _hpSlider.value = _health / startingHealth;
            }
            get { return _health; }
        }

        //public delegate void Action();
        public event Action OnDeath;

        void Awake()
        {
            _hpSlider = transform.GetComponentInChildren<Slider>();
            targetCamera = Camera.main;
        }

        protected virtual void Start()
        {
            Health = startingHealth;
        }

        public void TakeHit(float damage, RaycastHit hit)
        {
            //这里做一些伤害事件
            TakeDamage(damage);
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0 && !dead)
            {
                Die();
            }
        }

        protected void Die()
        {
            dead = true;
            if (OnDeath != null)
            {
                ItemDrop();
                OnDeath();
            }

            GameObject.Destroy(gameObject);
        }

        public void LookAtCamera() //看向摄像机但不移动y轴
        {
            //修正摄像机Y轴与血条对其
            //Vector3 heightCorrectedPoint = new Vector3(_hpSlider.transform.position.x,
                //targetCamera.transform.position.y, _hpSlider.transform.position.z); 
            //_hpSlider.transform.LookAt(heightCorrectedPoint);
            _hpSlider.transform.forward = -targetCamera.transform.forward;
        }
        protected virtual void ItemDrop()
        {
            
        }
    }
}