using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class SergiPlatform : MonoBehaviour, IPointerClickHandler
{
    public int id;
    public Image imageComponent; // GameObject'in Image bileşeni
    public Sprite boş; 
    public Sprite dolu;    // Yeni atanacak sprite
    public GameObject panel;
    public TextMeshProUGUI btnText;
    public TextMeshProUGUI modeText;


    void Update()
    {
        if (btnText.text == "Sergile") {
            modeText.text = "Aracı sergilemek istediğiniz platformu seçiniz";
        } else if (AraçAlımSatım.sergiler[id] == AraçAlımSatım.arabalar[AraçAlımSatım.curr_id]) {
            modeText.text = "Aracınız " + (id+1) + " numaralı platformda sergileniyor";
        }

        if (AraçAlımSatım.sergiler[id].gelişFiyat == 0) {
            imageComponent.sprite = boş;
        } else {
            imageComponent.sprite = dolu;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        bool containsTheCar = false;
        foreach (Araba araba in AraçAlımSatım.sergiler) {
            if (AraçAlımSatım.arabalar[AraçAlımSatım.curr_id].gelişFiyat == araba.gelişFiyat &&
                AraçAlımSatım.arabalar[AraçAlımSatım.curr_id].modelDerece == araba.modelDerece &&
                AraçAlımSatım.arabalar[AraçAlımSatım.curr_id].sene == araba.sene &&
                AraçAlımSatım.arabalar[AraçAlımSatım.curr_id].karBeklentisi == araba.karBeklentisi &&
                AraçAlımSatım.arabalar[AraçAlımSatım.curr_id].karOrani == araba.karOrani
            ) {
                containsTheCar = true;
            }
        }
        
        

        if ((imageComponent.sprite == boş) && (!containsTheCar)) {
            AraçAlımSatım.sergiler[id] = AraçAlımSatım.arabalar[AraçAlımSatım.curr_id];
            Debug.Log("aa");
            //panel.SetActive(false);
        } else if (AraçAlımSatım.sergiler[id].gelişFiyat == AraçAlımSatım.arabalar[AraçAlımSatım.curr_id].gelişFiyat &&
                AraçAlımSatım.sergiler[id].modelDerece == AraçAlımSatım.arabalar[AraçAlımSatım.curr_id].modelDerece &&
                AraçAlımSatım.sergiler[id].sene == AraçAlımSatım.arabalar[AraçAlımSatım.curr_id].sene &&
                AraçAlımSatım.sergiler[id].karBeklentisi == AraçAlımSatım.arabalar[AraçAlımSatım.curr_id].karBeklentisi &&
                AraçAlımSatım.sergiler[id].karOrani == AraçAlımSatım.arabalar[AraçAlımSatım.curr_id].karOrani) {
            AraçAlımSatım.sergiler[id] = new Araba(0,0,0,0,0);
            //panel.SetActive(false);
        }
    }
}
