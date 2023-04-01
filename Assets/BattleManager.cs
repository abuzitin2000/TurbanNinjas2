using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public RollbackNetcode rollbackNetcode;
    public PlayerInputs playerInputManager;

    public bool multiplayer;
    public bool start;

    public BattleGameState gameState;

    public GameObject characterPrefab;
    public GameObject character1;
    public GameObject character2;

    public PlayerButtons player1Buttons;
    public PlayerButtons player2Buttons;

    public double latencyServer;

    public TMPro.TMP_Text sayac;
    public TMPro.TMP_Text lag;
    public TMPro.TMP_Text inputText;
    public int sayacRollback;
    public int sayacFrame;
    public int sayacInput;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        character1 = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        character2 = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        gameState = new BattleGameState();
        player1Buttons = new PlayerButtons();
        player2Buttons = new PlayerButtons();

        gameState.frameTime = 0;
    }

    // FixedUpdate is called once per 1/60 seconds
    void FixedUpdate()
    {
        if (!start)
        {
            if (multiplayer)
            {
                return;
            }
            else
            {
                start = true;
            }
        }

        if (multiplayer)
        {
            rollbackNetcode.PredictOpponentInput();
            rollbackNetcode.ProcessOnlineInputs();

            if (rollbackNetcode.localPlayer1)
            {
                PlayerButtons tempPlayerButtons = rollbackNetcode.AddLocalDelay(playerInputManager.PollPlayer1Buttons());
                rollbackNetcode.onlinePlayer1.RpcSendButtonsToClientReliable(tempPlayerButtons);
                rollbackNetcode.onlinePlayer1.RpcSendButtonsToClientUnReliable(tempPlayerButtons);
            }
            else
            {
                PlayerButtons tempPlayerButtons = rollbackNetcode.AddLocalDelay(playerInputManager.PollPlayer1Buttons());
                rollbackNetcode.onlinePlayer2.CmdSendButtonsToServerReliable(tempPlayerButtons);
                rollbackNetcode.onlinePlayer2.CmdSendButtonsToServerUnReliable(tempPlayerButtons);
            }
            
            rollbackNetcode.AddLocalButtons();

            rollbackNetcode.AddGameState();

            rollbackNetcode.Rollback();

            rollbackNetcode.PollCurrentFrameButtons();

            AdvanceGame();
        }
        else
        {
            player1Buttons = playerInputManager.PollPlayer1Buttons();
            player2Buttons = playerInputManager.PollPlayer2Buttons();

            AdvanceGame();
        }

        sayacFrame++;
    }

    // Update is called once per frame
    void Update()
    {
        RenderGame();
    }

    public void AdvanceGame()
    {
        if (player1Buttons.up)
        {
            gameState.player1PositionY += 2f;
        }
        if (player1Buttons.down)
        {
            gameState.player1PositionY -= 2f;
        }
        if (player1Buttons.right)
        {
            gameState.player1PositionX += 2f;
        }
        if (player1Buttons.left)
        {
            gameState.player1PositionX -= 2f;
        }

        if (player2Buttons.up)
        {
            gameState.player2PositionY += 2f;
        }
        if (player2Buttons.down)
        {
            gameState.player2PositionY -= 2f;
        }
        if (player2Buttons.right)
        {
            gameState.player2PositionX += 2f;
        }
        if (player2Buttons.left)
        {
            gameState.player2PositionX -= 2f;
        }

        gameState.frameTime++;
        player1Buttons.frameTime = gameState.frameTime;
        player2Buttons.frameTime = gameState.frameTime;

        sayacRollback++;
    }

    public void RenderGame()
    {
        character1.transform.position = new Vector3(gameState.player1PositionX, gameState.player1PositionY, 0);
        character2.transform.position = new Vector3(gameState.player2PositionX, gameState.player2PositionY, 0);

        sayac.text = "Frame: " + gameState.frameTime + " Counted Frame: " + sayacFrame + " Rollback: " + (sayacRollback - sayacFrame);
        if (latencyServer > 0.0)
            lag.text = "Lag: " + latencyServer;
        else
            lag.text = "Lag: " + Mirror.NetworkTime.rtt / 2.0;
        inputText.text = "Gelen input: " + sayacInput;
    }
}
