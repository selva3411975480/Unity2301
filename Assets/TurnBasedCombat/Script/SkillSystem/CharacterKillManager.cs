using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  TurnBasedCombat.Skill
{
    /// <summary>
    /// 技能管理器
    /// </summary>
    public class CharacterKillManager : MonoBehaviour
    {
        /// <summary>
        /// 技能列表
        /// </summary>
        public SkillData[] skills;

        private void Start()
        {
            for (int i = 0; i < skills.Length; i++)
            {
                IniSkill(skills[i]);       
            }
        }
        /// <summary>
        /// 初始化技能预制件
        /// </summary>
        /// <param name="data"></param>
        private void IniSkill(SkillData data)
        {
            //根据名字找到技能特效预制件
            //data.PrefabName --> data.skillPerfab
            Resources.Load<GameObject>("");
        }
        /// <summary>
        /// 生成技能
        /// </summary>
        public void GenerateSkill(SkillData data)
        {
            //创建技能特效
            GameObject skillGo = Instantiate(data.skillPrefab,transform.position,Quaternion.identity);
            //销毁技能特效件
            Destroy(skillGo,2);
        }
        // private void CurrentPPDown()
        // {
        //     //data.CurentPP --> data.CurentPP
        //     
        // }
        
    }
}

