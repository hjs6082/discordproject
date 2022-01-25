using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Vector3 targetPosition;
    private Vector3 correctPosition;
    public int number;
    public bool inRightPlace;
    //private SpriteRenderer sr;
    

    void Start()
    {
        targetPosition = transform.position;
        correctPosition = transform.position;
        //sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        transform.position = Vector3.Lerp(a: transform.position, b: targetPosition, 0.05f);
        if (targetPosition == correctPosition)
        {
            inRightPlace = true;
        }
        else
        {
            inRightPlace = false;
        }
        /*      //맞으면 색바꾸는 기능인데 딱히 필요할지는 모르겠음.
 *      if(targetPosition == correctPosition)
        {
            sr.color = Color.green;
        }
        else
        {
            sr.color = Color.white;
        }*/
    }
}
