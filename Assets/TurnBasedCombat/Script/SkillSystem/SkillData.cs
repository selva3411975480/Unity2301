using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedCombat.Skill
{
    /// <summary>
    /// 技能类型枚举
    /// </summary>
    public enum SkillType
    {
        ATTACKSKILL,
        SPECIALATTACKSKILL,
        ATTRIBUTESKILL
    }
    /// <summary>
    /// 目标类型枚举
    /// </summary>
    public enum TargetType
    {
        ENEMYFIELD,
        ENEMYMONOMER,
        FRIENDLYFIELD,
        FRIENDLYMONOMER,
        ALLFIELD,
        ONSELF
    }
    /// <summary>
    /// 技能数据类
    /// </summary>
    [Serializable]
    public class SkillData
    {
        /// <summary> 技能所属 </summary>
        [HideInInspector] public GameObject owner;
        /// <summary> 技能ID </summary>
        public string skillID;
        /// <summary> 技能名 </summary>
        public string skillName;
        /// <summary> 技能描述 </summary>
        public string skillDescription;
        /// <summary> 技能最大使用次数 </summary>
        public int maxPP;
        /// <summary> 技能剩余使用次数 </summary>
        public int _currentPP;
        public int CurrentPP
        {
            set
            {
                _currentPP = value;
                _currentPP = Mathf.Clamp(_currentPP,0,maxPP);
            }
            get { return _currentPP; }
        }
        /// <summary> 技能类型 </summary>
        public SkillType skillType;
        /// <summary> 攻击目标tag </summary>
        public string[] attackTargetTags = {"Enemy"};
        /// <summary> 攻击目标对象数组 </summary>
        [HideInInspector] public Transform[] attackTargets;
        /// <summary> 技能影响类型 </summary>
        public string[] impactType = {"(牛逼到吊炸天的技能效果)","(牛逼到吊炸天的技能效果)"};
        /// <summary> 技能威力 </summary>
        public int skillDamage;
        /// <summary> 持续回合 </summary>
        public int Wiederholt;
        /// <summary> 技能目标范围 </summary>
        public TargetType targetType;
        /// <summary> 技能预制对象 </summary>
        [HideInInspector]
        public GameObject skillPrefab;
        /// <summary> 受到技能影响(成为技能或被指定为技能目标生效后)预制件名称 </summary>
        public string animationName;
    } 
}

