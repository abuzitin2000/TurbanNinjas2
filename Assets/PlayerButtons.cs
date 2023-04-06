using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtons
{
    public int frameTime;
    private ushort buttons;

    public PlayerButtons CreateCopy()
    {
        PlayerButtons tempPlayerButtons = new PlayerButtons();

        tempPlayerButtons.frameTime = this.frameTime;
        tempPlayerButtons.buttons = this.buttons;

        return tempPlayerButtons;
    }

    public bool CompareButtons(PlayerButtons compareButtons)
    {
        return compareButtons.buttons == this.buttons;
    }

    public void SetUp(bool up)
    {
        if (up)
        {
            buttons = (ushort)(buttons | 1);
        }
        else
        {
            buttons = (ushort)(buttons & ~1);
        }
    }

    public void SetDown(bool down)
    {
        if (down)
        {
            buttons = (ushort)(buttons | 2);
        }
        else
        {
            buttons = (ushort)(buttons & ~2);
        }
    }

    public void SetLeft(bool left)
    {
        if (left)
        {
            buttons = (ushort)(buttons | 4);
        }
        else
        {
            buttons = (ushort)(buttons & ~4);
        }
    }

    public void SetRight(bool right)
    {
        if (right)
        {
            buttons = (ushort)(buttons | 8);
        }
        else
        {
            buttons = (ushort)(buttons & ~8);
        }
    }

    public void SetLPunch(bool lpunch)
    {
        if (lpunch)
        {
            buttons = (ushort)(buttons | 16);
        }
        else
        {
            buttons = (ushort)(buttons & ~16);
        }
    }

    public void SetLKick(bool lkick)
    {
        if (lkick)
        {
            buttons = (ushort)(buttons | 32);
        }
        else
        {
            buttons = (ushort)(buttons & ~32);
        }
    }

    public void SetHPunch(bool hpunch)
    {
        if (hpunch)
        {
            buttons = (ushort)(buttons | 64);
        }
        else
        {
            buttons = (ushort)(buttons & ~64);
        }
    }

    public void SetHKick(bool hkick)
    {
        if (hkick)
        {
            buttons = (ushort)(buttons | 128);
        }
        else
        {
            buttons = (ushort)(buttons & ~128);
        }
    }

    public void SetMacro1(bool macro1)
    {
        if (macro1)
        {
            buttons = (ushort)(buttons | 256);
        }
        else
        {
            buttons = (ushort)(buttons & ~256);
        }
    }

    public void SetMacro2(bool macro2)
    {
        if (macro2)
        {
            buttons = (ushort)(buttons | 512);
        }
        else
        {
            buttons = (ushort)(buttons & ~512);
        }
    }

    public bool GetUp()
    {
        return (buttons & 1) == 1;
    }

    public bool GetDown()
    {
        return (buttons & 2) == 2;
    }

    public bool GetLeft()
    {
        return (buttons & 4) == 4;
    }

    public bool GetRight()
    {
        return (buttons & 8) == 8;
    }

    public bool GetLPunch()
    {
        return (buttons & 16) == 16;
    }

    public bool GetLKick()
    {
        return (buttons & 32) == 32;
    }

    public bool GetHPunch()
    {
        return (buttons & 64) == 64;
    }

    public bool GetHKick()
    {
        return (buttons & 128) == 128;
    }

    public bool GetMacro1()
    {
        return (buttons & 256) == 256;
    }

    public bool GetMacro2()
    {
        return (buttons & 512) == 512;
    }
}
