using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fight : MonoBehaviour
{
    // actionIcon을 actionBlock에 맞추면
    // 그 action을 실행함

    public Transform actionBlock;

    public GameObject actionIcon;
    public GameObject[] actionIcons;

    public Transform iconParent;

    private Ease ease;
    private Ease[] eases = {
        Ease.InQuad,
        Ease.Linear,
        Ease.OutQuart,
        Ease.InOutBack
    };

    public void InitBlockPos()
    {
        //float randomPosX = Random.Range();

        
    }

    public void IconStart(int _actionID)
    {
        actionIcon = Instantiate(actionIcons[_actionID], iconParent);
        ease = eases[_actionID];

        IconMove(ease);
    }

    public void IconMove(Ease _ease)
    {
        float duration = Random.Range(4f, 7f);

        actionIcon.transform
        .DOMoveX(actionBlock.position.x - 30f, duration)
        .SetEase(_ease);
    }

    public bool IconStop()
    {
        actionIcon.transform.DOKill();

        float iconPosX = actionIcon.transform.position.x;
        float blockPosX = actionBlock.position.x;

        if(Mathf.Abs(blockPosX - iconPosX) <= 1f)
        {
            return true;
        }

        return false;
    }
}
