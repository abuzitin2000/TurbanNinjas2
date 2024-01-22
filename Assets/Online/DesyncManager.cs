using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesyncManager : Mirror.NetworkBehaviour
{
    [HideInInspector]
    public RollbackNetcode rollbackNetcode;

    [Mirror.Command(channel = 0)]
    public void CmdSendGameStateToServerReliable(BattleGameState gameState)
    {
        if (isLocalPlayer)
        {
            return;
        }

        if (!isServer)
        {
            return;
        }

        rollbackNetcode.recievedGameState = gameState;
    }

    [Mirror.ClientRpc(channel = 0)]
    public void RpcSendGameStateToClientReliable(BattleGameState gameState)
    {
        if (isLocalPlayer)
        {
            return;
        }

        if (isServer)
        {
            return;
        }

        rollbackNetcode.recievedGameState = gameState;
    }
}
