using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LodingSceneScript : MonoBehaviour
{
    [SerializeField]
    private Camera[] cams;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LodingPlay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DepthReset()
    {
        cams[0].depth = 0f;
        cams[1].depth = 0f;
        cams[2].depth = 0f;
    }
    IEnumerator LodingPlay()
    {
        cams[0].transform.DORotate(new Vector3(cams[0].transform.rotation.x, cams[0].transform.rotation.y + 27.867f, cams[0].transform.rotation.z), 10f);
        yield return new WaitForSeconds(10f);
        DepthReset();
        cams[1].depth = 1f;
        cams[1].transform.DORotate(new Vector3(cams[1].transform.rotation.x, cams[1].transform.rotation.y - 14.35f, cams[1].transform.rotation.z), 10f);
        yield return new WaitForSeconds(10f);
        DepthReset();
        cams[2].depth = 1f;
        cams[2].transform.DORotate(new Vector3(cams[2].transform.rotation.x, cams[2].transform.rotation.y + 17f, cams[2].transform.rotation.z), 10f);
    }
}
