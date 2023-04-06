using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public BattleManager battleManager;

    public SpriteRenderer player1SpriteRenderer;
    public SpriteRenderer player2SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AnimateCharacter()
    {
        // Player 1 Jump
        if (battleManager.gameState.player1Jumping > 0)
        {
            player1SpriteRenderer.color = Color.red;
        }

        // Player 2 Jump
        if (battleManager.gameState.player2Jumping > 0)
        {
            player2SpriteRenderer.color = Color.red;
        }
    }
}
