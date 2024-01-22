using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RollbackNetcode : MonoBehaviour
{
    public BattleManager battleManager;

    // Recieved Inputs
    public List<PlayerButtons> unprocessedOnlineButtonsQueue;

    // Rollback Queues
    public List<BattleGameState> stateQueue;
    public List<PlayerButtons> localButtonsQueue;
    public List<PlayerButtons> opponentsButtonsQueue;
    public List<bool> confirmedOpponentsButtonsQueue;

    // Inputs Delay
    private int onlineDelay = 1;
    public List<PlayerButtons> delayQueue;

    public int oldestFrameToRollbackTo;
    public PlayerButtons oldButtons;

    public OnlinePlayerInputs onlinePlayer1;
    public OnlinePlayerInputs onlinePlayer2;

    public bool localPlayer1;

    // Maximum rollback frame
    private const int rollbackListSize = 200;

    public List<string> logger;

    // Start is called before the first frame update
    void Start()
    {
        stateQueue = new List<BattleGameState>();
        localButtonsQueue = new List<PlayerButtons>();
        opponentsButtonsQueue = new List<PlayerButtons>();

        delayQueue = new List<PlayerButtons>();
        unprocessedOnlineButtonsQueue = new List<PlayerButtons>();
        confirmedOpponentsButtonsQueue = new List<bool>();
        oldestFrameToRollbackTo = -1;
        oldButtons = new PlayerButtons();

        logger = new List<string>();
    }

    public void PredictOpponentButtons()
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

        // Remove oldest buttons if over rollback frame limit
        if (opponentsButtonsQueue.Count > rollbackListSize)
        {
            opponentsButtonsQueue.RemoveAt(0);
            confirmedOpponentsButtonsQueue.RemoveAt(0);
        }
    }

    public void ProcessRecievedOnlineButtons()
    {
        // Start from end of list to have better performance with RemoveAt()
        for (int i = unprocessedOnlineButtonsQueue.Count - 1; i >= 0; i--)
        {
            // Check if not from the future
            if (unprocessedOnlineButtonsQueue[i].frameTime <= battleManager.gameState.frameTime)
            {
                ConfirmOpponentButtons(unprocessedOnlineButtonsQueue[i]);
                unprocessedOnlineButtonsQueue.RemoveAt(i);
            }
        }
    }

    private void ConfirmOpponentButtons(PlayerButtons opponentsButtons)
    {
        if (opponentsButtons == null)
        {
            return;
        }

        if (opponentsButtonsQueue.Count == 0)
        {
            return;
        }

        // Discard if it's older than oldest kept buttons
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

    public void AddOnlineInputDelay(PlayerButtons delayButtons)
    {
        delayButtons.frameTime += onlineDelay;
        delayQueue.Add(delayButtons.CreateCopy());
    }

    public void SendLocalButtons(PlayerButtons localButtons)
    {
        // If the buttons didn't change don't send them to the opponent
        if (localButtons.CompareButtons(oldButtons))
        {
            return;
        }

        if (localPlayer1)
        {
            onlinePlayer1.RpcSendButtonsToClientReliable(localButtons);
            onlinePlayer1.RpcSendButtonsToClientUnReliable(localButtons);
        }
        else
        {
            onlinePlayer2.CmdSendButtonsToServerReliable(localButtons);
            onlinePlayer2.CmdSendButtonsToServerUnReliable(localButtons);
        }

        oldButtons = localButtons.CreateCopy();
    }

    public void SaveGameState()
    {
        stateQueue.Add(battleManager.gameState.CreateCopy());

        // Remove oldest state if over rollback frame limit
        if (stateQueue.Count > rollbackListSize)
        {
            stateQueue.RemoveAt(0);
        }
    }

    public void SaveLocalButtons()
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

        // Remove oldest buttons if over rollback frame limit
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
