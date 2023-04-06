using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameState
{
    public int frameTime;

    public int player1PositionX;
    public int player1PositionY;
    public int player2PositionX;
    public int player2PositionY;

    public int player1Stun;
    public int player2Stun;

    public int player1Jumping;
    public int player2Jumping;

    public int player1HorizontalVelocity;
    public int player2HorizontalVelocity;
    public int player1VerticalVelocity;
    public int player2VerticalVelocity;

    public int player1JumpingWindow;
    public int player2JumpingWindow;

    public bool player1Crouching;
    public bool player2Crouching;

    public bool player1Grounded;
    public bool player2Grounded;

    public bool player1Mirror;
    public bool player2Mirror;

    public BattleGameState CreateCopy()
    {
        BattleGameState tempBattleGameState = new BattleGameState();

        tempBattleGameState.frameTime = this.frameTime;

        tempBattleGameState.player1PositionX = this.player1PositionX;
        tempBattleGameState.player1PositionY = this.player1PositionY;
        tempBattleGameState.player2PositionX = this.player2PositionX;
        tempBattleGameState.player2PositionY = this.player2PositionY;

        tempBattleGameState.player1Stun = this.player1Stun;
        tempBattleGameState.player2Stun = this.player2Stun;

        return tempBattleGameState;
    }
}
