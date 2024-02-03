using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerButtons
{
    public int frameTime;
    public ushort buttons;

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

    public void ResetPresses()
    {
        SetUpPress(false);
        SetDownPress(false);
        SetLeftPress(false);
        SetRightPress(false);
        SetLPPress(false);
        SetHPPress(false);
        SetLKPress(false);
        SetHKPress(false);
    }

    // SETTERS

    public void SetUpPress(bool up)
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

    public void SetUpHold(bool up)
    {
        if (up)
        {
            buttons = (ushort)(buttons | 2);
        }
        else
        {
            buttons = (ushort)(buttons & ~2);
        }
    }

    public void SetDownPress(bool down)
    {
        if (down)
        {
            buttons = (ushort)(buttons | 4);
        }
        else
        {
            buttons = (ushort)(buttons & ~4);
        }
    }

    public void SetDownHold(bool down)
    {
        if (down)
        {
            buttons = (ushort)(buttons | 8);
        }
        else
        {
            buttons = (ushort)(buttons & ~8);
        }
    }

    public void SetLeftPress(bool left)
    {
        if (left)
        {
            buttons = (ushort)(buttons | 16);
        }
        else
        {
            buttons = (ushort)(buttons & ~16);
        }
    }

    public void SetLeftHold(bool left)
    {
        if (left)
        {
            buttons = (ushort)(buttons | 32);
        }
        else
        {
            buttons = (ushort)(buttons & ~32);
        }
    }

    public void SetRightPress(bool right)
    {
        if (right)
        {
            buttons = (ushort)(buttons | 64);
        }
        else
        {
            buttons = (ushort)(buttons & ~64);
        }
    }

    public void SetRightHold(bool right)
    {
        if (right)
        {
            buttons = (ushort)(buttons | 128);
        }
        else
        {
            buttons = (ushort)(buttons & ~128);
        }
    }

    public void SetLPPress(bool lp)
    {
        if (lp)
        {
            buttons = (ushort)(buttons | 256);
        }
        else
        {
            buttons = (ushort)(buttons & ~256);
        }
    }

    public void SetLPHold(bool lp)
    {
        if (lp)
        {
            buttons = (ushort)(buttons | 512);
        }
        else
        {
            buttons = (ushort)(buttons & ~512);
        }
    }

    public void SetLKPress(bool lk)
    {
        if (lk)
        {
            buttons = (ushort)(buttons | 1024);
        }
        else
        {
            buttons = (ushort)(buttons & ~1024);
        }
    }

    public void SetLKHold(bool lk)
    {
        if (lk)
        {
            buttons = (ushort)(buttons | 2048);
        }
        else
        {
            buttons = (ushort)(buttons & ~2048);
        }
    }

    public void SetHPPress(bool hp)
    {
        if (hp)
        {
            buttons = (ushort)(buttons | 4096);
        }
        else
        {
            buttons = (ushort)(buttons & ~4096);
        }
    }

    public void SetHPHold(bool hp)
    {
        if (hp)
        {
            buttons = (ushort)(buttons | 8192);
        }
        else
        {
            buttons = (ushort)(buttons & ~8192);
        }
    }

    public void SetHKPress(bool hk)
    {
        if (hk)
        {
            buttons = (ushort)(buttons | 16384);
        }
        else
        {
            buttons = (ushort)(buttons & ~16384);
        }
    }

    public void SetHKHold(bool hk)
    {
        if (hk)
        {
            buttons = (ushort)(buttons | 32768);
        }
        else
        {
            buttons = (ushort)(buttons & ~32768);
        }
    }

    // GETTERS

    public bool GetUp(bool hold)
    {
        return hold ? (buttons & 2) == 2 : (buttons & 1) == 1;
    }

    public bool GetDown(bool hold)
    {
        return hold ? (buttons & 8) == 8 : (buttons & 4) == 4;
    }

    public bool GetLeft(bool hold)
    {
        return hold ? (buttons & 32) == 32 : (buttons & 16) == 16;
    }

    public bool GetRight(bool hold)
    {
        return hold ? (buttons & 128) == 128 : (buttons & 64) == 64;
    }

    public bool GetLP(bool hold)
    {
        return hold ? (buttons & 512) == 512 : (buttons & 256) == 256;
    }

    public bool GetLK(bool hold)
    {
        return hold ? (buttons & 2048) == 2048 : (buttons & 1024) == 1024;
    }

    public bool GetHP(bool hold)
    {
        return hold ? (buttons & 8192) == 8192 : (buttons & 4096) == 4096;
    }

    public bool GetHK(bool hold)
    {
        return hold ? (buttons & 32768) == 32768 : (buttons & 16384) == 16384;
    }
}
