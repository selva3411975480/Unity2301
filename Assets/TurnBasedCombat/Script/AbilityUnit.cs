using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurnBasedCombat.Skill;

namespace TurnBasedCombat
{        
    /// <summary>
    /// 能力枚举
    /// 攻击(Attack)，防御(Defense)，特攻(SpecialAttack)，特防(SpecialDefense)，速度(Speed)，暴击率(CriticalStrike)，命中率(AccuracyRate)，闪避率(Agility)
    /// </summary>
    public enum Ability
    {
        ATTACK,
        DEFENSE,
        SPECIALATTACK,
        SPECIALDEFENSE,
        SPEED,
        CRITICALSTRIKE,
        ACCURACYRATE,
        AGILITY,
        PRIORITYLEVEL
    }
    public class AbilityUnit : Unit
    {
        [Header("BattleProperty")]

        #region BattleProperty 战斗属性

        public float battleMAXHealthPoint;
        public float _battleCurrentHealthPoint;
        public float BattleCurrentHealthPoint
        {
            set
            {
                _battleCurrentHealthPoint = value;
                _battleCurrentHealthPoint = Mathf.Clamp(_battleCurrentHealthPoint, 0, battleMAXHealthPoint);
            }
            get { return _battleCurrentHealthPoint; }
        }
        public float battleAttack; //攻击
        public float battleDefense; //防御
        public float battleSpecialAttack; //特攻
        public float battleSpecialDefense; //特攻
        public float battleSpeed; //速度
        public float battleCriticalStrike; //致命一击
        public float battleAccuracyRate; //命中率
        public float battleAgility; //敏捷
        #endregion

        [Header("AbilityLevel")]

        #region AbilityLevel 属性等级;

        /**攻击**/
        private float attackAbility;

        private int _attackLevel;

        private int AttackLevel
        {
            set
            {
                _attackLevel = value;
                _attackLevel = Mathf.Clamp(_attackLevel, -6, 6);
            }
            get { return _attackLevel; }
        }

        /**防御**/
        private float defenseAbility;

        private int _defenseLevel;

        private int DefenseLevel
        {
            set
            {
                _defenseLevel = value;
                _defenseLevel = Mathf.Clamp(_defenseLevel, -6, 6);
            }
            get { return _defenseLevel; }
        }

        /**特攻**/
        private float specialAttackAbility;

        private int _specialAttackLevel;

        private int SpecialAttackLevel
        {
            set
            {
                _specialAttackLevel = value;
                _specialAttackLevel = Mathf.Clamp(_specialAttackLevel, -6, 6);
            }
            get { return _specialAttackLevel; }
        }

        /**特防**/
        private float specialDefenseAbility;

        private int _specialDefenseLevel;

        private int SpecialDefenseLevel
        {
            set
            {
                _specialDefenseLevel = value;
                _specialDefenseLevel = Mathf.Clamp(_specialDefenseLevel, -6, 6);
            }
            get { return _specialDefenseLevel; }
        }

        /**速度**/
        private float speedAbility;

        private int _speedLevel;

        private int SpeedLevel
        {
            set
            {
                _speedLevel = value;
                _speedLevel = Mathf.Clamp(_speedLevel, -6, 6);
            }
            get { return _speedLevel; }
        }

        /**暴击率**/
        private int criticalStrikeAbility;

        private int _criticalStrikeLevel;

        private int CriticalStrikeLevel
        {
            set
            {
                _criticalStrikeLevel = value;
                _criticalStrikeLevel = Mathf.Clamp(_criticalStrikeLevel, -6, 6);
            }
            get { return _criticalStrikeLevel; }
        }

        /**命中率**/
        private int accuracyRateAbility;

        private int _accuracyRateLevel;

        private int AccuracyRateLevel
        {
            set
            {
                _accuracyRateLevel = value;
                _accuracyRateLevel = Mathf.Clamp(_accuracyRateLevel, -6, 6);
            }
            get { return _accuracyRateLevel; }
        }

        /**敏捷**/
        private int agilityAbility;

        private int _agilityLevel;

