using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class OneDraw_PenObject : MonoBehaviour
    {
        private LineRenderer line = null;

        private void Awake()
        {
            line = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            line.startWidth = 0.2f;
            line.endWidth   = 0.2f;
        }

        public void InputPen()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                this.gameObject.transform.position = mousePos;
                line.SetPosition(1, mousePos);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OneDraw_Manager.Instance.ClearVertex();
                OneDraw_Manager.Instance.RemovePen();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("dsjklsdfjklsdf");

            OneDraw_Vertex OV = other.transform.GetComponent<OneDraw_Vertex>();

            if (OneDraw_Manager.Instance.curVertex != null)
            {
                if (OneDraw_Manager.Instance.curVertex != OV && OneDraw_Manager.Instance.oldVertex != OV)
                {
                    for (int i = 0; i < OneDraw_Manager.Instance.curVertex.aroundVertex_List.Count; i++)
                    {
                        if (OneDraw_Manager.Instance.curVertex.aroundVertex_List[i] == OV)
                        {
                            OneDraw_Manager.Instance.curVertex.aroundVertex_List.Remove(OV);
                            OV.aroundVertex_List.Remove(OneDraw_Manager.Instance.curVertex);

                            OneDraw_Manager.Instance.oldVertex = OneDraw_Manager.Instance.curVertex;
                            OneDraw_Manager.Instance.curVertex = OV;

                            OneDraw_Manager.Instance.AddVertex(other.gameObject);

                            line.SetPosition(0, OV.transform.position);

                            OneDraw_Manager.Instance.ClearCheck();
                        }
                    }
                }
            }
            else
            {
                OneDraw_Manager.Instance.curVertex = OV;
                OneDraw_Manager.Instance.AddVertex(other.gameObject);
                
                line.SetPosition(0, OV.transform.position);
            }

        }
    }
}