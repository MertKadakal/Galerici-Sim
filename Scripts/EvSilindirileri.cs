using UnityEngine;
using System.Collections.Generic;
using System;

public class EvSilindirileri : MonoBehaviour
{
    public GameObject[] silindirler = new GameObject[45];
    public static GameObject evSilindir;
    GameObject player;
    void Update()
    {
        foreach (GameObject slndr in silindirler) {
            slndr.SetActive(false);
            if (Array.IndexOf(silindirler, slndr) == FirstPersonLook.evNo) {
                slndr.SetActive(true);
                evSilindir = slndr;
            }
        }


    }
}
