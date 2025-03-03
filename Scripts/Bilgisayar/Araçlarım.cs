using UnityEngine;

public class Donme : MonoBehaviour
{
    public float rotationSpeed = 50f; // Dönme hızı (derece/saniye)
    public int id;
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (id == AraçAlımSatım.curr_model && AraçAlımSatım.arabalar.Count > 0) {
            GetComponent<Renderer>().enabled = true;
        } else {
            GetComponent<Renderer>().enabled = false;
        }
    }

    
}
