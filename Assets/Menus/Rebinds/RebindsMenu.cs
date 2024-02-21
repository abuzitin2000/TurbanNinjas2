using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindsMenu : MonoBehaviour
{
	public TMPro.TextMeshProUGUI typeText;
	public TMPro.TextMeshProUGUI controllerText;

	public int type;
	public int controller;

	public List<GameObject> rebindMenus;
	public GameObject rebindTest;

	private void Start()
	{
		SwitchType();
		SwitchController();
	}

	public void SwitchType()
	{
		if (type > 2)
		{
			type = 0;
		}

		if (type < 0)
		{
			type = 2;
		}

		switch (type)
		{
			case 0:
				typeText.text = "MENU";
				break;
			case 1:
				typeText.text = "PLAYER 1";
				break;
			case 2:
				typeText.text = "PLAYER 2";
				break;
		}

		SwitchMenu();
	}

	public void SwitchController()
	{
		if (controller > 1)
		{
			controller = 0;
		}

		if (controller < 0)
		{
			controller = 1;
		}

		switch (controller)
		{
			case 0:
				controllerText.text = "GAMEPAD";
				break;
			case 1:
				controllerText.text = "KEYBOARD";
				break;
		}

		SwitchMenu();
	}

	private void SwitchMenu()
	{
		// Disable all menus
		foreach (GameObject menu in rebindMenus)
		{
			menu.SetActive(false);
		}

		// Enable current menu
		rebindMenus[type * 2 + controller].SetActive(true);
	}

	public void TestButtons()
	{
		if (!rebindTest.activeSelf)
		{
			PlayerInput[] playerInputs = GameObject.FindWithTag("InputManager").GetComponentsInChildren<PlayerInput>(false);
			foreach (PlayerInput playerInput in playerInputs)
			{
				playerInput.SwitchCurrentActionMap("BattleControls");
			}

            rebindTest.SetActive(true);
        }
		else
		{
            PlayerInput[] playerInputs = GameObject.FindWithTag("InputManager").GetComponentsInChildren<PlayerInput>(false);
            foreach (PlayerInput playerInput in playerInputs)
            {
                playerInput.SwitchCurrentActionMap("MenuControls");
            }

            rebindTest.SetActive(false);
        }
	}
}
