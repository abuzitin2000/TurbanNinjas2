using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public RebindsMenu rebinds;
    public VideoSettingsManager video;

    public SpriteRenderer stand;
    public Sprite[] stands;
    public List<GameObject> menus;

    public List<UnityEngine.UI.Button> buttons;
    public int selectedItem;
    public int selectedMenu;

    // Start is called before the first frame update
    void Start()
    {
        buttons = new List<UnityEngine.UI.Button>();
        
        stand.sprite = stands[Random.Range(0, stands.Length)];

        ChangeMenuItems(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
	}

    public void MenuUp()
    {
        selectedItem -= 1;

        if (selectedItem < 0)
        {
            selectedItem = buttons.Count - 1;
        }

        buttons[selectedItem].Select();
    }

    public void MenuDown()
    {
        selectedItem += 1;

        if (selectedItem > buttons.Count - 1)
        {
            selectedItem = 0;
        }

        buttons[selectedItem].Select();
    }

    public void MenuLeft()
    {
        switch (selectedMenu)
        {
            case 5:
                rebinds.MenuLeft(selectedItem);
                ChangeMenuItems(5, -1);
                break;
            case 6:
                video.MenuLeft(selectedItem);
                ChangeMenuItems(6, -1);
                break;
        }
    }

    public void MenuRight()
    {
        switch (selectedMenu)
        {
            case 5:
                rebinds.MenuRight(selectedItem);
                ChangeMenuItems(5, -1);
                break;
            case 6:
                video.MenuRight(selectedItem);
                ChangeMenuItems(6, -1);
                break;
        }
    }

    public void MenuSelect()
    {
        buttons[selectedItem].onClick.Invoke();
    }

    public void MenuBack()
    {
        if (selectedMenu == 5)
        {
            return;
        }

        SelectBack();
    }

    public void SelectStory()
	{
        //ChangeMenuItems(1);
	}

    public void SelectOnline()
    {
        ChangeMenuItems(2, 0);
    }

    public void SelectOffline()
    {
        ChangeMenuItems(3, 0);
    }

    public void SelectSettings()
    {
        ChangeMenuItems(4, 0);
    }

    public void SelectQuit()
    {
        Application.Quit();
    }

    public void SelectLobby()
	{
        SceneManager.LoadScene("ConnectionScene");
    }

    public void SelectVersus()
	{
        SceneManager.LoadScene("CharacterSelectScene");
    }

    public void SelectRebind()
	{
        rebinds.SwitchType();
        rebinds.SwitchController();
        ChangeMenuItems(5, 0);
    }

    public void SelectVideo()
    {
        ChangeMenuItems(6, 0);
    }

    public void SelectBack()
    {
        switch (selectedMenu)
        {
            // Main
            case 0:
                break;
            // Story
            case 1:
                ChangeMenuItems(0, 0);
                break;
            // Online
            case 2:
                ChangeMenuItems(0, 1);
                break;
            // Offline
            case 3:
                ChangeMenuItems(0, 2);
                break;
            // Settings
            case 4:
                ChangeMenuItems(0, 3);
                break;
            // Rebinds
            case 5:
                ChangeMenuItems(4, 3);
                break;
            // Video
            case 6:
                if (video.resolutionDropdown.activeSelf)
                    video.CloseDropdown();
                else
                    video.SaveSettings();
                    ChangeMenuItems(4, 0);
                break;
            default:
                ChangeMenuItems(0, 0);
                break;
        }
    }

    public void ChangeMenuItems(int selectedMenu, int selectButton)
	{
        this.selectedMenu = selectedMenu;

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
            if (button.enabled)
            {
                buttons.Add(button);
            }
		}

        // Select First Button as Default
        if (selectButton != -1)
		{
            selectedItem = selectButton;
            buttons[selectedItem].Select();
        }
	}
}
