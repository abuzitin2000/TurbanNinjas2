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

    public CharacterState character1;
    public CharacterState character2;

    public BattleGameState()
    {
        character1 = new CharacterState();
        character2 = new CharacterState();
    }

    public BattleGameState CreateCopy()
    {
        BattleGameState tempBattleGameState = new BattleGameState();

        // General Battle State
        tempBattleGameState.frameTime = this.frameTime;

        // Players Battle States
        CharacterState tempPlayer1 = new CharacterState();
        tempBattleGameState.character1 = tempPlayer1;

        CharacterState tempPlayer2 = new CharacterState();
        tempBattleGameState.character2 = tempPlayer2;

        tempPlayer1.positionX = character1.positionX;
        tempPlayer2.positionX = character2.positionX;

        tempPlayer1.positionY = character1.positionY;
        tempPlayer2.positionY = character2.positionY;

        tempPlayer1.health = character1.health;
        tempPlayer2.health = character2.health;

        tempPlayer1.mirrored = character1.mirrored;
        tempPlayer2.mirrored = character2.mirrored;

        tempPlayer1.stun = character1.stun;
        tempPlayer2.stun = character2.stun;

        tempPlayer1.pushback = character1.pushback;
        tempPlayer2.pushback = character2.pushback;

        tempPlayer1.attacking = character1.attacking;
        tempPlayer2.attacking = character2.attacking;

        tempPlayer1.blocking = character1.blocking;
        tempPlayer2.blocking = character2.blocking;

        tempPlayer1.cancellable = character1.cancellable;
        tempPlayer2.cancellable = character2.cancellable;

        tempPlayer1.hit = character1.hit;
        tempPlayer2.hit = character2.hit;

        tempPlayer1.jumping = character1.jumping;
        tempPlayer2.jumping = character2.jumping;

        tempPlayer1.crouching = character1.crouching;
        tempPlayer2.crouching = character2.crouching;

        tempPlayer1.grounded = character1.grounded;
        tempPlayer2.grounded = character2.grounded;

        tempPlayer1.velocityX = character1.velocityX;
        tempPlayer2.velocityX = character2.velocityX;

        tempPlayer1.velocityY = character1.velocityY;
        tempPlayer2.velocityY = character2.velocityY;

        tempPlayer1.animation = character1.animation;
        tempPlayer2.animation = character2.animation;

        tempPlayer1.frame = character1.frame;
        tempPlayer2.frame = character2.frame;

        tempPlayer1.jumpWindow = character1.jumpWindow;
        tempPlayer2.jumpWindow = character2.jumpWindow;

        return tempBattleGameState;
    }

    public bool CompareStates(BattleGameState gameState)
    {
        bool answer = true;

        answer = answer && (gameState.character1.positionX == character1.positionX);
        answer = answer && (gameState.character2.positionX == character2.positionX);

        answer = answer && (gameState.character1.positionY == character1.positionY);
        answer = answer && (gameState.character2.positionY == character2.positionY);

        answer = answer && (gameState.character1.health == character1.health);
        answer = answer && (gameState.character2.health == character2.health);

        answer = answer && (gameState.character1.mirrored == character1.mirrored);
        answer = answer && (gameState.character2.mirrored == character2.mirrored);

        answer = answer && (gameState.character1.stun == character1.stun);
        answer = answer && (gameState.character2.stun == character2.stun);

        answer = answer && (gameState.character1.pushback == character1.pushback);
        answer = answer && (gameState.character2.pushback == character2.pushback);

        answer = answer && (gameState.character1.attacking == character1.attacking);
        answer = answer && (gameState.character2.attacking == character2.attacking);

        answer = answer && (gameState.character1.blocking == character1.blocking);
        answer = answer && (gameState.character2.blocking == character2.blocking);

        answer = answer && (gameState.character1.cancellable == character1.cancellable);
        answer = answer && (gameState.character2.cancellable == character2.cancellable);

        answer = answer && (gameState.character1.hit == character1.hit);
        answer = answer && (gameState.character2.hit == character2.hit);

        answer = answer && (gameState.character1.jumping == character1.jumping);
        answer = answer && (gameState.character2.jumping == character2.jumping);

        answer = answer && (gameState.character1.crouching == character1.crouching);
        answer = answer && (gameState.character2.crouching == character2.crouching);

        answer = answer && (gameState.character1.grounded == character1.grounded);
        answer = answer && (gameState.character2.grounded == character2.grounded);

        answer = answer && (gameState.character1.velocityX == character1.velocityX);
        answer = answer && (gameState.character2.velocityX == character2.velocityX);

        answer = answer && (gameState.character1.velocityY == character1.velocityY);
        answer = answer && (gameState.character2.velocityY == character2.velocityY);

        answer = answer && (gameState.character1.animation == character1.animation);
        answer = answer && (gameState.character2.animation == character2.animation);

        answer = answer && (gameState.character1.frame == character1.frame);
        answer = answer && (gameState.character2.frame == character2.frame);

        answer = answer && (gameState.character1.jumpWindow == character1.jumpWindow);
        answer = answer && (gameState.character2.jumpWindow == character2.jumpWindow);

        return answer;
    }
}