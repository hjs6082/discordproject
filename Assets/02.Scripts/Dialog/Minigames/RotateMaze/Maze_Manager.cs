using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Dialogue
{
    public class Maze_Manager : Minigame
    {
        #region 싱글톤
        private static Maze_Manager instance;
        public static Maze_Manager Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public static Action mazeEnable;

        private Maze_Spawner maze_Spawner = null;
        private Maze_Ctrl    maze_Ctrl    = null;

        public GameObject    maze_Prefab  = null;
        public GameObject    maze_Object  = null;
        public GameObject    maze_MoveObj = null;
        public Transform     maze_Parent  = null;

        public float curDegree = 0.0f;
        public bool  canRotate = true;
        public bool  canMove   = false;

        protected override void Awake()
        {
            #region 싱글톤
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            #endregion

            InitGame();
        }

        protected override void Start()
        {

        }

        protected override void Update()
        {
            if(Dialogue_Manager.Instance.cur_eMinigame == eMinigame.MAZE)
            {
                maze_Ctrl.InputMaze(maze_Object.transform);
            }
        }

        private void InitClass()
        {
            // TODO : 여기에 클래스 변수 초기화
            maze_Spawner = GetComponent<Maze_Spawner>();
            maze_Ctrl    = GetComponent<Maze_Ctrl>();
        }

        private void InitValue()
        {
            // TODO : 여기에 변수 초기화
            curDegree = 90.0f;
            canRotate = true;
            canMove   = false;
        }

        private void InitMaze()
        {
            maze_Spawner.SpawnMaze(maze_Prefab);

            Maze_Object.check?.Invoke();
        }

        public override void InitGame()
        {
            InitClass();
            InitValue();
            InitMaze();
        }

        public override void SetGamePos()
        {
            maze_Parent.position = Dialogue_Manager.DEFAULT_MINIGAME_POSITION;

            canMove = true;
        }

        public override void LoadGame(Transform _minigameTrm, Action _action = null)
        {
            _minigameTrm = maze_Object.transform;

            base.LoadGame(_minigameTrm);
        }

        public override void Download()
        {
            base.Download();
        }
    }
}