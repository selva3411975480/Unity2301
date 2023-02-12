using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  道具旋转
/// </summary>
namespace RobotFighting
{
    public class ItemRotate : MonoBehaviour
    {
        private void Update()
        {
            //道具在Y轴旋转
            transform.Rotate(Vector3.up, Time.deltaTime * 10);
            //道具上下往返跳动
            float h = Mathf.PingPong(Time.time / 6, .24f);
            transform.position = new Vector3(transform.position.x, h + 0.5f, transform.position.z);
        }
    }
}