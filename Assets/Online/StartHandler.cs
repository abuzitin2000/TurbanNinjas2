using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartHandler : Mirror.NetworkBehaviour
{
    public bool done;
    public bool started;
    public double startTime;

    public BattleManager battleManager;

    // Start is called before the first frame update
    void Start()
    {
        startTime = -1.0;
    }

    void FixedUpdate()
    {
        if (started)
        {
            return;
        }

        if (!done)
        {
            return;
        }

        if (battleManager == null)
        {
            return;
        }

        if (startTime == -1.0)
        {
            return;
        }

        if (Mirror.NetworkTime.localTime > startTime)
        {
            battleManager.StartGame();
            started = true;
        }
    }

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

        HandleStart();
    }

    void HandleStart()
    {
        GameObject battleManagerObject = GameObject.FindGameObjectWithTag("BattleManager");

        if (battleManagerObject == null)
        {
            return;
        }

        battleManager = battleManagerObject.GetComponent<BattleManager>();

        if (battleManager == null)
        {
            return;
        }

        if (battleManager.start)
        {
            done = true;
            return;
        }

        if (isServer)
        {
            ServerCheckReady();
        }
    }

    [Mirror.Server]
    public void ServerCheckReady()
    {
        RpcStartBattle();
    }

    [Mirror.ClientRpc]
    public void RpcStartBattle()
    {
        if (isServer)
        {
            return;
        }

        if (battleManager == null)
        {
            return;
        }

        done = true;

        startTime = Mirror.NetworkTime.localTime + Mirror.NetworkTime.rtt + 3.0;
        CmdServerStartBattle((Mirror.NetworkTime.rtt / 2.0) + 3.0);
    }

    [Mirror.Command(requiresAuthority = false)]
    public void CmdServerStartBattle(double cmdStartTime)
    {
        if (!isServer)
        {
            return;
        }

        done = true;

        startTime = Mirror.NetworkTime.localTime + cmdStartTime;
    }

    public void HandleRoundStart()
	{
        if (Mirror.NetworkTime.localTime > startTime)
		{
            done = false;
            started = false;
            startTime = -1;
        }
		else
		{
            started = false;
		}
	}
}
