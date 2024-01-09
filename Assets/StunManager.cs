using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunManager : MonoBehaviour
{
    public BattleManager battleManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ReduceStuns()
	{
        ReduceStun(battleManager.gameState.player1, battleManager.player1Data);
        ReduceStun(battleManager.gameState.player2, battleManager.player2Data);
        ReducePushBacks(battleManager.gameState.player1, battleManager.player1Data);
        ReducePushBacks(battleManager.gameState.player2, battleManager.player2Data);
    }

    private void ReduceStun(BattleGameState.CharacterState characterState, CharacterData characterData)
	{
        if (characterState.stun > 0)
		{
            characterState.stun -= 1;

            // End Stun
            if (characterState.stun == 0)
			{
                battleManager.characterAnimator.SetAnimation(characterState, characterData, "Idle");
			}
        }
    }

    private void ReducePushBacks(BattleGameState.CharacterState characterState, CharacterData characterData)
    {
        if (characterState.pushback > 0)
        {
            characterState.pushback -= 1;

            // End Stun
            if (characterState.pushback == 0)
            {
                characterState.velocityX = 0;
            }
        }
    }
}
