using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 道具效果
/// </summary>
namespace RobotFighting
{
    public class PropEffect : ItemRotate
    {
        private enum PropEffectEnum
        {
            Recover = 8,
            shootingSpeed = 16,
            Attack = 24
        }

        public int PropEffectID;

        public void Effect(GameObject player)
        {
            if (PropEffectID == (int) PropEffectEnum.Recover)
            {
                //Debug.Log("拾取道具1");
                player.GetComponent<Player>().Recover(3);
            }

            if (PropEffectID == (int) PropEffectEnum.shootingSpeed)
            {

                //Debug.Log("拾取道具2");
                //player.GetComponent<GunController>().startingGun._muzzleVelocity += 1;
                //player.GetComponent<GunController>().startingGun._msBetweenShootingSpeed -= 1;
                GameObject.Find("Player/Weapon Hold/Gun(Clone)").GetComponent<Gun>().AlterMuzzleVelocity(1);
                GameObject.Find("Player/Weapon Hold/Gun(Clone)").GetComponent<Gun>().AlterMsBetweenShootingSpeed(-10);
            }

            if (PropEffectID == (int) PropEffectEnum.Attack)
            {
                Debug.Log("拾取道具3");
                GameObject.Find("Player/Weapon Hold/Gun(Clone)").GetComponent<Gun>().projectile.AlterDamage(0.2f);
                //GetComponent<Projectile>().AddDamage(1);
            }
        }
    }
}