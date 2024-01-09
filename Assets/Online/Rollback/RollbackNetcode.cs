using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RollbackNetcode : MonoBehaviour
{
    public BattleManager battleManager;

    public List<BattleGameState> stateQueue;
    public List<PlayerButtons> localButtonsQueue;
    public List<PlayerButtons> opponentsButtonsQueue;

    public List<PlayerButtons> delayQueue;
    public List<PlayerButtons> onlineButtonsQueue;
    public List<bool> confirmedOpponentsButtonsQueue;
    public int oldestFrameToRollbackTo;
    public PlayerButtons oldButtons;

    public OnlinePlayerInputs onlinePlayer1;
    public OnlinePlayerInputs onlinePlayer2;

    public bool localPlayer1;

    public List<string> logger;

    private const int rollbackListSize = 200;

    // Start is called before the first frame update
    void Start()
    {
        stateQueue = new List<BattleGameState>();
        localButtonsQueue = new List<PlayerButtons>();
        opponentsButtonsQueue = new List<PlayerButtons>();

        delayQueue = new List<PlayerButtons>();
        onlineButtonsQueue = new List<PlayerButtons>();
        confirmedOpponentsButtonsQueue = new List<bool>();
        oldestFrameToRollbackTo = -1;
        oldButtons = new PlayerButtons();

        logger = new List<string>();
    }

    public void PredictOpponentInput()
    {
        if (localPlayer1)
        {
            opponentsButtonsQueue.Add(battleManager.player2Buttons.CreateCopy());
        }
        else
        {
            opponentsButtonsQueue.Add(battleManager.player1Buttons.CreateCopy());
        }

        confirmedOpponentsButtonsQueue.Add(false);

        // Remove if over limit
        if (opponentsButtonsQueue.Count > rollbackListSize)
        {
            opponentsButtonsQueue.RemoveAt(0);
            confirmedOpponentsButtonsQueue.RemoveAt(0);
        }
    }

    public void ProcessOnlineInputs()
    {
        for (int i = onlineButtonsQueue.Count - 1; i >= 0; i--)
        {
            if (onlineButtonsQueue[i].frameTime <= battleManager.gameState.frameTime)
            {
                AddOpponentInput(onlineButtonsQueue[i]);
                onlineButtonsQueue.RemoveAt(i);
            }
        }
    }

    public void AddOpponentInput(PlayerButtons opponentsButtons)
    {
        if (opponentsButtons == null)
        {
            return;
        }

        if (opponentsButtonsQueue.Count == 0)
        {
            return;
        }

        // Discard if it's too old
        if (opponentsButtons.frameTime < opponentsButtonsQueue[0].frameTime)
        {
            return;
        }

        // Search for same frameTime
        for (int i = opponentsButtonsQueue.Count - 1; i >= 0; i--)
        {
            // If frameTime in the queue is the same as the new buttons
            if (opponentsButtonsQueue[i].frameTime == opponentsButtons.frameTime)
            {
                // Check if already confirmed
                if (confirmedOpponentsButtonsQueue[i])
                {
                    return;
                }

                // Compare if same
                if (opponentsButtonsQueue[i].CompareButtons(opponentsButtons))
                {
                    confirmedOpponentsButtonsQueue[i] = true;
                    return;
                }

                // Change inputs
                opponentsButtonsQueue[i] = opponentsButtons.CreateCopy();
                confirmedOpponentsButtonsQueue[i] = true;

                // Change which frame we should rollback to
                if (opponentsButtons.frameTime < battleManager.gameState.frameTime)
                {
                    if (opponentsButtons.frameTime < oldestFrameToRollbackTo || oldestFrameToRollbackTo == -1)
                    {
                        oldestFrameToRollbackTo = opponentsButtons.frameTime;
                    }
                }

                return;
            }
        }

        Debug.LogError("FRAME COULDN'T BE FOUND!");
    }

    public void ProcessLocalInputs()
    {
        // Add the buttons to the delay queue
        PlayerButtons tempPlayerButtons = AddLocalDelay(battleManager.playerInputManager.PollPlayer1Buttons());

        if (localPlayer1)
        {
            // If the buttons didn't change don't send them to the opponent
            if (tempPlayerButtons.CompareButtons(oldButtons))
            {
                return;
            }

            onlinePlayer1.RpcSendButtonsToClientReliable(tempPlayerButtons);
            onlinePlayer1.RpcSendButtonsToClientUnReliable(tempPlayerButtons);
        }
        else
        {
            // If the buttons didn't change don't send them to the opponent
            if (tempPlayerButtons.CompareButtons(oldButtons))
            {
                return;
            }

            onlinePlayer2.CmdSendButtonsToServerReliable(tempPlayerButtons);
            onlinePlayer2.CmdSendButtonsToServerUnReliable(tempPlayerButtons);
        }

        oldButtons = tempPlayerButtons;
    }

    public void Rollback()
    {
        if (oldestFrameToRollbackTo == -1)
        {
            return;
        }

        // Search oldest frame to rollback to
        int i;
        for (i = stateQueue.Count - 1; i >= 0; i--)
        {
            if (stateQueue[i].frameTime == oldestFrameToRollbackTo)
            {
                break;
            }

            if (i == 0)
            {
                Debug.LogError("State to rollback to couldn't be found!");
                return;
            }
        }

        // Rollback to when the opponent pressed their button
        battleManager.gameState = stateQueue[i].CreateCopy();

        // Redo with the new game state
        while (i < stateQueue.Count - 1)
        {
            stateQueue[i] = battleManager.gameState.CreateCopy();

            // Predict same buttons if not already confirmed
            if (!confirmedOpponentsButtonsQueue[i])
            {
                int tempFrametime = opponentsButtonsQueue[i].frameTime;
                opponentsButtonsQueue[i] = opponentsButtonsQueue[i - 1].CreateCopy();
                opponentsButtonsQueue[i].frameTime = tempFrametime;
            }

            if (localPlayer1)
            {
                battleManager.player1Buttons = localButtonsQueue[i].CreateCopy();
                battleManager.player2Buttons = opponentsButtonsQueue[i].CreateCopy();
                battleManager.AdvanceGame();
            }
            else
            {
                battleManager.player1Buttons = opponentsButtonsQueue[i].CreateCopy();
                battleManager.player2Buttons = localButtonsQueue[i].CreateCopy();
                battleManager.AdvanceGame();
            }

            i++;
        }

        // Set states and buttons for the current frame after rollback is done
        stateQueue[stateQueue.Count - 1] = battleManager.gameState.CreateCopy();

        if (!confirmedOpponentsButtonsQueue[confirmedOpponentsButtonsQueue.Count - 1])
        {
            if (localPlayer1)
            {
                opponentsButtonsQueue[opponentsButtonsQueue.Count - 1] = battleManager.player2Buttons;
            }
            else
            {
                opponentsButtonsQueue[opponentsButtonsQueue.Count - 1] = battleManager.player1Buttons;
            }
        }

        oldestFrameToRollbackTo = -1;
    }

    public void AddGameState()
    {
        stateQueue.Add(battleManager.gameState.CreateCopy());

        if (stateQueue.Count > rollbackListSize)
        {
            stateQueue.RemoveAt(0);
        }
    }

    public PlayerButtons AddLocalDelay(PlayerButtons delayButtons)
    {
        delayButtons.frameTime = battleManager.gameState.frameTime + delay;
        delayQueue.Add(delayButtons.CreateCopy());

        return delayButtons;
    }

    public void AddLocalButtons()
    {
        // Search for same frameTime
        for (int i = 0; i < delayQueue.Count; i++)
        {
            // If frameTime in the queue is the same as the game state
            if (delayQueue[i].frameTime == battleManager.gameState.frameTime)
            {
                localButtonsQueue.Add(delayQueue[i].CreateCopy());
                delayQueue.RemoveAt(i);
                break;
            }
        }

        if (localButtonsQueue.Count > rollbackListSize)
        {
            localButtonsQueue.RemoveAt(0);
        }
    }

    public void PollCurrentFrameButtons()
    {
        if (localPlayer1)
        {
            if (localButtonsQueue.Count > 0)
            {
                battleManager.player1Buttons = localButtonsQueue[localButtonsQueue.Count - 1].CreateCopy();
            }

            if (opponentsButtonsQueue.Count > 0)
            {
                battleManager.player2Buttons = opponentsButtonsQueue[opponentsButtonsQueue.Count - 1].CreateCopy();
            }
        }
        else
        {
            if (localButtonsQueue.Count > 0)
            {
                battleManager.player2Buttons = localButtonsQueue[localButtonsQueue.Count - 1].CreateCopy();
            }

            if (opponentsButtonsQueue.Count > 0)
            {
                battleManager.player1Buttons = opponentsButtonsQueue[opponentsButtonsQueue.Count - 1].CreateCopy();
            }
        }
    }

    void OnApplicationQuit()
    {
        string fileName = "test2.txt";

        if (localPlayer1)
            fileName = "test1.txt";
        
        foreach (string log in logger)
        {
            File.AppendAllText(fileName, log);
            File.AppendAllText(fileName, "\n");
        }
    }
}
