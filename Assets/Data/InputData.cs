using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Inputs")]
[System.Serializable]
public class InputData : ScriptableObject
{
    public int inputDelay;

    public int simultaneousWindow;

    public int jumpingWindow;

    public int qcfForwardWindow;
    public int qcfDownWindow;
        
    public int qcbBackWindow;
    public int qcbDownWindow;

    public int dpfForwardSecondWindow;
    public int dpfDownWindow;
    public int dpfForwardFirstWindow;

    public int dpbForwardSecondWindow;
    public int dpbDownWindow;
    public int dpbForwardFirstWindow;

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
