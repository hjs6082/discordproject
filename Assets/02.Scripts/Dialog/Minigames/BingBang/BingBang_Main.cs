using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BingBang
{
    public class BingBang_Main : MonoBehaviour
    {
        private Action bingBang;

        public Image guage;
        private RotateObj rotateObj;

        public Transform twinkleParent; // 오브젝트의 부모
        public GameObject twinklePrefab; // 프리팹
        private GameObject twinkleObj; // 생성돼있는 오브젝트

        private float increment = 0.05f;
        private float decrement = -0.1f;

        private bool isCol = false;
        private bool isPress = false;

        private void Awake()
        {
            rotateObj = GetComponentInParent<RotateObj>();
            twinkleObj = Instantiate(twinklePrefab, this.gameObject.transform.position, Quaternion.identity, twinkleParent);
        }

        private void Start()
        {
            bingBang += CircleTurn;

            RandomTwinkle();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPress = true;
                bingBang?.Invoke();
            }
        }

        void CircleTurn()
        {
            float fillAmount = 0.0f;

            if (guage != null)
            {
                fillAmount = guage.fillAmount;
            }

            if (isCol) { guage?.DOFillAmount(fillAmount + increment, 0.3f); Debug.Log("True"); }
            else { guage?.DOFillAmount(fillAmount + decrement, 0.3f); Debug.Log("False"); }

            rotateObj.rotateDegree = 0.0f;
            rotateObj.rotateSpeed *= -1.0f;
            Debug.Log(rotateObj.rotateSpeed);

            RandomTwinkle();
        }

        void RandomTwinkle()
        {
            float randomDegree = UnityEngine.Random.Range(60.0f, 300.0f);

            twinkleObj.transform.RotateAround(rotateObj.gameObject.transform.position, Vector3.forward, randomDegree);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            isCol = true;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            isCol = false;

            if (!isPress)
            {
                CircleTurn();
            }

            isPress = false;
        }
    }
}