using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlinePlayerHandler : Mirror.NetworkBehaviour
{
    bool done;

    // Update is called once per frame
    void Update()
    {
        if (done)
        {
            return;
        }

        if (SceneManager.GetActiveScene().name != "BattleScene")
        {
            return;
        }

        SetupRollbackObject();
    }

    void SetupRollbackObject()
    {
        GameObject rollbackNetcodeObject = GameObject.FindGameObjectWithTag("RollbackManager");

        if (rollbackNetcodeObject == null)
        {
            return;
        }

        RollbackNetcode rollbackNetcode = rollbackNetcodeObject.GetComponent<RollbackNetcode>();

        if (rollbackNetcode == null)
        {
            return;
        }

        if (isLocalPlayer)
        {
            if (isServer)
            {
                rollbackNetcode.onlinePlayer1 = gameObject.GetComponent<OnlinePlayerInputs>();
                rollbackNetcode.desyncManagerPlayer1 = gameObject.GetComponent<DesyncManager>();
                rollbackNetcode.localPlayer1 = true;
            }
            else
            {
                rollbackNetcode.onlinePlayer2 = gameObject.GetComponent<OnlinePlayerInputs>();
                rollbackNetcode.desyncManagerPlayer2 = gameObject.GetComponent<DesyncManager>();
                rollbackNetcode.localPlayer1 = false;
            }
        }
        else
        {
            if (isServer)
            {
                rollbackNetcode.onlinePlayer2 = gameObject.GetComponent<OnlinePlayerInputs>();
                rollbackNetcode.desyncManagerPlayer2 = gameObject.GetComponent<DesyncManager>();
            }
            else
            {
                rollbackNetcode.onlinePlayer1 = gameObject.GetComponent<OnlinePlayerInputs>();
                rollbackNetcode.desyncManagerPlayer1 = gameObject.GetComponent<DesyncManager>();
            }
        }

        OnlinePlayerInputs onlinePlayerInputs = GetComponent<OnlinePlayerInputs>();

        if (onlinePlayerInputs == null)
        {
            return;
        }

        onlinePlayerInputs.rollbackNetcode = rollbackNetcode;

        DesyncManager desyncManager = GetComponent<DesyncManager>();

        if (desyncManager == null)
        {
            return;
        }

        desyncManager.rollbackNetcode = rollbackNetcode;

        rollbackNetcode.battleManager.multiplayer = true;
        rollbackNetcode.battleManager.start = false;
        rollbackNetcode.battleManager.gameState = new BattleGameState();

        done = true;
    }
}
