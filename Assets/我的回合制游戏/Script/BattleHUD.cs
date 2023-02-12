using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 战斗处理
/// </summary>
namespace TurnBased
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
        public void setHUD(Unit unit)
        {
            nameText.text = unit.unityName;
            levelText.text = "lv:"+unit.unitLevel;
            hpSlider.maxValue = unit.basicsMaxHp;
            hpSlider.value = unit.battleCurrentHP;
        }
        /// <summary>
        /// 用来修改spSlider.value
        /// </summary>
        /// <param name="hp">需要一个hp</param>
        public void SetHP(float hp)
        {
            hpSlider.value = hp;
        }
    }
}