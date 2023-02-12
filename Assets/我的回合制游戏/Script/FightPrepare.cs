using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 战斗准备
/// </summary>
namespace TurnBased
{
    public class FightPrepare : MonoBehaviour
    {
        public Transform target;
        private void Start()
        {
            transform.LookAt(target);
        }
    }
}


