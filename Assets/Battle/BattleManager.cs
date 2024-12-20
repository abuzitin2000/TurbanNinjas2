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

    // Selected Characters and Weapons
    public int player1Ninja;
    public int player2Ninja;

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
    public InputData inputData;
    public AnimationsData baseNinjaData;
    public List<AnimationsData> ninjaDatas;
    public List<AnimationsData> weaponDatas;
    public List<StatsData> ninjaStats;

    // Constructed Character Datas
    public List<AnimationsData> character1Datas;
    public List<AnimationsData> character2Datas;

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
        character1 = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        character2 = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        character1Datas = new List<AnimationsData>() { baseNinjaData, ninjaDatas[player1Ninja], weaponDatas[0] };
        character2Datas = new List<AnimationsData>() { baseNinjaData, ninjaDatas[player2Ninja], weaponDatas[0] };

        characterAnimator.character1Animator = character1.GetComponentInChildren<Animator>();
        characterAnimator.character2Animator = character2.GetComponentInChildren<Animator>();

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

        character1.transform.position = transform.position + new Vector3(gameState.character1.positionX / renderRatio, gameState.character1.positionY / renderRatio, 0);
        character2.transform.position = transform.position + new Vector3(gameState.character2.positionX / renderRatio, gameState.character2.positionY / renderRatio, 0);

        player1Health.text = gameState.character1.health.ToString();
        player2Health.text = gameState.character2.health.ToString();

        player1Wins.text = "WINS " + roundManager.player1Wins.ToString();
        player2Wins.text = roundManager.player2Wins.ToString() + " WINS";

        if (rollbackNetcode.desyncError)
            desnycText.text = "DESYNC";

        koText.enabled = roundManager.endTimer > -1 ? true : false;
    }

    public void StartGame()
	{
        gameState.character1.health = ninjaStats[player1Ninja].health;
        gameState.character2.health = ninjaStats[player2Ninja].health;

        gameState.character1.positionX = battleData.startPosition * -1;
        gameState.character2.positionX = battleData.startPosition;
        gameState.character1.positionY = battleData.groundLevel;
        gameState.character2.positionY = battleData.groundLevel;

        start = true;
	}
}
