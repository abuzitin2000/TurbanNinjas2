using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeviceSelection : MonoBehaviour
{
    public GameObject characterSelectorMenu;
    public GameObject deviceSelectorMenu;

    public RectTransform gamepad1;
    public RectTransform gamepad2;
    public RectTransform keyboard1;
    public RectTransform keyboard2;

    public float iconXPos;

    private int gamepad1Player;
    private int gamepad2Player;
    private int keyboard1Player;
    private int keyboard2Player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Left(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        if (player1)
        {
            if (gamepad)
            {
                gamepad1Player -= 1;

                if (gamepad1Player < -1)
                {
                    gamepad1Player = -1;
                }
            }
            else
            {
                keyboard1Player -= 1;

                if (keyboard1Player < -1)
                {
                    keyboard1Player = -1;
                }
            }
        }
        else
        {
            if (gamepad)
            {
                gamepad2Player -= 1;

                if (gamepad2Player < -1)
                {
                    gamepad2Player = -1;
                }
            }
            else
            {
                keyboard2Player -= 1;

                if (keyboard2Player < -1)
                {
                    keyboard2Player = -1;
                }
            }
        }

        ChangePositions();
    }

    public void Right(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        if (player1)
        {
            if (gamepad)
            {
                gamepad1Player += 1;

                if (gamepad1Player > 1)
                {
                    gamepad1Player = 1;
                }
            }
            else
            {
                keyboard1Player += 1;

                if (keyboard1Player > 1)
                {
                    keyboard1Player = 1;
                }
            }
        }
        else
        {
            if (gamepad)
            {
                gamepad2Player += 1;

                if (gamepad2Player > 1)
                {
                    gamepad2Player = 1;
                }
            }
            else
            {
                keyboard2Player += 1;

                if (keyboard2Player > 1)
                {
                    keyboard2Player = 1;
                }
            }
        }

        ChangePositions();
    }

    public void Select()
    {
        PlayerInputPairing pairings = GameObject.FindWithTag("InputManager").GetComponent<PlayerInputPairing>();
        pairings.gamepad1.GetComponent<PlayerInputBattle>().player1 = gamepad1Player == 1 ? false : true;
        pairings.gamepad2.GetComponent<PlayerInputBattle>().player1 = gamepad2Player == 1 ? false : true;
        pairings.keyboard1.GetComponent<PlayerInputBattle>().player1 = keyboard1Player == 1 ? false : true;
        pairings.keyboard2.GetComponent<PlayerInputBattle>().player1 = keyboard2Player == 1 ? false : true;

        characterSelectorMenu.SetActive(true);
        deviceSelectorMenu.SetActive(false);

        SceneManager.LoadScene("3D Test Scene");
    }

    private void ChangePositions()
    {
        gamepad1.anchoredPosition = new Vector2(iconXPos * gamepad1Player, gamepad1.anchoredPosition.y);
        keyboard1.anchoredPosition = new Vector2(iconXPos * keyboard1Player, keyboard1.anchoredPosition.y);
        gamepad2.anchoredPosition = new Vector2(iconXPos * gamepad2Player, gamepad2.anchoredPosition.y);
        keyboard2.anchoredPosition = new Vector2(iconXPos * keyboard2Player, keyboard2.anchoredPosition.y);
    }
}
