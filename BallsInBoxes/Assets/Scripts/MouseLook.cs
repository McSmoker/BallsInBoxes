using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MouseLook : MonoBehaviour
{
        Vector2 rotation = new Vector2(0, 0);
        public float speed = 3;

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                rotation.y += Input.GetAxis("Mouse X");
                rotation.x += -Input.GetAxis("Mouse Y");
                transform.eulerAngles = (Vector2)rotation * speed;
            }
        }
}