using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MenuData/InputIcons")]
public class InputIcons : ScriptableObject
{
    public GamepadIcons xbox;
    public GamepadIcons ps4;
    public KeyboardIcons keyboard;

    [Serializable]
    public struct GamepadIcons
    {
        public Sprite buttonSouth;
        public Sprite buttonNorth;
        public Sprite buttonEast;
        public Sprite buttonWest;
        public Sprite startButton;
        public Sprite selectButton;
        public Sprite leftTrigger;
        public Sprite rightTrigger;
        public Sprite leftShoulder;
        public Sprite rightShoulder;
        public Sprite dpad;
        public Sprite dpadUp;
        public Sprite dpadDown;
        public Sprite dpadLeft;
        public Sprite dpadRight;
        public Sprite leftStick;
        public Sprite rightStick;
        public Sprite leftStickPress;
        public Sprite rightStickPress;

        public Sprite GetSprite(string key)
        {
            switch (key)
            {
                case "buttonSouth": return buttonSouth;
                case "buttonNorth": return buttonNorth;
                case "buttonEast": return buttonEast;
                case "buttonWest": return buttonWest;
                case "start": return startButton;
                case "select": return selectButton;
                case "leftTrigger": return leftTrigger;
                case "rightTrigger": return rightTrigger;
                case "leftShoulder": return leftShoulder;
                case "rightShoulder": return rightShoulder;
                case "dpad": return dpad;
                case "dpad/up": return dpadUp;
                case "dpad/down": return dpadDown;
                case "dpad/left": return dpadLeft;
                case "dpad/right": return dpadRight;
                case "leftStick": return leftStick;
                case "rightStick": return rightStick;
                case "leftStickPress": return leftStickPress;
                case "rightStickPress": return rightStickPress;
            }
            return null;
        }
    }

    [Serializable]
    public struct KeyboardIcons
    {
        public Sprite a;
        public Sprite b;
        public Sprite c;
        public Sprite d;
        public Sprite e;
        public Sprite f;
        public Sprite g;
        public Sprite h;
        public Sprite i;
        public Sprite j;
        public Sprite k;
        public Sprite l;
        public Sprite m;
        public Sprite n;
        public Sprite o;
        public Sprite p;
        public Sprite q;
        public Sprite r;
        public Sprite s;
        public Sprite t;
        public Sprite u;
        public Sprite v;
        public Sprite w;
        public Sprite x;
        public Sprite y;
        public Sprite z;
        public Sprite zero;
        public Sprite one;
        public Sprite two;
        public Sprite three;
        public Sprite four;
        public Sprite five;
        public Sprite six;
        public Sprite seven;
        public Sprite eight;
        public Sprite nine;
        public Sprite asterisk;
        public Sprite bracketleft;
        public Sprite bracketright;
        public Sprite markleft;
        public Sprite markright;
        public Sprite minus;
        public Sprite plus;
        public Sprite question;
        public Sprite quote;
        public Sprite semicolon;
        public Sprite slash;
        public Sprite tilda;
        public Sprite f1;
        public Sprite f2;
        public Sprite f3;
        public Sprite f4;
        public Sprite f5;
        public Sprite f6;
        public Sprite f7;
        public Sprite f8;
        public Sprite f9;
        public Sprite f10;
        public Sprite f11;
        public Sprite f12;
        public Sprite space;
        public Sprite ctrl;
        public Sprite alt;
        public Sprite shift;
        public Sprite cmd;
        public Sprite win;
        public Sprite tab;
        public Sprite enter;
        public Sprite esc;
        public Sprite backspace;
        public Sprite capslock;
        public Sprite numlock;
        public Sprite insert;
        public Sprite del;
        public Sprite home;
        public Sprite end;
        public Sprite pageup;
        public Sprite pagedown;
        public Sprite print;
        public Sprite up;
        public Sprite down;
        public Sprite left;
        public Sprite right;


        public Sprite GetSprite(string key)
        {
            switch (key)
            {
                case "a": return a;
                case "b": return b;
                case "c": return c;
                case "d": return d;
                case "e": return e;
                case "f": return f;
                case "g": return g;
                case "h": return h;
                case "i": return i;
                case "j": return j;
                case "k": return k;
                case "l": return l;
                case "m": return m;
                case "n": return n;
                case "o": return o;
                case "p": return p;
                case "q": return q;
                case "r": return r;
                case "s": return s;
                case "t": return t;
                case "u": return u;
                case "v": return v;
                case "w": return w;
                case "x": return x;
                case "y": return y;
                case "z": return z;
                case "0": return zero;
                case "1": return one;
                case "2": return two;
                case "3": return three;
                case "4": return four;
                case "5": return five;
                case "6": return six;
                case "7": return seven;
                case "8": return eight;
                case "9": return nine;
                case "numpad0": return zero;
                case "numpad1": return one;
                case "numpad2": return two;
                case "numpad3": return three;
                case "numpad4": return four;
                case "numpad5": return five;
                case "numpad6": return six;
                case "numpad7": return seven;
                case "numpad8": return eight;
                case "numpad9": return nine;
                case "*": return asterisk;
                case "[": return bracketleft;
                case "]": return bracketright;
                case "<": return markleft;
                case ">": return markright;
                case "-": return minus;
                case "+": return plus;
                case "?": return question;
                case "\"": return quote;
                case ";": return semicolon;
                case "/": return slash;
                case "é": return tilda;
                case "f1": return f1;
                case "f2": return f2;
                case "f3": return f3;
                case "f4": return f4;
                case "f5": return f5;
                case "f6": return f6;
                case "f7": return f7;
                case "f8": return f8;
                case "f9": return f9;
                case "f10": return f10;
                case "f11": return f11;
                case "f12": return f12;
                case "space": return space;
                case "control": return ctrl;
                case "alt": return alt;
                case "shift": return shift;
                case "command": return cmd;
                case "windows": return win;
                case "tab": return tab;
                case "enter": return enter;
                case "escape": return esc;
                case "backspace": return backspace;
                case "capslock": return capslock;
                case "numlock": return numlock;
                case "insert": return insert;
                case "delete": return del;
                case "home": return home;
                case "end": return end;
                case "pageup": return pageup;
                case "pagedown": return pagedown;
                case "printscreen": return print;
                case "up": return up;
                case "down": return down;
                case "left": return left;
                case "right": return right;
            }
            return null;
        }
    }
}
