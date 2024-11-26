using Meta.XR.InputActions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class UI : MonoBehaviour
{
    public GameObject menu;
    public GameObject options;
    public static bool menuON;

    // Start is called before the first frame update
    void Start()
    {
        menuON = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        menu.SetActive(false);
        menuON = false;
    }
    public void Options()
    {
        menu.SetActive(false);
        options.SetActive(true);
    }
    public void BackToMenu()
    {
        menu.SetActive(true);
        options.SetActive(false);
    }
}
