using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using TMPro;

[System.Serializable]

public class Çalışanlar : MonoBehaviour
{
    public static List<Çalışan> çalışanlar = new List<Çalışan>{};
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public TextMeshProUGUI btn1Text;
    public TextMeshProUGUI btn2Text;
    public TextMeshProUGUI btn3Text;
    
    void Start() {
        btn1.interactable = (çalışanlar.Count > 0);
        btn2.interactable = (çalışanlar.Count > 1);
        btn3.interactable = (çalışanlar.Count > 2);
        btn1Text.text = (çalışanlar.Count > 0) ? $"<b>İsim:</b> {çalışanlar[0].isim}\n<b>Pay:</b> %{çalışanlar[0].pay}" : "";
        btn2Text.text = (çalışanlar.Count > 1) ? $"<b>İsim:</b> {çalışanlar[1].isim}\n<b>Pay:</b> %{çalışanlar[1].pay}" : "";
        btn3Text.text = (çalışanlar.Count > 2) ? $"<b>İsim:</b> {çalışanlar[2].isim}\n<b>Pay:</b> %{çalışanlar[2].pay}" : "";

        PlayerPrefs.SetInt("çalışan", 0);
        PlayerPrefs.SetInt("görev var", 0);
    }
    public void Back() {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ÇalışanSeç(int çalışan) {
        PlayerPrefs.SetInt("çalışan", çalışan);
        Back();
    }
}