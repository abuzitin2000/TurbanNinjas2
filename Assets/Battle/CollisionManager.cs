using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
	public BattleManager battleManager;

	public void CalculateCollisions()
	{
		// Character Animator
		CharacterAnimation animator = battleManager.characterAnimator;

		// Character Animation Phase Data
		AnimationsData.CharacterAnimationPhase character1Phase = animator.GetAnimationPhase(true);
        AnimationsData.CharacterAnimationPhase character2Phase = animator.GetAnimationPhase(false);

        // Character States
        BattleGameState.CharacterState character1State = battleManager.gameState.character1;
        BattleGameState.CharacterState character2State = battleManager.gameState.character2;

        // Player 1 Hitboxes
        foreach (AnimationsData.HitboxData hitbox in character1Phase.hitBoxes)
		{
			// Player 2 Hurtboxes
			foreach (AnimationsData.HurtboxData hurtbox in character2Phase.hurtBoxes)
			{
				if (CheckCollision(character1State.positionX, character1State.positionY, hitbox.sizeX, hitbox.sizeY, hitbox.positionoffsetX, hitbox.positionoffsetY, character1State.mirrored, character2State.positionX, character2State.positionY, hurtbox.sizeX, hurtbox.sizeY, hurtbox.positionoffsetX, hurtbox.positionoffsetY, character2State.mirrored))
				{
					ApplyHit(true, hitbox);
				}
			}
		}

		// Player 2 Hitboxes
		foreach (AnimationsData.HitboxData hitbox in character2Phase.hitBoxes)
		{
			// Player 1 Hurtboxes
			foreach (AnimationsData.HurtboxData hurtbox in character1Phase.hurtBoxes)
			{
				if (CheckCollision(character2State.positionX, character2State.positionY, hitbox.sizeX, hitbox.sizeY, hitbox.positionoffsetX, hitbox.positionoffsetY, character2State.mirrored, character1State.positionX, character1State.positionY, hurtbox.sizeX, hurtbox.sizeY, hurtbox.positionoffsetX, hurtbox.positionoffsetY, character1State.mirrored))
				{
					ApplyHit(false, hitbox);
				}
			}
		}
	}

	private bool CheckCollision(int oneCharPosX, int oneCharPosY, int oneSizeX, int oneSizeY, int onePositionOffsetX, int onePositionOffsetY, bool oneMirrored, int twoCharPosX, int twoCharPosY, int twoSizeX, int twoSizeY, int twoPositionOffsetX, int twoPositionOffsetY, bool twoMirrored)
	{
		// Calculate start and end positions of hitboxes due to mirroring
		int oneStartX = oneMirrored ? oneCharPosX + (onePositionOffsetX * -1) + (oneSizeX * -1) : oneCharPosX + onePositionOffsetX;
		int oneEndX = oneMirrored ? oneCharPosX + (onePositionOffsetX * -1) : oneCharPosX + onePositionOffsetX + oneSizeX;

		// Calculate start and end positions of hitboxes due to mirroring
		int twoStartX = twoMirrored ? twoCharPosX + (twoPositionOffsetX * -1) + (twoSizeX * -1) : twoCharPosX + twoPositionOffsetX;
		int twoEndX = twoMirrored ? twoCharPosX + (twoPositionOffsetX * -1) : twoCharPosX + twoPositionOffsetX + twoSizeX;

		// Check collisions
		bool collisionX = oneEndX >= twoStartX && twoEndX >= oneStartX;
		bool collisionY = oneCharPosY + onePositionOffsetY - oneSizeY <= twoCharPosY + twoPositionOffsetY && twoCharPosY + twoPositionOffsetY - twoSizeY <= oneCharPosY + onePositionOffsetY;

		return collisionX && collisionY;
	}

	private void ApplyHit(bool isAttackerCharacter1, AnimationsData.HitboxData hitbox)
	{
        // Character States
        BattleGameState.CharacterState attacker = isAttackerCharacter1 ? battleManager.gameState.character1 : battleManager.gameState.character2;
        BattleGameState.CharacterState defender = isAttackerCharacter1 ? battleManager.gameState.character2 : battleManager.gameState.character1;

        // Don't hit if already hit
        if (attacker.hit)
		{
			return;
		}

		if (defender.blocking)
		{
			if (hitbox.type == 0)
			{
				if (defender.crouching)
				{
					battleManager.characterAnimator.SetAnimation(!isAttackerCharacter1, "CrouchingBlock");
					DealChipDamage(attacker, defender, hitbox);
				}
				else
				{
					battleManager.characterAnimator.SetAnimation(!isAttackerCharacter1, "StandingBlock");
					DealChipDamage(attacker, defender, hitbox);
				}
			}
			else if (hitbox.type == 1)
			{
				if (defender.crouching)
				{
					battleManager.characterAnimator.SetAnimation(!isAttackerCharacter1, "CrouchingBlock");
					DealChipDamage(attacker, defender, hitbox);
				}
				else
				{
					battleManager.characterAnimator.SetAnimation(!isAttackerCharacter1, "StandingHit");
					DealDamage(attacker, defender, hitbox);
				}
			}
			else if (hitbox.type == 2)
			{
				if (defender.crouching)
				{
					battleManager.characterAnimator.SetAnimation(!isAttackerCharacter1, "CrouchingHit");
					DealDamage(attacker, defender, hitbox);
				}
				else
				{
					battleManager.characterAnimator.SetAnimation(!isAttackerCharacter1, "StandingBlock");
					DealChipDamage(attacker, defender, hitbox);
				}
			}
			else if (hitbox.type == 3)
			{
				if (defender.crouching)
				{
					battleManager.characterAnimator.SetAnimation(!isAttackerCharacter1, "CrouchingHit");
					DealDamage(attacker, defender, hitbox);
				}
				else
				{
					battleManager.characterAnimator.SetAnimation(!isAttackerCharacter1, "StandingHit");
					DealDamage(attacker, defender, hitbox);
				}
			}
		}
		else
		{
			if (defender.crouching)
			{
				battleManager.characterAnimator.SetAnimation(!isAttackerCharacter1, "CrouchingHit");
				DealDamage(attacker, defender, hitbox);
			}
			else
			{
				battleManager.characterAnimator.SetAnimation(!isAttackerCharacter1, "StandingHit");
				DealDamage(attacker, defender, hitbox);
			}
		}
	}

	private void DealDamage(BattleGameState.CharacterState attacker, BattleGameState.CharacterState defender, AnimationsData.HitboxData hitbox)
	{
		defender.health -= hitbox.damage;
		defender.stun = hitbox.hitStun;
		defender.pushback = hitbox.pushbackDuration;
		defender.velocityY += hitbox.juggleStrength;

		if (defender.mirrored)
		{
			defender.velocityX = hitbox.pushbackStrength;
		}
		else
		{
			defender.velocityX = hitbox.pushbackStrength * -1;
		}

		battleManager.gameState.hitStopTime = hitbox.hitStop;

		attacker.hit = true;
	}

	private void DealChipDamage(BattleGameState.CharacterState attacker, BattleGameState.CharacterState defender, AnimationsData.HitboxData hitbox)
	{
		defender.health -= hitbox.chipDamage;
		defender.stun = hitbox.blockStun;
		defender.pushback = hitbox.pushbackDuration;

		if (defender.mirrored)
		{
			defender.velocityX = hitbox.pushbackStrength;
		}
		else
		{
			defender.velocityX = hitbox.pushbackStrength * -1;
		}

		battleManager.gameState.hitStopTime = hitbox.hitStop;

		attacker.hit = true;
	}

	public void HandleCollisionBoxes()
	{
        // Character Animator
        CharacterAnimation animator = battleManager.characterAnimator;

        // Character Animation Phase Data
        AnimationsData.CharacterAnimationPhase character1Phase = animator.GetAnimationPhase(true);
        AnimationsData.CharacterAnimationPhase character2Phase = animator.GetAnimationPhase(false);

        // Character States
        BattleGameState.CharacterState character1State = battleManager.gameState.character1;
        BattleGameState.CharacterState character2State = battleManager.gameState.character2;

		// Collision Boxes
        List<AnimationsData.collisionboxdata> character1CollisionBoxes = character1Phase.collisionBoxes;
		List<AnimationsData.collisionboxdata> character2CollisionBoxes = character2Phase.collisionBoxes;

		if (character1CollisionBoxes.Count == 0 || character2CollisionBoxes.Count == 0)
		{
			return;
		}

		if (!CheckCollision(character1State.positionX, character1State.positionY, character1CollisionBoxes[0].sizeX, character1CollisionBoxes[0].sizeY, character1CollisionBoxes[0].positionoffsetX, character1CollisionBoxes[0].positionoffsetY, character1State.mirrored, character2State.positionX, character2State.positionY, character2CollisionBoxes[0].sizeX, character2CollisionBoxes[0].sizeY, character2CollisionBoxes[0].positionoffsetX, character2CollisionBoxes[0].positionoffsetY, character2State.mirrored))
		{
			return;
		}

		// Player 1 Dash moves
		if (character1State.attacking && !character2State.attacking)
		{
			if (character1State.positionX < character2State.positionX)
			{
				int player1CollisionBoxStartX = character1State.mirrored ? character1State.positionX + (character1CollisionBoxes[0].positionoffsetX * -1) + (character1CollisionBoxes[0].sizeX * -1) : character1State.positionX + character1CollisionBoxes[0].positionoffsetX;
				int player2CollisionBoxStartX = character2State.mirrored ? character2State.positionX + (character2CollisionBoxes[0].positionoffsetX * -1) + (character2CollisionBoxes[0].sizeX * -1) : character2State.positionX + character2CollisionBoxes[0].positionoffsetX;
				character1State.positionX -= character1CollisionBoxes[0].sizeX - (player2CollisionBoxStartX - player1CollisionBoxStartX);

				// Check Wall
				if (character1State.positionX < battleManager.battleData.stageSize * -1)
				{
					int offset = battleManager.battleData.stageSize * -1 - character1State.positionX;
					character1State.positionX += offset;
					character2State.positionX += offset;
				}
			}
			else
			{
				int player1CollisionBoxEndX = character1State.mirrored ? character1State.positionX + (character1CollisionBoxes[0].positionoffsetX * -1) : character1State.positionX + character1CollisionBoxes[0].positionoffsetX + character1CollisionBoxes[0].sizeX;
				int player2CollisionBoxEndX = character2State.mirrored ? character2State.positionX + (character2CollisionBoxes[0].positionoffsetX * -1) : character2State.positionX + character2CollisionBoxes[0].positionoffsetX + character2CollisionBoxes[0].sizeX;
				character1State.positionX += character1CollisionBoxes[0].sizeX - (player1CollisionBoxEndX - player2CollisionBoxEndX);

				// Check Wall
				if (character1State.positionX > battleManager.battleData.stageSize)
				{
					int offset = character1State.positionX - battleManager.battleData.stageSize;
					character1State.positionX -= offset;
					character2State.positionX -= offset;
				}
			}
		}

		// Player 2 Dash moves
		else if (!character1State.attacking && character2State.attacking)
		{
			if (character2State.positionX < character1State.positionX)
			{
				int player1CollisionBoxStartX = character1State.mirrored ? character1State.positionX + (character1CollisionBoxes[0].positionoffsetX * -1) + (character1CollisionBoxes[0].sizeX * -1) : character1State.positionX + character1CollisionBoxes[0].positionoffsetX;
				int player2CollisionBoxStartX = character2State.mirrored ? character2State.positionX + (character2CollisionBoxes[0].positionoffsetX * -1) + (character2CollisionBoxes[0].sizeX * -1) : character2State.positionX + character2CollisionBoxes[0].positionoffsetX;
				character2State.positionX -= character2CollisionBoxes[0].sizeX - (player1CollisionBoxStartX - player2CollisionBoxStartX);

				// Check Wall
				if (character2State.positionX < battleManager.battleData.stageSize * -1)
				{
					int offset = battleManager.battleData.stageSize * -1 - character2State.positionX;
					character1State.positionX += offset;
					character2State.positionX += offset;
				}
			}
			else
			{
				int player1CollisionBoxEndX = character1State.mirrored ? character1State.positionX + (character1CollisionBoxes[0].positionoffsetX * -1) : character1State.positionX + character1CollisionBoxes[0].positionoffsetX + character1CollisionBoxes[0].sizeX;
				int player2CollisionBoxEndX = character2State.mirrored ? character2State.positionX + (character2CollisionBoxes[0].positionoffsetX * -1) : character2State.positionX + character2CollisionBoxes[0].positionoffsetX + character2CollisionBoxes[0].sizeX;
				character2State.positionX += character2CollisionBoxes[0].sizeX - (player2CollisionBoxEndX - player1CollisionBoxEndX);

				// Check Wall
				if (character2State.positionX > battleManager.battleData.stageSize)
				{
					int offset = character2State.positionX - battleManager.battleData.stageSize;
					character1State.positionX -= offset;
					character2State.positionX -= offset;
				}
			}
		}

		// Both are moving or not attacking
		else
		{
			if (character1State.positionX < character2State.positionX)
			{
				int player1CollisionBoxStartX = character1State.mirrored ? character1State.positionX + (character1CollisionBoxes[0].positionoffsetX * -1) + (character1CollisionBoxes[0].sizeX * -1) : character1State.positionX + character1CollisionBoxes[0].positionoffsetX;
				int player2CollisionBoxStartX = character2State.mirrored ? character2State.positionX + (character2CollisionBoxes[0].positionoffsetX * -1) + (character2CollisionBoxes[0].sizeX * -1) : character2State.positionX + character2CollisionBoxes[0].positionoffsetX;

				int offset = character1CollisionBoxes[0].sizeX - (player2CollisionBoxStartX - player1CollisionBoxStartX);
				character1State.positionX -= offset / 2;
				character2State.positionX += offset / 2;

				// Check Wall
				if (character1State.positionX < battleManager.battleData.stageSize * -1)
				{
					int walloffset = battleManager.battleData.stageSize * -1 - character1State.positionX;
					character1State.positionX += walloffset;
					character2State.positionX += walloffset;
				}

				// Check Wall
				if (character2State.positionX > battleManager.battleData.stageSize)
				{
					int walloffset = character2State.positionX - battleManager.battleData.stageSize;
					character1State.positionX -= walloffset;
					character2State.positionX -= walloffset;
				}
			}
			else
			{
				int player1CollisionBoxEndX = character1State.mirrored ? character1State.positionX + (character1CollisionBoxes[0].positionoffsetX * -1) : character1State.positionX + character1CollisionBoxes[0].positionoffsetX + character1CollisionBoxes[0].sizeX;
				int player2CollisionBoxEndX = character2State.mirrored ? character2State.positionX + (character2CollisionBoxes[0].positionoffsetX * -1) : character2State.positionX + character2CollisionBoxes[0].positionoffsetX + character2CollisionBoxes[0].sizeX;

				int offset = character1CollisionBoxes[0].sizeX - (player1CollisionBoxEndX - player2CollisionBoxEndX);
				character1State.positionX += offset / 2;
				character2State.positionX -= offset / 2;

				// Check Wall
				if (character1State.positionX > battleManager.battleData.stageSize)
				{
					int walloffset = character1State.positionX - battleManager.battleData.stageSize;
					character1State.positionX -= walloffset;
					character2State.positionX -= walloffset;
				}

				// Check Wall
				if (character2State.positionX < battleManager.battleData.stageSize * -1)
				{
					int walloffset = battleManager.battleData.stageSize * -1 - character2State.positionX;
					character1State.positionX += walloffset;
					character2State.positionX += walloffset;
				}
			}
		}
	}
}
