using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtons
{
    public int frameTime;

    public bool up;
    public bool down;
    public bool left;
    public bool right;

    public PlayerButtons CreateCopy()
    {
        PlayerButtons tempPlayerButtons = new PlayerButtons();

        tempPlayerButtons.frameTime = this.frameTime;

        tempPlayerButtons.up = this.up;
        tempPlayerButtons.down = this.down;
        tempPlayerButtons.left = this.left;
        tempPlayerButtons.right = this.right;

        return tempPlayerButtons;
    }

    public bool CompareButtons(PlayerButtons compareButtons)
    {
        if (compareButtons.up != this.up)
        {
            return false;
        }
        if (compareButtons.down != this.down)
        {
            return false;
        }
        if (compareButtons.left != this.left)
        {
            return false;
        }
        if (compareButtons.right != this.right)
        {
            return false;
        }

        return true;
    }
}
