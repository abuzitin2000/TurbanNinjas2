using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Inputs")]
public class InputData : ScriptableObject
{
    [System.Serializable]
    public class inputdata
    {
        public int inputDelay;

        public int jumpingWindow;

        public int qcfForwardWindow;
        public int qcfDownWindow;
        
        public int qcbBackWindow;
        public int qcbDownWindow;

        public int hcfForwardWindow;
        public int hcfDownWindow;
        public int hcfBackwardWindow;

        public int hcbBackwardWindow;
        public int hcbDownWindow;
        public int hcbForwardWindow;

        public int bfForwardWindow;
        public int bfBackChargeWindow;

        public int duUpWindow;
        public int duDownChargeWindow;
    }

    [SerializeField]
    public inputdata inputData;
}
