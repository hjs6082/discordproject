using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        private float ROTATE_SPEED = 30f;
        private Transform playerTrm;

        private float rotX;
        private float rotY;

        private void Awake()
        {
            playerTrm = this.gameObject.transform;
        }

        private void Update()
        {
            Rotate();
        }

        public void Rotate()
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            rotX += x * ROTATE_SPEED * Time.deltaTime;
            rotY += y * ROTATE_SPEED * Time.deltaTime;

            rotY = Mathf.Clamp(rotY, -30f, 30f);

            playerTrm.eulerAngles = new Vector3(-rotY, rotX, 0.0f);
        }
    }
}
