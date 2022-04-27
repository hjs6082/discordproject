using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Dialogue
{
    public class Maze_Ctrl : MonoBehaviour
    {
        private Dictionary<KeyCode, float> maze_Key_Dic = new Dictionary<KeyCode, float>()
    {
        {KeyCode.A,          +90.0f},
        {KeyCode.D,          -90.0f},
        {KeyCode.LeftArrow,  +90.0f},
        {KeyCode.RightArrow, -90.0f}
    };

        public void InputMaze(Transform _mazeTrm)
        {
            if (Input.anyKeyDown && Maze_Manager.Instance.canRotate)
            {
                float degree = Maze_Manager.Instance.curDegree;

                foreach (var maze_Item in maze_Key_Dic)
                {
                    if (Input.GetKeyDown(maze_Item.Key))
                    {
                        Maze_Manager.Instance.canRotate = false;
                        degree += maze_Item.Value;
                        RotateMaze(_mazeTrm, degree);
                        Maze_Manager.Instance.curDegree = degree % 360.0f;
                        return;
                    }
                }
            }
        }

        private void RotateMaze(Transform _mazeTrm, float degree)
        {
            Vector3 rotate_Degree = new Vector3(0.0f, 0.0f, degree);

            _mazeTrm.DORotate(rotate_Degree, 0.2f)
            .SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                Maze_Object.check?.Invoke();
            });
        }
    }
}