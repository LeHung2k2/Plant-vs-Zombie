using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "LevelData", menuName = "Scriptable Objects/Data/LevelData")]
public class LevelSO : ScriptableObject
{
    public  List<LevelData> zombieQuantities = new List<LevelData>();
}
