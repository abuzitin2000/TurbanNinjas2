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
    public RoundManager roundManager;

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
    public TMPro.TMP_Text desnycText;
    public TMPro.TMP_Text player1Wins;
    public TMPro.TMP_Text player2Wins;
    public TMPro.TMP_Text koText;

    // Setup Battle
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

    // Actual Gameplay Frame
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
            rollbackNetcode.PredictOpponentButtons();
            rollbackNetcode.ProcessRecievedOnlineButtons();

            PlayerButtons send = playerInputManager.DelayPlayer1PolledButtons();
            rollbackNetcode.AddOnlineInputDelay(send);
            rollbackNetcode.SendLocalButtons(send);

            rollbackNetcode.SaveLocalButtons();
            rollbackNetcode.SaveGameState();

            rollbackNetcode.Rollback();
            rollbackNetcode.CheckDesync();

            rollbackNetcode.PollCurrentFrameButtons();
        }
        else
        {
            playerInputManager.DelayPlayer1PolledButtons();
            playerInputManager.DelayPlayer2PolledButtons();

            player1Buttons = playerInputManager.GetPlayer1ProcessedButtons();
            player2Buttons = playerInputManager.GetPlayer2ProcessedButtons();
        }

        AdvanceGame();
    }

    // Normal Update used for Rendering and Input Polling
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
        playerInputManager.SaveButtons();

        if (gameState.hitStopTime <= 0)
        {
            roundManager.CheckRoundOver();

            characterAnimator.AdvanceFrames();

            stunManager.ReduceStuns();

            characterMovement.CalculateCharactersMovement();

            characterAttacker.AttackCharacters();

            characterAnimator.CallSpecialFunctions();

            collisionManager.CalculateCollisions();

            characterMovement.MoveCharacters();
        }
        else
        {
            gameState.hitStopTime -= 1;
        }

        if (rollbackNetcode.desyncError)
            rollbackNetcode.logger.Add("DESYNC");
        //rollbackNetcode.logger.Add(gameState.frameTime + " B1:" + player1Buttons.buttons + " B2:" + player2Buttons.buttons + " X1:" + gameState.player1.positionX + " X2:" + gameState.player2.positionX);

        gameState.frameTime++;
    }

    public void RenderGame()
    {
        characterAnimator.AnimateCharacters();

        character1.transform.position = new Vector3(gameState.player1.positionX / renderRatio, gameState.player1.positionY / renderRatio, 0);
        character2.transform.position = new Vector3(gameState.player2.positionX / renderRatio, gameState.player2.positionY / renderRatio, 0);

        player1Health.text = gameState.player1.health.ToString();
        player2Health.text = gameState.player2.health.ToString();

        player1Wins.text = "WINS " + roundManager.player1Wins.ToString();
        player2Wins.text = roundManager.player2Wins.ToString() + " WINS";

        if (rollbackNetcode.desyncError)
            desnycText.text = "DESYNC";

        koText.enabled = roundManager.endTimer > -1 ? true : false;
    }

    public void StartGame()
	{
        gameState.player1.health = player1Data.stats.health;
        gameState.player2.health = player2Data.stats.health;

        gameState.player1.positionX = battleData.startPosition * -1;
        gameState.player2.positionX = battleData.startPosition;
        gameState.player1.positionY = battleData.groundLevel;
        gameState.player2.positionY = battleData.groundLevel;

        start = true;
	}
}
