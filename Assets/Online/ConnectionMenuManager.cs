using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionMenuManager : MonoBehaviour
{
    public TMPro.TMP_InputField textInput;
    public UnityEngine.UI.Button hostButton;
    public UnityEngine.UI.Button clientButton;
    public TMPro.TMP_Text steamName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        steamName.text = Steamworks.SteamFriends.GetPersonaName();
    }
}
