using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
/// <summary>
/// 基本属性
/// </summary>
namespace TurnBased
{
    public class Unit : MonoBehaviour
    {
        #region PublicUnit 公共属性
        public string unityName;
        public int unitLevel;
        public float damage;
        public float empirical;
        #endregion
        
        #region BasicsProperty 基础属性
        public float basicsMaxHp;//最大血量
        public float basicsAttack;//攻击
        public float basicsDefense;//防御
        public float basicsSpecialAttack;//特攻
        public float basicsSpecialDefense;//特攻
        public float basicsSpeed;//速度
        #endregion    
        
        #region BattleProperty 战斗属性
        public float battleCurrentHP;//当前血量
        public float battleAttack;//攻击
        public float battleDefense;//防御
        public float battleSpecialAttack;//特攻
        public float battleSpecialDefense;//特攻
        public float battleSpeed;//速度
        public float battleCriticalStrikeLevel;//致命一击
        #endregion 
        
        #region PropertyLevel 属性等级;
        public int _propertyLevel;
        public int PropertyLevel
        {
            set
            {
                _propertyLevel = value;
                _propertyLevel = Mathf.Clamp(_propertyLevel, -6, 6);
            }
            get { return _propertyLevel; }
        }
        public float _attackLevel;//攻击
        public float _defenseLevel;//防御
        public float _specialAttackLevel;//特攻
        public float _specialDefenseLevel;//特防
        public float _speedLevel;//速度     
        public float _criticalStrikeLevel;//暴击率
        public float _AccuracyRateLevel;//命中率
        #endregion
        /// <summary>
        /// TakeDamage获取伤害附加给当前体力(battleCurrentHP)判断若当前体力(battleCurrentHP)小于0则返回true否则返回false
        /// </summary>
        /// <param name="damage">需要一个伤害值</param>
        /// <returns>血量小于0则返回false</returns>
        public bool TakeDamage(float damage)
        {
            battleCurrentHP -= damage;
            if (battleCurrentHP <= 0)
                return true;
            else
                return false;
        }
    }   
}
