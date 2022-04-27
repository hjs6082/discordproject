using UnityEngine;

namespace Dialogue
{
    public class Maze_Spawner : MonoBehaviour
    {
        public void SpawnMaze(GameObject _mazePrefab)
        {
            Transform maze_Parent = Maze_Manager.Instance.maze_Parent;

            GameObject maze = Instantiate<GameObject>(_mazePrefab, maze_Parent.position, Quaternion.Euler(0.0f, 0.0f, 90.0f), maze_Parent);
            Maze_Manager.Instance.maze_Object = maze;
        }
    }
}