using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCam : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera playerCam;
    [SerializeField]
    private CinemachineVirtualCamera chessCam;

    private void Awake()
    {
        playerCam.gameObject.SetActive(true);
        chessCam.gameObject.SetActive(true);

        MoveCam();
    }

    public void MoveCam()
    {
        GameManager.Instance.isPuzzle = false;
        playerCam.Priority = 20;
        chessCam.Priority = 10;
    }

    public void ChessCam()
    {
        GameManager.Instance.isPuzzle = true;
        playerCam.Priority = 10;
        chessCam.Priority = 20;
    }

    public CinemachineVirtualCamera GetPlayerCam()
    {
        return playerCam;
    }
}
