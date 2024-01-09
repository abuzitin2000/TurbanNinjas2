using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Battle")]
public class BattleData : ScriptableObject
{
    [System.Serializable]
    public class battledata
    {
        public int groundLevel;
        public int stageSize;
        public int timer;
    }

    [SerializeField]
    public battledata battleData;
}
