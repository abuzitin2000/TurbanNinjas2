using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeviceSelection : MonoBehaviour
{
    public GameObject characterSelectorMenu;
    public GameObject deviceSelectorMenu;
    public GameObject characterSelector;
    public GameObject deviceSelector;

    public RectTransform gamepad1;
    public RectTransform gamepad2;
    public RectTransform keyboard1;
    public RectTransform keyboard2;

    public float iconXPos;
    public float iconSpeed;

    public int gamepad1Player;
    public int gamepad2Player;
    public int keyboard1Player;
    public int keyboard2Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangePositions(gamepad1, gamepad1Player);
        ChangePositions(gamepad2, gamepad2Player);
        ChangePositions(keyboard1, keyboard1Player);
        ChangePositions(keyboard2, keyboard2Player);
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
    }

    public void Select(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        // Set Device Pairings
        GameObject.FindWithTag("InputManager").GetComponent<PlayerInputPairing>().ChangePairings(gamepad1Player == 1 ? false : true,
            gamepad2Player == 1 ? false : true, keyboard1Player == 1 ? false : true, keyboard2Player == 1 ? false : true);

        characterSelectorMenu.SetActive(true);
        characterSelector.SetActive(true);
        deviceSelectorMenu.SetActive(false);
        deviceSelector.SetActive(false);
    }

    public void Back(bool player1, bool active, bool gamepad)
    {
        if (!active)
        {
            return;
        }

        SceneManager.LoadScene("MainMenuScene");
    }

    public void ChangePositions(RectTransform icon, int direction)
    {
        if (icon.anchoredPosition.x < iconXPos * direction)
        {
            icon.anchoredPosition = new Vector2(icon.anchoredPosition.x + iconSpeed * Time.deltaTime, icon.anchoredPosition.y);

            if (icon.anchoredPosition.x > iconXPos * direction)
            {
                icon.anchoredPosition = new Vector2(iconXPos * direction, icon.anchoredPosition.y);
            }
        }
        if (icon.anchoredPosition.x > iconXPos * direction)
        {
            icon.anchoredPosition = new Vector2(icon.anchoredPosition.x - iconSpeed * Time.deltaTime, icon.anchoredPosition.y);

            if (icon.anchoredPosition.x < iconXPos * direction)
            {
                icon.anchoredPosition = new Vector2(iconXPos * direction, icon.anchoredPosition.y);
            }
        }
    }
}
