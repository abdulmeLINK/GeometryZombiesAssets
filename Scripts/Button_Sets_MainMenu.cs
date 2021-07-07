using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_Sets_MainMenu : MonoBehaviour
{
    public Animator PanelAnimator;
    public Animator ColorAnimator;
    public GameObject VideoPaneli,OlumPaneli;
    public float sayac;
    public Text sayacTXT,KalanSure;
    public float hiz;
    public Spawner spawner;
    public int SkorToplam,skor;
    public bool videoizlendi;
    public Text PreviousLTXT, NextLTXT;
    public int Level;
    public int Coin;
    public Slider LevelS;
    public ParticleSystem Konfeti; 
    public Text LevelAtlamaTXT;
    private GPgames gpgames;
    public float skorZaman;
    public int zamanToplam;
    public int TopTime;
    bool sendScored = true;
    public Image ShopLevelImage;
    public Text shopCoinText, shopLevelText;
    public void Start()
    {
        Level = PlayerPrefs.GetInt("level", Level);
        skor = PlayerPrefs.GetInt("skor", skor);
        SkorToplam = PlayerPrefs.GetInt("skortoplam", SkorToplam);
       
        gpgames = gameObject.GetComponent<GPgames>();

        

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            
          
            
            PreviousLTXT.text = Level.ToString();
            NextLTXT.text = (Level + 1).ToString();
            LevelS.maxValue = Level * 30;
            

            skorZaman = (int)PlayerPrefs.GetFloat("sz");
           
            if (SkorToplam > 0)
            {
                gpgames.RepScore("CgkIwreX97gEEAIQAA", SkorToplam);
            }
            if (TopTime > skorZaman)
            {
                gpgames.RepScore("CgkIwreX97gEEAIQAw", (int)skorZaman);
            }
        }
        else
        {

            spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();        
            PlayerPrefs.SetFloat("hiz", hiz);

        }
        
            
    }
    public void Update()
    {
      
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            //Level value set
             LevelS.value = skor;
            if (LevelS.value == LevelS.maxValue)
            {
                LevelAtlamaTXT.gameObject.SetActive(false);
                SkorToplam += skor;               
                Level += 1;
                PreviousLTXT.text = Level.ToString();
                NextLTXT.text = (Level + 1).ToString();
                if (LevelS.maxValue < skor)
                {
                    LevelS.value = 0;
                    LevelS.value += (skor - LevelS.maxValue);
                    skor = Mathf.RoundToInt(LevelS.value);

                }
                else
                {
                    skor = 0;
                }
                Konfeti.Play();
                GameObject.FindGameObjectWithTag("LevelUp").GetComponent<AudioSource>().Play();
                LevelAtlamaTXT.gameObject.SetActive(true);
                LevelAtlamaTXT.text = "Level " + Level.ToString();
                LevelS.value = skor;
                PlayerPrefs.SetInt("skor", skor);
                PlayerPrefs.SetInt("level", Level);
                LevelS.maxValue = Level * 30;
            }

            //Shop Status Bar Set Up
            ShopLevelImage.fillAmount = LevelS.value / LevelS.maxValue ;
            shopLevelText.text = Level.ToString();
            shopCoinText.text = Coin.ToString();

        }
        else
        {
            if (VideoPaneli.active && sayac > 0)
            {
                hiz = 0;
                PlayerPrefs.SetFloat("hiz", hiz);
                sayac -= Time.deltaTime;
                sayacTXT.text = Mathf.RoundToInt(sayac).ToString();
            }
            else
            {
                sayac = 5;

            }
            if (sayac <= 0)
            {
                OlumPaneli.SetActive(true);
                KalanSure.text = "-" + spawner.RoundedZaman.ToString() + "s"; ;
                VideoPaneli.SetActive(false);
            }
            if (OlumPaneli.active)
            {
                if (SceneManager.GetActiveScene().name == "Emode")
                {
                    KalanSure.text = "+" + (int)skorZaman + "s";
                    int comp = (int)PlayerPrefs.GetFloat("sz");
                    if ((int)skorZaman > comp)
                    {
                        PlayerPrefs.SetFloat("sz", skorZaman);

                      
                          if (comp > skorZaman)
                            {
                                gpgames.RepScore("CgkIwreX97gEEAIQAw", (int)skorZaman);
                            }
                    
                    }
                }
                else
                {
                    KalanSure.text = "-" + spawner.RoundedZaman.ToString() + "s";
                }

                hiz = 0;
                PlayerPrefs.SetFloat("hiz", hiz);

            }
            else {
                if (SceneManager.GetActiveScene().name == "Emode")
                {
                    skorZaman += Time.deltaTime;
                }
            }
        }
    }
   
    public void LevelChange(string SceneName)
    {   
        SceneManager.LoadScene(SceneName);
    }
    public void LevelButton()
    {
        PanelAnimator.SetBool("ToDown",true);
        PanelAnimator.SetBool("ToUp", false);
        PanelAnimator.SetBool("OnIdle", true);
        ColorAnimator.SetBool("ToDown", true);
        ColorAnimator.SetBool("ToUp", false);
        ColorAnimator.SetBool("OnIdle", true);
    }
    public void LevelClose()
    {
        PanelAnimator.SetBool("ToUp", true);
        PanelAnimator.SetBool("ToDown", false);
        ColorAnimator.SetBool("ToUp", true);
        ColorAnimator.SetBool("ToDown", false);
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void Restart()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SplitingStringChar()
    {
        string a = SceneManager.GetActiveScene().name;
        char[] cr = a.ToCharArray();
        int LevelNum = int.Parse(cr[5].ToString()) +1;
        SceneManager.LoadScene("Level" + LevelNum.ToString());
    }
    public void ButtonSound()
    {
        GameObject.FindGameObjectWithTag("blupS").GetComponent<AudioSource>().Play();
    }
    
    
}
