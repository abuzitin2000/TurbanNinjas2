using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundManager : MonoBehaviour
{
    public BattleManager battleManager;

	public int player1Wins = 0;
	public int player2Wins = 0;

	public int endTimer = -1;

	private const int endTimerGoal = 180;

	private bool player1Winning = false;
	private bool player2Winning = false;

	public void CheckRoundOver()
	{
		if (battleManager.gameState.player1.health <= 0 || battleManager.gameState.player2.health <= 0)
		{
			if (endTimer == -1)
			{
				endTimer = 0;
				PlayRoundEndAnimations();
			}

			battleManager.player1Buttons = new PlayerButtons();
			battleManager.player2Buttons = new PlayerButtons();

			endTimer += 1;
		}
		else
		{
			endTimer = -1;
			player1Winning = false;
			player2Winning = false;
		}

		if (endTimer >= endTimerGoal)
		{
			GiveWins();
			EndRound();
		}

		// Win Anim on Landing
		if (player1Winning && battleManager.gameState.player1.grounded)
		{
			battleManager.characterAnimator.SetAnimation(battleManager.gameState.player1, battleManager.player1Data, "Win");
			battleManager.gameState.player1.attacking = true;
			player1Winning = false;
		}

		if (player2Winning && battleManager.gameState.player2.grounded)
		{
			battleManager.characterAnimator.SetAnimation(battleManager.gameState.player2, battleManager.player2Data, "Win");
			battleManager.gameState.player2.attacking = true;
			player2Winning = false;
		}
	}

	private void GiveWins()
	{
		if (battleManager.gameState.player1.health > 0)
		{
			player1Wins += 1;
		}
		else if (battleManager.gameState.player2.health > 0)
		{
			player2Wins += 1;
		}
		else
		{
			player1Wins += 1;
			player2Wins += 1;
		}
	}

	private void PlayRoundEndAnimations()
	{
		if (battleManager.gameState.player1.health > 0)
		{
			player1Winning = true;
			battleManager.characterAnimator.SetAnimation(battleManager.gameState.player2, battleManager.player2Data, "Lose");
		}
		else if (battleManager.gameState.player2.health > 0)
		{
			battleManager.characterAnimator.SetAnimation(battleManager.gameState.player1, battleManager.player1Data, "Lose");
			player2Winning = true;
		}
		else
		{
			battleManager.characterAnimator.SetAnimation(battleManager.gameState.player1, battleManager.player1Data, "Lose");
			battleManager.characterAnimator.SetAnimation(battleManager.gameState.player2, battleManager.player2Data, "Lose");
		}
	}

	private void EndRound()
	{
		battleManager.start = false;

		battleManager.gameState = new BattleGameState();
		battleManager.player1InputHistory = new Dictionary<int, PlayerButtons>();
		battleManager.player2InputHistory = new Dictionary<int, PlayerButtons>();

		if (battleManager.multiplayer)
		{
			battleManager.rollbackNetcode.stateQueue = new List<BattleGameState>();
			battleManager.rollbackNetcode.localButtonsQueue = new List<PlayerButtons>();
			battleManager.rollbackNetcode.opponentsButtonsQueue = new List<PlayerButtons>();
			battleManager.rollbackNetcode.delayQueue = new List<PlayerButtons>();
			battleManager.rollbackNetcode.confirmedOpponentsButtonsQueue = new List<bool>();
			battleManager.rollbackNetcode.oldestFrameToRollbackTo = -1;
			battleManager.rollbackNetcode.oldButtons = new PlayerButtons();

			battleManager.rollbackNetcode.unprocessedOnlineButtonsQueue = new List<PlayerButtons>();

			// Let Online Objects handle round start
			GameObject[] startHandlers = GameObject.FindGameObjectsWithTag("OnlinePlayer");
			foreach (GameObject handler in startHandlers)
			{
				handler.GetComponent<StartHandler>().HandleRoundStart();
			}
		}
		else
		{
			battleManager.StartGame();
		}
	}
}
