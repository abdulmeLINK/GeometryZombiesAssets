using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touch_Controller : MonoBehaviour
{
    public Vector2 touchPos;
    public Rigidbody2D rb;
   private bool VirtualBoolean, activate;
    private GameObject target;
    public float hiz;
    private Button_Sets_MainMenu LevelManager;
    private Spawner spawner;
    private string Tag;
    private GameObject exText;
    public GameObject exTextprefab;
    public AudioSource blup, bonus, KE;
    public GameObject goldCollector,coinPrefab;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("testere");
        VirtualBoolean = false;
        activate = false;
        LevelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Button_Sets_MainMenu>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        Tag = gameObject.tag;
        blup = GameObject.FindGameObjectWithTag("blupS").GetComponent<AudioSource>();
        bonus = GameObject.FindGameObjectWithTag("bonusS").GetComponent<AudioSource>();
        KE = GameObject.FindGameObjectWithTag("kalpeksilmeS").GetComponent<AudioSource>();
        goldCollector = GameObject.FindGameObjectWithTag("goldCollector");
    }


    void Update()
    {

        // Touch phases 
        if (!VirtualBoolean)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * hiz);
        }
        if (Tag == "draggable") {
            hiz = PlayerPrefs.GetFloat("hiz", LevelManager.hiz);
            if (Input.touchCount > 0)
            {            
               Touch touch = Input.GetTouch(0);
                touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                if (touch.phase == TouchPhase.Began && GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                {

                    VirtualBoolean = true;
                    activate = true;

                }
                if (touch.phase == TouchPhase.Moved && activate)
                {

                    VirtualBoolean = true;
                    activate = true;

                }
                if (activate)
                {

                    Vector2 pos;
                    pos = new Vector2(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y);
                    rb.MovePosition(pos);

                }
                if (touch.phase == TouchPhase.Ended) {

                    VirtualBoolean = false;
                    activate = false;

                }

            }
        }
        // Touch phases 

        // Game object attributes
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            // Cicek attributes
            if (gameObject.GetComponent<SpriteRenderer>().sprite.name == "Cicek2mor" && PlayerPrefs.GetFloat("hiz", LevelManager.hiz) > 0)
            {
                hiz = (PlayerPrefs.GetFloat("hiz", LevelManager.hiz)) + 0.50f;
            }

            if ((Tag == "Mob" || Tag == "MobHarm") && PlayerPrefs.GetFloat("hiz", LevelManager.hiz) > 0)
            {
                hiz = (PlayerPrefs.GetFloat("hiz", LevelManager.hiz)) + 1;
            }
            else if (PlayerPrefs.GetFloat("hiz", LevelManager.hiz) == 0) {
                hiz = 0;
            }
           
        }
       

    
        if (Tag == "Mob")
        {
           
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                {
                    spawner.Geçiciskor += 15;
                    exText = Instantiate(exTextprefab, transform.position, exTextprefab.transform.rotation);
                    exText.transform.SetParent(GameObject.FindGameObjectWithTag("uicanvas").transform);
                    exText.transform.localScale = new Vector3(1, 1, 1);
                    exText.GetComponent<Text>().text = "+15";
                    exText.GetComponent<TextAnm>().harmful = false;
                    bonus.Play();
                    Instantiate(coinPrefab, transform.position, coinPrefab.transform.rotation);
                    GameObject cp = GameObject.FindGameObjectWithTag("coin");
                   
                    Destroy(gameObject);
                    
                }
            }
        }

        if (Tag == "MobHarm")
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                {
                    spawner.Geçiciskor -= 10;
                    exText = Instantiate(exTextprefab, transform.position, exTextprefab.transform.rotation);
                    exText.transform.SetParent(GameObject.FindGameObjectWithTag("uicanvas").transform);
                    exText.transform.localScale = new Vector3(1, 1, 1);
                    exText.GetComponent<Text>().text = "-10";
                    exText.GetComponent<TextAnm>().harmful = true;
                    KE.Play();
                   
                    Destroy(gameObject);
                    
                }
            }
        }
        // Game object attributes

    }

}

