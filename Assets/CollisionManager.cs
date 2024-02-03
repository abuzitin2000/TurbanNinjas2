using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
	public BattleManager battleManager;

	public void CalculateCollisions()
	{
		// Player 1 Hitboxes
		foreach (CharacterData.hitboxdata hitbox in battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].frames[battleManager.characterAnimator.FindSprite(battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame)].hitBoxes)
		{
			// Player 2 Hurtboxes
			foreach (CharacterData.hurtboxdata hurtbox in battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].frames[battleManager.characterAnimator.FindSprite(battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame)].hurtBoxes)
			{
				if (CheckCollision(battleManager.gameState.player1.positionX, battleManager.gameState.player1.positionY, hitbox.sizeX, hitbox.sizeY, hitbox.positionoffsetX, hitbox.positionoffsetY, battleManager.gameState.player1.mirrored, battleManager.gameState.player2.positionX, battleManager.gameState.player2.positionY, hurtbox.sizeX, hurtbox.sizeY, hurtbox.positionoffsetX, hurtbox.positionoffsetY, battleManager.gameState.player2.mirrored))
				{
					ApplyHit(battleManager.gameState.player1, battleManager.player1Data, battleManager.gameState.player2, battleManager.player2Data, hitbox);
				}
			}
		}

		// Player 2 Hitboxes
		foreach (CharacterData.hitboxdata hitbox in battleManager.player2Data.characterAnimations[battleManager.gameState.player2.animation].frames[battleManager.characterAnimator.FindSprite(battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame)].hitBoxes)
		{
			// Player 1 Hurtboxes
			foreach (CharacterData.hurtboxdata hurtbox in battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].frames[battleManager.characterAnimator.FindSprite(battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame)].hurtBoxes)
			{
				if (CheckCollision(battleManager.gameState.player2.positionX, battleManager.gameState.player2.positionY, hitbox.sizeX, hitbox.sizeY, hitbox.positionoffsetX, hitbox.positionoffsetY, battleManager.gameState.player2.mirrored, battleManager.gameState.player1.positionX, battleManager.gameState.player1.positionY, hurtbox.sizeX, hurtbox.sizeY, hurtbox.positionoffsetX, hurtbox.positionoffsetY, battleManager.gameState.player1.mirrored))
				{
					ApplyHit(battleManager.gameState.player2, battleManager.player2Data, battleManager.gameState.player1, battleManager.player1Data, hitbox);
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

	private void ApplyHit(BattleGameState.CharacterState attacker, CharacterData attackerData, BattleGameState.CharacterState defender, CharacterData defenderData, CharacterData.hitboxdata hitbox)
	{
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
					battleManager.characterAnimator.SetAnimation(defender, defenderData, "CrouchingBlock");
					DealChipDamage(attacker, defender, hitbox);
				}
				else
				{
					battleManager.characterAnimator.SetAnimation(defender, defenderData, "StandingBlock");
					DealChipDamage(attacker, defender, hitbox);
				}
			}
			else if (hitbox.type == 1)
			{
				if (defender.crouching)
				{
					battleManager.characterAnimator.SetAnimation(defender, defenderData, "CrouchingBlock");
					DealChipDamage(attacker, defender, hitbox);
				}
				else
				{
					battleManager.characterAnimator.SetAnimation(defender, defenderData, "StandingHit");
					DealDamage(attacker, defender, hitbox);
				}
			}
			else if (hitbox.type == 2)
			{
				if (defender.crouching)
				{
					battleManager.characterAnimator.SetAnimation(defender, defenderData, "CrouchingHit");
					DealDamage(attacker, defender, hitbox);
				}
				else
				{
					battleManager.characterAnimator.SetAnimation(defender, defenderData, "StandingBlock");
					DealChipDamage(attacker, defender, hitbox);
				}
			}
			else if (hitbox.type == 3)
			{
				if (defender.crouching)
				{
					battleManager.characterAnimator.SetAnimation(defender, defenderData, "CrouchingHit");
					DealDamage(attacker, defender, hitbox);
				}
				else
				{
					battleManager.characterAnimator.SetAnimation(defender, defenderData, "StandingHit");
					DealDamage(attacker, defender, hitbox);
				}
			}
		}
		else
		{
			if (defender.crouching)
			{
				battleManager.characterAnimator.SetAnimation(defender, defenderData, "CrouchingHit");
				DealDamage(attacker, defender, hitbox);
			}
			else
			{
				battleManager.characterAnimator.SetAnimation(defender, defenderData, "StandingHit");
				DealDamage(attacker, defender, hitbox);
			}
		}
	}

	private void DealDamage(BattleGameState.CharacterState attacker, BattleGameState.CharacterState defender, CharacterData.hitboxdata hitbox)
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

	private void DealChipDamage(BattleGameState.CharacterState attacker, BattleGameState.CharacterState defender, CharacterData.hitboxdata hitbox)
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
		List<CharacterData.collisionboxdata> player1CollisionBoxes = battleManager.player1Data.characterAnimations[battleManager.gameState.player1.animation].frames[battleManager.characterAnimator.FindSprite(battleManager.player1Data, battleManager.gameState.player1.animation, battleManager.gameState.player1.frame)].collisionBoxes;
		List<CharacterData.collisionboxdata> player2CollisionBoxes = battleManager.player1Data.characterAnimations[battleManager.gameState.player2.animation].frames[battleManager.characterAnimator.FindSprite(battleManager.player2Data, battleManager.gameState.player2.animation, battleManager.gameState.player2.frame)].collisionBoxes;

		if (player1CollisionBoxes.Count == 0 || player2CollisionBoxes.Count == 0)
		{
			return;
		}

		if (!CheckCollision(battleManager.gameState.player1.positionX, battleManager.gameState.player1.positionY, player1CollisionBoxes[0].sizeX, player1CollisionBoxes[0].sizeY, player1CollisionBoxes[0].positionoffsetX, player1CollisionBoxes[0].positionoffsetY, battleManager.gameState.player1.mirrored, battleManager.gameState.player2.positionX, battleManager.gameState.player2.positionY, player2CollisionBoxes[0].sizeX, player2CollisionBoxes[0].sizeY, player2CollisionBoxes[0].positionoffsetX, player2CollisionBoxes[0].positionoffsetY, battleManager.gameState.player2.mirrored))
		{
			return;
		}

		// Player 1 Dash moves
		if (battleManager.gameState.player1.attacking && !battleManager.gameState.player2.attacking)
		{
			if (battleManager.gameState.player1.positionX < battleManager.gameState.player2.positionX)
			{
				int player1CollisionBoxStartX = battleManager.gameState.player1.mirrored ? battleManager.gameState.player1.positionX + (player1CollisionBoxes[0].positionoffsetX * -1) + (player1CollisionBoxes[0].sizeX * -1) : battleManager.gameState.player1.positionX + player1CollisionBoxes[0].positionoffsetX;
				int player2CollisionBoxStartX = battleManager.gameState.player2.mirrored ? battleManager.gameState.player2.positionX + (player2CollisionBoxes[0].positionoffsetX * -1) + (player2CollisionBoxes[0].sizeX * -1) : battleManager.gameState.player2.positionX + player2CollisionBoxes[0].positionoffsetX;
				battleManager.gameState.player1.positionX -= player1CollisionBoxes[0].sizeX - (player2CollisionBoxStartX - player1CollisionBoxStartX);

				// Check Wall
				if (battleManager.gameState.player1.positionX < battleManager.battleData.stageSize * -1)
				{
					int offset = battleManager.battleData.stageSize * -1 - battleManager.gameState.player1.positionX;
					battleManager.gameState.player1.positionX += offset;
					battleManager.gameState.player2.positionX += offset;
				}
			}
			else
			{
				int player1CollisionBoxEndX = battleManager.gameState.player1.mirrored ? battleManager.gameState.player1.positionX + (player1CollisionBoxes[0].positionoffsetX * -1) : battleManager.gameState.player1.positionX + player1CollisionBoxes[0].positionoffsetX + player1CollisionBoxes[0].sizeX;
				int player2CollisionBoxEndX = battleManager.gameState.player2.mirrored ? battleManager.gameState.player2.positionX + (player2CollisionBoxes[0].positionoffsetX * -1) : battleManager.gameState.player2.positionX + player2CollisionBoxes[0].positionoffsetX + player2CollisionBoxes[0].sizeX;
				battleManager.gameState.player1.positionX += player1CollisionBoxes[0].sizeX - (player1CollisionBoxEndX - player2CollisionBoxEndX);

				// Check Wall
				if (battleManager.gameState.player1.positionX > battleManager.battleData.stageSize)
				{
					int offset = battleManager.gameState.player1.positionX - battleManager.battleData.stageSize;
					battleManager.gameState.player1.positionX -= offset;
					battleManager.gameState.player2.positionX -= offset;
				}
			}
		}

		// Player 2 Dash moves
		else if (!battleManager.gameState.player1.attacking && battleManager.gameState.player2.attacking)
		{
			if (battleManager.gameState.player2.positionX < battleManager.gameState.player1.positionX)
			{
				int player1CollisionBoxStartX = battleManager.gameState.player1.mirrored ? battleManager.gameState.player1.positionX + (player1CollisionBoxes[0].positionoffsetX * -1) + (player1CollisionBoxes[0].sizeX * -1) : battleManager.gameState.player1.positionX + player1CollisionBoxes[0].positionoffsetX;
				int player2CollisionBoxStartX = battleManager.gameState.player2.mirrored ? battleManager.gameState.player2.positionX + (player2CollisionBoxes[0].positionoffsetX * -1) + (player2CollisionBoxes[0].sizeX * -1) : battleManager.gameState.player2.positionX + player2CollisionBoxes[0].positionoffsetX;
				battleManager.gameState.player2.positionX -= player2CollisionBoxes[0].sizeX - (player1CollisionBoxStartX - player2CollisionBoxStartX);

				// Check Wall
				if (battleManager.gameState.player2.positionX < battleManager.battleData.stageSize * -1)
				{
					int offset = battleManager.battleData.stageSize * -1 - battleManager.gameState.player2.positionX;
					battleManager.gameState.player1.positionX += offset;
					battleManager.gameState.player2.positionX += offset;
				}
			}
			else
			{
				int player1CollisionBoxEndX = battleManager.gameState.player1.mirrored ? battleManager.gameState.player1.positionX + (player1CollisionBoxes[0].positionoffsetX * -1) : battleManager.gameState.player1.positionX + player1CollisionBoxes[0].positionoffsetX + player1CollisionBoxes[0].sizeX;
				int player2CollisionBoxEndX = battleManager.gameState.player2.mirrored ? battleManager.gameState.player2.positionX + (player2CollisionBoxes[0].positionoffsetX * -1) : battleManager.gameState.player2.positionX + player2CollisionBoxes[0].positionoffsetX + player2CollisionBoxes[0].sizeX;
				battleManager.gameState.player2.positionX += player2CollisionBoxes[0].sizeX - (player2CollisionBoxEndX - player1CollisionBoxEndX);

				// Check Wall
				if (battleManager.gameState.player2.positionX > battleManager.battleData.stageSize)
				{
					int offset = battleManager.gameState.player2.positionX - battleManager.battleData.stageSize;
					battleManager.gameState.player1.positionX -= offset;
					battleManager.gameState.player2.positionX -= offset;
				}
			}
		}

		// Both are moving or not attacking
		else
		{
			if (battleManager.gameState.player1.positionX < battleManager.gameState.player2.positionX)
			{
				int player1CollisionBoxStartX = battleManager.gameState.player1.mirrored ? battleManager.gameState.player1.positionX + (player1CollisionBoxes[0].positionoffsetX * -1) + (player1CollisionBoxes[0].sizeX * -1) : battleManager.gameState.player1.positionX + player1CollisionBoxes[0].positionoffsetX;
				int player2CollisionBoxStartX = battleManager.gameState.player2.mirrored ? battleManager.gameState.player2.positionX + (player2CollisionBoxes[0].positionoffsetX * -1) + (player2CollisionBoxes[0].sizeX * -1) : battleManager.gameState.player2.positionX + player2CollisionBoxes[0].positionoffsetX;

				int offset = player1CollisionBoxes[0].sizeX - (player2CollisionBoxStartX - player1CollisionBoxStartX);
				battleManager.gameState.player1.positionX -= offset / 2;
				battleManager.gameState.player2.positionX += offset / 2;

				// Check Wall
				if (battleManager.gameState.player1.positionX < battleManager.battleData.stageSize * -1)
				{
					int walloffset = battleManager.battleData.stageSize * -1 - battleManager.gameState.player1.positionX;
					battleManager.gameState.player1.positionX += walloffset;
					battleManager.gameState.player2.positionX += walloffset;
				}

				// Check Wall
				if (battleManager.gameState.player2.positionX > battleManager.battleData.stageSize)
				{
					int walloffset = battleManager.gameState.player2.positionX - battleManager.battleData.stageSize;
					battleManager.gameState.player1.positionX -= walloffset;
					battleManager.gameState.player2.positionX -= walloffset;
				}
			}
			else
			{
				int player1CollisionBoxEndX = battleManager.gameState.player1.mirrored ? battleManager.gameState.player1.positionX + (player1CollisionBoxes[0].positionoffsetX * -1) : battleManager.gameState.player1.positionX + player1CollisionBoxes[0].positionoffsetX + player1CollisionBoxes[0].sizeX;
				int player2CollisionBoxEndX = battleManager.gameState.player2.mirrored ? battleManager.gameState.player2.positionX + (player2CollisionBoxes[0].positionoffsetX * -1) : battleManager.gameState.player2.positionX + player2CollisionBoxes[0].positionoffsetX + player2CollisionBoxes[0].sizeX;

				int offset = player1CollisionBoxes[0].sizeX - (player1CollisionBoxEndX - player2CollisionBoxEndX);
				battleManager.gameState.player1.positionX += offset / 2;
				battleManager.gameState.player2.positionX -= offset / 2;

				// Check Wall
				if (battleManager.gameState.player1.positionX > battleManager.battleData.stageSize)
				{
					int walloffset = battleManager.gameState.player1.positionX - battleManager.battleData.stageSize;
					battleManager.gameState.player1.positionX -= walloffset;
					battleManager.gameState.player2.positionX -= walloffset;
				}

				// Check Wall
				if (battleManager.gameState.player2.positionX < battleManager.battleData.stageSize * -1)
				{
					int walloffset = battleManager.battleData.stageSize * -1 - battleManager.gameState.player2.positionX;
					battleManager.gameState.player1.positionX += walloffset;
					battleManager.gameState.player2.positionX += walloffset;
				}
			}
		}
	}
}
