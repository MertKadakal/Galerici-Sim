using UnityEngine;

public class GarajKapıları : MonoBehaviour
{
    public GameObject car;

    private Quaternion targetRotation;
    private float rotationDuration = 2f;
    private float elapsedTime = 0f;
    private bool isRotating = false;

    private bool isOpen = false; 
    public int door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    

    void Update()
    {
        float distanceToCar = Vector3.Distance(transform.position, car.transform.position);

        if (distanceToCar < 5f && Input.GetKeyDown(KeyCode.O) && car.transform.position.z < transform.position.z)
        {
            if (isOpen) // 90 dereceye yakınsa 0'a dön
                targetRotation = Quaternion.Euler(0, 0, 0);
            else
                targetRotation = Quaternion.Euler(0, door*90, 0);
            
            isOpen = !isOpen;

            elapsedTime = 0f;
            isRotating = true;
        }

        if (isRotating)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rotationDuration;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);

            if (t >= 1f)  // Dönüş tamamlandıysa durdur
                isRotating = false;
        }
    }
}
