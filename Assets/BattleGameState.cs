using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleGameState
{
    public int frameTime;
    public int hitStopTime;

    [System.Serializable]
    public class CharacterState
    {
        public int positionX;
        public int positionY;

        public int health;

        public bool mirrored;

        public int stun;
        public int pushback;

        public bool attacking;
        public bool blocking;
        public bool cancellable;
        public bool hit;

        public int jumping;
        public bool crouching;
        public bool grounded;

        public int velocityX;
        public int velocityY;

        public int animation;
        public int frame;

        public int jumpWindow;
    }

    public CharacterState player1;
    public CharacterState player2;

    public BattleGameState()
    {
        player1 = new CharacterState();
        player2 = new CharacterState();
    }

    public BattleGameState CreateCopy()
    {
        BattleGameState tempBattleGameState = new BattleGameState();

        // General Battle State
        tempBattleGameState.frameTime = this.frameTime;

        // Players Battle States
        CharacterState tempPlayer1 = new CharacterState();
        tempBattleGameState.player1 = tempPlayer1;

        CharacterState tempPlayer2 = new CharacterState();
        tempBattleGameState.player2 = tempPlayer2;

        tempPlayer1.positionX = player1.positionX;
        tempPlayer2.positionX = player2.positionX;

        tempPlayer1.positionY = player1.positionY;
        tempPlayer2.positionY = player2.positionY;

        tempPlayer1.health = player1.health;
        tempPlayer2.health = player2.health;

        tempPlayer1.mirrored = player1.mirrored;
        tempPlayer2.mirrored = player2.mirrored;

        tempPlayer1.stun = player1.stun;
        tempPlayer2.stun = player2.stun;

        tempPlayer1.pushback = player1.pushback;
        tempPlayer2.pushback = player2.pushback;

        tempPlayer1.attacking = player1.attacking;
        tempPlayer2.attacking = player2.attacking;

        tempPlayer1.blocking = player1.blocking;
        tempPlayer2.blocking = player2.blocking;

        tempPlayer1.cancellable = player1.cancellable;
        tempPlayer2.cancellable = player2.cancellable;

        tempPlayer1.hit = player1.hit;
        tempPlayer2.hit = player2.hit;

        tempPlayer1.jumping = player1.jumping;
        tempPlayer2.jumping = player2.jumping;

        tempPlayer1.crouching = player1.crouching;
        tempPlayer2.crouching = player2.crouching;

        tempPlayer1.grounded = player1.grounded;
        tempPlayer2.grounded = player2.grounded;

        tempPlayer1.velocityX = player1.velocityX;
        tempPlayer2.velocityX = player2.velocityX;

        tempPlayer1.velocityY = player1.velocityY;
        tempPlayer2.velocityY = player2.velocityY;

        tempPlayer1.animation = player1.animation;
        tempPlayer2.animation = player2.animation;

        tempPlayer1.frame = player1.frame;
        tempPlayer2.frame = player2.frame;

        tempPlayer1.jumpWindow = player1.jumpWindow;
        tempPlayer2.jumpWindow = player2.jumpWindow;

        return tempBattleGameState;
    }

    public bool CompareStates(BattleGameState gameState)
    {
        bool answer = true;

        answer = answer && (gameState.player1.positionX == player1.positionX);
        answer = answer && (gameState.player2.positionX == player2.positionX);

        answer = answer && (gameState.player1.positionY == player1.positionY);
        answer = answer && (gameState.player2.positionY == player2.positionY);

        answer = answer && (gameState.player1.health == player1.health);
        answer = answer && (gameState.player2.health == player2.health);

        answer = answer && (gameState.player1.mirrored == player1.mirrored);
        answer = answer && (gameState.player2.mirrored == player2.mirrored);

        answer = answer && (gameState.player1.stun == player1.stun);
        answer = answer && (gameState.player2.stun == player2.stun);

        answer = answer && (gameState.player1.pushback == player1.pushback);
        answer = answer && (gameState.player2.pushback == player2.pushback);

        answer = answer && (gameState.player1.attacking == player1.attacking);
        answer = answer && (gameState.player2.attacking == player2.attacking);

        answer = answer && (gameState.player1.blocking == player1.blocking);
        answer = answer && (gameState.player2.blocking == player2.blocking);

        answer = answer && (gameState.player1.cancellable == player1.cancellable);
        answer = answer && (gameState.player2.cancellable == player2.cancellable);

        answer = answer && (gameState.player1.hit == player1.hit);
        answer = answer && (gameState.player2.hit == player2.hit);

        answer = answer && (gameState.player1.jumping == player1.jumping);
        answer = answer && (gameState.player2.jumping == player2.jumping);

        answer = answer && (gameState.player1.crouching == player1.crouching);
        answer = answer && (gameState.player2.crouching == player2.crouching);

        answer = answer && (gameState.player1.grounded == player1.grounded);
        answer = answer && (gameState.player2.grounded == player2.grounded);

        answer = answer && (gameState.player1.velocityX == player1.velocityX);
        answer = answer && (gameState.player2.velocityX == player2.velocityX);

        answer = answer && (gameState.player1.velocityY == player1.velocityY);
        answer = answer && (gameState.player2.velocityY == player2.velocityY);

        answer = answer && (gameState.player1.animation == player1.animation);
        answer = answer && (gameState.player2.animation == player2.animation);

        answer = answer && (gameState.player1.frame == player1.frame);
        answer = answer && (gameState.player2.frame == player2.frame);

        answer = answer && (gameState.player1.jumpWindow == player1.jumpWindow);
        answer = answer && (gameState.player2.jumpWindow == player2.jumpWindow);

        return answer;
    }
}