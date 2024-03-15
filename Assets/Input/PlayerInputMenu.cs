using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputMenu : MonoBehaviour
{
    [SerializeField]
    private MainMenuManager menuManager;
    [SerializeField]
    private PlayerInputPairing pairing;

    private float holdUp = -1f;
    private float holdDown = -1f;
    private const float holdSpeed = 0.1f;

    void Update()
    {
        if (menuManager == null)
		{
            menuManager = FindAnyObjectByType<MainMenuManager>();
		}

        if (pairing == null)
        {
            pairing = FindAnyObjectByType<PlayerInputPairing>();
        }

        if (holdUp > 0f)
        {
            holdUp -= Time.deltaTime;

            if (holdUp <= 0f)
            {
                menuManager.MenuUp();
                holdUp = holdSpeed;
            }
        }

        if (holdDown > 0f)
        {
            holdDown -= Time.deltaTime;

            if (holdDown <= 0f)
            {
                menuManager.MenuDown();
                holdDown = holdSpeed;
            }
        }
    }

    public void MenuUp(InputAction.CallbackContext context)
    {
        if (menuManager == null)
		{
            return;
		}

        if (pairing != null)
        {
            if (context.control.device is Keyboard)
            {
                pairing.lastPlayer1Device = 2;
                pairing.lastPlayer2Device = 3;
            }
            else
            {
                pairing.lastPlayer1Device = 0;
                pairing.lastPlayer2Device = 1;
            }
        }

        if (context.started)
        {
            menuManager.MenuUp();
        }

        if (context.performed)
        {
            holdUp = holdSpeed;
        }

        if (context.canceled)
        {
            holdUp = -1f;
        }
    }

    public void MenuDown(InputAction.CallbackContext context)
    {
        if (menuManager == null)
        {
            return;
        }

        if (pairing != null)
        {
            if (context.control.device is Keyboard)
            {
                pairing.lastPlayer1Device = 2;
                pairing.lastPlayer2Device = 3;
            }
            else
            {
                pairing.lastPlayer1Device = 0;
                pairing.lastPlayer2Device = 1;
            }
        }

        if (context.started)
        {
            menuManager.MenuDown();
        }

        if (context.performed)
        {
            holdDown = holdSpeed;
        }

        if (context.canceled)
        {
            holdDown = -1f;
        }
    }

    public void MenuLeft(InputAction.CallbackContext context)
    {
        if (menuManager == null)
        {
            return;
        }

        if (!context.performed)
        {
            return;
        }

        if (pairing != null)
        {
            if (context.control.device is Keyboard)
            {
                pairing.lastPlayer1Device = 2;
                pairing.lastPlayer2Device = 3;
            }
            else
            {
                pairing.lastPlayer1Device = 0;
                pairing.lastPlayer2Device = 1;
            }
        }

        menuManager.MenuLeft();
    }

    public void MenuRight(InputAction.CallbackContext context)
    {
        if (menuManager == null)
        {
            return;
        }

        if (!context.performed)
        {
            return;
        }

        if (pairing != null)
        {
            if (context.control.device is Keyboard)
            {
                pairing.lastPlayer1Device = 2;
                pairing.lastPlayer2Device = 3;
            }
            else
            {
                pairing.lastPlayer1Device = 0;
                pairing.lastPlayer2Device = 1;
            }
        }

        menuManager.MenuRight();
    }

    public void MenuSelect(InputAction.CallbackContext context)
    {
        if (menuManager == null)
        {
            return;
        }

        if (!context.performed)
        {
            return;
        }

        if (pairing != null)
        {
            if (context.control.device is Keyboard)
            {
                pairing.lastPlayer1Device = 2;
                pairing.lastPlayer2Device = 3;
            }
            else
            {
                pairing.lastPlayer1Device = 0;
                pairing.lastPlayer2Device = 1;
            }
        }

        menuManager.MenuSelect();
    }

    public void MenuBack(InputAction.CallbackContext context)
    {
        if (menuManager == null)
        {
            return;
        }

        if (!context.performed)
        {
            return;
        }

        if (pairing != null)
        {
            if (context.control.device is Keyboard)
            {
                pairing.lastPlayer1Device = 2;
                pairing.lastPlayer2Device = 3;
            }
            else
            {
                pairing.lastPlayer1Device = 0;
                pairing.lastPlayer2Device = 1;
            }
        }

        menuManager.MenuBack();
    }
}
