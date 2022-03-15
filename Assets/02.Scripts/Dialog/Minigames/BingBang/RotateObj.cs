using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BingBang
{
    public class RotateObj : MonoBehaviour
    {
        private const float DEFAULT_ROTATE_SPEED = 150.0f;

        public float rotateSpeed;
        public float rotateDegree = 0.0f;

        private void Awake()
        {
            rotateSpeed = DEFAULT_ROTATE_SPEED;
        }

        void Update()
        {
            this.gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);

            rotateDegree += Time.deltaTime * rotateSpeed;
        }
    }
}
