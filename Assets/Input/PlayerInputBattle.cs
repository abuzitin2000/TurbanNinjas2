using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInputBattle : MonoBehaviour
{
    public bool player1;

    private PlayerInputs playerInputs;
    private BattleInputsAsMenu battleInputsAsMenu;

    void Update()
    {
        if (battleInputsAsMenu == null)
        {
            battleInputsAsMenu = FindAnyObjectByType<BattleInputsAsMenu>();
            playerInputs = null;
        }

        if (battleInputsAsMenu == null && playerInputs == null)
        {
            playerInputs = FindAnyObjectByType<PlayerInputs>();
        }
    }

    public void Menu(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
    }

    public void Left(InputAction.CallbackContext context)
    {
        // Battle Inputs
        if (playerInputs != null)
        {
            playerInputs.InputLeft(player1, context.performed);
        }

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.Left(player1, context.performed, context.control.device is Gamepad);
        }

        Debug.Log("sex");
    }

    public void Right(InputAction.CallbackContext context)
    {
        // Battle Inputs
        if (playerInputs != null)
        {
            playerInputs.InputRight(player1, context.performed);
        }

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.Right(player1, context.performed, context.control.device is Gamepad);
        }
    }

    public void Up(InputAction.CallbackContext context)
    {
        // Battle Inputs
        if (playerInputs != null)
        {
            playerInputs.InputUp(player1, context.performed);
        }

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.Up(player1, context.performed, context.control.device is Gamepad);
        }
    }

    public void Down(InputAction.CallbackContext context)
    {
        // Battle Inputs
        if (playerInputs != null)
        {
            playerInputs.InputDown(player1, context.performed);
        }

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.Down(player1, context.performed, context.control.device is Gamepad);
        }
    }

    public void LP(InputAction.CallbackContext context)
    {
        // Battle Inputs
        if (playerInputs != null)
        {
            playerInputs.InputLP(player1, context.performed);
        }

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.LP(player1, context.performed, context.control.device is Gamepad);
        }
    }

    public void HP(InputAction.CallbackContext context)
    {
        // Battle Inputs
        if (playerInputs != null)
        {
            playerInputs.InputHP(player1, context.performed);
        }

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.HP(player1, context.performed, context.control.device is Gamepad);
        }
    }

    public void LK(InputAction.CallbackContext context)
    {
        // Battle Inputs
        if (playerInputs != null)
        {
            playerInputs.InputLK(player1, context.performed);
        }

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.LK(player1, context.performed, context.control.device is Gamepad);
        }
    }

    public void HK(InputAction.CallbackContext context)
    {
        // Battle Inputs
        if (playerInputs != null)
        {
            playerInputs.InputHK(player1, context.performed);
        }

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.HK(player1, context.performed, context.control.device is Gamepad);
        }
    }

    public void Macro1(InputAction.CallbackContext context)
    {
        // Battle Inputs
        if (playerInputs != null)
        {
            playerInputs.InputLP(player1, context.performed);
            playerInputs.InputLK(player1, context.performed);
        }

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.LP(player1, context.performed, context.control.device is Gamepad);
            battleInputsAsMenu.LK(player1, context.performed, context.control.device is Gamepad);
        }
    }

    public void Macro2(InputAction.CallbackContext context)
    {
        // Battle Inputs
        if (playerInputs != null)
        {
            playerInputs.InputHP(player1, context.performed);
            playerInputs.InputHK(player1, context.performed);
        }

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.HP(player1, context.performed, context.control.device is Gamepad);
            battleInputsAsMenu.HK(player1, context.performed, context.control.device is Gamepad);
        }
    }

    public void Macro3(InputAction.CallbackContext context)
    {
        // Battle Inputs
        if (playerInputs != null)
        {
            playerInputs.InputLP(player1, context.performed);
            playerInputs.InputHP(player1, context.performed);
        }

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.LP(player1, context.performed, context.control.device is Gamepad);
            battleInputsAsMenu.HP(player1, context.performed, context.control.device is Gamepad);
        }
    }

    public void Macro4(InputAction.CallbackContext context)
    {
        // Battle Inputs
        if (playerInputs != null)
        {
            playerInputs.InputLK(player1, context.performed);
            playerInputs.InputHK(player1, context.performed);
        }

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.LK(player1, context.performed, context.control.device is Gamepad);
            battleInputsAsMenu.HK(player1, context.performed, context.control.device is Gamepad);
        }
    }

    // GAMEPAD 2 (THIS IS REQUIRED DUE TO UNITY REBINDING, I KNOW IT SUCKS)
    // -------------------------------------------------------------------
    // -------------------------------------------------------------------
    // -------------------------------------------------------------------

    public void Left2(InputAction.CallbackContext context)
    {
        Left(context);
    }

    public void Right2(InputAction.CallbackContext context)
    {
        Right(context);
    }

    public void Up2(InputAction.CallbackContext context)
    {
        Up(context);
    }

    public void Down2(InputAction.CallbackContext context)
    {
        Down(context);
    }

    public void LP2(InputAction.CallbackContext context)
    {
        LP(context);
    }

    public void HP2(InputAction.CallbackContext context)
    {
        HP(context);
    }

    public void LK2(InputAction.CallbackContext context)
    {
        LK(context);
    }

    public void HK2(InputAction.CallbackContext context)
    {
        HK(context);
    }

    public void Macro12(InputAction.CallbackContext context)
    {
        Macro1(context);
    }

    public void Macro22(InputAction.CallbackContext context)
    {
        Macro2(context);
    }

    public void Macro32(InputAction.CallbackContext context)
    {
        Macro3(context);
    }

    public void Macro42(InputAction.CallbackContext context)
    {
        Macro4(context);
    }
}
