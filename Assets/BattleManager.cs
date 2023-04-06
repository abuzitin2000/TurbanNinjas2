using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public RollbackNetcode rollbackNetcode;
    public PlayerInputs playerInputManager;
    public CharacterMovement characterMovement;

    public bool multiplayer;
    public bool start;

    public BattleGameState gameState;
    public PlayerButtons player1Buttons;
    public PlayerButtons player2Buttons;

    public GameObject characterPrefab;
    public GameObject character1;
    public GameObject character2;

    public CharacterAnimation characterAnimator;

    public BattleData battleData;
    public CharacterData player1Data;
    public CharacterData player2Data;
    public InputData inputData;

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
        player1Buttons = new PlayerButtons();
        player2Buttons = new PlayerButtons();

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
                start = true;
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
            player1Buttons = playerInputManager.PollPlayer1Buttons();
            player2Buttons = playerInputManager.PollPlayer2Buttons();
        }

        AdvanceGame();
    }

    // Update is called once per frame
    void Update()
    {
        RenderGame();
    }

    public void AdvanceGame()
    {
        characterMovement.MoveCharacters();

        gameState.frameTime++;
        player1Buttons.frameTime = gameState.frameTime;
        player2Buttons.frameTime = gameState.frameTime;
    }

    public void RenderGame()
    {
        const float renderRatio = 100;

        characterAnimator.AnimateCharacter();

        character1.transform.position = new Vector3(gameState.player1PositionX / renderRatio, gameState.player1PositionY / renderRatio, 0);
        character2.transform.position = new Vector3(gameState.player2PositionX / renderRatio, gameState.player2PositionY / renderRatio, 0);
    }
}
