using UnityEngine;

public class AcikArttirmaArac : MonoBehaviour
{
    public float rotationSpeed = 50f; // Dönme hızı (derece/saniye)
    public int id;
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (id == AçıkArttırmaSatici.model-1) {
            GetComponent<Renderer>().enabled = true;
        } else {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
