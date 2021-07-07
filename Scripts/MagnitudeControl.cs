using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnitudeControl : MonoBehaviour
{
    PointEffector2D PE_testere;
    public float magnitude;
    void Start()
    {
        PE_testere = transform.GetComponent<PointEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PE_testere.forceMagnitude = magnitude;
    }
}
