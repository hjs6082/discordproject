using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class Arrow_Obj : MonoBehaviour
    {
        public  GameObject[] arrow_Prefabs;
        private Transform    arrow_Parent;

        public List<GameObject> arrow_Obj_List   = new List<GameObject>();
        public List<int>        arrow_Index_List = new List<int>();

        public bool bComplete = false;

        private void Awake()
        {
            arrow_Parent = this.transform;
        }

        public void InitArrow()
        {
            if (arrow_Obj_List.Count > 0)
            {
                foreach (GameObject arrow in arrow_Obj_List)
                {
                    Destroy(arrow);
                }

                arrow_Obj_List.Clear();
                arrow_Index_List.Clear();
            }

            StartCoroutine(SpawnArrow());

            bComplete = true;
        }

        private void SetArrowParent()
        {
            arrow_Parent = this.transform;
        }

        public bool IsCheckArrow(int _index)
        {
            bool bCheck = false;

            if (bComplete)
            {

                if (_index == arrow_Index_List[0])
                {
                    GameObject arrow = arrow_Obj_List[0];
                    Destroy(arrow);

                    arrow_Obj_List.RemoveAt(0);
                    arrow_Index_List.RemoveAt(0);

                    if (arrow_Index_List.Count <= 0)
                    {
                        bCheck = true;
                    }
                }
            }

            return bCheck;
        }

        private IEnumerator SpawnArrow()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int rand_Index = Random.Range(0, 4);

                    Vector3 pos = new Vector3(j - 2.0f, i - 1.0f, 0.0f);

                    GameObject arrow_Prefab = arrow_Prefabs[rand_Index];
                    GameObject arrow = Instantiate<GameObject>(arrow_Prefab, arrow_Parent);
                    arrow.transform.position = pos;

                    arrow_Obj_List.Add(arrow.gameObject);
                    arrow_Index_List.Add(rand_Index);

                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }
}