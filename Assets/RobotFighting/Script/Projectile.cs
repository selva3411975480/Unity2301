using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹
/// </summary>
namespace RobotFighting
{
    public class Projectile : MonoBehaviour
    {
        private static Projectile uniqueInstanceProjectile;

        private Projectile()
        {
        }

        public static Projectile GetInstance()
        {
            if (uniqueInstanceProjectile == null)
            {
                uniqueInstanceProjectile = new Projectile();
            }

            return uniqueInstanceProjectile;
        }

        public LayerMask collisionMask;
        private float speed = 10;
        public float startingDamage = 1;

        public float Damage
        {
            set
            {
                startingDamage = value;
                startingDamage = Mathf.Clamp(startingDamage, 0, 2.6f);
            }
            get { return startingDamage; }
        }

        private float Lifetime = 2.4f;
        private float skinWidth = .1f;

        private void Start()
        {
            Destroy(gameObject, Lifetime);

            Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
            if (initialCollisions.Length > 0)
            {
                OnHitObject(initialCollisions[0]);
            }
        }

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }

        private void Update()
        {
            float moveDistance = speed * Time.deltaTime;
            CheckCollisions(moveDistance);
            transform.Translate(translation: Vector3.forward * moveDistance);
        }

        void CheckCollisions(float moveDistance)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide))
            {
                OnHitObject(hit);
            }
        }

        void OnHitObject(RaycastHit hit)
        {
            IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                damageableObject.TakeHit(Damage, hit);
                Debug.Log("Projectile+damage:" + Damage);
            }
            GameObject.Destroy(gameObject);
        }
        void OnHitObject(Collider c)
        {
            IDamageable damageableObject = c.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                damageableObject.TakeDamage(Damage);
            }
            GameObject.Destroy(gameObject);
        }
        public void AlterDamage(float value)
        {
            Damage += value;
            //Debug.Log("AlterDamage"+Damage);
        }
    }
}