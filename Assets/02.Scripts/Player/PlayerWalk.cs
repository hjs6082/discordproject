using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerWalk : MonoBehaviour
    {
        private float MOVE_SPEED = 10f;
        private float JUMP_POWER = 5f;

        private Camera mainCam;
        private Transform playerTrm;
        private Rigidbody playerRig;

        private bool bJumping = false; // 점프
        private bool onLadder = false; // 사다리
        public bool isPuzzle = false;
        public bool isScan = false;

        private void Awake()
        {
            mainCam = Camera.main;
            playerRig = GetComponent<Rigidbody>();
            playerTrm = this.gameObject.transform;

        }

        private void Start()
        {
            GameManager.Instance.Fade_In(1.0f);
        }

        private void Update()
        {
            if (!GameManager.Instance.isPuzzle)
            {
                if (Input.anyKey)
                {
                    if (!onLadder)
                    {
                        if (!isScan)
                        {
                            Move();
                        }
                    }
                    else
                    {

                    }

                    if (Input.GetKeyDown(KeyCode.Space) && !bJumping)
                    {
                        Jump();
                    }
                }
                else
                {

                }
            }
        }

        public void Move()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 _moveHorizontal = mainCam.transform.right * h;
            Vector3 _moveVertical = mainCam.transform.forward * v;

            Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * MOVE_SPEED;
            _velocity = new Vector3(_velocity.x, 0f, _velocity.z);
            playerTrm.position += _velocity * Time.deltaTime;
        }

        public void Jump()
        {
            bJumping = true;
            playerRig.AddForce(Vector3.up * JUMP_POWER, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.tag.Equals("LADDER"))
            {
                onLadder = true;
            }

            bJumping = false;
        }
    }
}
