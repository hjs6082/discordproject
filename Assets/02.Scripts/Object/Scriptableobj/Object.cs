using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "Scriptable Object/Object", order = 1)]
public class Object : ScriptableObject
{
    [SerializeField]
    private string objName;
    public string ObjName { get { return objName; } }
}
