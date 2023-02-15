using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : Singleton<MouseManager>
{
    public event Action<Vector3> OnMouseClicked;
    public Texture2D point, doorway, attack, target, arrow;
    private RaycastHit _hitInfo;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        SetCursorTexture();
        MouseController();
    }


    /// <summary>
    /// 设置鼠标贴图
    /// </summary>
    private void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hitInfo))
        {
            switch (_hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target,new Vector2(16,16),CursorMode.Auto);
                    break;
            } 
        }
        
    }

    private void MouseController()
    {
        if (Input.GetMouseButtonDown(0)&& _hitInfo.collider!=null)
        {
            if (_hitInfo.collider.CompareTag("Ground"))
            {
                OnMouseClicked?.Invoke(_hitInfo.point);//_hitInfo.point:世界空间中射线命中碰撞体的撞击点。
            }
        }
    }
    
    
}
