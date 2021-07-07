using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimHandlerByValueChange : MonoBehaviour
{
    Slider ThisSlider;   
    Animator animator;
    public Image FillArea;
    public Text Prev, Next;
    float zaman;
    bool ValueChecker;
    void Start()
    {
        ThisSlider = gameObject.GetComponent<Slider>();
        animator = gameObject.GetComponent<Animator>();
        ValueChecker = false;
        zaman = 0;
    }

   
    void Update()
    {
        if(ValueChecker && zaman > 0)
        {
            zaman -= Time.deltaTime;
        }
        else
        {
            ValueChecker = false;
            FillArea.color = new Color32(77, 83, 91, 255);
        }
    }
    public void OnValueChange()
    {

        FillArea.color = new Color32(240, 6, 219, 255);
        ValueChecker = true;
        zaman = 0.80f;
    }
}
