using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public static Locker instance;
    
    public static string myPassword;
    public string redPassword;
    public string bluePassword;
    public string yellowPassword;
    public string greenPassword;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        myPassword = redPassword + bluePassword + yellowPassword + greenPassword;
    }

}
