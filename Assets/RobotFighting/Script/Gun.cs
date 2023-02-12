using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotFighting
{
    public class Gun : MonoBehaviour
    {
        public Transform muzzle;
        public Projectile projectile;
        public float startingMsBetweenShootingSpeed;
        private float _msBetweenShootingSpeed;

        private float MsBetweenShootingSpeed
        {
            set
            {
                _msBetweenShootingSpeed = value;
                _msBetweenShootingSpeed = Mathf.Clamp(_msBetweenShootingSpeed, startingMsBetweenShootingSpeed * 0.4f,
                    startingMsBetweenShootingSpeed);
                //Debug.Log("msBetweenShootingSpeed"+_msBetweenShootingSpeed);
            }
            get { return _msBetweenShootingSpeed; }
        }

        public float startingMuzzleVelocity;
        private float _muzzleVelocity;

        private float MuzzleVelocity
        {
            set
            {
                _muzzleVelocity = value;
                _muzzleVelocity = Mathf.Clamp(_muzzleVelocity, 0, startingMuzzleVelocity * 4);
                //Debug.Log("_muzzleVelocity:"+_muzzleVelocity);
            }
            get { return _muzzleVelocity; }
        }

        private float nextShotTime;

        private void Start()
        {
            MuzzleVelocity = startingMuzzleVelocity;
            MsBetweenShootingSpeed = startingMsBetweenShootingSpeed;
        }

        public void Shoot()
        {
            if (Time.time > nextShotTime)
            {
                nextShotTime = Time.time + MsBetweenShootingSpeed / 1000;
                Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation);
                newProjectile.SetSpeed(MuzzleVelocity);
            }
        }

        public void AlterMsBetweenShootingSpeed(float value)
        {
            MsBetweenShootingSpeed += value;
        }

        public void AlterMuzzleVelocity(float value)
        {
            MuzzleVelocity += value;
        }
    }
}