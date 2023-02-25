using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 战斗处理
/// </summary>
namespace TurnBasedCombat
{
    public class BattleHUD : MonoBehaviour
    {
        public Text nameText;
        public Text levelText;
        public Slider hpSlider;

        /// <summary>
        /// 战斗人员信息处理
        /// </summary>
        /// <param name="unit">需要一个unit组件的等级，名字，体力最大值，当前体力</param>
        public void setHUD (AbilityUnit abilityUnit)
        {
            nameText.text = abilityUnit.unitName;
            levelText.text = "lv:"+abilityUnit.unitLevel;
            hpSlider.maxValue = abilityUnit.battleMAXHealthPoint;
            hpSlider.value = abilityUnit.BattleCurrentHealthPoint;
        }
        /// <summary>
        /// 用来修改spSlider.value
        /// </summary>
        /// <param name="hp">需要一个当前血量(BattleCurrentHealthPoint)</param>
        public void SetHP(float hp)
        {
            hpSlider.value = hp;
        }
    }
}