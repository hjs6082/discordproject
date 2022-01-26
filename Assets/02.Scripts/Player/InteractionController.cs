using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera cam;

    [SerializeField]
    Texture2D searchCursorImg;
    [SerializeField]
    Texture2D findCursorImg;

    RaycastHit hitInfo;

    bool isContact = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.instance.searchVisionActive == true)
        {
            CheckObject();
        }
    }

    void CheckObject()
    {
        Vector3 t_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        if(Physics.Raycast(cam.ScreenPointToRay(t_MousePos), out hitInfo, 100))
        {
            Contact();
        }
        else
        {
            Cursor.SetCursor(searchCursorImg, Vector2.zero, CursorMode.ForceSoftware);
            NotContact();
        }
    }

    void Contact() //������Ʈ�� ���� ������ ���
    {
        if(hitInfo.transform.CompareTag("Interaction"))
        {
            Cursor.SetCursor(findCursorImg, Vector2.zero, CursorMode.ForceSoftware);
            if (Input.GetMouseButtonDown(0))
            {
                // ����ٰ� ���� ����
                Debug.Log("Click");
            }
            if (!isContact)
            {
                isContact = true;
            }
        }
        else
        {
            NotContact();
        }
        if(hitInfo.transform.CompareTag("ChessPuzzle"))
        {
            Cursor.SetCursor(findCursorImg, Vector2.zero, CursorMode.ForceSoftware);
            if (Input.GetMouseButtonDown(0))
            {
                UIManager.instance.ResetCursor();
                GameManager.Instance.curPlayerPos = Camera.main.transform.parent.position;
                LoadScene.LoadingScene("ChessPuzzle");

            }
            if (!isContact)
            {
                isContact = true;
            }
        }
        else
        {
            NotContact();
        }
        if (hitInfo.transform.CompareTag("MovePuzzle"))
        {
            Cursor.SetCursor(findCursorImg, Vector2.zero, CursorMode.ForceSoftware);
            if (Input.GetMouseButtonDown(0))
            {
                UIManager.instance.ResetCursor();
                SceneManager.LoadScene("MovePuzzle");

            }
            if (!isContact)
            {
                isContact = true;
            }
        }
        else
        {
            NotContact();
        }
    }

    void NotContact() // ������Ʈ�� ���� �Ұ����� ���
    {
        if(isContact)
        {
            isContact = false;
        }
    }
}
