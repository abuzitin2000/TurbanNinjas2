using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameState
{
    public int frameTime;

    public float player1PositionX;
    public float player1PositionY;
    public float player2PositionX;
    public float player2PositionY;

    public BattleGameState CreateCopy()
    {
        BattleGameState tempBattleGameState = new BattleGameState();

        tempBattleGameState.frameTime = this.frameTime;

        tempBattleGameState.player1PositionX = this.player1PositionX;
        tempBattleGameState.player1PositionY = this.player1PositionY;
        tempBattleGameState.player2PositionX = this.player2PositionX;
        tempBattleGameState.player2PositionY = this.player2PositionY;

        return tempBattleGameState;
    }
}
