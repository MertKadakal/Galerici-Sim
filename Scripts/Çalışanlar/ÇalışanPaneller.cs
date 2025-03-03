using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ÇalışanPaneller : MonoBehaviour
{
    public GameObject çalışanPanel;
    public Camera anaCam;
    //public Camera çalışan1cam;
    //public Camera çalışan2cam;
    //public Camera çalışan3cam;
    public TextMeshProUGUI bilgiler;

    public Camera[] çalışanCamlar;

    void Start() {
        çalışanCamlar[0].gameObject.SetActive(false);
        çalışanCamlar[1].gameObject.SetActive(false);
        çalışanCamlar[2].gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("çalışan") != 0) {
            Göster(PlayerPrefs.GetInt("çalışan"));
        }
    
    }

void Update()
{
    if ((PlayerPrefs.GetInt("çalışan") == 0 && çalışanPanel.activeSelf) || PlayerPrefs.GetInt("görev var") == 1)
    {
        Back();
    }
}

    
    void Göster(int i) {
        bilgiler.text = $"<b>Çalışan Bilgiler</b>\n\n<b>İsim:</b> {Çalışanlar.çalışanlar[i-1].isim}\n<b>Pay:</b> %{Çalışanlar.çalışanlar[i-1].pay}\n\n\n<b>Görevlendir</b>";
        anaCam.gameObject.SetActive(false);
        çalışanCamlar[i-1].gameObject.SetActive(true);
        çalışanPanel.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
        
    public void Back() {
        anaCam.gameObject.SetActive(true);
        çalışanCamlar[0].gameObject.SetActive(false);
        çalışanCamlar[1].gameObject.SetActive(false);
        çalışanCamlar[2].gameObject.SetActive(false);
        çalışanPanel.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        //PlayerPrefs.SetInt("çalışan", 0);
        //PlayerPrefs.SetInt("görev var", 0);
    }
}
