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
    public int qcfDownForwardWindow;
    public int qcfDownWindow;
        
    public int qcbBackWindow;
    public int qcbDownBackWindow;
    public int qcbDownWindow;

    public int dpfDownForwardWindow;
    public int dpfDownWindow;
    public int dpfForwardWindow;

    public int dpbDownBackWindow;
    public int dpbDownWindow;
    public int dpbBackWindow;

    public int hcfForwardWindow;
    public int hcfDownWindow;
    public int hcfBackWindow;

    public int hcbBackWindow;
    public int hcbDownWindow;
    public int hcbForwardWindow;

    public int bfForwardWindow;
    public int bfBackChargeWindow;
    public int bfBackChargeRequired;

    public int duUpWindow;
    public int duDownChargeWindow;
    public int duDownChargeRequired;
}
