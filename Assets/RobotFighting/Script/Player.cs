using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotFighting
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(GunController))]
    public class Player : LivingEntity
    {
        public float moveSpeed = 5f;

        private Camera viewCamera;
        private PlayerController controller;
        private GunController gunController;

        protected override void Start()
        {
            controller = GetComponent<PlayerController>();
            gunController = GetComponent<GunController>();
            viewCamera = Camera.main;
            base.Start();
        }

        private void Update()
        {
            //移动 操控杆
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            Vector3 moveVelocity = moveInput.normalized * moveSpeed;
            controller.Move(moveVelocity);

            //看向目标 操控杆
            Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDisTance;

            if (groundPlane.Raycast(ray, out rayDisTance))
            {
                Vector3 point = ray.GetPoint(rayDisTance);
                //Debug.DrawLine(ray.origin,point,Color.red);
                controller.LookAt(point);
            }

            //武器 操控杆
            if (Input.GetMouseButton(0))
            {
                gunController.Shoot();
            }

            //Debug.Log(health);
            LookAtCamera();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Prop"))
            {
                other.GetComponent<PropEffect>().Effect(gameObject);
                Destroy(other.gameObject);
            }
        }

        public void Recover(float value)
        {
            Health += value;
        }
    }
}