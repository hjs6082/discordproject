using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoBack : MonoBehaviour
{
    public GameObject memo;

    public void memoClose()
    {
        memo.SetActive(false);
    }
}
