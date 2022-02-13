using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject selectedPiece;
    RaycastHit hitInfo;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 t_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            if (Physics.Raycast(_camera.ScreenPointToRay(t_MousePos), out hitInfo, 100))
            {
                if (hitInfo.transform.CompareTag("JigsawPuzzle"))
                {
                    selectedPiece = hitInfo.transform.gameObject;
                }
            }
        }
        if (selectedPiece != null)
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y);
        }
    }
}
