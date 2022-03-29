using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Player.PlayerWalk pr;

    public static Phone instance;
    private Vector3 startPosition;
    private Vector3 goalPosition;


    private bool isPhoneStart;
    private bool isPhone;
    public bool isPlay;

    [SerializeField]
    private Sprite offPhone;

    [SerializeField]
    private Sprite onPhone;

    [SerializeField]
    private GameObject inGamePhone;

    [SerializeField]
    private AudioSource armSound;
    void Start()
    {
        armSound.Play();
        startPosition = this.gameObject.transform.position;
        goalPosition = new Vector3(startPosition.x, startPosition.y + 550f, startPosition.z);
        if (!isPhoneStart)
        {
            PhoneUp();
            StartCoroutine(StartPhone());
        }
        instance = this;
        pr = player.GetComponent<Player.PlayerWalk>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPhoneStart == true)
        {
            StopCoroutine(StartPhone());
            PhoneDown();
            isPhoneStart = false;
        }
        if (isPlay == false)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (isPhone == false)
                {

                    isPhone = true;
                    gameObject.transform.DOMove(new Vector3(startPosition.x, startPosition.y + 550f, startPosition.z), 1f, false);
                    player.GetComponent<Player.PlayerWalk>().isScan = true;
                    StartCoroutine(PhoneOn());
                }
                else
                {
                    StopCoroutine(PhoneOn());
                    isPhone = false;
                    player.GetComponent<Player.PlayerWalk>().isScan = false;
                    PhoneDown();
                }
            }
        }
    }

    public void PhoneUp()
    {
        gameObject.transform.DOMove(goalPosition, 1f, false);
    }
    
    public void PhoneDown()
    {
        gameObject.transform.DOMove(new Vector3(startPosition.x, startPosition.y, startPosition.z), 1f, false);
        gameObject.GetComponent<Image>().sprite = offPhone;
        gameObject.GetComponent<Image>().enabled = true;
        inGamePhone.gameObject.SetActive(false);
    }

    IEnumerator PhoneOn()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<Image>().sprite = onPhone;
        gameObject.GetComponent<Image>().enabled = false;
        inGamePhone.SetActive(true);
    }

    IEnumerator StartPhone()
    {
        yield return new WaitForSeconds(1f);
        isPhoneStart = true;
    }
}
