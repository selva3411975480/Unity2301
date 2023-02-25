//---框架已改废仅作未来战棋游戏的逻辑参考---
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using RobotFighting;
// using UnityEngine;
// using UnityEngine.UI;
// /// <summary>
// /// 战斗系统 
// /// </summary>
// namespace Turn懒得改
// {
//     //public enum BattleState2 { START,PLAYERREADY,PLAYERGO,ENEMYREADY,ENEMYGO,WOW,LOST}
//     public class Battle懒得改: MonoBehaviour 
//     {
//         public GameObject playerPrefab;
//         public GameObject enemyPrefab;
//
//         public Transform playerBattleStation;//玩家的草坪
//         public Transform enemyBattleStation;//敌人的草坪
//         //public BattleState2 state;
//
//         private AbilityUnit playerUnit;
//         private AbilityUnit enemyUnit;
//
//         public SkillList playerSkill;
//         public SkillList enemySkill;
//
//         private bool playerAction;
//         private bool enemyAction;
//         
//         public Text dialogueText;//战斗信息对话
//
//         public BattleHUD playerHUD;
//         public BattleHUD enemyHUD;
//         
//         private void Start() 
//         {
//             playerAction = false;
//             enemyAction = false;
//             state = BattleState.START;
//             GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
//             //从敌人平台上生成一个玩家
//             playerUnit = playerGO.GetComponent<AbilityUnit>();
//             GameObject enemyGO = Instantiate(enemyPrefab,enemyBattleStation);
//             //从玩家平台上生成一个玩家
//             enemyUnit = enemyGO.GetComponent<AbilityUnit>();
//             StartCoroutine(SetupBattle());
//         }
//         /// <summary>
//         /// 设置战斗人员
//         /// </summary>
//         IEnumerator SetupBattle()
//         {
//             //enemyUnit.unityName;
//             dialogueText.text += '\n'+playerUnit.unityName+"进入了战场......" + "\n"+enemyUnit.unityName+"进入了战场......";
//             
//             playerHUD.setHUD(playerUnit);
//             enemyHUD.setHUD(enemyUnit);
//             
//             yield return new WaitForSeconds(2f);
//             
//             state = BattleState.PLAYERREADY;
//             
//             state = BattleState.ENEMYREADY;
//         }
//
//         /// <summary>
//         /// 玩家行动
//         /// </summary>
//         /// <param name="attackType">攻击类型</param>
//         /// <param name="skilllDamage">攻击力</param>
//         /// 暂时无特效（攻击特效未实装）
//         /// <returns>延迟2秒</returns>
//         IEnumerator Action()
//         {
//             //攻击敌人
//             playerAction = true;
//             bool isDead = false;
//             switch (AbilityUnit.AttackType.AttackSkill)
//             {
//                 case AbilityUnit.AttackType.AttackSkill:
//                     isDead = enemyUnit.TakeDamage(AbilityUnit.AttackType.AttackSkill, 300, playerUnit.unitLevel,
//                         playerUnit.battleAttack);
//                     break;
//                 case AbilityUnit.AttackType.SpecialAttackSkill:
//                     isDead = enemyUnit.TakeDamage(AbilityUnit.AttackType.AttackSkill, 300, playerUnit.unitLevel,
//                         playerUnit.battleAttack);
//                     break;
//                 case AbilityUnit.AttackType.AttributeSkill:
//                     break;
//             }
//
//             /// <summary>
//             /// 玩家行动
//             /// </summary>
//             /// <param name="attackType">攻击类型</param>
//             /// <param name="skilllDamage">攻击力</param>
//             /// 暂时无特效（攻击特效未实装）
//             /// <returns>延迟2秒</returns>
//             IEnumerator PlayerAction()
//             {
//                 //攻击敌人
//                 playerAction = true;
//                 bool isDead = false;
//                 switch (AbilityUnit.AttackType.AttackSkill)
//                 {
//                     case AbilityUnit.AttackType.AttackSkill:
//                         isDead = enemyUnit.TakeDamage(AbilityUnit.AttackType.AttackSkill, 300, playerUnit.unitLevel,
//                             playerUnit.battleAttack);
//                         break;
//                     case AbilityUnit.AttackType.SpecialAttackSkill:
//                         isDead = enemyUnit.TakeDamage(AbilityUnit.AttackType.AttackSkill, 300, playerUnit.unitLevel,
//                             playerUnit.battleAttack);
//                         break;
//                     case AbilityUnit.AttackType.AttributeSkill:
//                         break;
//                 }
//
//                 enemyHUD.SetHP(enemyUnit.BattleCurrentHealthPoint);
//                 //Debug.Log("敌人当前血量"+enemyUnit.BattleCurrentHealthPoint);
//                 dialogueText.text += '\n' + playerUnit.unityName + "对" + enemyUnit.unityName + "发起了进攻";
//                 if (isDead)
//                 {
//                     state = BattleState.WOW;
//                     EndBattle();
//                     //结束战斗
//                 }
//                 else
//                 {
//                     //Debug.Log("玩家回合playerAction"+playerAction);
//                     //Debug.Log("玩家回合enemyAction"+enemyAction);
//                     state = BattleState.PLAYERGO;
//                     yield return new WaitForSeconds(2f);
//                     state = BattleState.ENEMYREADY;
//                     if (enemyAction != true)
//                     {
//                         EnemyTurn();
//                     } //敌人行动
//                     else
//                         TurnBasedBattleEnd();
//                 }
//                 //检查敌人是否死亡
//                 //根据发生的事情改变状态
//             }
//         }
//
//         /// <summary>
//             /// 双方行动
//             /// </summary>
//             /// <param name="attackType">攻击类型</param>
//             /// <param name="skilllDamage">攻击力</param>
//             /// 暂时无特效（攻击特效未实装）
//             /// <returns>延迟2秒</returns>
//             /// <summary>
//             /// 战斗结束判断你的生命值
//             /// </summary>
//             IEnumerator EnemyAction()
//             {
//                 //攻击敌人
//                 playerAction = true;
//                 bool isDead = false;
//                 switch (AbilityUnit.AttackType.AttackSkill)
//                 {
//                     case AbilityUnit.AttackType.AttackSkill:
//                         isDead = enemyUnit.TakeDamage(AbilityUnit.AttackType.AttackSkill, 300, playerUnit.unitLevel,
//                             playerUnit.battleAttack);
//                         break;
//                     case AbilityUnit.AttackType.SpecialAttackSkill:
//                         isDead = enemyUnit.TakeDamage(AbilityUnit.AttackType.AttackSkill, 300, playerUnit.unitLevel,
//                             playerUnit.battleAttack);
//                         break;
//                     case AbilityUnit.AttackType.AttributeSkill:
//                         break;
//                 }
//
//                 enemyHUD.SetHP(enemyUnit.BattleCurrentHealthPoint);
//                 //Debug.Log("敌人当前血量"+enemyUnit.BattleCurrentHealthPoint);
//                 dialogueText.text += '\n' + playerUnit.unityName + "对" + enemyUnit.unityName + "发起了进攻";
//                 if (isDead)
//                 {
//                     state = BattleState.WOW;
//                     EndBattle();
//                     //结束战斗
//                 }
//                 else
//                 {
//                     //Debug.Log("玩家回合playerAction"+playerAction);
//                     //Debug.Log("玩家回合enemyAction"+enemyAction);
//                     state = BattleState.PLAYERGO;
//                     yield return new WaitForSeconds(2f);
//                     state = BattleState.ENEMYREADY;
//                     if (enemyAction != true)
//                     {
//                         EnemyTurn();
//                     } //敌人行动
//                     else
//                         TurnBasedBattleEnd();
//                 }
//                 //检查敌人是否死亡
//                 //根据发生的事情改变状态
//             }
//         }
//
//         void EndBattle()
//         {
//             if (state == BattleState.WOW)
//             {
//                 dialogueText.text += "\n你赢得了这场战斗";
//             }
//             else if(state == )
//             {
//                 dialogueText.text += "\n你被打败了";
//             }
//         }
//         /// <summary>
//         /// 跳转到玩家出手
//         /// </summary>
//         void PlayerTurn()
//         {
//             
//         }
//         /// <summary>
//         /// 跳转到敌人出手
//         /// </summary>
//         void EnemyTurn()
//         {
//             StartCoroutine(PlayerAction());
//         }
//         public enum Submitter { Player,Enemy }
//         /// <summary>
//         /// 回合开始
//         /// </summary>
//         public void TurnBasedBattleStare()
//         {
//             //if (state == BattleState.PLAYERGO)
//                 //提交
//             //if (state == BattleState.ENEMYGO)    
//                 //提交
//             if (state == BattleState.PLAYERGO && state == BattleState.ENEMYGO)
//             {
//                 state = BattleState.START;
//                 if (enemyUnit.priorityLevel > playerUnit.priorityLevel)
//                 {
//                     StartCoroutine(PlayerAction());
//                 }
//                 else if (enemyUnit.priorityLevel < playerUnit.priorityLevel)
//                 {
//                     
//                 }
//                 else if (enemyUnit.priorityLevel == playerUnit.priorityLevel)
//                 {
//                     if (enemyUnit.battleSpeed >= playerUnit.battleSpeed)
//                     {
//                         
//                     }
//                     else
//                     {
//                         
//                     }
//                 }
//             }
//         }
//         /// <summary>
//         /// 回合结束
//         /// </summary>
//         public void TurnBasedBattleEnd()
//         {
//             playerAction = false;
//             enemyAction = false;
//             playerUnit.ImprovementOfAbility(AbilityUnit.Ability.PriorityLevel,0);
//             enemyUnit.ImprovementOfAbility(AbilityUnit.Ability.PriorityLevel,0);
//             Debug.Log("回合结束");
//             //执行回合结束的逻辑
//             TurnBasedBattleStare();
//         }
//         public void PlayerReady()
//         {
//             dialogueText.text += "\n"+playerUnit.unityName+"行动:选择一个技能";
//         }
//         /// <summary>
//         /// 一号技能槽
//         /// </summary>
//         public void OnAttackSkill_01Button()
//         {
//             if (state != BattleState.PLAYERREADY) 
//                 return;
//         }
//         /// <summary>
//         /// 二号技能槽
//         /// </summary>
//         public void OnAttackSkill_02Button()
//         {
//             if (state != BattleState.PLAYERREADY) 
//                 return;
//         }
//         /// <summary>
//         /// 三号技能槽
//         /// </summary>
//         public void OnAttackSkill_03Button()
//         {
//             if (state != BattleState.PLAYERREADY) 
//                 return;
//         }
//         /// <summary>
//         /// 四号技能槽
//         /// </summary>
//         public void OnAttackSkill_04Button()
//         {
//             if (state != BattleState.PLAYERREADY) 
//                 return;
//         }
//         // public void OnAttributeSkillButton()
//         // {
//         //     if (state != BattleState.PLAYESTART)
//         //         return;
//         //     StartCoroutine(PlayerHeal());
//         // }
//         // public void OnAttackUpUpSkillButton()
//         // {
//         //     if (state != BattleState.PLAYESTART)
//         //         return;
//         //     StartCoroutine(AttackUpUp());
//         // }