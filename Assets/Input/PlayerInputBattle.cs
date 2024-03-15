using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputBattle : MonoBehaviour
{
    public bool active;
    public bool player1;

    [SerializeField]
    private PlayerInputs playerInputs;
    [SerializeField]
    private BattleInputsAsMenu battleInputsAsMenu;
    [SerializeField]
    private PlayerInputPairing pairing;

    void Update()
    {
        battleInputsAsMenu = FindAnyObjectByType<BattleInputsAsMenu>();

        if (battleInputsAsMenu != null)
        {    
            playerInputs = null;
        }

        if (battleInputsAsMenu == null && playerInputs == null)
        {
            playerInputs = FindAnyObjectByType<PlayerInputs>();
        }

        if (pairing == null)
        {
            pairing = FindAnyObjectByType<PlayerInputPairing>();
        }
    }

    public void Select(InputAction.CallbackContext context)
    {
        if (!active)
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

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.Select(player1, context.performed, context.control.device is Gamepad);
        }
        else
        {
            //EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void GamepadSpecificSelect(InputAction.CallbackContext context)
    {
        if (!active)
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

        Select(context);
    }

    public void Back(InputAction.CallbackContext context)
    {
        if (!active)
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

        // Menu Inputs
        if (battleInputsAsMenu != null)
        {
            battleInputsAsMenu.Back(player1, context.performed, context.control.device is Gamepad);
        }
        else
        {
            //EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void GamepadSpecificBack(InputAction.CallbackContext context)
    {
        if (!active)
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

        Back(context);
    }

    public void Left(InputAction.CallbackContext context)
    {
        if (!active)
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
    }

    public void Right(InputAction.CallbackContext context)
    {
        if (!active)
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
        if (!active)
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
        if (!active)
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
        if (!active)
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
        if (!active)
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
        if (!active)
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
        if (!active)
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
        if (!active)
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
        if (!active)
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
        if (!active)
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
        if (!active)
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
}
