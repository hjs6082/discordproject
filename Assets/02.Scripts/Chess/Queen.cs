using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QueenPuzzle
{
    public class Queen : MonoBehaviour
    {
        private RaycastHit hit;

        private List<Vector3> dirs;

        public bool bQueenOn = false;

        private void Awake()
        {
            dirs = new List<Vector3>()
            {
                Vector3.forward,
                Vector3.back,
                Vector3.right,
                Vector3.left,
                Vector3.Normalize(Vector3.forward + Vector3.right),
                Vector3.Normalize(Vector3.forward + Vector3.left),
                Vector3.Normalize(Vector3.back + Vector3.right),
                Vector3.Normalize(Vector3.back + Vector3.left)
            };
        }

        private void OnCollisionStay(Collision other)
        {
            //Debug.Log("col");
            bQueenOn = true;
        }

        private void OnCollisionExit(Collision other)
        {
            //Debug.Log("col");
            bQueenOn = false;
        }

        public bool CheckQueen()
        {
            for (int i = 0; i < dirs.Count; i++)
            {
                Vector3 pos = this.transform.position + new Vector3(0, 1f, 0);
                Vector3 dir = dirs[i];
                Physics.Raycast(pos, dir, out hit, 15f);
                Debug.DrawRay(pos, dir, Color.blue, 0.5f);

                if (hit.transform != null)
                {
                    if (hit.transform.tag.Equals("QUEEN"))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}