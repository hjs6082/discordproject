using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackTween : MonoBehaviour
{
    public float attackAngle; // 공격시 꺾을 각도

    private RectTransform rectTrm;

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();    
    }

    public void Attack()
    {
        rectTrm.DORotate(new Vector3(0, 0, attackAngle), 0.1f).OnComplete(() => 
        {
            AudioManager.Instance.AttackSound();
            rectTrm.DORotate(new Vector3(0, 0, 0), 0.1f).SetEase(Ease.OutQuad);
        });
    }
}
