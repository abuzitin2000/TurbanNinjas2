using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpecials : MonoBehaviour
{
    public void LoseAnimation(bool player1, BattleGameState gameState)
    {
        if (player1)
		{
            gameState.player1.stun = 1000;
            gameState.player1.pushback = 20;
            gameState.player1.positionY += 150;

            if (!gameState.player1.mirrored)
			{
                gameState.player1.velocityX = -15;
            }
			else
			{
                gameState.player1.velocityX = 15;
            }
        }
    }
}
