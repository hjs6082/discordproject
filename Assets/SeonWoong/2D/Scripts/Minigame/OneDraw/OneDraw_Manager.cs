using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Dialogue
{
    public class OneDraw_Manager : Minigame
    {
        #region 싱글톤
        public static OneDraw_Manager Instance
        {
            get
            {
                return instance;
            }
        }
        private static OneDraw_Manager instance = null;
        #endregion

        public GameObject        draw_Object = null;
        public GameObject        pen_Prefab  = null;
        public GameObject        pen_Object  = null;
        public LineRenderer      line        = null;
        public OneDraw_Vertex    oldVertex   = null;
        public OneDraw_Vertex    curVertex   = null;
        public OneDraw_PenObject oneDrawPen  = null;

        public List<GameObject> vertex_List = new List<GameObject>();
        public OneDraw_Vertex[] OV_Arr = null;

        public int lineCount = 0;
        public int totalLineCount = 0;

        public bool bClear = false;

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

            line = GetComponent<LineRenderer>();
        }

        protected override void Start()
        {
            InitLine();
        }

        protected override void Update()
        {
            if (Dialogue_Manager.Instance.cur_eMinigame == eMinigame.ONEDRAW)
            {
                oneDrawPen?.InputPen();
            }
        }

        public override void InitGame()
        {
            InitLine();
        }

        public override void SetGamePos()
        {
            draw_Object.SetActive(false);
        }

        public override void LoadGame(Transform _minigameTrm, Action _action = null)
        {
            //base.LoadGame(_minigameTrm);
            draw_Object.SetActive(true);
        }

        public override void Download()
        {
            base.Download();
        }

        private void InitLine()
        {
            line.positionCount = 0;

            line.startWidth = 0.2f;
            line.endWidth = 0.2f;

            line.startColor = UnityEngine.Random.ColorHSV();
            line.endColor   = UnityEngine.Random.ColorHSV();
        }

        public void SetPen()
        {
            if (pen_Object == null)
            {
                pen_Object = Instantiate<GameObject>(pen_Prefab);
                oneDrawPen = pen_Object.GetComponent<OneDraw_PenObject>();
            }
        }

        public void RemovePen()
        {
            if (pen_Object != null)
            {
                Destroy(pen_Object);

                pen_Object = null;
                oneDrawPen = null;
            }
        }

        public void AddVertex(GameObject _vertex)
        {
            vertex_List.Add(_vertex);
            line.positionCount = vertex_List.Count;
            line.SetPosition(vertex_List.Count - 1, _vertex.transform.position);
        }

        public void ClearVertex()
        {
            for (int i = 0; i < OV_Arr.Length; i++)
            {
                OV_Arr[i].ResetAroundVertex();
            }

            vertex_List.RemoveAll(x => x);

            curVertex = null;
            oldVertex = null;

            lineCount = 0;
        }

        public void ClearCheck()
        {
            lineCount++;

            if (lineCount >= totalLineCount)
            {
                Debug.Log("fdsafjkds");

                RemovePen();

                Dialogue_Manager.Instance.MinigameClear();
                
                bClear = true;
            }
        }
    }
}

// 선택 -> PC화면 -> 미니게임 진행 -> 클리어 후 대화 진행 => * 3 =