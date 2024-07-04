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

    public int lastPlayer1Device;
    public int lastPlayer2Device;

    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        ChangedActiveScene(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());

        ++InputUser.listenForUnpairedDeviceActivity;
        InputUser.onUnpairedDeviceUsed += (ctx, event_ptr) =>
        {
            var device = ctx.device;

            if ((device is Keyboard) || (device is Gamepad))
            {
                DeviceAdded(device);
            }
        };

        PairDevices();
    }

    public void DeviceAdded(InputDevice device)
	{
        PairDevices();
        Debug.Log("Device Added " + device.name);
    }

    public void ChangePairings(bool gamepad1Player1, bool gamepad2Player1, bool keyboard1Player1, bool keyboard2Player1)
    {
        if (gamepad1 == null || gamepad2 == null || keyboard1 == null || keyboard2 == null)
        {
            return;
        }

        gamepad1.GetComponent<PlayerInputBattle>().player1 = gamepad1Player1;
        gamepad2.GetComponent<PlayerInputBattle>().player1 = gamepad2Player1;
        keyboard1.GetComponent<PlayerInputBattle>().player1 = keyboard1Player1;
        keyboard2.GetComponent<PlayerInputBattle>().player1 = keyboard2Player1;
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
            for (int i = 1; i < Gamepad.all.Count; i++)
            {
                InputUser.PerformPairingWithDevice(Gamepad.all[i], user: gamepad2.user);
            }
            
            gamepad2.user.ActivateControlScheme("Gamepad2");
        }
    }

    private void ChangeActionMaps(bool[] maps)
    {
        if (gamepad1 == null || gamepad2 == null || keyboard1 == null || keyboard2 == null)
        {
            return;
        }

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
        // Action Maps
        if (next.name == "StageSakura" || next.name == "BattleScene" || next.name == "3D Test Scene" || next.name == "CharacterSelectScene")
        {
            ChangeActionMaps(new bool[] { true, true, true, true });
        }
		else
		{
            ChangeActionMaps(new bool[] { false, false, false, false });
        }

        // Pairings
        if (next.name == "MainMenuScene")
        {
            // Reset Device Pairings
            ChangePairings(true, false, true, false);
        }
    }
}
