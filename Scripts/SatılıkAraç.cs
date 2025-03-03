using UnityEngine;

public class SatılıkAraç : MonoBehaviour
{
    public float rotationSpeed = 50f; // Dönme hızı (derece/saniye)
    public int model;
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (model == AraçPazarı.modelDerece) {
            GetComponent<Renderer>().enabled = true;
        } else {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
