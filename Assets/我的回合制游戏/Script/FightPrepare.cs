using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 战斗准备
/// </summary>
namespace TurnBased
{
    /// <summary>
    /// 战斗开始时让双防对视
    /// </summary>
    public class FightPrepare : MonoBehaviour
    {
        public Transform target;
        private void Start()
        {
            transform.LookAt(target);
        }
    }
}


