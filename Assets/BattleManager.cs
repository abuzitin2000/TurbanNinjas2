using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // Managers
    public RollbackNetcode rollbackNetcode;
    public PlayerInputs playerInputManager;
    public CharacterMovement characterMovement;
    public CharacterAnimation characterAnimator;
    public CharacterAttacks characterAttacker;
    public CollisionManager collisionManager;
    public StunManager stunManager;

    // Game Starters
    public bool multiplayer;
    public bool start;

    // Game State
    public BattleGameState gameState;
    public PlayerButtons player1Buttons;
    public PlayerButtons player2Buttons;

    // Input History
    public Dictionary<int, PlayerButtons> player1InputHistory;
    public Dictionary<int, PlayerButtons> player2InputHistory;

    // Character Objects
    public GameObject characterPrefab;
    public GameObject character1;
    public GameObject character2;

    // Game Data
    public BattleData battleData;
    public CharacterData player1Data;
    public CharacterData player2Data;
    public InputData inputData;

    // Renders
    public const float renderRatio = 100;
    public TMPro.TMP_Text player1Health;
    public TMPro.TMP_Text player2Health;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        character1 = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        character2 = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        characterAnimator.player1SpriteRenderer = character1.GetComponent<SpriteRenderer>();
        characterAnimator.player2SpriteRenderer = character2.GetComponent<SpriteRenderer>();

        gameState = new BattleGameState();
        player1InputHistory = new Dictionary<int, PlayerButtons>();
        player2InputHistory = new Dictionary<int, PlayerButtons>();

        gameState.frameTime = 0;
    }

    // FixedUpdate is called once per 1/60 seconds
    void FixedUpdate()
    {
        if (!start)
        {
            if (multiplayer)
            {
                return;
            }
            else
            {
                StartGame();
            }
        }

        if (multiplayer)
        {
            rollbackNetcode.PredictOpponentInput();
            rollbackNetcode.ProcessOnlineInputs();

            rollbackNetcode.ProcessLocalInputs();
            
            rollbackNetcode.AddLocalButtons();

            rollbackNetcode.AddGameState();

            rollbackNetcode.Rollback();

            rollbackNetcode.PollCurrentFrameButtons();
        }
        else
        {
            player1Buttons = playerInputManager.GetPlayer1Buttons();
            player2Buttons = playerInputManager.GetPlayer2Buttons();
        }

        AdvanceGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (multiplayer)
		{
            playerInputManager.PollPlayer1Buttons();
		}
		else
		{
            playerInputManager.PollPlayer1Buttons();
            playerInputManager.PollPlayer2Buttons();
        }

        RenderGame();
    }

    public void AdvanceGame()
    {
        if (gameState.hitStopTime <= 0)
		{
            characterAnimator.AdvanceFrames();

            stunManager.ReduceStuns();

            characterMovement.CalculateCharactersMovement();

            characterAttacker.AttackCharacters();

            collisionManager.CalculateCollisions();

            characterMovement.MoveCharacters();
        }
		else
		{
            gameState.hitStopTime -= 1;
		}

        player1InputHistory[gameState.frameTime] = player1Buttons.CreateCopy();
        player2InputHistory[gameState.frameTime] = player2Buttons.CreateCopy();

        gameState.frameTime++;
        player1Buttons.frameTime = gameState.frameTime;
        player2Buttons.frameTime = gameState.frameTime;
    }

    public void RenderGame()
    {
        characterAnimator.AnimateCharacters();

        character1.transform.position = new Vector3(gameState.player1.positionX / renderRatio, gameState.player1.positionY / renderRatio, 0);
        character2.transform.position = new Vector3(gameState.player2.positionX / renderRatio, gameState.player2.positionY / renderRatio, 0);

        player1Health.text = gameState.player1.health.ToString();
        player2Health.text = gameState.player2.health.ToString();
    }

    public void StartGame()
	{
        gameState.player1.health = player1Data.stats.health;
        gameState.player2.health = player2Data.stats.health;

        start = true;
	}
}
