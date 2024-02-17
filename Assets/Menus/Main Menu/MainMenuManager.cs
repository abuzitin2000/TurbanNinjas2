using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public RebindsMenu rebinds;

    public SpriteRenderer stand;
    public Sprite[] stands;
    public List<GameObject> menus;

    private List<UnityEngine.UI.Button> buttons;
    private int selectedItem;

    // Start is called before the first frame update
    void Start()
    {
        buttons = new List<UnityEngine.UI.Button>();
        
        stand.sprite = stands[Random.Range(0, stands.Length)];

        ChangeMenuItems(0, true);
    }

    // Update is called once per frame
    void Update()
    {
        
	}

    public void MenuUp(InputAction.CallbackContext context)
	{
        if (!context.performed)
		{
            return;
		}

        selectedItem -= 1;

        if (selectedItem < 0)
        {
            selectedItem = buttons.Count - 1;
        }

        buttons[selectedItem].Select();
    }

    public void MenuDown(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        selectedItem += 1;

        if (selectedItem > buttons.Count - 1)
        {
            selectedItem = 0;
        }

        buttons[selectedItem].Select();
    }

    public void MenuLeft(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

		switch (buttons[selectedItem].name)
		{
            case "ButtonRebindsType":
                rebinds.type -= 1;
                rebinds.SwitchType();
                ChangeMenuItems(5, false);
                break;
            case "ButtonRebindsController":
                rebinds.controller -= 1;
                rebinds.SwitchController();
                ChangeMenuItems(5, false);
                break;
		}
	}

    public void MenuRight(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        switch (buttons[selectedItem].name)
        {
            case "ButtonRebindsType":
                rebinds.type += 1;
                rebinds.SwitchType();
                ChangeMenuItems(5, false);
                break;
            case "ButtonRebindsController":
                rebinds.controller += 1;
                rebinds.SwitchController();
                ChangeMenuItems(5, false);
                break;
        }
    }

    public void MenuSelect(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        buttons[selectedItem].onClick.Invoke();
    }

    public void SelectStory()
	{
        //ChangeMenuItems(1);
	}

    public void SelectOnline()
    {
        //ChangeMenuItems(2);
        SceneManager.LoadScene("ConnectionScene");
    }

    public void SelectOffline()
    {
        ChangeMenuItems(3, true);
    }

    public void SelectSettings()
    {
        ChangeMenuItems(4, true);
    }
    public void SelectBack()
    {
        ChangeMenuItems(0, true);
    }

    public void SelectQuit()
    {
        Application.Quit();
    }

    public void SelectRebind()
	{
        ChangeMenuItems(5, true);
    }

    private void ChangeMenuItems(int selectedMenu, bool resetSelected)
	{
        // Change Enabled Menu
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        menus[selectedMenu].SetActive(true);

        // Change Buttons to Current Menu
        buttons.Clear();

		foreach (UnityEngine.UI.Button button in menus[selectedMenu].GetComponentsInChildren<UnityEngine.UI.Button>(false))
		{
            buttons.Add(button);
		}

        // Select First Button as Default
        if (resetSelected)
		{
            selectedItem = 0;
            buttons[selectedItem].Select();
        }
	}
}
