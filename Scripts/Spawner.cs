using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    private Text Counter;
    public float zaman, MobSpawnTime, EmodeSpawner;
    public int RoundedZaman;
    public FriendlyCommander fc;
    private Button_Sets_MainMenu LevelManager;
    private Reklam reklam;
    public GameObject WinPanel;
    public int Geçiciskor, ZamanKayıtı;
    public Text skorText;
    public Text skorZamanText;
    public bool exit;
    public bool isSpawn, isEmode;
    public GameObject[] SpawnSupObj;
    public GameObject[] SpawnEmode;
    public Image[] Can_Images;
    public Sprite[] Kalp;
    public int can;
    public int Can_Index;
    
    void Start()
    {

        LevelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Button_Sets_MainMenu>();
        reklam = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Reklam>();
        ZamanKayıtı = Mathf.RoundToInt(zaman);
        exit = false;
        MobSpawnTime = UnityEngine.Random.Range(3, 8);
        EmodeSpawner = UnityEngine.Random.Range(20, 25);

        Counter = GameObject.FindGameObjectWithTag("time").GetComponent<Text>();

        if (isEmode)
        {
            zaman = 0;
        }
        
        Can_Images = new Image[GameObject.FindGameObjectsWithTag("can_images").Length];
       for (int i = 0; i < GameObject.FindGameObjectsWithTag("can_images").Length; i++)
       {
                Can_Images[i] = GameObject.FindGameObjectsWithTag("can_images")[i].GetComponent<Image>();
       }
        can = Can_Images.Length;
        Debug.Log(can);
        if(skorZamanText != null)
        skorZamanText.gameObject.SetActive(isEmode);
    }


    void Update()
    {
        GameObject[] cp = GameObject.FindGameObjectsWithTag("coin");
        if (cp != null)
            for (int i = 0; i < cp.Length; i++)
            {
                
                cp[i].transform.position = Vector2.MoveTowards(cp[i].transform.position, GameObject.FindGameObjectWithTag("goldCollector").transform.position, Time.deltaTime * 3);
                if (cp[i].transform.position == GameObject.FindGameObjectWithTag("goldCollector").transform.position)
                {
                    GameObject.FindGameObjectWithTag("bonusS").GetComponent<AudioSource>().Play();
                    Destroy(cp[i]);
                }
                else
                    i = 0;
                GameObject.FindGameObjectWithTag("goldCollector").GetComponent<Animator>().SetBool("param1", true);
            }
        if(cp.Length == 0)
           GameObject.FindGameObjectWithTag("goldCollector").GetComponent<Animator>().SetBool("param1",false);
           
             


            // Mob spawn
        if (SpawnSupObj != null && !LevelManager.VideoPaneli.activeSelf && !LevelManager.OlumPaneli.activeSelf && !reklam.izlendi && isSpawn && !WinPanel.activeSelf)
            {
                if (MobSpawnTime > 0)
                {
                    MobSpawnTime -= Time.deltaTime;
                }
                else if (MobSpawnTime <= 0)
                {
                    MobSpawnTime = UnityEngine.Random.Range(4, 6);
                    int n = UnityEngine.Random.Range(0, SpawnSupObj.Length);
                    Instantiate(SpawnSupObj[n], gameObject.transform.position, Quaternion.identity);
                    
                }
            }

            if (SpawnEmode != null && !LevelManager.VideoPaneli.activeSelf && !LevelManager.OlumPaneli.activeSelf && isEmode && !WinPanel.activeSelf) {
                if (EmodeSpawner > 0)
                {
                    EmodeSpawner -= Time.deltaTime;
                }
                else if (EmodeSpawner <= 0)
                {
                    int n = UnityEngine.Random.Range(0, SpawnEmode.Length);
                    Instantiate(SpawnEmode[n], gameObject.transform.position, Quaternion.identity);
                    EmodeSpawner = UnityEngine.Random.Range(40, 50);
                }
            
            // Mob spawn

            
       }
            // counter
            if (!LevelManager.VideoPaneli.activeSelf && !LevelManager.OlumPaneli.activeSelf && !reklam.izlendi && !isEmode && !WinPanel.activeSelf)
            {
                if (zaman > 0)
                {
                    zaman -= Time.deltaTime;
                }
                RoundedZaman = ((int)Mathf.RoundToInt(zaman));
                
                if (RoundedZaman > 60)
                { 
                Counter.text = (RoundedZaman / 60).ToString() + (RoundedZaman - ((RoundedZaman / 60)) * 60).ToString() ;
                }
            else
            {
if (RoundedZaman < 60 && RoundedZaman >= 10)
                {
                    Counter.text = "0:" + RoundedZaman.ToString();
                }
                else if (RoundedZaman < 10)
                {
                    Counter.text = "0:0" + RoundedZaman.ToString();
                }
            }
        }
            // counter

            if (SceneManager.GetActiveScene().name == "Emode")
        {


            if (EmodeSpawner > 0 && isEmode)
            {

                int zt = ((int)Mathf.RoundToInt(LevelManager.skorZaman));

                if (zt < 60 && zt >= 10)
                {
                    skorZamanText.text = "0:" + zt.ToString();
                }
                else if (zt < 10)
                {
                    skorZamanText.text = "0:0" + zt.ToString();
                }
                else if (zt >= 60)
                {
                    skorZamanText.text = (zt / 60).ToString() +"m : "+ (zt - ((zt / 60)) * 60).ToString() + "s";
                }
            }
        }
            // save and exit
            if (zaman <= 0 && !exit)
            {
               LevelManager.hiz = 0;
            
            
                Geçiciskor += (ZamanKayıtı * 7);
                LevelManager.skor += Geçiciskor;
                PlayerPrefs.SetInt("skor", LevelManager.skor);
                skorText.text = Geçiciskor.ToString() + " Xp";
            
                PlayerPrefs.SetFloat("hiz", LevelManager.hiz);
                WinPanel.SetActive(true);
                
                exit = true;
            }
            // save and exit
    }

    public void Can_Azaltma_Fonksiyonu()
    {     
        Can_Images[can-1].sprite = Kalp[0];
        can -= 1;        
    }
    
}
