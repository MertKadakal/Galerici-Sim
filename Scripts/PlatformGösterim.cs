using UnityEngine;

public class PlatformGösterim : MonoBehaviour
{
    public int id;
    public int model;
    void Update()
    {
        if (AraçAlımSatım.sergiler[id] != null && AraçAlımSatım.sergiler[id].modelDerece == model) {
            GetComponent<Renderer>().enabled = true;
        } else {
            GetComponent<Renderer>().enabled = false;
        }
        
    }
}
