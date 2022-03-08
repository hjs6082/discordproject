using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePuzzle : MonoBehaviour
{
    [SerializeField] GameObject emptySpace;
    private Camera _camera;
    [SerializeField] private TileScript[] tiles;
    private int emptySpaceIndex = 15;

    RaycastHit hitInfo;

    public bool puzzleStart;
    public bool isWrong;
    public bool isClear;
    public bool isMove;

    void Awake()
    {
        _camera = Camera.main;
        //Shuffle();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && puzzleStart == true && !isMove)
        {
            isMove = true;
            StartCoroutine(WaitForMove(0.4f));
            Vector3 t_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            if (Physics.Raycast(_camera.ScreenPointToRay(t_MousePos), out hitInfo, 100))
            {
                if (Vector3.Distance(a: emptySpace.transform.position, b: hitInfo.transform.position) < 0.5)
                {
                    Vector3 lastEmptySpacePosition = emptySpace.transform.position;
                    TileScript thisTile = hitInfo.transform.GetComponent<TileScript>();
                    emptySpace.transform.position = hitInfo.transform.position;
                    thisTile.targetPosition = lastEmptySpacePosition;
                    int tileIndex = findIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }
        }


        int correctTiles = 0;
        foreach (var a in tiles)
        {
            if (a != null)
            {
                if (a.inRightPlace)
                {
                    correctTiles++;
                }

            }
            if (correctTiles == tiles.Length - 1)
            {
                
            }
        }
    }



    public void ClearCheck()
    {
        if (puzzleStart == true)
        {
            if (tiles[0].name == "Map_0" && tiles[1].name == "Map_1" && tiles[2].name == "Map_2"
              && tiles[3].name == "Map_3" && tiles[4].name == "Map_4" && tiles[5].name == "Map_5"
              && tiles[6].name == "Map_6" && tiles[7].name == "Map_7" && tiles[8].name == "Map_8"
              && tiles[9].name == "Map_9" && tiles[10].name == "Map_10" && tiles[11].name == "Map_11"
              && tiles[12].name == "Map_12" && tiles[13].name == "Map_13" && tiles[14].name == "Map_14")
            {
                isClear = true;
                puzzleStart = false;
            }
           else
            {
                isWrong = true;
            }
        }
    }

    public void Shuffle()
    {
        if(emptySpaceIndex != 15)
        {
            var tileOn15LastPos = tiles[15].targetPosition;
            tiles[15].targetPosition = emptySpace.transform.position;
            emptySpace.transform.position = tileOn15LastPos;
            tiles[emptySpaceIndex] = tiles[15];
            tiles[15] = null;
            emptySpaceIndex = 15;
        }
        int invertion;
        do
        {
            for (int i = 0; i <= 14; i++)
            {
                var lastPos = tiles[i].targetPosition;
                int randomIndex = Random.Range(0, 14);
                tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                tiles[randomIndex].targetPosition = lastPos;
                var tile = tiles[i];
                tiles[i] = tiles[randomIndex];
                tiles[randomIndex] = tile;
            }
            invertion = GetInversions();
            Debug.Log("PuzzleShuffle");
        } while (invertion % 2 != 0); 
    }

    public int findIndex(TileScript ts)
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if(tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    int GetInversions()
    {
        int inversionsSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            int thisTileInvertion = 0;
            for(int j = i; j < tiles.Length; j++)
            {
                if(tiles[j] != null)
                {
                    if(tiles[i].number > tiles[j].number)
                    {
                        thisTileInvertion++;
                    }
                }
            }
            inversionsSum += thisTileInvertion;
        }
        return inversionsSum;
    }

    IEnumerator WaitForMove(float second)
    {
        yield return new WaitForSeconds(second);
        isMove = false;
    }
}
