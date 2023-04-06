using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Inputs")]
public class InputData : ScriptableObject
{
    [System.Serializable]
    public class inputdata
    {
        public int jumpingWindow;
    }

    [SerializeField]
    public inputdata inputData;
}
