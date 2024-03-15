using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonPromptIcon : MonoBehaviour
{
    // Icons
    public InputIcons inputIcons;
    private Image icon;

    // Action Settings
    public InputActionReference inputActionGamepad;
    public int indexGamepad;
    public InputActionReference inputActionKeyboard1;
    public int indexKeyboard1;
    public InputActionReference inputActionKeyboard2;
    public int indexKeyboard2;

    public bool player1;
    
    // Device
    [SerializeField]
    private int currentDevice;
    [SerializeField]
    private PlayerInputPairing pairing;

    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pairing == null)
        {
            pairing = FindObjectOfType<PlayerInputPairing>();
        }

        if (pairing != null)
        {
            if (player1)
            {
                currentDevice = pairing.lastPlayer1Device;
            }
            else
            {
                currentDevice = pairing.lastPlayer2Device;
            }
        }

        string device = "";
        string path = "";

        switch (currentDevice)
        {
            case 0:
                inputActionGamepad.action.GetBindingDisplayString(indexGamepad, out device, out path);

                if(pairing.gamepad1.devices.Count > 0 && pairing.gamepad1.devices[0].device.description.product.Contains("DualShock"))
                {
                    icon.sprite = inputIcons.ps4.GetSprite(path);
                }
                else
                {
                    icon.sprite = inputIcons.xbox.GetSprite(path);
                }

                break;
            case 1:
                inputActionGamepad.action.GetBindingDisplayString(indexGamepad, out device, out path);

                if (pairing.gamepad2.devices.Count > 0 && pairing.gamepad2.devices[0].device.description.product.Contains("DualShock"))
                {
                    icon.sprite = inputIcons.ps4.GetSprite(path);
                }
                else
                {
                    icon.sprite = inputIcons.xbox.GetSprite(path);
                }

                break;
            case 2:
                inputActionKeyboard1.action.GetBindingDisplayString(indexKeyboard1, out device, out path);
                icon.sprite = inputIcons.keyboard.GetSprite(path);
                break;
            case 3:
                inputActionKeyboard2.action.GetBindingDisplayString(indexKeyboard2, out device, out path);
                icon.sprite = inputIcons.keyboard.GetSprite(path);
                break;
        }
    }
}
