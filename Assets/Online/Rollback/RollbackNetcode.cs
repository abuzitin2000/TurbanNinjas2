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

    public List<PlayerButtons> onlineButtonsQueue;
    public List<bool> confirmedOpponentsButtonsQueue;
    public int oldestFrameToRollbackTo;

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

        onlineButtonsQueue = new List<PlayerButtons>();
        confirmedOpponentsButtonsQueue = new List<bool>();
        oldestFrameToRollbackTo = -1;

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
            if (onlineButtonsQueue[i].frameTime < battleManager.gameState.frameTime)
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
                if (opponentsButtons.frameTime < oldestFrameToRollbackTo || oldestFrameToRollbackTo == -1)
                {
                    oldestFrameToRollbackTo = opponentsButtons.frameTime;
                }

                return;
            }
        }

        // If it's older than the oldest buttons in queue
        if (opponentsButtons.frameTime < opponentsButtonsQueue[0].frameTime)
        {
            Debug.LogError("FRAME WAS LOST");
        }
        else
        {
            Debug.LogError("FRAME FROM FUTURE");
        }
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
            }
        }

        // Rollback to when the opponent pressed their button
        battleManager.gameState = stateQueue[i].CreateCopy();

        // Redo with the new game state
        while (i < stateQueue.Count)
        {
            AddGameState(i);

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

        oldestFrameToRollbackTo = -1;
    }

    public void AddGameState(int rollbackFrame = -1)
    {
        if (rollbackFrame == -1)
        {
            stateQueue.Add(battleManager.gameState.CreateCopy());

            if (stateQueue.Count > rollbackListSize)
            {
                stateQueue.RemoveAt(0);
            }
        }
        else
        {
            stateQueue[rollbackFrame] = battleManager.gameState.CreateCopy();
        }
    }

    public void AddLocalButtons()
    {
        if (localPlayer1)
        {
            localButtonsQueue.Add(battleManager.player1Buttons);
        }
        else
        {
            localButtonsQueue.Add(battleManager.player2Buttons);
        }

        if (localButtonsQueue.Count > rollbackListSize)
        {
            localButtonsQueue.RemoveAt(0);
        }
    }

    void OnApplicationQuit()
    {
        string fileName = "test2.txt";

        if (localPlayer1)
            fileName = "test1.txt";
        
        foreach (string log in logger)
        {
            //File.AppendAllText(fileName, log);
            //File.AppendAllText(fileName, "\n");
        }
    }
}
