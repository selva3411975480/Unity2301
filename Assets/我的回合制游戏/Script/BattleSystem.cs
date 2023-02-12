using System;
using System.Collections;
using System.Collections.Generic;
using RobotFighting;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 战斗系统 
/// </summary>
namespace TurnBased
{
    public enum BattleState { START,PLAYERTURN,ENEMYTURN,WOW,LOST}
    public class BattleSystem : MonoBehaviour
    {
        public GameObject playerPrefab;
        public GameObject enemyPrefab;

        public Transform playerBattleStation;//玩家的草坪
        public Transform enemyBattleStation;//敌人的草坪
        public BattleState state;

        private Unit playerUnit;
        private Unit enemyUnit;

        public Text dialogueText;//战斗信息对话

        public BattleHUD playerHUD;
        public BattleHUD enemyHUD;
        
        private void Start()
        {
            state = BattleState.START;
            GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
            //从敌人平台上生成一个玩家
            playerUnit = playerGO.GetComponent<Unit>();
            GameObject enemyGO = Instantiate(enemyPrefab,enemyBattleStation);
            //从玩家平台上生成一个玩家
            enemyUnit = enemyGO.GetComponent<Unit>();
            StartCoroutine(SetupBattle());
        }
        /// <summary>
        /// 设置战斗人员
        /// </summary>
        IEnumerator SetupBattle()
        {
            //enemyUnit.unityName;
            dialogueText.text += '\n'+playerUnit.unityName+"进入了战场......" + "\n"+enemyUnit.unityName+"进入了战场......";
            
            playerHUD.setHUD(playerUnit);
            enemyHUD.setHUD(enemyUnit);
            
            yield return new WaitForSeconds(2f);
            
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        /// <summary>
        /// 玩家对敌人发进攻
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayerAttack()
        {
            //攻击敌人
            bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
            enemyHUD.SetHP(enemyUnit.battleCurrentHP);
            dialogueText.text += '\n'+playerUnit.unityName+"进攻成功了!";
            if (isDead)
            {
                state = BattleState.WOW;
                EndBattle();
                //结束战斗
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
                //敌人行动
            }
            yield return new WaitForSeconds(2f);
            //检查敌人是否死亡
            //根据发生的事情改变状态
        }
        /// <summary>
        /// 敌人回合跳转
        /// </summary>
        /// <returns>延时1秒后对玩家发动进攻</returns>
        IEnumerator EnemyTurn()
        {
            dialogueText.text += '\n'+enemyUnit.unityName + "对"+playerUnit.unityName+"发起了进攻";
            yield return new WaitForSeconds(1f);
            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
            playerHUD.SetHP(playerUnit.battleCurrentHP);
            yield return new WaitForSeconds(1f);
            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
        /// <summary>
        /// 战斗结束判断你的生命值
        /// </summary>
        void EndBattle()
        {
            if (state == BattleState.WOW)
            {
                dialogueText.text += "\n你赢得了这场战斗";
            }
            else if(state == BattleState.LOST)
            {
                dialogueText.text += "\n你被打败了";
            }
        }
        void PlayerTurn()
        {
            dialogueText.text += "\n"+playerUnit.unityName+"行动:选择一个技能";
        }
        public void OnAttackButton()
        {
            if (state != BattleState.PLAYERTURN)
                return;
            StartCoroutine(PlayerAttack());
        }
    }
}