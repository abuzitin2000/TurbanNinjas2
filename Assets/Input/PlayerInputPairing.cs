using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public class PlayerInputPairing : MonoBehaviour
{
    public PlayerInput gamepad1;
    public PlayerInput gamepad2;
    public PlayerInput keyboard1;
    public PlayerInput keyboard2;

    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;

        PairDevices();
    }

    public void DeviceAdded()
	{
        
	}

    private void PairDevices()
	{
        // Discard existing assignments.
        gamepad1.user.UnpairDevices();
        gamepad2.user.UnpairDevices();
        keyboard1.user.UnpairDevices();
        keyboard2.user.UnpairDevices();

        // Assign devices and control schemes.
        if (Keyboard.current != null)
        {
            InputUser.PerformPairingWithDevice(Keyboard.current, user: keyboard1.user);
            InputUser.PerformPairingWithDevice(Keyboard.current, user: keyboard2.user);

            keyboard1.user.ActivateControlScheme("Keyboard1");
            keyboard2.user.ActivateControlScheme("Keyboard2");
        }

        var gamepadCount = Gamepad.all.Count;
        if (gamepadCount > 0)
        {
            InputUser.PerformPairingWithDevice(Gamepad.all[0], user: gamepad1.user);
            gamepad1.user.ActivateControlScheme("Gamepad1");
        }
        if (gamepadCount > 1)
        {
            InputUser.PerformPairingWithDevice(Gamepad.all[1], user: gamepad2.user);
            gamepad2.user.ActivateControlScheme("Gamepad2");
        }
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (next.name == "BattleScene")
        {
            gamepad1.SwitchCurrentActionMap("BattleControls");
            gamepad2.SwitchCurrentActionMap("BattleControls");
            keyboard1.SwitchCurrentActionMap("BattleControls");
            keyboard2.SwitchCurrentActionMap("BattleControls");
        }
		else
		{
            gamepad1.SwitchCurrentActionMap("MenuControls");
            gamepad2.SwitchCurrentActionMap("MenuControls");
            keyboard1.SwitchCurrentActionMap("MenuControls");
            keyboard2.SwitchCurrentActionMap("MenuControls");
        }
    }
}
