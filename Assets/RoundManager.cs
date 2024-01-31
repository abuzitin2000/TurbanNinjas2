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
		}

		if (endTimer >= endTimerGoal)
		{
			GiveWins();
			EndRound();
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
			battleManager.characterAnimator.SetAnimation(battleManager.gameState.player1, battleManager.player1Data, "Win");
			battleManager.characterAnimator.SetAnimation(battleManager.gameState.player2, battleManager.player2Data, "Lose");
		}
		else if (battleManager.gameState.player2.health > 0)
		{
			battleManager.characterAnimator.SetAnimation(battleManager.gameState.player1, battleManager.player1Data, "Lose");
			battleManager.characterAnimator.SetAnimation(battleManager.gameState.player2, battleManager.player2Data, "Win");
		}
		else
		{
			battleManager.characterAnimator.SetAnimation(battleManager.gameState.player1, battleManager.player1Data, "Lose");
			battleManager.characterAnimator.SetAnimation(battleManager.gameState.player2, battleManager.player2Data, "Lose");
		}
	}

	public void LoseAnimFunction()
	{
		Debug.Log("HELLO");
	}

	private void EndRound()
	{
		battleManager.start = false;
		battleManager.gameState = new BattleGameState();
		battleManager.player1InputHistory = new Dictionary<int, PlayerButtons>();
		battleManager.player2InputHistory = new Dictionary<int, PlayerButtons>();
	}
}
