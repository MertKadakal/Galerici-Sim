using System;
using UnityEngine;
using System.Collections.Generic;

public class ÇalışanRotalar : MonoBehaviour
{
    public GameObject[] araç1;
    public GameObject[] araç2;
    public GameObject[] araç3;
    public GameObject[] araç4;
    public GameObject[] araç5;
    public GameObject[] araç6;
    public GameObject[] garaj;
    public GameObject[][] rotalar;

    void Start() {
            rotalar = new GameObject[][] {
            araç1,
            araç2,
            araç3,
            araç4,
            araç5,
            araç6,
            garaj
        };
    }
    
}
