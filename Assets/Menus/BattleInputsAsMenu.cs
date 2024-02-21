using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleInputsAsMenu : MonoBehaviour
{
    private RebindTester inputTester;
    private DeviceSelection deviceAssigner;

    void Update()
    {
        if (inputTester == null)
        {
            inputTester = FindAnyObjectByType<RebindTester>();
            deviceAssigner = null;
        }

        if (inputTester == null && deviceAssigner == null)
        {
            deviceAssigner = FindAnyObjectByType<DeviceSelection>();
        }
    }

    public void Left(bool player1, bool active, bool gamepad)
    {
        // Test Inputs
        if (inputTester != null)
        {
            inputTester.Left(player1, active);
        }

        // Device Selection
        if (deviceAssigner != null)
        {
            deviceAssigner.Left(player1, active, gamepad);
        }
    }

    public void Right(bool player1, bool active, bool gamepad)
    {
        // Test Inputs
        if (inputTester != null)
        {
            inputTester.Right(player1, active);
        }

        // Device Selection
        if (deviceAssigner != null)
        {
            deviceAssigner.Right(player1, active, gamepad);
        }
    }

    public void Up(bool player1, bool active, bool gamepad)
    {
        // Test Inputs
        if (inputTester != null)
        {
            inputTester.Up(player1, active);
        }
    }

    public void Down(bool player1, bool active, bool gamepad)
    {
        // Test Inputs
        if (inputTester != null)
        {
            inputTester.Down(player1, active);
        }
    }

    public void LP(bool player1, bool active, bool gamepad)
    {
        // Test Inputs
        if (inputTester != null)
        {
            inputTester.LP(player1, active);
        }
    }

    public void HP(bool player1, bool active, bool gamepad)
    {
        // Test Inputs
        if (inputTester != null)
        {
            inputTester.HP(player1, active);
        }
    }

    public void LK(bool player1, bool active, bool gamepad)
    {
        // Test Inputs
        if (inputTester != null)
        {
            inputTester.LK(player1, active);
        }
    }

    public void HK(bool player1, bool active, bool gamepad)
    {
        // Test Inputs
        if (inputTester != null)
        {
            inputTester.HK(player1, active);
        }
    }
}
