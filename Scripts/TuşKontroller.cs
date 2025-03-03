using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class TuşKontroller : MonoBehaviour
{

    public GameObject obje;
    public GameObject obje2;
    
    public static string son_ekran;

    public string olay;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && olay == "araç alım satım") {
            float dst2 = Vector3.Distance(obje2.transform.position, obje.transform.position);
            if (dst2 < 3f)
            {
                SceneManager.LoadScene(2);
                Cursor.lockState = CursorLockMode.None;
                son_ekran = "araç alım satım";
            }
        }

        if (Input.GetKeyDown(KeyCode.M) && olay == "mailler") {
            float dst2 = Vector3.Distance(obje2.transform.position, obje.transform.position);
            if (dst2 < 3f)
            {
                SceneManager.LoadScene(6);
                Cursor.lockState = CursorLockMode.None;
                son_ekran = "mailler";
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && olay == "çalışanlar") {
            float dst2 = Vector3.Distance(obje2.transform.position, obje.transform.position);
            if (dst2 < 3f)
            {
                SceneManager.LoadScene(7);
                Cursor.lockState = CursorLockMode.None;
                son_ekran = "çalışanlar";
            }
        }
    }
}
