using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class OneDraw_Vertex : MonoBehaviour
    {
        public List<OneDraw_Vertex> aroundVertex_List = new List<OneDraw_Vertex>();
        public List<OneDraw_Vertex> copy_List         = new List<OneDraw_Vertex>();

        private void Awake()
        {
            InitVertexList(aroundVertex_List, copy_List);
        }

        public void ResetAroundVertex()
        {
            InitVertexList(copy_List, aroundVertex_List);
        }

        private void InitVertexList(List<OneDraw_Vertex> list1, List<OneDraw_Vertex> list2)
        {
            list2.RemoveAll(x => x);
            for (int i = 0; i < list1.Count; i++)
            {
                list2.Add(list1[i]);
            }
        }

        private void OnMouseDown()
        {
            OneDraw_Manager.Instance.SetPen();
        }
    }
}