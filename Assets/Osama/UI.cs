using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public GameObject menu;
    public GameObject options;
    public GameObject canvas;
    public GameObject credits;
    public GameObject shop;
    public static bool menuON;
    public AudioSource MusicSource;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        menuON = true;
        MusicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Volume()
    {
        MusicSource.volume = slider.value;
    }
    public void Play()
    {
        canvas.SetActive(false);
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
        credits.SetActive(false);
        shop.SetActive(false);
    }
    public void ToCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }
    public void ToShop()
    {
        menu.SetActive(false);
        shop.SetActive(true);
    }
}
