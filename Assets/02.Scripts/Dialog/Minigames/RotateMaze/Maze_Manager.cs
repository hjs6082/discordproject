using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze_Manager : MonoBehaviour
{
    #region 싱글톤
    private static Maze_Manager instance;
    public static Maze_Manager Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    public static Action mazeEnable;

    private Maze_Spawner maze_Spawner = null;
    private Maze_Ctrl maze_Ctrl = null;

    public GameObject maze_Prefab = null;
    public GameObject maze_Object = null;
    
    public float curDegree = 0.0f;
    public bool canRotate = true;

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion

        InitClass();
        InitValue();
    }

    private void Start()
    {
        maze_Spawner.SpawnMaze(maze_Prefab);

        Maze_Object.check?.Invoke();
    }

    private void Update()
    {
        maze_Ctrl.InputMaze(maze_Object.transform);
    }

    private void InitClass()
    {
        // TODO : 여기에 클래스 변수 초기화
        maze_Spawner = GetComponent<Maze_Spawner>();
        maze_Ctrl = GetComponent<Maze_Ctrl>();
    }

    private void InitValue()
    {
        // TODO : 여기에 변수 초기화
    }
}
