using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlinePlayerInputs : Mirror.NetworkBehaviour
{
    [HideInInspector]
    public RollbackNetcode rollbackNetcode;

    [Mirror.Command(channel = 0)]
    public void CmdSendButtonsToServerReliable(PlayerButtons sendButtons)
    {
        if (isLocalPlayer)
        {
            return;
        }

        if (!isServer)
        {
            return;
        }

        if (rollbackNetcode == null)
        {
            return;
        }

        rollbackNetcode.onlineButtonsQueue.Add(sendButtons.CreateCopy());

        rollbackNetcode.battleManager.sayacInput++;
    }

    [Mirror.Command(channel = 1)]
    public void CmdSendButtonsToServerUnReliable(PlayerButtons sendButtons)
    {
        if (isLocalPlayer)
        {
            return;
        }

        if (!isServer)
        {
            return;
        }

        if (rollbackNetcode == null)
        {
            return;
        }

        rollbackNetcode.onlineButtonsQueue.Add(sendButtons.CreateCopy());

        rollbackNetcode.battleManager.sayacInput++;
    }

    [Mirror.ClientRpc(channel = 0)]
    public void RpcSendButtonsToClientReliable(PlayerButtons sendButtons)
    {
        if (isLocalPlayer)
        {
            return;
        }

        if (isServer)
        {
            return;
        }

        if (rollbackNetcode == null)
        {
            return;
        }

        rollbackNetcode.onlineButtonsQueue.Add(sendButtons.CreateCopy());

        rollbackNetcode.battleManager.sayacInput++;
    }

    [Mirror.ClientRpc(channel = 1)]
    public void RpcSendButtonsToClientUnReliable(PlayerButtons sendButtons)
    {
        if (isLocalPlayer)
        {
            return;
        }

        if (isServer)
        {
            return;
        }

        if (rollbackNetcode == null)
        {
            return;
        }

        rollbackNetcode.onlineButtonsQueue.Add(sendButtons.CreateCopy());

        rollbackNetcode.battleManager.sayacInput++;
    }
}
