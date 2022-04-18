using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze_Spawner : MonoBehaviour
{
    public void SpawnMaze(GameObject _mazePrefab)
    {
        Vector3 maze_Pos = new Vector3(0, 0, 0);
        Transform maze_Parent = Maze_Manager.Instance.transform;
        
        GameObject maze = Instantiate<GameObject>(_mazePrefab, maze_Pos, Quaternion.Euler(0.0f, 0.0f, 90.0f), maze_Parent);
        Maze_Manager.Instance.maze_Object = maze;
    }
}
