using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shelf : MonoBehaviour
{
    private const float CLOSE_POS_Z = 0.58f;
    private const float OPEN_POS_Z  = 1.0f;

    public eSight item_Sight = eSight.MIDDLE;
    public bool isLocked = false;
    private bool isOpen = false;
    private bool bCursorChanged = false;

    private FPP_ObjScript fpp_Obj = null;

    private void Awake()
    {
        fpp_Obj = GetComponent<FPP_ObjScript>();
    }

    private void OnMouseDown()
    {
        if (item_Sight == FPP_Manager.Instance.GetMove().curSight)
        {
            if (isLocked && Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 1.0f)
            {
                for (int i = 0; i < Inventory.instance.items.Count; i++)
                {
                    if (Inventory.instance.items[i].itemName == "서랍 키")
                    {
                        isLocked = false;
                        break;
                    }
                }

                if (isLocked)
                {
                    return;
                }
            }

            float moveOffset = (isOpen) ? CLOSE_POS_Z : OPEN_POS_Z;

            MoveShelf(moveOffset);
        }

    }

    private void OnMouseEnter()
    {

        if (Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 1.0f)
        {
            fpp_Obj.OnOutline(item_Sight);

            if (!bCursorChanged)
            {
                bCursorChanged = true;

                FPP_MouseCursor.ChangeCursor(FPP_Manager.Instance.cursor_Textures[2]); // 막대화살
            }
        }
    }

    private void OnMouseExit()
    {
        fpp_Obj.OffOutline();

        bCursorChanged = false;
        FPP_MouseCursor.ChangeCursor(FPP_Manager.Instance.cursor_Textures[0], false); // 손바닥
    }

    private void MoveShelf(float _dis)
    {
        isOpen = !isOpen;
        transform.DOLocalMoveZ(_dis, 0.5f)
        .SetEase(Ease.OutQuad);
    }
}
