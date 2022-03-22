using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow_Obj : MonoBehaviour
{
    public GameObject[] arrow_Prefabs;
    private Transform arrow_Parent;

    public List<GameObject> arrow_Obj_List = new List<GameObject>();
    public List<int> arrow_Index_List = new List<int>();

    private void Awake()
    {
        arrow_Parent = this.transform;
    }

    public void InitArrow()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                int rand_Index = Random.Range(0, 4);

                Vector3 pos = new Vector3((j * 100.0f) - 200.0f, -250.0f - (i * 100.0f), 0.0f);

                Image arrow_Prefab = arrow_Prefabs[rand_Index].GetComponent<Image>();
                Image arrow = Instantiate<Image>(arrow_Prefab, arrow_Parent);
                arrow.rectTransform.anchoredPosition = pos;

                arrow_Obj_List.Add(arrow.gameObject);
                arrow_Index_List.Add(rand_Index);
            }
        }
    }

    public bool bCheckArrow(int _index)
    {
        bool bCheck = false;

        if(_index == arrow_Index_List[0])
        {
            GameObject arrow = arrow_Obj_List[0];
            Destroy(arrow);

            arrow_Obj_List.RemoveAt(0);
            arrow_Index_List.RemoveAt(0);

            if(arrow_Index_List.Count <= 0)
            {
                bCheck = true;
            }
        }
        
        return bCheck;
    }
}