        private int AgilityLevel
        {
            set
            {
                _agilityLevel = value;
                _agilityLevel = Mathf.Clamp(_agilityLevel, -6, 6);
            }
            get { return _agilityLevel; }
        }
        
        public int priorityLevel;//优先级

        #endregion

        protected override void Awake()
        {
            base.Awake();
            InitializationBattleData();
            InitializationAbilityLevelData();
        }
        /// <summary>
        /// 战斗中需要初始化的值
        /// </summary>
        public void InitializationBattleData()
        {
            //初始化战斗属性
            //最大体力
            battleMAXHealthPoint = ConstantHealthPoint;
            //当前体力
            BattleCurrentHealthPoint = battleMAXHealthPoint;
            //攻击
            battleAttack = ConstantAttack;
            //防御
            battleDefense = ConstantDefense;
            //特攻
            battleSpecialAttack = ConstantSpecialAttack;
            //特防
            battleSpecialDefense = ConstantSpecialDefense;
            //速度
            battleSpeed = ConstantSpeed;
            //暴击
            battleCriticalStrike = 80;
            //命中
            battleAccuracyRate = 80;
            //闪避
            battleAgility = 80;
        }
        /// <summary>
        /// 属性等级初始化
        /// </summary>
        public void InitializationAbilityLevelData()
        {
            //初始化属性等级
            AttackLevel = 0;
            DefenseLevel = 0;
            SpecialAttackLevel = 0;
            SpecialDefenseLevel = 0;
            SpeedLevel = 0;
            CriticalStrikeLevel = 0;
            AccuracyRateLevel = 0;
            AgilityLevel = 0;
        }
        /// <summary>
        /// 判断死亡
        /// </summary>
        /// <returns>战斗中当前生命小于等于0则死亡返回true</returns>
        public bool JudgeDeath()
        {
            if (BattleCurrentHealthPoint <= 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// --受伤演绎-- TakeDamage获取伤害附加给当前体力(battleCurrentHP)判断若当前体力(battleCurrentHP)小于0则返回true否则返回false
        /// </summary>
        /// <param name="attackType">需要一个技能类型</param>
        /// <param name="skillDamage">需要一个技能威力</param>
        /// <param name="level">需要一个攻击者(attacker)的等级</param>
        /// <param name="attack">需要一个攻击者(attacker)的攻击力</param>
        /// <param name="accuracyRate">需要一个攻击者(attacker)的命中率</param>
        /// <returns>血量小于0则返回false</returns>
        public float TakeDamage(SkillType attackType,int  skillDamage, int level, float attack,int accuracyRate)
        {
            /*--暴击--*/
            int criticalStrikeRandom = Random.Range(0, 1001);
            /*--命中--*/
            int accuracyRateRandom = Random.Range(0, 1001);
            //Debug.Log("防御为"+battleDefense);
            if (-accuracyRate - battleAgility > 0)
                if (accuracyRateRandom > -accuracyRate - battleAgility)
                    switch (attackType)
                {
                    case SkillType.ATTACKSKILL:
                        if (criticalStrikeRandom < battleCriticalStrike)
                            return BattleCurrentHealthPoint -= (((0.4f * level + 2) * attack * skillDamage)/ battleDefense / 64)*2;
                        else 
                            return BattleCurrentHealthPoint -= ((0.4f * level + 2) * attack * skillDamage) / battleDefense / 64;
                    case SkillType.SPECIALATTACKSKILL:
                        if (criticalStrikeRandom < 10)
                            return BattleCurrentHealthPoint -= (((0.4f * level + 2) * battleSpecialAttack * skillDamage) / SpecialDefenseLevel / 64)*2;
                        else
                            return BattleCurrentHealthPoint -= ((0.4f * level + 2) * battleSpecialAttack * skillDamage) / battleSpecialDefense / 64;
                    case SkillType.ATTRIBUTESKILL:
                            return 0;
                }
            return 0;
            //Debug.Log("造成了" + ((0.4f * level + 2) * attack * skillDamage) / battleDefense / 50 + "点伤害");
        }
        /// <summary>
        /// --生命演绎--直接扣除体力或回复体力传一个参数返回一个恢复值
        /// </summary>
        /// <param name="amout">数量（amout）可以是负数</param>
        public void DeductionOfLife(float amout)
        {
            BattleCurrentHealthPoint += amout;
            //Debug.Log("体力恢复了" + amout + "点");
        }
        /// <summary>
        /// --战斗属性演绎--
        /// </summary>
        /// <param name="ability">传入属性类型枚举Unit.Ability:攻击(Attack)，防御(Defense)，特攻(SpecialAttack)，特防(SpecialDefense)，速度(Speed)，暴击率(CriticalStrike)，命中率(AccuracyRate)，闪避率(Agility)</param>
        /// <param name="upLevel">传入提升等级</param>
        public void ImprovementOfAbility(Ability ability, int upLevel)
        {
            switch (ability)
            {
                /*攻击*/
                case Ability.ATTACK:
                    AttackLevel += upLevel;
                    battleAttack = CalculusOfValues(AttackLevel,attackAbility,ConstantAttack);
                    break;
                /*防御*/
                case Ability.DEFENSE:
                    DefenseLevel += upLevel;
                    battleDefense = CalculusOfValues(DefenseLevel,defenseAbility,ConstantDefense);
                    break;
                /*特攻*/
                case Ability.SPECIALATTACK:
                    SpecialAttackLevel += upLevel;
                    battleSpecialAttack = CalculusOfValues(SpecialAttackLevel,specialAttackAbility,ConstantSpecialAttack);
                    break;
                /*特防*/
                case Ability.SPECIALDEFENSE:
                    SpecialDefenseLevel += upLevel;
                    battleSpecialDefense = CalculusOfValues(SpecialDefenseLevel,specialDefenseAbility,ConstantSpecialDefense);
                    break;
                /*速度*/
                case Ability.SPEED:
                    SpeedLevel += upLevel;
                    battleSpeed = CalculusOfValues(SpeedLevel,speedAbility,ConstantSpeed);
                    break;
                /*暴击*/
                case Ability.CRITICALSTRIKE:                 
                    CriticalStrikeLevel += upLevel;
                    battleCriticalStrike = ProbabilisticCalculus(CriticalStrikeLevel,criticalStrikeAbility);
                    break;
                /*命中*/
                case Ability.ACCURACYRATE:
                    AccuracyRateLevel += upLevel;   
                    battleAccuracyRate = ProbabilisticCalculus(AccuracyRateLevel,accuracyRateAbility);
                    break;
                /*敏捷*/
                case Ability.AGILITY:
                    AgilityLevel += upLevel;        
                    battleAgility = ProbabilisticCalculus(AgilityLevel,agilityAbility);     
                    break;
                /*优先级*/
                case Ability.PRIORITYLEVEL:
                    priorityLevel += upLevel;
                    if (priorityLevel >= 0)
                    {
                        priorityLevel = upLevel;
                    }
                    else
                    {
                        priorityLevel = upLevel;
                    }
                    break;
            }
        }
        /// <summary>
        /// 数值类属性演绎计算
        /// </summary>
        /// <param name="level">属性等级</param>
        /// <param name="ability">属性值</param>
        /// <param name="Constant">常量值</param>
        /// <returns>演算结束后的值</returns>
        public float CalculusOfValues(int level,float ability,float Constant)
        {
            if (level >= 0)
            {
                ability = Constant * level * .5f;
                return Constant + ability;
                //Debug.Log("攻击力提升了攻击力：" + battleAttack + "攻击等级" + AttackLevel);
            }
            else
            {
                ability = Constant;
                for (int i = 0; i > level; i--)
                {
                    ability -= Constant * 2 / 3;
                }
                return ability;
            }
        }
        /// <summary>
        /// 概率类属性演绎计算
        /// </summary>
        /// <param name="level">属性等级</param>
        /// <param name="ability">属性值</param>
        /// <returns>演算结束后的值</returns>
        public float ProbabilisticCalculus(int level,int ability)
        {
            if (AccuracyRateLevel >= 0)
            {
                accuracyRateAbility = AccuracyRateLevel * 60;
                return 80 + accuracyRateAbility;
            }
            else
            {
                accuracyRateAbility = AccuracyRateLevel * -60;
                return 80 + accuracyRateAbility;
            }
        }
    }
}