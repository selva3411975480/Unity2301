using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
/// <summary>
/// 基本属性
/// </summary>
namespace TurnBasedCombat
{
    public class Unit : MonoBehaviour
    {
        [Header("publicUnit")]

        #region PublicUnit 个体公共属性
        public int unitID;
        public string unitName; //名字
        public byte unitLevel; //等级
        public float empirical; //经验条
        #endregion
        [Header("unitProperty")]
        #region unitProperty 个体点
        public float unitHealthPoint;
        public float unitAttack;
        public float unitDefense;
        public float unitSpecialAttack;
        public float unitSpecialDefense;
        public float unitSpeed;

        #endregion
        [Header("talent")]
        #region talent 天赋点
        public int _talentHealthPoint;
        public int TalentHealthPoint
        {
            set
            {
                _talentHealthPoint = value;
                _talentHealthPoint = Mathf.Clamp(_talentHealthPoint,0,62);
            }
            get { return _talentHealthPoint;}
        }
        public int _talentAttack;
        public int TalentAttack
        {
            set
            {
                _talentAttack = value;
                _talentAttack = Mathf.Clamp(_talentAttack,0,62);
            }
            get{return _talentAttack;}
        }
        public int _talentDefense;
        public int TalentDefense
        {
            set
            {
                _talentDefense= value;
                _talentDefense= Mathf.Clamp(_talentDefense,0,62);
            }
            get{return _talentDefense;}
        }
        public int _talentSpecialAttack;
        public int TalentSpecialAttack
        {
            set
            {
                _talentSpecialAttack= value;
                _talentSpecialAttack= Mathf.Clamp(_talentSpecialAttack,0,62);
            }
            get{return _talentSpecialAttack;}
        }
        public int _talentSpecialDefense;
        public int TalentSpecialDefense
        {
            set
            {
                _talentSpecialDefense= value;
                _talentSpecialDefense= Mathf.Clamp(_talentSpecialDefense,0,62);
            }
            get{return _talentSpecialDefense;}
        }
        public int _talentSpeed;
        public int TalentSpeed
        {
            set
            {
                _talentSpeed = value;
                _talentSpeed = Mathf.Clamp(_talentSpeed,0,62);
            }
            get { return _talentSpeed; }
        }

        #endregion
        [Header("study")]
        #region Study 努力点
        /*体力*/
        public float _studyHealthPoint;
        public float StudyHealthPoint
        {
            set
            {
                if (StudyHealthPoint + StudyAttack + StudyDefense + StudySpecialAttack + StudySpecialDefense +
                    StudySpeed < 510)
                {
                    _studyHealthPoint = value;
                    _studyHealthPoint = Mathf.Clamp(_studyHealthPoint, 0, 255);
                }
                else _studyHealthPoint = 0;
            }
            get { return _studyHealthPoint; }
        }
        /*攻击*/
        public float _studyAttack;
        public float StudyAttack
        {
            set
            {
                if (_studyHealthPoint + _studyAttack + _studyDefense + _studySpecialAttack + _studySpecialDefense +
                    _studySpeed < 510)
                {
                    _studyAttack = value;
                    _studyAttack = Mathf.Clamp(_studyAttack, 0, 255);
                }
                else _studyHealthPoint = 0;

                return;
            }
            get { return _studyAttack; }
        }
        /*防御*/
        public float _studyDefense;
        public float StudyDefense
        {
            set
            {
                if (_studyHealthPoint + _studyAttack + _studyDefense + _studySpecialAttack + _studySpecialDefense +
                    _studySpeed < 510)
                {
                    _studyDefense = value;
                    _studyDefense = Mathf.Clamp(_studyDefense, 0, 255);
                }
                else
                    _studyDefense = 0;
            }
            get { return _studyDefense; }
        }
        /*特攻*/
        public float _studySpecialAttack;
        public float StudySpecialAttack
        {
            set
            {
                if (_studyHealthPoint + _studyAttack + _studyDefense + _studySpecialAttack + _studySpecialDefense +
                    _studySpeed < 510)
                {
                    _studySpecialAttack = value;
                    _studySpecialAttack = Mathf.Clamp(_studySpecialAttack, 0, 255);
                }
                else
                    _studySpecialAttack = 0;
            }
            get { return _studySpecialAttack; }
        }
        /*特防*/
        public float _studySpecialDefense;
        public float StudySpecialDefense
        {
            set
            {
                if (_studyHealthPoint + _studyAttack + _studyDefense + _studySpecialAttack + _studySpecialDefense +
                    _studySpeed < 510)
                {
                    _studySpecialDefense = value;
                    _studySpecialDefense = Mathf.Clamp(_studySpecialDefense, 0, 255);
                }
                else _studySpecialDefense = 0;
                return;
            }
            get { return _studySpecialDefense; }
        }
        /*速度*/
        public float _studySpeed;
        public float StudySpeed
        {
            set
            {
                if (_studyHealthPoint + _studyAttack + _studyDefense + _studySpecialAttack + _studySpecialDefense +
                    _studySpeed < 510)
                {
                    StudySpeed = value;
                    StudySpeed = Mathf.Clamp(StudySpeed, 0, 255);
                }
                else StudySpeed = 0;
            }
            get { return _studySpeed; }
        }

        #endregion
        [Header("ConstantProperty")]
        #region ConstantProperty 常量属性

        protected float ConstantHealthPoint; //最大血量
        protected float ConstantAttack; //攻击
        protected float ConstantDefense; //防御
        protected float ConstantSpecialAttack; //特攻
        protected float ConstantSpecialDefense; //特攻
        protected float ConstantSpeed; //速度

        #endregion

        protected virtual void Awake()
        {
            InitializationConstantData();
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 常量看板数据更新
        /// </summary>
        protected void InitializationConstantData()
        {
            //初始化常量能力值
            ConstantHealthPoint =
                ((unitHealthPoint * 4 + TalentHealthPoint * 2 + (StudyHealthPoint / 2) + 200) * 1) * unitLevel / 100 +
                20;
            ConstantAttack =
                ((unitAttack * 4 + TalentAttack * 2 + (StudyAttack / 2)) * 1) * unitLevel / 100 + 10;
            ConstantDefense =
                ((unitDefense * 4 + TalentDefense * 2 + (StudyHealthPoint / 2)) * 1) * unitLevel / 100 + 10;
            ConstantSpecialAttack =
                ((unitSpecialAttack * 4 + TalentSpecialAttack * 2 + (StudySpecialAttack / 2)) * 1) * unitLevel / 100 +
                10;
            ConstantSpecialDefense =
                ((unitSpecialDefense * 4 + TalentSpecialDefense * 2 + (StudySpecialDefense / 2)) * 1) * unitLevel /
                100 +
                10;
            ConstantSpeed =
                ((unitSpeed * 4 + TalentSpeed * 2 + (StudySpeed / 2)) * 1) * unitLevel / 100 + 10;
        }
    }
}
