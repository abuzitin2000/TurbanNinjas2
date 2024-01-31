using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Battle")]
[System.Serializable]
public class BattleData : ScriptableObject
{
    public int groundLevel;
    public int stageSize;
    public int timer;
    public int startPosition;
}
