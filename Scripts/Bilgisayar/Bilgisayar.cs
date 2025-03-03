using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using TMPro;

public class Bilgisayar : MonoBehaviour
{
    public TextMeshProUGUI garajStok;
    public TextMeshProUGUI para;
    public Button pazar;
    public TMP_InputField inputField;  // UI InputField bileşeni
    void Start() {
        garajStok.text = "Galeri Doluluk: " + (AraçAlımSatım.arabalar.Count + Mailler.kiradakiler.Count) +"/"+AraçAlımSatım.galeriMax;
    }

    void Update() {
        para.text = string.Format("Para : {0:N0}", AraçAlımSatım.para);
    }

    public void Araçlar() {
        SceneManager.LoadScene(1);
    }

    public void Teklifler() {
        SceneManager.LoadScene(3);
    }
    public void Geçmiş() {
        SceneManager.LoadScene(4);
    }

    public void Pazar() {
        SceneManager.LoadScene(5);
    }

    public void Back() {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void IsimDegistir()
    {
        string userInput = inputField.text;
        if (userInput != "" && userInput.Length < 15) {
            KöşeBilgiler.galeriname = userInput;
            inputField.text = "";
        }
    }
}
