using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*单例*/
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// 属性 是否初始化
    /// </summary>
    public static bool IsInitialized
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T) this;
        }
    }

    /// <summary>
    /// 释放空间
    /// </summary>
    protected void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}