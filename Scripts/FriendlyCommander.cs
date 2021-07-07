using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FriendlyCommander : MonoBehaviour
{
    public ParticleSystem ps,psOdun;
    private ParticleSystem psZygote;
    public Vector2 targetvector;
    public GameObject uyari;
    public int kopyasayisi;
    private GameObject kopya;
    private Animator Vibration;
    public bool isFinished;
    public GameObject VideoPanel;
    private Button_Sets_MainMenu LevelManager;
    public Spawner spawner;
    public GameObject exTextprefab;
    public GameObject exText;
    public int refresher;
    public GameObject goldCollector, coinPrefab;
    
    private void Start()
    {
       
        Vibration = Camera.main.gameObject.GetComponent<Animator>();
        Vibration.SetBool("bitti", true);
        LevelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Button_Sets_MainMenu>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        goldCollector = GameObject.FindGameObjectWithTag("goldCollector");
    }
    private void Update()
    {
        DistanceCalculator();   
    }
    private void DistanceCalculator()
    {
       Vector2 self = transform.position;
        float mesafe;
        float farkx = Mathf.Abs(targetvector.x - self.x);
        float farky = Mathf.Abs(targetvector.y - self.y);
        mesafe = Mathf.Sqrt((float)Mathf.Pow(farkx, 2) + (float)Mathf.Pow(farky, 2));

        if (gameObject.tag == "draggable")
        {
            if (mesafe < 2 && kopyasayisi == 0)
            {
                kopya = Instantiate(uyari, transform.position, Quaternion.identity) as GameObject;
                kopya.transform.SetParent(transform);
                
                kopyasayisi++;
            }
            else if (mesafe > 2)
            {
                Destroy(kopya);
                kopyasayisi = 0;
            }
            if (kopya)
            {
                kopya.transform.position = transform.position;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("testere"))
        {
            if (gameObject.tag == "draggable")
            {
                //Animasyonlar ve Particle system prefab zigotu INITIATE

                Vibration.SetBool("Titret", true);
                Vibration.SetBool("bitti", false);
                psZygote = Instantiate(ps, transform.position, Quaternion.identity);
                Destroy(kopya);
                psZygote.Play();
                //Videolu devam etme panelini açma... 
                if (LevelManager.spawner.can > 0)
                {
                    GameObject.FindGameObjectWithTag("kalpeksilmeS").GetComponent<AudioSource>().Play();

                    if (LevelManager.spawner.can <= 0)
                    {
                        if (!LevelManager.videoizlendi)
                        {
                            VideoPanel.SetActive(true);
                            GameObject.FindGameObjectWithTag("PauseBTN").GetComponent<Button>().interactable = false;
                        }
                        else if (LevelManager.videoizlendi)
                        {
                            LevelManager.OlumPaneli.SetActive(true);
                        }

                    }
                    LevelManager.spawner.Can_Azaltma_Fonksiyonu();
                }
                else {
                    if (SceneManager.GetActiveScene().name == "Emode")
                    {
                        LevelManager.OlumPaneli.SetActive(true);
                    }
                    else
                    {
                        if (!LevelManager.videoizlendi)
                        {
                            VideoPanel.SetActive(true);
                            GameObject.FindGameObjectWithTag("PauseBTN").GetComponent<Button>().interactable = false;
                        }
                        else if (LevelManager.videoizlendi)
                        {
                            LevelManager.OlumPaneli.SetActive(true);
                        }
                    }
                }
                Destroy(transform.gameObject);
            }
            if (gameObject.tag == "MobHarm")
            {
                if (refresher == 0)
                {
                    refresher++;
                    GameObject.FindGameObjectWithTag("bonusS").GetComponent<AudioSource>().Play();
                    psZygote = Instantiate(psOdun, transform.position, Quaternion.identity);
                    spawner.Geçiciskor += 15;
                    exText = Instantiate(exTextprefab, transform.position, exTextprefab.transform.rotation);
                    exText.transform.SetParent(GameObject.FindGameObjectWithTag("uicanvas").transform);
                    exText.transform.localScale = new Vector3(1, 1, 1);
                    exText.GetComponent<Text>().text = "+15";
                    exText.GetComponent<TextAnm>().harmful = false;
                    
                    Instantiate(coinPrefab, transform.position, coinPrefab.transform.rotation);
                    GameObject cp = GameObject.FindGameObjectWithTag("coin");
                    
                }
               
                Destroy(transform.gameObject,0.40f);
                
                
                
            }
            if (gameObject.tag == "Mob")
            {
                GameObject.FindGameObjectWithTag("kalpeksilmeS").GetComponent<AudioSource>().Play();
                spawner.Geçiciskor -= 5;
                exText = Instantiate(exTextprefab, transform.position, exTextprefab.transform.rotation);
                exText.transform.SetParent(GameObject.FindGameObjectWithTag("uicanvas").transform);
                exText.transform.localScale = new Vector3(1,1,1);
                exText.GetComponent<Text>().text = "-5";
                exText.GetComponent<TextAnm>().harmful = true;
                Destroy(transform.gameObject);
            }

            //Kendini imha etme...

          
        }
    }

}
