using UnityEngine;
/// <summary>
/// 伤害系统
/// </summary>
namespace RobotFighting
{
    public interface IDamageable
    {
        //委托受到一个伤害参数掉血
        void TakeHit(float damage, RaycastHit hit);
        //委托传递一个伤害参数
        void TakeDamage(float damage);
    }
}