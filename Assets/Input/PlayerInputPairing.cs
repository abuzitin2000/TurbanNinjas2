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
        ChangedActiveScene(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());

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

    public void ChangeActionMaps(bool[] maps)
    {
        if (maps[0])
        {
            gamepad1.SwitchCurrentActionMap("BattleControls");
        }
        else
        {
            gamepad1.SwitchCurrentActionMap("MenuControls");
        }

        if (maps[1])
        {
            gamepad2.SwitchCurrentActionMap("BattleControls");
        }
        else
        {
            gamepad2.SwitchCurrentActionMap("MenuControls");
        }

        if (maps[2])
        {
            keyboard1.SwitchCurrentActionMap("BattleControls");
        }
        else
        {
            keyboard1.SwitchCurrentActionMap("MenuControls");
        }

        if (maps[3])
        {
            keyboard2.SwitchCurrentActionMap("BattleControls");
        }
        else
        {
            keyboard2.SwitchCurrentActionMap("MenuControls");
        }
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (next.name == "BattleScene" || next.name == "3D Test Scene")
        {
            ChangeActionMaps(new bool[] { true, true, true, true });
        }
		else
		{
            ChangeActionMaps(new bool[] { false, false, false, false });
        }
    }
}
