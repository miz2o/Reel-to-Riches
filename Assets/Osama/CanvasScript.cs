using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public GameObject Menu;
    public GameObject OptionsMenu;
    public GameObject HUD;
    public GameObject Music;
    public static bool menuON;

    private void Start()
    {
        menuON = true;
    }
    private void Update()
    {
        if(menuON == false)
        {
            Time.timeScale = 1f;
        }
    }
    public void MainMenu()
    {
        Menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        HUD.SetActive(true);
        Music.SetActive(false);

        menuON = false;
    }
    public void Options()
    {
        Menu.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void BackToMenu()
    {
        Menu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    List<int> widths = new List<int>() { 1920,1280,800 };
    List<int> heights = new List<int>() { 1080,720,600 };

    public void SetScreenSize(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);
    }

    public void SetFullscreen(bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;
    }
}
