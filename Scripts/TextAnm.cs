using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnm : MonoBehaviour
{
    public byte speedOfChange;
    public byte alpha;
    public bool harmful;
    public Color32 cArray;
    Text text;
    void Start()
    {
        text = gameObject.GetComponent<Text>();
     
        if (harmful)
        {
           cArray = new Color32(227, 32, 73, 255);
        }
        else
        {
            cArray = new Color32(122, 157, 104, 255);
        }
         text.color = cArray;
    }

    // Update is called once per frame
    void Update()
    {

        if (alpha <= 10)
        {
            Destroy(gameObject);
        }
        else
        {
            alpha -= speedOfChange;
            
          cArray = new Color32(cArray.r, cArray.g, cArray.b, alpha);
          text.color = cArray;
          
        }
    }
}
