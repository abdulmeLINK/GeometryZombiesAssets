using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reklam : MonoBehaviour
{
    private Button_Sets_MainMenu LevelManager;
    public float sayac;
    public bool izlendi;
    private Text Counter;
    void Start()
    {
        LevelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Button_Sets_MainMenu>();
        sayac = 3;
        Counter = GameObject.FindGameObjectWithTag("time").GetComponent<Text>();
    }

  
    void Update()
    {
        if (izlendi && sayac > 0)
        {
            sayac -= Time.deltaTime;            
            Counter.text = Mathf.RoundToInt(sayac).ToString() + "...";
        }
        else if (sayac <= 0)
        {
            LevelManager.hiz = 0.65f;
            PlayerPrefs.SetFloat("hiz", LevelManager.hiz);
            izlendi = false;
        }
    }
    public void VideoResume()
    {
        LevelManager.VideoPaneli.SetActive(false);
        izlendi = true;
        LevelManager.videoizlendi = true;
        GameObject.FindGameObjectWithTag("PauseBTN").GetComponent<Button>().interactable = true;
    }
}
