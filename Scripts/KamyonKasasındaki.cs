using UnityEngine;

public class KamyonKasasındaki : MonoBehaviour
{
    public int model;
    void Update()
    {
        if (PlayerPrefs.GetInt("model") == model) {
            GetComponent<Renderer>().enabled = true;
        } else {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
