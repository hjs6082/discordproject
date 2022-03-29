using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneTalk : MonoBehaviour
{
    public GameObject[] talk1;
    public GameObject[] talk2;

    private bool talk1End;
    private bool talk2End;

    private bool isDestory;

    private bool phoneOn;
    
    // Start is called before the first frame update
    void Start()
    {
        Phone.instance.isPlay = true;
        StartCoroutine(TalkPlay(talk1, talk1End, 0.5f, 2f));
    }
    // Update is called once per frame
    void Update()
    {
        if(talk1[8].gameObject.activeSelf)
        {
            StopCoroutine("TalkPlay");
            StartCoroutine(TalkDestory(talk1));
            talk1End = true;
            TalkDestory(talk1);
            isDestory = true;
            StartCoroutine(TalkPlay(talk2, talk2End, 0.5f, 2f));
        }
        if(talk1End == true)
        {
            StopCoroutine(TalkDestory(talk1));
        }
        if(talk2[8].gameObject.activeSelf)
        {
            if (phoneOn == false)
            {
                Phone.instance.isPlay = false;
                Phone.instance.PhoneDown();
                phoneOn = true;
            }
        }   
    }

    IEnumerator TalkDestory(GameObject[] talk)
    {
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < talk.Length; i++)
        {
            talk[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator TalkPlay(GameObject[] talkList, bool talkEnd, float firstWait, float secondWait)
    {
        yield return new WaitForSeconds(firstWait);
        for(int i = 0; i < talkList.Length; i++)
        {
            yield return new WaitForSeconds(secondWait);
            talkList[i].gameObject.SetActive(true);
        }
    }

}
