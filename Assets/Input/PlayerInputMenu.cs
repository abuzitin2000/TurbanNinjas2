using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputMenu : MonoBehaviour
{
    private MainMenuManager menuManager;

    void Update()
    {
        if (menuManager == null)
		{
            menuManager = FindAnyObjectByType<MainMenuManager>();
		}
    }

    public void MenuUp(InputAction.CallbackContext context)
    {
        if (menuManager == null)
		{
            return;
		}

        if (!context.performed)
        {
            return;
        }

        menuManager.MenuUp();
    }

    public void MenuDown(InputAction.CallbackContext context)
    {
        if (menuManager == null)
        {
            return;
        }

        if (!context.performed)
        {
            return;
        }

        menuManager.MenuDown();
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

        menuManager.MenuBack();
    }
}
