using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 战斗系统
/// </summary>
namespace TurnBasedCombat
{
    public enum BattleState
    {
        START,
        END,
        WOW,
        LOST
    }
    public enum RolePlayState
    {
        READY,
        GO
    }
    public delegate void Effect(int skillDamage,AbilityUnit attacker,AbilityUnit target);
    public class BattleSystem : MonoBehaviour
    {
        /// <summary>
        /// 游戏状态
        /// </summary>
        public BattleState state;
        public RolePlayState playerState;
        public RolePlayState enemyState;
        //双方预设体
        //public GameObject[] rolePrefab;

        //双方生成位置
        public Transform playerBattleStation;
        public Transform enemyBattleStation;

        //双方战斗调用单位
        public AbilityUnit[] roleUnit = new AbilityUnit[6];
        
        //双方技能暂存区
        //public event Effect playerSkill;
        //public event Effect enemySkill;
        
        //判断双方是否出手
        private bool playerAction;
        private bool enemyAction;

        public Text dialogueText;
        //双方信息UI反馈
        public BattleHUD playerHUD;
        public BattleHUD enemyHUD;
        
        /// <summary>
        /// 初始化
        /// </summary>
        private void Start()
        {
            playerAction = false;
            enemyAction = false;
            state = BattleState.START;
            playerState = RolePlayState.READY;
            enemyState = RolePlayState.READY;
            //从玩家平台生成一个玩家
            for (int i = 0; i < roleUnit.Length; i++)
            {
                
                // switch (roleUnit[i].tag)
                // {
                //      case "Player":
                //          if(roleUnit[i] != null)
                //     break;
                //         Instantiate(roleUnit[i], enemyBattleStation);
                //         roleUnit[i] = roleUnit[i].GetComponent<AbilityUnit>();
                //         break;
                //     case "Enemy": 
                //         if(roleUnit[i] != null)
                //     break;
                //         Instantiate(roleUnit[i], enemyBattleStation);
                //         roleUnit[i] = roleUnit[i].GetComponent<AbilityUnit>();
                //         break;
                // }
            }
            //AbilityUnit player = Instantiate(roleUnit[0], enemyBattleStation);
            //roleUnit[0] = player.GetComponent<AbilityUnit>();
            //从敌人平台生成一个敌人
            playerHUD.setHUD(roleUnit[0]);
            enemyHUD.setHUD(roleUnit[0]);
            EnemySkillReady();
        }
        /// <summary>
        /// 玩家准备选择技能
        /// </summary>
        public void PlayerSkillReady()
        {
            if (playerState != RolePlayState.READY)
            return;
            playerState = RolePlayState.GO;
            TurnBasedBattleStart();
        }
        /// <summary>
        /// 敌人准备选择技能
        /// </summary>
        public void EnemySkillReady()
        {
            if (enemyState != RolePlayState.READY)
            return;
            enemyState = RolePlayState.GO;
            TurnBasedBattleStart();
        }
        /// <summary>
        /// 战斗开始
        /// </summary>
        public void TurnBasedBattleStart()
        {
            if (playerState == RolePlayState.GO && enemyState == RolePlayState.GO)
            {
                state = BattleState.START;
                // for (int i = 0; i < enemyUnit.Length; i++)
                // {
                //     for (int j = 0; j < enemyUnit.Length; j++)
                //     {
                //         //enemyUnit.
                //     }
                // }
                if (roleUnit[0].priorityLevel > roleUnit[0].priorityLevel)
                {
                    StartCoroutine(EnemyAction());
                }
                else if (roleUnit[0].priorityLevel < roleUnit[0].priorityLevel)
                {
                    StartCoroutine(PlayerAction());
                }
                else if (roleUnit[0].priorityLevel == roleUnit[0].priorityLevel)
                {
                    if (roleUnit[0].battleSpeed >= roleUnit[0].battleSpeed)
                    {
                        StartCoroutine(EnemyAction());
                    }
                    else
                    {
                        StartCoroutine(PlayerAction());
                    }
                }
            }
        }
        /// <summary>
        /// 战斗结束
        /// </summary>
        public void TurnBasedBattleEnd()
        {            
            playerAction = false;
            enemyAction = false;
            state = BattleState.END;
            playerState = RolePlayState.READY;
            enemyState = RolePlayState.READY;
            Debug.Log("EnemyState(敌人状态)"+enemyState);
            EnemySkillReady();
        }
        /// <summary>
        /// 玩家行动
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayerAction()
        {
            playerAction = true;
            yield return new WaitForSeconds(2f);
            if (enemyAction != true)
                StartCoroutine(EnemyAction());
            else
            {
                state = BattleState.START;
                TurnBasedBattleEnd();
            }
        }
        /// <summary>
        /// 敌人行动
        /// </summary>
        /// <returns></returns>
        IEnumerator EnemyAction()
        {
            enemyAction = true;
            yield return new WaitForSeconds(2f);
            if (enemyAction != true) 
                StartCoroutine(PlayerAction());
            else
            {
                state = BattleState.START;
                TurnBasedBattleEnd();
            }
        }
        public void ActionTurn()
        {

        }
    }
}