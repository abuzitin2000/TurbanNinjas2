using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionHandler : Mirror.NetworkBehaviour
{
    public Mirror.NetworkManager manager;
    public Mirror.NetworkMatch match;
    public ConnectionMenuManager menuManager;

    void Start()
    {

    }

    void Update()
    {
        if (isServer)
        {
            ServerCheckConnectionCount();
        }
    }

    [Mirror.Server]
    public void ServerCheckConnectionCount()
    {
        if (Mirror.NetworkServer.connections.Count >= 2)
        {
            RpcStartGame();
        }
    }

    [Mirror.ClientRpc]
    public void RpcStartGame()
    {
        if (!isServer)
        {
            SceneManager.LoadScene("3D Test Scene", LoadSceneMode.Single);
            CmdServerStartGame();
        }
    }

    [Mirror.Command(requiresAuthority = false)]
    public void CmdServerStartGame()
    {
        if (isServer)
        {
            SceneManager.LoadScene("3D Test Scene", LoadSceneMode.Single);
        }
    }

    // Host button pressed
    public void OnHostButton()
    {
        string steamID = Steamworks.SteamUser.GetSteamID().m_SteamID.ToString();
        GUIUtility.systemCopyBuffer = steamID;
        menuManager.textInput.text = steamID;

        manager.networkAddress = steamID;
        manager.StartHost();
    }

    // Client button pressed
    public void OnClientButton()
    {
        manager.networkAddress = menuManager.textInput.text;
        manager.StartClient();
    }
}
